namespace Nop.Plugin.Misc.WebApi.Frontend.Dto.Orders;
public partial class CustomerOrderDetailsModelDto
{
    public int OrderId { get; set; }

    public string TotalAmount { get; set; }

    public DateTime OrderDate { get; set; }
}
