using FinanceApp.Class;
using FinanceApp.Interface;

namespace FinanceApp
{

    class Program
    {
        private readonly IBalanceSheetService budgetService;

        public Program(IBalanceSheetService budgetService)
        {
            this.budgetService = budgetService;
        }

        static void Main(string[] args)
        {
            IBalanceSheetService budgetService = new BalanceSheetService();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Balance sheet");
                Console.WriteLine("2. Add Income");
                Console.WriteLine("3. Add Expense");
                Console.WriteLine("4. Delete Income");
                Console.WriteLine("5. Delete Expense");
                Console.WriteLine("6. Edit Income");
                Console.WriteLine("7. Edit Expense");
                Console.WriteLine("8. Exit");
                Console.WriteLine();

                Console.Write("Enter a number to select an option: ");

                int option;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        ViewBalanceSheet(budgetService);
                        break;
                    case 2:
                        AddIncome(budgetService);
                        break;
                    case 3:
                        AddExpense(budgetService);
                        break;
                    case 4:
                        DeleteIncome(budgetService);
                        break;
                    case 5:
                        DeleteExpense(budgetService);
                        break;
                    case 6:
                        EditIncome(budgetService);
                        break;
                    case 7:
                        EditExpense(budgetService);
                        break;
                    case 8:
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        static void ViewBalanceSheet(IBalanceSheetService budgetService)
        {
            DisplayIncomes(budgetService);
            DisplayExpenses(budgetService);
            Console.WriteLine($"Current balance: {budgetService.CalculateNetIncome():C}");
        }

        private static bool DisplayExpenses(IBalanceSheetService budgetService)
        {
            bool flag;
            var expenses = budgetService.GetExpenses();
            if (expenses.Any())
            {
                Console.WriteLine("Your current expenses:");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.ID} {expense.Category}: {expense.Amount:C}");
            }
            Console.WriteLine();
                flag = true;
            }
            else flag = false;
            return flag;
        }

        private static bool DisplayIncomes(IBalanceSheetService budgetService)
        {
            bool flag;
            var incomes = budgetService.GetIncomes();
            if (incomes.Any())
            {
                Console.WriteLine("Your current income:");
                foreach (var income in incomes)
                {
                    Console.WriteLine($"{income.ID} {income.Source}: {income.Amount:C}");
                }
                Console.WriteLine();
                flag = true;
            }
            else flag = false;
            return flag;
        }

        private static void AddIncome(IBalanceSheetService budgetService)
        {
            Console.WriteLine("Enter the income source:");
            string source = Console.ReadLine();

            decimal amount;
            bool isValidAmount = false;
            do
            {
                Console.Write("Enter amount of income: ");
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out amount))
                {
                    if (amount >= 0)
                    {
                        isValidAmount = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number for the amount.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the amount.");
                }
            } while (!isValidAmount);

