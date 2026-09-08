using Shared.Entities;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Repositories.Others;

public interface ISubsidiaryHandler
{
    Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetSubsidiariesAsync(DataGridIntent intent);
    Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetCurrentUserSubsidiariesAsync(DataGridIntent intent);
    Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetSubsidiariesByVendorAsync(DataGridIntent intent, int vendorId);
    Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetSubsidiariesByCustomerAsync(DataGridIntent intent, int customerId);
    Task<(IEnumerable<PurchaseCategoryVM> Data, int Count)> GetPurchaseCategoriesAsync(DataGridIntent intent);
    Task<(IEnumerable<PurchaseSubcategoryVM> Data, int Count)> GetPurchaseSubCategoriesAsync(PurchaseCategoryVM category, DataGridIntent intent);
    Task<SubsidiaryVM?> GetSubsidiaryAsync(int id);
}
