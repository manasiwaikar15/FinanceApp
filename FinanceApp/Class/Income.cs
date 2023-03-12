using FinanceApp.Interface;

namespace FinanceApp.Class
{
    public class Income : IBalanceSheetItem
    {
        public int ID { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Income(int id,string source, decimal amount, DateTime date)
        {
            ID = id;
            Source = source;
            Amount = amount;
            Date = date;
        }
    }
}
