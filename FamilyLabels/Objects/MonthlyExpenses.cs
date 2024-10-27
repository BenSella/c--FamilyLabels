namespace FamilyLabels.Objects
{
    public class MonthlyExpenses
    {
        public int Id { get; set; } // Primary key
        public string? Expense {  get; set; }
        public DateTime? DateTime { get; set; }
        public double? Amount { get; set; }
    }
}
