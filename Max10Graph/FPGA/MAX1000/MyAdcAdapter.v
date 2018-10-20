`timescale 1 ps / 1 ps

module MyAdcAdapter (
		input  wire        csi_clk,                //      csi.clk
		input  wire        rsi_reset,              //      rsi.reset
		
		input  wire [11:0] asi_in0_data,           //  asi_in0.data
		input  wire        asi_in0_valid,          //         .valid
		input  wire [4:0]  asi_in0_channel,        //         .channel
		input  wire        asi_in0_startofpacket,  //         .startofpacket
		input  wire        asi_in0_endofpacket,    //         .endofpacket
		
		output wire [31:0] aso_out0_data,          // aso_out0.data
		output wire        aso_out0_valid          //         .valid
	);

	reg [11:0] asi_in0_data_d;
	
	always @(posedge csi_clk) begin
		if (rsi_reset) begin
			asi_in0_data_d <= 0;
		end else begin
			if (asi_in0_startofpacket)
				asi_in0_data_d <= asi_in0_data;
		end
	end

	assign aso_out0_data = { 4'b0000, asi_in0_data, 4'b0000, asi_in0_data_d };
	assign aso_out0_valid = asi_in0_endofpacket;

endmodule
