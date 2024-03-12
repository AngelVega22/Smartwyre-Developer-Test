using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Text.Json;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        private readonly ProductDataStore productDataStore;
        private readonly RebateDataStore rebateDataStore;
        private readonly RebateService rebateService;

        public Program(ProductDataStore productDataStore, RebateDataStore rebateDataStore, RebateService rebateService)
        {
            this.productDataStore = productDataStore;
            this.rebateDataStore = rebateDataStore;
            this.rebateService = rebateService;
        }

        static void Main(string[] args)
        {
            var productDataStore = new ProductDataStore();
            var rebateDataStore = new RebateDataStore();
            var rebateService = new RebateService();

            var program = new Program(productDataStore, rebateDataStore, rebateService);
            program.Run();
        }

        public void Run()
        {
            Console.Write("Hello! ");
            Console.Write('\n');
            Console.Write("This is the list of products");
            var Products = productDataStore._products;
            DisplayJson("Product", Products);



            Console.Write("Please enter Product Identifier: ");
            var productIdentifier = Console.ReadLine().ToUpper();

            // Retrieve product from ProductDataStore
            var product = productDataStore.GetProduct(productIdentifier);

            // Check if the product is found
            if (product != null)
            {
                DisplayJson("Product", product);

                Console.Write("This is the list of rebates");
                var Rebates = rebateDataStore._rebates;
                DisplayJson("Rebates", Rebates);

                Console.Write("Please enter Rebate Identifier: ");
                var rebateIdentifier = Console.ReadLine().ToUpper();

                // Retrieve rebate from RebateDataStore
                var rebate = rebateDataStore.GetRebate(rebateIdentifier);

                // Check if the rebate is found
                if (rebate != null)
                {
                    DisplayJson("Rebate", rebate);

                    CalculateRebate(product, rebate);

                    Console.Write('\n');

                    DisplayStoredResults();
                }
                else
                {
                    Console.WriteLine($"Rebate with identifier {rebateIdentifier} not found.");
                }
            }
            else
            {
                Console.WriteLine($"Product with identifier {productIdentifier} not found.");
            }

            Console.ReadLine();
        }

        private void DisplayJson(string title, object data)
        {
            object jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine($"{title}: \n{jsonData}\n");
        }

        private void CalculateRebate(Product product, Rebate rebate)
        {
            decimal volume;

            Console.Write("Please enter the volume: ");

            while (!decimal.TryParse(Console.ReadLine(), out volume))
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal value.");
                Console.Write("Please enter the volume: ");
            }
            var request = new CalculateRebateRequest
            {
                RebateIdentifier = rebate?.Identifier,
                ProductIdentifier = product?.Identifier,
                Volume = volume
            };

            var result = rebateService.Calculate(request);

            HandleRebateCalculationResult(product, result);
         }

        private void HandleRebateCalculationResult(Product product, CalculateRebateResult result)
        {
            if (result.Success)
            {
                Console.WriteLine($"Rebate calculation for product {product.Identifier} successful!");
                Console.WriteLine(result.Success);
            }
            else
            {
                Console.WriteLine($"Rebate calculation for product {product.Identifier} failed. Check the input parameters and supported incentives.");
            }
        }
        private void DisplayStoredResults()
        {
            var storedResults = rebateDataStore.GetStoredResults();

            Console.WriteLine("Stored Rebate Calculation Results:");

            foreach (var (rebate, rebateAmount) in storedResults)
            {
                Console.WriteLine($"Rebate Identifier: {rebate.Identifier}");
                Console.WriteLine($"Rebate Percentage: {rebate.Percentage * 100}%");
                Console.WriteLine($"Rebate Incentive: {rebate.Incentive}");
                Console.WriteLine($"Rebate Amount: {rebateAmount} ");
            }
        }
    }
}
