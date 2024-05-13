namespace UnicontaAPI.Shared
{
    public sealed class CreateInvoiceDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "CompanyId is required")]
        public int CompanyId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "OrderNumber is required")]
        public int OrderNumber { get; set; }
        public long InvoiceNumber { get; set; }
        public DateTime? Date { get; set; }
    }
}