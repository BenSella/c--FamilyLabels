using FamilyLabels.Objects;
using FamilyLabels.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyLabels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyExpensesController : ControllerBase
    {
        IConfiguration _configuration;
        private readonly FamilyDbContext _context;
        public FamilyExpensesController(IConfiguration configuration, FamilyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        #region Expense Section

        [HttpGet ("GetEspense")]
        public IActionResult GetFamilyExpenses(int familyTreeId)
        {
            var familyTree = _context.FamilyTrees.Find(familyTreeId);

            if (familyTree == null)
            {
                return NotFound("Family not found.");
            }

            _context.Entry(familyTree).Collection("MonthlyExpenses").Load();
            return Ok(familyTree.MonthlyExpenses);
        }

        [HttpGet("GetAllFamiliesEspenses")]
        public IActionResult GetAllEspenses()
        {
            List<FamilyTree> allFamilies = _context.FamilyTrees.ToList();

            // Load related MonthlyExpenses for each family
            List<MonthlyExpenses> allExpenses = new List<MonthlyExpenses>();
            foreach (FamilyTree family in allFamilies)
            {
                _context.Entry(family).Collection("MonthlyExpenses").Load();
                // Add all expenses from this family to the allExpenses list
                allExpenses.AddRange(family.MonthlyExpenses);
            }

            return Ok(allExpenses);

        }
        [HttpPost("AddExpense")]
        public IActionResult FamilyExpenses(int expenses, string expense, int familyTreeId, DateTime dateTime)
        {
            var familyTree = _context.FamilyTrees.Find(familyTreeId);

            if (familyTree == null)
            {
                return NotFound("Family not found.");
            }

            _context.Entry(familyTree).Collection("MonthlyExpenses").Load();
           
            familyTree.MonthlyExpenses ??= new List<MonthlyExpenses>();

            MonthlyExpenses monthlyExpenses = new MonthlyExpenses
            {
                Expense = expense,
                DateTime = dateTime,
                Amount = expenses
            };

            familyTree.MonthlyExpenses.Add(monthlyExpenses);
            _context.SaveChanges();

            return Ok(familyTree);

        }
        #endregion
    }
}
