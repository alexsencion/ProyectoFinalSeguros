using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using InsuranceCompany.Entities;
using InsuranceCompany.Helpers;
using InsuranceCompany.Services.Interfaces;

namespace InsuranceCompany.Services.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly InsuranceCompanyContext _context;

        public UserRepository(InsuranceCompanyContext context)
        {
            _context = context;
        }

        public List<Role> GetRoleByUserId(int userId)
        {
            return _context.Users.Where(user => user.Id == userId).Select(us => us.Role).ToList();
        }

        public User ValidateUser(string userName, string password)
        {
            User user;
            try
            {
                var encodedPassword = EncryptionUtil.Encode(password);
                user = _context.Users.FirstOrDefault(us => us.UserName == userName && us.Password == encodedPassword);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(InvalidOperationException))
                    return null;
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }

            return user;
        }
    }
}