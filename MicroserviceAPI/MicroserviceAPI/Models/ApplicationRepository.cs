using MicroserviceAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceAPI.Model
{
    public class ApplicationRepository : IApplicationRepository
    {
        private ApplicationDBContext context;

        public ApplicationRepository(ApplicationDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Customer> Customers => context.Customers;
        public IQueryable<State> States => context.States;
        public IQueryable<Lga> Lgas => context.Lgas;
        public IQueryable<Bank> Banks => context.Banks;


        public void AddCustomer(Customer a)
        {
            context.Customers.Add(a);
          
        }
        public void AddBank(Bank b)
        {
            context.Banks.Add(b);
        }
        public void AddState(State c)
        {
            context.States.Add(c);
        }
        public void AddLga(Lga d)
        {
            context.Lgas.Add(d);
        }


        public void RemoveCustomer(Customer a)
        {
            context.Customers.Remove(a);
        }
        public void RemoveBank(Bank a)
        {
            context.Banks.Remove(a);
        }
        public void RemoveState(State a)
        {
            context.States.Remove(a);
        }
        public void RemoveLga(Lga a)
        {
            context.Lgas.Remove(a);
        }

        public void Save()
        {
            context.SaveChanges();

        }

        public void Remove()
        {
            context.SaveChanges();
        }

    }
}


