using System.Data.Entity.Migrations;
using StudyABP.Migrations.SeedData;

namespace StudyABP.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StudyABP.EntityFramework.StudyABPDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "StudyABP";
        }

        protected override void Seed(StudyABP.EntityFramework.StudyABPDbContext context)
        {
            new InitialDataBuilder(context).Build();
        }
    }
}
