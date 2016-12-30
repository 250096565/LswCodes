using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using EFDataAccess;

namespace EFModule
{
    /// <summary>
    /// EF预热模块
    /// </summary>
    public class EfPreheatModule
    {

        public static void Init()
        {
            using (var dbContext = new SAYEntities())
            {
                var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }
    }
}
