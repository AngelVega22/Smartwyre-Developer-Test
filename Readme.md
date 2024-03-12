# Smartwyre Developer Test 

This project implements a rebate calculation system as part of the Smartwyre Developer Test. 
It includes a RebateService that calculates rebates based on different incentive types and stores the results in a data store.


## Installation
Clone the repository:

`
https://github.com/AngelVega22/Smartwyre-Developer-Test.git
`

### Usage

1. **Open the solution:**

    Open the solution in Visual Studio or your preferred code editor.

2. **Run the tests:**

    Ensure everything is working correctly by running the tests.

    ```bash
    dotnet test
    ```

3. **Explore the code:**

    Explore the `RebateService` class, `ProductDataStore`, and `RebateDataStore` for rebate calculation and data storage.

4. **Modify the `Program.cs` file:**

    Modify the `Program.cs` file in the `Smartwyre.DeveloperTest.Runner` project to interact with the rebate calculation system through the console.

5. **Run the console application:**

    Run the `Smartwyre.DeveloperTest.Runner` project to interactively calculate rebates.

    ```bash
    dotnet run --project Smartwyre.DeveloperTest.Runner
    ```