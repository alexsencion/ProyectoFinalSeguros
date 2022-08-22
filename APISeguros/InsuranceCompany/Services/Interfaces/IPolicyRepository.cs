using InsuranceCompany.Entities;
using System.Collections.Generic;

namespace InsuranceCompany.Services.Interfaces
{
    public interface IPolicyRepository
    {
        List<Policy> GetPolicyByClientName(string name);
    }
}
