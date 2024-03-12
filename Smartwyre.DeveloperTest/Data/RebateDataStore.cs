using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore
{
    private static List<(Rebate Rebate, decimal RebateAmount)> storedResults = new List<(Rebate, decimal)>();

    public List<Rebate> _rebates;

    public RebateDataStore()
    {
        // Initialize with three example rebates
        _rebates = new List<Rebate>
        {
            new Rebate
            {
                Identifier = "REB1",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 10.0m,
                Percentage = 0.5m
            },
            new Rebate
            {
                Identifier = "REB2",
                Incentive = IncentiveType.AmountPerUom,
                Amount = 25.0m,
                Percentage = 0.0m
            },
            new Rebate
            {
                Identifier = "REB3",
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 5.0m,
                Percentage = 0.1m
            }
        };
    }
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return _rebates.FirstOrDefault(r => r.Identifier.Equals(rebateIdentifier, StringComparison.OrdinalIgnoreCase));

    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
        storedResults.Add((account, rebateAmount));
    }

    public List<(Rebate Rebate, decimal RebateAmount)> GetStoredResults()
    {
        return storedResults;
    }
 
}
