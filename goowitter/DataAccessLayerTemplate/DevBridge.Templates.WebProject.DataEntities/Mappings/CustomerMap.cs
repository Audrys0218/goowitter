using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public class CustomerMap : EntityMapBase<Customer>
    {
        public CustomerMap()
        {
            Map(m => m.FirstName).Length(50).Not.Nullable();
            Map(m => m.LastName).Length(50).Not.Nullable();
            Map(m => m.Email).Length(100).Not.Nullable();
            Map(m => m.Birthday).Nullable();

            HasMany(m => m.Orders);
        }
    }
}