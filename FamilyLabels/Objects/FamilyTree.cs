namespace FamilyLabels.Objects
{
    public class FamilyTree
    {
        public int Id { get; set; } // Primary key
        public string? FamilyName { get; set; }
        public int? Members {  get; set; }
        public List<MonthlyExpenses> MonthlyExpenses { get; set; } = new List<MonthlyExpenses>();

    }
}
