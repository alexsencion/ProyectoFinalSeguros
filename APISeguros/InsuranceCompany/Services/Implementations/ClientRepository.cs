using InsuranceCompany.Entities;
using System.Collections.Generic;
using System.Linq;
using InsuranceCompany.Services.Interfaces;

namespace InsuranceCompany.Services.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly InsuranceCompanyContext _context;

        public ClientRepository(InsuranceCompanyContext context)
        {
            _context = context;
        }
        public List<Client> GetClients()
        {
            //Calling ToList() ensures that iteration has to happen; and to achieve that, the query must be executed in our DB.
            return _context.Clients.OrderBy(c => c.Name).ToList();
        }

        public List<Client> GetClientsByName(string name)
        {
            return _context.Clients.Where(client => client.Name == name).ToList();
        }

        public Client GetClientsById(string clientId)
        {
            return _context.Clients.FirstOrDefault(client => client.Id == clientId);
        }

        public Client GetClientByPolicy(string policyId)
        {
            return _context.Policies.Where(policy => policy.Id == policyId).Select(p => p.Client).FirstOrDefault();
        }

        public void CreateDatabase()
        {
            _context.EnsureSeedDataForContext();
        }
    }
}