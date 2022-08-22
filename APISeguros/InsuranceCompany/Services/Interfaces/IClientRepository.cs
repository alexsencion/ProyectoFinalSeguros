using System.Collections.Generic;
using InsuranceCompany.Entities;

namespace InsuranceCompany.Services.Interfaces
{
    public interface IClientRepository
    {
        List<Client> GetClients();
        List<Client> GetClientsByName(string name);
        Client GetClientsById(string clientId);
        Client GetClientByPolicy(string policyId);
        void CreateDatabase();
    }
}
