using FinanceApp.Interface;

namespace FinanceApp.Class
{
    public class BalanceSheetService : IBalanceSheetService
    {
        private readonly List<Income> incomes;
        private readonly List<Expense> expenses;
        
        public BalanceSheetService()
        {
            this.incomes = new List<Income>();
            this.expenses = new List<Expense>();
            
        }

        public List<Income> GetIncomes()
        {
            return incomes;
        }

        public List<Expense> GetExpenses()
        {
            return expenses;
        }

        public decimal GetTotalIncome()
        {
            decimal total = 0;
            foreach (var item in incomes)
            {
                total += item.Amount;
            }
            return total;
        }

        public decimal GetTotalExpenses()
        {
            decimal total = 0;
            foreach (var item in expenses)
            {
                total += item.Amount;
            }
            return total;
        }

        public decimal CalculateNetIncome()
        {
            decimal totalIncome = GetTotalIncome();
            decimal totalExpenses = GetTotalExpenses();
            return totalIncome - totalExpenses;
        }

        public void AddToBudget(IBalanceSheetItem item)
        {
            if (item is Income income)
            {
                incomes.Add(income);
            }
            else if (item is Expense expense)
            {
                expenses.Add(expense);
            }
        }
        public static int GetNextIncomeId(List<Income> items)
        {
            return items.Count == 0 ? 1 : items.Max(i => i.ID) + 1;
        }

        public static int GetNextExpenseId(List<Expense> items)
        {
            return items.Count == 0 ? 1 : items.Max(i => i.ID) + 1;
        }

        public void RemoveExpenseById(int id)
        {
            var expense = expenses.FirstOrDefault(e => e.ID == id);

            if (expense == null)
            {
                throw new ArgumentException("Expense with the given ID was not found.");
            }

            expenses.Remove(expense);
        }

        public void RemoveIncomeById(int id)
        {
            var income = incomes.FirstOrDefault(e => e.ID == id);

            if (income == null)
            {
                throw new ArgumentException("Income with the given ID was not found.");
            }

            incomes.Remove(income);
        }

        public void EditIncomeById(int id, decimal newAmount)
        {
            var income = incomes.FirstOrDefault(i => i.ID == id);

            if (income == null)
            {
                throw new ArgumentException("Income with the given ID was not found.", nameof(id));
            }

            income.Amount = newAmount;
        }

        public void EditExpenseById(int id, decimal newAmount, string newDescription)
        {
            var expense = expenses.FirstOrDefault(e => e.ID == id);

            if (expense == null)
            {
                throw new ArgumentException("Expense with the given ID was not found.", nameof(id));
            }

            expense.Amount = newAmount;
            expense.Category = newDescription;
        }

    }
}
