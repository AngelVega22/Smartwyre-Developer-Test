using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

public class RebateServiceTests : IClassFixture<RebateDataStoreFixture>
{
    private readonly RebateService _rebateService;
    private readonly RebateDataStore _rebateDataStore;
    private readonly ProductDataStore _productDataStore;  

    public RebateServiceTests(RebateDataStoreFixture fixture)
    {
        _rebateService = new RebateService();
        _rebateDataStore = fixture.RebateDataStore;
        _productDataStore = new ProductDataStore();  

    }

    [Fact]
    public void Calculate_RebateWithFixedCashAmount_Success()
    {
        string ProductId = "PROD3";
        string RebateId = "REB1";
        var product = _productDataStore.GetProduct(ProductId);   
        var rebate = _rebateDataStore.GetRebate(RebateId);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 1
        };

        var result = _rebateService.Calculate(request);

        Assert.True(result.Success);
        Assert.Equal(10.0m, _rebateDataStore.GetStoredResults().FirstOrDefault().Item2);
        _rebateDataStore.GetStoredResults().Clear();

    }

    [Fact]
    public void Calculate_RebateWithAmountPerUom_Success()
    {
        string ProductId = "PROD2";
        string RebateId = "REB2";
        var product = _productDataStore.GetProduct(ProductId);
        var rebate = _rebateDataStore.GetRebate(RebateId);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 3
        };

        var result = _rebateService.Calculate(request);

        Assert.True(result.Success);
        Assert.Equal(75.0m, _rebateDataStore.GetStoredResults().FirstOrDefault().Item2);
        _rebateDataStore.GetStoredResults().Clear();

    }

    [Fact]
    public void Calculate_RebateWithFixedRateRebate_Success()
    {
        string ProductId = "PROD1";
        string RebateId = "REB3";
        var product = _productDataStore.GetProduct(ProductId);
        var rebate = _rebateDataStore.GetRebate(RebateId);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 2
        };

        var result = _rebateService.Calculate(request);

        Assert.True(result.Success);
        Assert.Equal(10.0m, _rebateDataStore.GetStoredResults().FirstOrDefault().Item2);
        _rebateDataStore.GetStoredResults().Clear();

    }

    [Fact]
    public void Calculate_RebateWithInvalidIncentive_Failure()
    {
        string ProductId = "PROD3";
        string RebateId = "REB2";
        var product = _productDataStore.GetProduct(ProductId);
        var rebate = _rebateDataStore.GetRebate(RebateId);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 0
        };

        var result = _rebateService.Calculate(request);

        Assert.False(result.Success);
        _rebateDataStore.GetStoredResults().Clear();

    }
}

public class RebateDataStoreFixture : IDisposable
{
    public RebateDataStore RebateDataStore { get; }

    public RebateDataStoreFixture()
    {
        RebateDataStore = new RebateDataStore();
    }

    public void Dispose()
    {
        RebateDataStore.GetStoredResults().Clear();
    }
}
