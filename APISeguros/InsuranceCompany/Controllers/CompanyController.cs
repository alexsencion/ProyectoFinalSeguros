using System;
using AutoMapper;
using InsuranceCompany.Entities;
using InsuranceCompany.Models.DTO;
using InsuranceCompany.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace InsuranceCompany.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPolicyRepository _policyRepository;

        public CompanyController(IClientRepository clientRepository, IPolicyRepository policyRepository)
        {
            _clientRepository = clientRepository;
            _policyRepository = policyRepository;
        }
        
        [HttpGet]
        [Route("companyApi/clients")]
        public List<ClientDto> GetClients()
        {
            return Mapper.Map<List<Client>, List<ClientDto>>(_clientRepository.GetClients());
        }

        [HttpGet]
        [Route("companyApi/clientbyId/{id}")]
        [Authorize(Roles = "User, Admin")]
        public ClientDto GetClientById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(id);
            }

            return Mapper.Map<Client, ClientDto>(_clientRepository.GetClientsById(id));
        }

        [HttpGet]
        [Route("companyApi/clientByName/{name}")]
        [Authorize(Roles = "User, Admin")]
        public List<ClientDto> GetClientByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(name);
            }

            return  Mapper.Map<List<Client>, List<ClientDto>>(_clientRepository.GetClientsByName(name)); 
        }

        [HttpGet]
        [Route("companyApi/policiesByClientName/{clientName}")]
        [Authorize(Roles = "Admin")]
        public List<PolicyDto> GetPoliciesByClientName(string clientName)
        {
            if (string.IsNullOrWhiteSpace(clientName))
            {
                throw new ArgumentNullException(clientName);
            }

            return Mapper.Map<List<Policy>, List<PolicyDto>>(_policyRepository.GetPolicyByClientName(clientName));
        }

        [HttpGet]
        [Route("companyApi/clientPolicy/{policyId}")]
        [Authorize(Roles = "Admin")]
        public ClientDto GetClientByPolicy(string policyId)
        {
            if (string.IsNullOrWhiteSpace(policyId))
            {
                throw new ArgumentNullException(policyId);
            }

            return Mapper.Map<Client, ClientDto>(_clientRepository.GetClientByPolicy(policyId));
        }

        [HttpGet]
        [Route("companyApi/createDatabase")]
        //[Authorize(Roles = "admin")]
        public IHttpActionResult CreateDatabase()
        {
            try
            {
                _clientRepository.CreateDatabase();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            
            return Ok("Database created");
        }
    }
}
