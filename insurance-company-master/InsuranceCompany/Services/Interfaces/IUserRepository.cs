using System.Collections.Generic;
using InsuranceCompany.Entities;

namespace InsuranceCompany.Services.Interfaces
{
    public interface IUserRepository
    {
        User ValidateUser(string userName, string password);
        List<Role> GetRoleByUserId(int userId);
    }
}
