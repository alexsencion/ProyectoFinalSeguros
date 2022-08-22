using AutoMapper;
using InsuranceCompany.Entities;
using InsuranceCompany.Helpers;
using InsuranceCompany.Models.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace InsuranceCompany
{
    public static class InsuranceCompanyExtensions
    {
        public static void EnsureSeedDataForContext(this InsuranceCompanyContext context)
        {
            context.Database.EnsureCreated();
            //If we already have data, we don't insert anything. 
            if (context.Clients.Any())
            {
                return;
            }

            InsertClients(context);
            InsertPolicies(context);
            InsertUsers(context);

            context.SaveChanges();
        }

        private static void InsertPolicies(InsuranceCompanyContext context)
        {
            var policyJson = new System.Net.WebClient().DownloadString(ConfigurationManager.AppSettings["PolicyUri"]);
            var policiesList = JsonConvert.DeserializeObject<JsonPoliciesDto>(policyJson);
            context.Policies.AddRange(Mapper.Map<List<PolicyDto>, List<Policy>>(policiesList.Policies));
        }

        private static void InsertClients(InsuranceCompanyContext context)
        {
            var clientJson = new System.Net.WebClient().DownloadString(ConfigurationManager.AppSettings["ClientsUri"]);
            var clientList = JsonConvert.DeserializeObject<JsonClientsDto>(clientJson);
            context.Clients.AddRange(Mapper.Map<List<ClientDto>, List<Client>>(clientList.Clients));
        }

        private static void InsertUsers(InsuranceCompanyContext context)
        {
            var users = new List<User>()
            {
                new User()
                {
                    UserName = "admin",
                    Password = EncryptionUtil.Encode("admin"),
                    Role = new Role()
                    {
                        Rol = "Admin"
                    }
                },
                new User()
                {
                    UserName = "user",
                    Password = EncryptionUtil.Encode("user"),
                    Role = new Role()
                    {
                        Rol = "User"
                    }
                }
            };
            context.Users.AddRange(users);
        }
    }
}