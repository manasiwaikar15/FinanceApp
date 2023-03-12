namespace FinanceApp.Interface
{
    public interface IBalanceSheetItem
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
