`timescale 1 ps / 1 ps

module top
(
	input wire CLK,
		
	input wire SPI_NSS,
	input wire SPI_MOSI,
	output wire SPI_MISO,
	input wire SPI_SCLK,
	
	output wire COMPLETE,
	
	output wire [7:0] LED
);

	reg spi_mosi;
	
	wire adc_clk;
	wire clk;
	wire locked;
	
	wire [31:0] pio_0;
	
	wire sample_valid;
	wire [31:0] sample_data;	
	reg [11:0] ch1;
	reg [11:0] ch2;
	
	always @(posedge SPI_SCLK) begin
		spi_mosi <= SPI_MOSI;
	end
	
	pll pll_inst (
		.inclk0 (CLK),
		.c0 (adc_clk),
		.c1 (clk),
		.locked (locked)
	);
	
	QsysCore QsysCore_inst (
		.clk_clk                                                                                         (clk),
		.reset_reset_n                                                                                   (1'b1),
		.spi_slave_to_avalon_mm_master_bridge_0_export_0_mosi_to_the_spislave_inst_for_spichain          (spi_mosi),
		.spi_slave_to_avalon_mm_master_bridge_0_export_0_nss_to_the_spislave_inst_for_spichain           (SPI_NSS),
		.spi_slave_to_avalon_mm_master_bridge_0_export_0_miso_to_and_from_the_spislave_inst_for_spichain (SPI_MISO),
		.spi_slave_to_avalon_mm_master_bridge_0_export_0_sclk_to_the_spislave_inst_for_spichain          (SPI_SCLK),
		.pio_0_external_connection_export                                                                (pio_0),
		.mybasicstorage_0_in0_data                                                                       (sample_data),
		.mybasicstorage_0_in0_valid                                                                      (sample_valid),
		.mybasicstorage_0_conduit_end_0_export                                                           (COMPLETE)
	);
	
	always @(posedge clk) begin
		ch1 <= ch1 + 1;
		ch2 <= ch2 + 16;
	end
	assign sample_data = { 4'h0, ch2, 4'h0, ch1 };
	assign sample_valid = 1'b1;
	
	assign LED = pio_0[7:0];

endmodule
