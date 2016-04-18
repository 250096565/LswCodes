using StudyABP.EntityFramework;
using EntityFramework.DynamicFilters;

namespace StudyABP.Migrations.SeedData
{
    public class InitialDataBuilder
    {
        private readonly StudyABPDbContext _context;

        public InitialDataBuilder(StudyABPDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            _context.DisableAllFilters();

            new DefaultEditionsBuilder(_context).Build();
            new DefaultTenantRoleAndUserBuilder(_context).Build();
            new DefaultLanguagesBuilder(_context).Build();
        }
    }
}
