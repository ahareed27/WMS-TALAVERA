namespace Web.BlazorServer.ViewModels.Others;

public class PurchaseSubcategoryVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PurchaseCategoryId { get; set; }
}
