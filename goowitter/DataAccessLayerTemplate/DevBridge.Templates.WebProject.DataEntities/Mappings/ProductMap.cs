using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public class ProductMap : EntityMapBase<Product>
    {
        public ProductMap()
        {
            Map(m => m.Name);
            Map(m => m.Description);
        }
    }
}