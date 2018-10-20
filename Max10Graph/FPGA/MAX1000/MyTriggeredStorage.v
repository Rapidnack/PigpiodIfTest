`timescale 1 ps / 1 ps

module MyTriggeredStorage (
		input  wire        csi_clk,          //     csi.clk
		input  wire        rsi_reset,        //     rsi.reset

		input  wire [9:0]  avs_s0_address,   //  avs_s0.address
		input  wire        avs_s0_read,      //        .read
		output wire [31:0] avs_s0_readdata,  //        .readdata
		input  wire        avs_s0_write,     //        .write
		input  wire [31:0] avs_s0_writedata, //        .writedata

		input  wire [31:0] asi_in0_data,     // asi_in0.data
		input  wire        asi_in0_valid,    //        .valid
		
		output reg         coe_complete
	);
	
	localparam ADDR_SIZE = 512;
	localparam ADDR_WIDTH = 9;

	localparam STATE_IDLE = 0;
	localparam STATE_ARMING = 1;
	localparam STATE_WAITING1 = 2;
	localparam STATE_WAITING2 = 3;
	localparam STATE_TRIGGERED = 4;
	localparam STATE_COMPLETED = 5;
	
	reg run;
	reg [31:0] div;
	reg [ADDR_WIDTH-1:0] pre;
	reg [4:0] source;
	reg slope;
	reg [15:0] level;

	reg [15:0] div_count;
	wire divided_valid;
	
	reg [2:0] state;
	reg [31:0] sample;
	wire wren;

	reg [ADDR_WIDTH-1:0] addr;
	
	wire [31:0] q;
	
	always @(posedge csi_clk) begin
		if (rsi_reset) begin
			run <= 0;
			div <= 0;
			pre <= ADDR_SIZE >> 1;
			level <= 128;
		end
		else if (avs_s0_write && avs_s0_address == 10'h200)
			run <= avs_s0_writedata[0];
		else if (avs_s0_write && avs_s0_address == 10'h201)
			div <= avs_s0_writedata[31:0];
		else if (avs_s0_write && avs_s0_address == 10'h202)
			pre <= avs_s0_writedata[ADDR_WIDTH-1:0];
		else if (avs_s0_write && avs_s0_address == 10'h203)
			source <= avs_s0_writedata[4:0];
		else if (avs_s0_write && avs_s0_address == 10'h204)
			slope <= avs_s0_writedata[0];
		else if (avs_s0_write && avs_s0_address == 10'h205)
			level <= avs_s0_writedata[15:0];
	end

	assign avs_s0_readdata = (avs_s0_address == 10'h200) ? state :
									 (avs_s0_address == 10'h201) ? sample :
									 q;
									 
	always @(posedge csi_clk) begin
		if (rsi_reset) begin
			div_count <= 0;
		end else if (asi_in0_valid) begin
			if (div_count == div - 1)
				div_count <= 0;
			else
				div_count <= div_count + 1;		
		end
	end
	assign divided_valid = asi_in0_valid && (div_count == 0);
	
	always @(posedge csi_clk) begin
		if (rsi_reset) begin
			state <= STATE_IDLE;
		end else begin
			case (state)			
			STATE_IDLE : begin
				if (run) begin
					if (pre == 0)
						state <= STATE_WAITING1;
					else
						state <= STATE_ARMING;
				end
				
				sample <= 0;
				coe_complete <= 1'b0;
			end			
			STATE_ARMING : begin
				if (run == 1'b0)
					state <= STATE_IDLE;
				else if (sample == pre)
					state <= STATE_WAITING1;

				if (divided_valid)
					sample <= sample + 1;
			end			
			STATE_WAITING1 : begin
				if (run == 1'b0)
					state <= STATE_IDLE;
				else if (source == 5'd0 && slope == 1'b0) begin
					if (asi_in0_valid && (asi_in0_data & 16'hffff) < level)
						state <= STATE_WAITING2;
				end else if (source == 5'd0 && slope == 1'b1) begin
					if (asi_in0_valid && (asi_in0_data & 16'hffff) > level)
						state <= STATE_WAITING2;
				end else if (source == 5'd1 && slope == 1'b0) begin
					if (asi_in0_valid && (asi_in0_data >> 16) < level)
						state <= STATE_WAITING2;
				end else if (source == 5'd1 && slope == 1'b1) begin
					if (asi_in0_valid && (asi_in0_data >> 16) > level)
						state <= STATE_WAITING2;
				end
			end			
			STATE_WAITING2 : begin
				if (run == 1'b0)
					state <= STATE_IDLE;
				else if (source == 5'd0 && slope == 1'b0) begin
					if (asi_in0_valid && (asi_in0_data & 16'hffff) > level)
						state <= STATE_TRIGGERED;
				end else if (source == 5'd0 && slope == 1'b1) begin
					if (asi_in0_valid && (asi_in0_data & 16'hffff) < level)
						state <= STATE_TRIGGERED;
				end else if (source == 5'd1 && slope == 1'b0) begin
					if (asi_in0_valid && (asi_in0_data >> 16) > level)
						state <= STATE_TRIGGERED;
				end else if (source == 5'd1 && slope == 1'b1) begin
					if (asi_in0_valid && (asi_in0_data >> 16) < level)
						state <= STATE_TRIGGERED;
				end
			end			
			STATE_TRIGGERED : begin
				if (run == 1'b0)
					state <= STATE_IDLE;
				else if (sample == ADDR_SIZE)
					state <= STATE_COMPLETED;

				if (divided_valid)
					sample <= sample + 1;
			end			
			STATE_COMPLETED : begin
				if (run == 1'b0)
					state <= STATE_IDLE;
						
				coe_complete <= 1'b1;
			end			
			endcase
		end
	end	
	assign wren = divided_valid && 
		(state != STATE_IDLE && state != STATE_COMPLETED);
		
	always @(posedge csi_clk) begin
		if (rsi_reset) begin
			addr <= 0;
		end else if (wren) begin
			addr <= addr + 1;
		end
	end
	
	ram2_32x512 ram2_32x512_inst (
		.clock(csi_clk),
		.wraddress(addr),
		.data(asi_in0_data),
		.wren(wren),
		.rdaddress(addr + avs_s0_address),
		.q(q)
	);
	
endmodule
