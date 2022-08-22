using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using InsuranceCompany.Entities;
using InsuranceCompany.Services.Implementations;
using Microsoft.Owin.Security.OAuth;

namespace InsuranceCompany.AuthorizationProviders
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); 
            await Task.Delay(1);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Delay(1);
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            var userRepository = new UserRepository(new InsuranceCompanyContext(connectionString));
            var user = userRepository.ValidateUser(context.UserName, context.Password);

            if(user == null)
            {
                context.SetError("Invalid_Grant", "There was an error with your login details. Please check them and try again");
                return;
            }

            var roles = userRepository.GetRoleByUserId(user.Id);

            foreach (var role in roles) {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Rol));
            }
         
            identity.AddClaim(new Claim("username", user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            context.Validated(identity);       
        }
    }
}