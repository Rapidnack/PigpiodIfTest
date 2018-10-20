`timescale 1 ps / 1 ps

module MyBasicStorage (
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
	localparam STATE_RUNNING = 1;
	localparam STATE_COMPLETED = 2;	
	
	reg run;
	
	reg [2:0] state;
	reg [ADDR_WIDTH:0] sample;
	wire wren;
	
	reg [ADDR_WIDTH-1:0] addr;
	
	wire [31:0] q;
	
	always @(posedge csi_clk) begin
		if (rsi_reset) begin
			run <= 0;
		end
		else if (avs_s0_write && avs_s0_address == 10'h200)
			run <= avs_s0_writedata[0];
	end
	
	assign avs_s0_readdata = (avs_s0_address == 10'h200) ? state :
									 (avs_s0_address == 10'h201) ? sample :
									 q;
	
	always @(posedge csi_clk) begin
		if (rsi_reset) begin
			state <= STATE_IDLE;
		end else begin
			case (state)			
			STATE_IDLE : begin
				if (run)
					state <= STATE_RUNNING;
				
				sample <= 0;
				coe_complete <= 1'b0;
			end			
			STATE_RUNNING : begin
				if (run == 1'b0)
					state <= STATE_IDLE;
				else if (sample == ADDR_SIZE)
					state <= STATE_COMPLETED;

				if (asi_in0_valid)
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
	assign wren = asi_in0_valid && state == STATE_RUNNING;
	
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
