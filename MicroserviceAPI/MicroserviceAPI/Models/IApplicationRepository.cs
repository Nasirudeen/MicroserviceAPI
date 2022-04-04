using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceAPI.Model;
using MicroserviceAPI.Models;

namespace MicroserviceAPI
{
    public interface IApplicationRepository
    {
        IQueryable<Customer> Customers { get; }
        IQueryable<Bank> Banks { get; }
        IQueryable<State> States { get; }
        IQueryable<Lga> Lgas { get; }

        public void AddCustomer(Customer w);
        public void AddBank(Bank w);
        public void AddState(State w);
        public void AddLga(Lga w);

        public void RemoveCustomer(Customer w);
        public void RemoveBank(Bank w);
        public void RemoveState(State w);
        public void RemoveLga(Lga w);
        



        public void Save();
        public void Remove();
    }
}

