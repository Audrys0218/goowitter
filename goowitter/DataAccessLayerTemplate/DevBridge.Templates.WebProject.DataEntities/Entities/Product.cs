namespace DevBridge.Templates.WebProject.DataEntities.Entities
{
    public class Product : EntityBase<Product>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}