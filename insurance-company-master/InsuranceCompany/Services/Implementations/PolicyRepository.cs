using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceCompany.Entities;
using InsuranceCompany.Services.Interfaces;

namespace InsuranceCompany.Services.Implementations
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly InsuranceCompanyContext _context;

        public PolicyRepository(InsuranceCompanyContext context)
        {
            _context = context;
        }

        public List<Policy> GetPolicyByClientName(string clientName)
        {
            return _context.Policies.Where(policy => policy.Client.Name == clientName).ToList();
        }
    }
}