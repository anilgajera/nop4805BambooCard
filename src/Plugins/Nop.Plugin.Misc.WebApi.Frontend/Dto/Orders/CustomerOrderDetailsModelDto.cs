namespace Nop.Plugin.Misc.WebApi.Frontend.Dto.Orders;
public partial class CustomerOrderDetailsModelDto
{
    public CustomerOrderDetailsModelDto(int orderId, string totalAmount, DateTime orderDate)
    {
        OrderId = orderId;
        TotalAmount = totalAmount;
        OrderDate = orderDate;
    }

    public int OrderId { get; set; }

    public string TotalAmount { get; set; }

    public DateTime OrderDate { get; set; }
}
