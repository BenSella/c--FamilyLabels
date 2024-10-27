using FamilyLabels.Objects;
using FamilyLabels.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FamilyLabels.Controllers
{
    public class FamilyController : ControllerBase
    {
        IConfiguration _configuration;
        private readonly FamilyDbContext _context;

        public FamilyController(IConfiguration configuration, FamilyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        #region Family Section

        [HttpGet("GetFamily")]
        public IActionResult GetFamilyTree(int familyId)
        {
            FamilyTree familyTree = _context.FamilyTrees.Find(familyId);

            if (familyTree == null)
            {
                return NotFound($"Family with ID {familyId} not found.");
            }
            _context.Entry(familyTree).Collection("MonthlyExpenses").Load();
            return Ok(familyTree);
        }

        [HttpGet("GetAllFamilies")]
        public IActionResult GetAllFamilies()
        {
            List<FamilyTree> allFamilies = _context.FamilyTrees.ToList();
            foreach (FamilyTree family in allFamilies)
            {
                _context.Entry(family).Collection("MonthlyExpenses").Load();
            }
            return Ok(allFamilies);

        }

        [HttpPost("AddFamily")]
        public IActionResult AddFamilyTree(string familyName, int members)
        {

            FamilyTree familyTree = new FamilyTree()
            {
                FamilyName = familyName,
                Members = members
            };
            _context.FamilyTrees.Add(familyTree);
            _context.SaveChanges();

            return Ok(familyTree);

        }

        #endregion
    }
}
