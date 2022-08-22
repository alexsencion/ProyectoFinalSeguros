using InsuranceCompany.Controllers;
using InsuranceCompany.Entities;
using InsuranceCompany.Models.DTO;
using InsuranceCompany.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace InsuranceCompany.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTest
    {
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<IPolicyRepository> _mockPolicyRepository;
        private readonly CompanyController _objectToTest;

        public CompanyControllerTest()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockPolicyRepository = new Mock<IPolicyRepository>();

            _objectToTest = new CompanyController(_mockClientRepository.Object, _mockPolicyRepository.Object);

            Startup.MappingConfigs.SetupMappings();
        }

        [TestClass]
        public class GetClientTests : CompanyControllerTest
        {
            [TestMethod]
            public void Returns_Client_List()
            {
                // Arrange
                _mockClientRepository.Setup(x => x.GetClients()).Returns(new List<Entities.Client>());
                var expected = new List<ClientDto>();
                
                // Act
                var result = _objectToTest.GetClients();

                // Assert
                Assert.IsNotNull(result);
                CollectionAssert.AreEqual(expected, result);
            }
        }

        [TestClass]
        public class GetClientByIdTests : CompanyControllerTest
        {
            [TestMethod]
            public void Returns_A_Client()
            {
                // Arrange
                _mockClientRepository.Setup(x => x.GetClientsById(It.IsAny<string>())).Returns(new Client() { Id = "1" });
                var expected = new ClientDto(){ Id = "1"};
                
                // Act
                var result = _objectToTest.GetClientById("1");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(expected.Id, result.Id);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Throws_Exception_If_Id_Is_NullOrWhiteSpace()
            {
                // Arrange
                var Id = "";

                // Act
                _objectToTest.GetClientById(Id);

            }
        }

        [TestClass]
        public class GetClientByNameTests : CompanyControllerTest
        {
            [TestMethod]
            public void Returns_List_Of_Clients()
            {
                // Arrange
                _mockClientRepository.Setup(x => x.GetClientsByName(It.IsAny<string>())).Returns(new List<Client>());
                var expected = new List<ClientDto>();
                
                // Act
                var result = _objectToTest.GetClientByName("name");

                // Assert
                Assert.IsNotNull(result);
                CollectionAssert.AreEqual(expected, result);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Throws_Exception_If_Name_Is_NullOrWhiteSpace()
            {
                // Arrange
                var name = "";

                // Act
                _objectToTest.GetClientByName(name);

            }
        }

        [TestClass]
        public class GetPoliciesByClientNameTests : CompanyControllerTest
        {
            [TestMethod]
            public void Returns_List_Of_Policies()
            {
                // Arrange
                var returnedValue = new List<Policy>();
                _mockPolicyRepository.Setup(x => x.GetPolicyByClientName(It.IsAny<string>())).Returns(returnedValue);
                var expected = new List<PolicyDto>();
                
                // Act
                var result = _objectToTest.GetPoliciesByClientName("name");

                // Assert
                Assert.IsNotNull(result);
                CollectionAssert.AreEqual(expected, result);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Throws_Exception_If_ClientName_Is_NullOrWhiteSpace()
            {
                // Arrange
                var name = "";

                // Act
                _objectToTest.GetPoliciesByClientName(name);

            }
        }

        [TestClass]
        public class GetClientByPolicyIdTests : CompanyControllerTest
        {
            [TestMethod]
            public void Returns_A_Client()
            {
                // Arrange
                _mockClientRepository.Setup(x => x.GetClientByPolicy(It.IsAny<string>())).Returns(new Client(){ Id = "1"});
                var expected = new ClientDto(){ Id = "1" };
                
                // Act
                var result = _objectToTest.GetClientByPolicy("policy");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(expected.Id, result.Id);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Throws_Exception_If_Id_Is_NullOrWhiteSpace()
            {
                // Arrange
                var Id = "";

                // Act
                _objectToTest.GetClientByPolicy(Id);

            }
        }
    }
}
