using FinanceApp.Interface;

namespace FinanceApp.Class
{
    public class Expense : IBalanceSheetItem
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Expense(int id, string category, decimal amount, DateTime date)
        {
            ID = id;
            Category = category;
            Amount = amount;
            Date = date;
        }
    }
}
