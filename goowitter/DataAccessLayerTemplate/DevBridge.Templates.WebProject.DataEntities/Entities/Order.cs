using System;
using System.Collections.Generic;

namespace DevBridge.Templates.WebProject.DataEntities.Entities
{
    public class Order : EntityBase<Order>
    {
        public virtual DateTime OrderDate { get; set; }
        public virtual decimal OrderTotal { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
