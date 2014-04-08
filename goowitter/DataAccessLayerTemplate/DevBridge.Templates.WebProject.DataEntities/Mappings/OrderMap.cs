using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public class OrderMap : EntityMapBase<Order>
    {
        public OrderMap()
        {
            Map(m => m.OrderDate).Not.Nullable();
            Map(m => m.OrderTotal).Not.Nullable();

            References(m => m.Customer);
            HasManyToMany(m => m.Products).Cascade.AllDeleteOrphan(); 
        }
    }
}