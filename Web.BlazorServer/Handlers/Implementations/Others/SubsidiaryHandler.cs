using Application.UseCases.Queries.Others;
using Application.UseCases.Queries.Transaction.SupplierReturn;
using Mapster;
using MediatR;
using Shared.Entities;
using Shared.Libraries.Utilities;
using System.Text.Json;
using Web.BlazorServer.Handlers.Repositories.Others;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Implementations.Others;

public class SubsidiaryHandler(IHttpContextAccessor contextAccessor, ISender sender) : ISubsidiaryHandler
{
    public async Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetSubsidiariesAsync(DataGridIntent intent)
    {
        GetSubsidiariesQry qry = new(intent);
        (var data, int count) = await sender.Send(qry);
        return (data.Adapt<IEnumerable<SubsidiaryVM>>(), count);
    }
    public async Task<SubsidiaryVM?> GetSubsidiaryAsync(int id)
    {
        GetSubsidiaryQry qry = new(id);
        var data = await sender.Send(qry);
        if (data is null) return null;

        return data.Adapt<SubsidiaryVM>();
    }
    public async Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetCurrentUserSubsidiariesAsync(DataGridIntent intent)
    {
        var newIntent = intent.Adapt<DataGridIntent>();

        string? claimValue = contextAccessor.HttpContext?.User?.FindFirst("com.direcbusiness.wms.nsAllowedSubsidiaries")?.Value;
        int[] userSubsidiaries = claimValue is null ? [] : JsonSerializer.Deserialize<List<int>>(claimValue)?.ToArray() ?? [];

        newIntent.Filters.Add(
            DataGridFilterUtilities.In(nameof(SubsidiaryVM.Id), userSubsidiaries)
        );

        GetSubsidiariesQry qry = new(newIntent);
        (var data, int count) = await sender.Send(qry);
        return (data.Adapt<IEnumerable<SubsidiaryVM>>(), count);
    }

    public async Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetSubsidiariesByVendorAsync(DataGridIntent intent, int vendorId)
    {
        GetSubsidiariesByVendorQry qry = new(intent, vendorId);
        (var data, int count) = await sender.Send(qry);
        return (data.Adapt<IEnumerable<SubsidiaryVM>>(), count);
    }
    public async Task<(IEnumerable<SubsidiaryVM> Data, int Count)> GetSubsidiariesByCustomerAsync(DataGridIntent intent, int customerId)
    {
        GetSubsidiariesByCustomerQry query = new(intent, customerId);
        (var data, int count) = await sender.Send(query);
        return (data.Adapt<IEnumerable<SubsidiaryVM>>(), count);
    }

    public async Task<(IEnumerable<PurchaseCategoryVM> Data, int Count)> GetPurchaseCategoriesAsync(DataGridIntent intent)
    {
        GetPurchaseCategoriesQry query = new(intent);
        (var data, int count) = await sender.Send(query);
        return (data.Adapt<IEnumerable<PurchaseCategoryVM>>(), count);
    }

    public async Task<(IEnumerable<PurchaseSubcategoryVM> Data, int Count)> GetPurchaseSubCategoriesAsync(PurchaseCategoryVM category, DataGridIntent intent)
    {
        var newIntent = intent.Adapt<DataGridIntent>();
        newIntent.Filters.Add(
            DataGridFilterUtilities.Equal(nameof(PurchaseSubcategoryVM.PurchaseCategoryId), category.Id)
        );
        newIntent.Take = 10;

        GetPurchaseSubcategoriesQry query = new(newIntent);
        (var data, int count) = await sender.Send(query);

        //if (category.Id == 3)
        //{
        //    data = data.Where(x => x.Id == 5).ToList();
        //    count = data.Count();
        //}

        return (data.Adapt<IEnumerable<PurchaseSubcategoryVM>>(), count);
    }
}
