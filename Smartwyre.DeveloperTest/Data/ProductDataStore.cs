using Smartwyre.DeveloperTest.Types;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore
{

    public Product[] _products;

    public ProductDataStore()
    {
        _products = new Product[]
        {
            new Product
            {
                Id = 1,
                Identifier = "PROD1",
                Price = 50.0m,
                Uom = "Pack",
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            },
            new Product
            {
                Id = 2,
                Identifier = "PROD2",
                Price = 30.0m,
                Uom = "Box",
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            },
            new Product
            {
                Id = 3,
                Identifier = "PROD3",
                Price = 20.0m,
                Uom = "Each",
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            },
        };

     }
    public Product GetProduct(string productIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 

        return _products.FirstOrDefault(p => p.Identifier == productIdentifier);

    }
}