            DateTime date;
            while (true)
            {
                Console.Write("Enter a date (MM/dd/yyyy): ");
                string dateString = Console.ReadLine();

                if (DateTime.TryParse(dateString, out date))
                {
                    if (date > DateTime.Now)
                    {
                        Console.WriteLine("Date cannot be in future.");
                    }
                    else
                    break;
                }
                
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a date in the format MM/dd/yyyy.");
                }
            }

            int nextId = BalanceSheetService.GetNextIncomeId(budgetService.GetIncomes());
            budgetService.AddToBudget(new Income(nextId, source, amount, date));
            Console.WriteLine("Income added successfully.");
        }

        private static void AddExpense(IBalanceSheetService budgetService)
        {
            Console.WriteLine("Enter the expense description:");
            string description = Console.ReadLine();

            decimal amount;
            bool isValidAmount = false;

            do
            {
                Console.Write("Enter amount of expense: ");
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out amount))
                {
                    if (amount >= 0)
                    {
                        isValidAmount = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number for the amount.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the amount.");
                }
            } while (!isValidAmount);

            DateTime date;
            while (true)
            {
                Console.Write("Enter a date (MM/dd/yyyy): ");
                string dateString = Console.ReadLine();

                if (DateTime.TryParse(dateString, out date))
                {
                    if (date > DateTime.Now)
                    {
                        Console.WriteLine("Date cannot be in future.");
                    }
                    else
                        break;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a date in the format MM/dd/yyyy.");
                }
            }
            int nextId = BalanceSheetService.GetNextExpenseId(budgetService.GetExpenses());

            budgetService.AddToBudget(new Expense(nextId, description, amount, date));
            Console.WriteLine("Expense added successfully.");
        }

        private static void DeleteIncome(IBalanceSheetService budgetService)
        {
            try
            {
                bool result = DisplayIncomes(budgetService);
                if (result)
                {
                    Console.WriteLine("Enter the income id to delete:");
                    int id = Convert.ToInt32(Console.ReadLine());

                    try
                    {
                        budgetService.RemoveIncomeById(id);
                        Console.WriteLine("Income deleted successfully.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("You dont have anything to delete. Please add an income.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                return;
            }
        }

        private static void DeleteExpense(IBalanceSheetService budgetService)
        {
            try
            {
                bool result = DisplayExpenses(budgetService);
                if (result)
                {
                    Console.WriteLine("Enter the expense id to delete:");
                    int id = Convert.ToInt32(Console.ReadLine());

                    try
                    {
                        budgetService.RemoveExpenseById(id);
                        Console.WriteLine("Expense deleted successfully.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("You dont have anything to delete. Please add an expense.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                return;
            }
        }
        private static void EditIncome(IBalanceSheetService budgetService)
        {
            try
            {
                bool result = DisplayIncomes(budgetService);
                if (result)
                {
                    Console.WriteLine("Enter the ID of the income you want to edit:");
                    int incomeId = int.Parse(Console.ReadLine());

                    var income = budgetService.GetIncomes().Find(i => i.ID == incomeId);
                    if (income == null)
                    {
                        Console.WriteLine($"Income with ID {incomeId} not found.");
                        return;
                    }

                    Console.WriteLine($"Editing income with ID {incomeId} and amount:{income.Amount}\n");

                    Console.WriteLine("Enter the new name of the income (or press Enter to keep the current name):");
                    string newName = Console.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        income.Source = newName;
                    }

                    Console.WriteLine("Enter the new amount of the income (or press Enter to keep the current amount):");
                    string newAmountString = Console.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(newAmountString) && decimal.TryParse(newAmountString, out decimal newAmount))
                    {
                        income.Amount = newAmount;
                    }

                    Console.WriteLine($"Income with ID {incomeId} updated to:{income.Amount}");
                }
                else { Console.WriteLine("You dont have anything to edit. Please enter income first."); }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                return;
            }
        }

        private static void EditExpense(IBalanceSheetService budgetService)
        {
            try
            {
                bool result = DisplayExpenses(budgetService);
                if (result)
                {
                    Console.WriteLine("Enter the ID of the expense you want to edit:");
                    int expenseId = int.Parse(Console.ReadLine());

                    var expense = budgetService.GetExpenses().Find(i => i.ID == expenseId);
                    if (expense == null)
                    {
                        Console.WriteLine($"Expense with ID {expenseId} not found.");
                        return;
                    }

                    Console.WriteLine($"Editing expense with ID {expenseId} and amount:{expense.Amount}\n");

                    Console.WriteLine("Enter the new name of the expense (or press Enter to keep the current name):");
                    string newName = Console.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        expense.Category = newName;
                    }

                    Console.WriteLine("Enter the new amount of the expense (or press Enter to keep the current amount):");
                    string newAmountString = Console.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(newAmountString) && decimal.TryParse(newAmountString, out decimal newAmount))
                    {
                        expense.Amount = newAmount;
                    }

                    Console.WriteLine($"Income with ID {expenseId} updated to:{expense.Amount}");
                }
                else
                { Console.WriteLine("You dont have anything to edit. Please enter expense first.");}
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                return;
            }
        }
    }
}
