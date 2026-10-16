namespace Shop_WebApi.Dto
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int itemsCount { get; set; }
        public string Total { get; set; } = string.Empty;
        
        public List<ItemsResponseDto> Items { get; set; } = new List<ItemsResponseDto>();

    }

    public class ItemsResponseDto
    {
        public string Title { get; set; } = string.Empty ;
        public int Quantity { get; set; }
        public string Price { get; set; } = string .Empty ;
    }

    public class ErrorResponseDto
    {
        public string Error { get; set; } = string.Empty;
    }
}
