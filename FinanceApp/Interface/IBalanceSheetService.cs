using FinanceApp.Class;

namespace FinanceApp.Interface
{
    public interface IBalanceSheetService
    {      
        List<Income> GetIncomes();
        List<Expense> GetExpenses();
        decimal GetTotalIncome();
        decimal GetTotalExpenses();
        decimal CalculateNetIncome();
        void AddToBudget(IBalanceSheetItem item);
        void RemoveExpenseById(int id);
        void RemoveIncomeById(int id);
        void EditIncomeById(int id, decimal newAmount);
        void EditExpenseById(int id, decimal newAmount, string newDescription);
    }
}
