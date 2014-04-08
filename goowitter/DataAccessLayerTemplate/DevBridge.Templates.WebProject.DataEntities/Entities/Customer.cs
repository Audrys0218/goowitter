using System;
using System.Collections.Generic;

namespace DevBridge.Templates.WebProject.DataEntities.Entities
{
    public class Customer : EntityBase<Customer>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime? Birthday { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}