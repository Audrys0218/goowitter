using System;
using System.Transactions;

using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;

using NHibernate.Criterion;

using Order = DevBridge.Templates.WebProject.DataEntities.Entities.Order;

namespace ConsoleApplication
{
    class Program
    {
        private static readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        static void Main(string[] args)
        {
            CreateCustomer();
            CreateCustomerWithOrder();
            SelectCustomers();

            Console.ReadLine();
        }

        private static void CreateCustomer()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            var customer = new Customer
                               {
                                   FirstName = "TestFirstName",
                                   LastName = "TestLastName",
                                   Email = "test.email@test.com"
                               };

            repository.Save(customer);

        }

        private static void CreateCustomerWithOrder()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            using (var transaction = new TransactionScope())
            {
                var customer = new Customer()
                                        {
                                            FirstName = "CustomerWithOrder",
                                            LastName = "CustomerWithOrder",
                                            Email = "test.email@test.com"
                                        };

                repository.Save(customer);

                var order = new Order
                                {
                                    Customer = customer,
                                    OrderDate = DateTime.Now,
                                    OrderTotal = 134.87M
                                };

                repository.Save(order);


                repository.Commit();
                transaction.Complete();
            }
        }

        private static void SelectCustomers()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            Customer customerAlias = null;

            var list = repository
                .AsQueryOver(() => customerAlias)
                .Where(Restrictions.On(() => customerAlias.Email).IsLike("%test%"))
                .List();
        }
    }
}
