using System.Linq;
using Abp.Application.Editions;
using StudyABP.Editions;
using StudyABP.EntityFramework;

namespace StudyABP.Migrations.SeedData
{
    public class DefaultEditionsBuilder
    {
        private readonly StudyABPDbContext _context;

        public DefaultEditionsBuilder(StudyABPDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                //TODO: Add desired features to the standard edition, if wanted!
            }   
        }
    }
}