using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryAPI_tests
{
    public class OwnerRepositoryTests
    {
        // Create Mock of Dependencies
        Mock<FactoryContext> contextMock;

        // use fake object as dependency
        OwnerRepository ownerRepository;
        public OwnerRepositoryTests()
        {
            // Create Mock of Dependencies
            contextMock = new();

            // use fake object as dependency
            ownerRepository = new(contextMock.Object);
        }
        [Fact]
        [Trait("Author", "Noran")]
        [Trait("Priorty", "3")]
        public void GetOwnerById_AskForOwnerId12_OwnerObject()
        {
            // Arrange
            // Build the mock data
            List<Owner> owners = new List<Owner>() {
                new Owner() { Id = 11, Name = "Noran" },
                new Owner() { Id = 12, Name = "Tag" },
                new Owner() { Id = 13, Name = "Emad" }
            };
            // setup called Dbsets
            contextMock.Setup(o => o.Owners).ReturnsDbSet(owners);

            // Act
            Owner owner = ownerRepository.GetOwnerById(12);

            // Assert
            Assert.NotNull(owner);
        }

        [Fact]
        [Trait("Author", "Tag")]
        [Trait("Priorty", "2")]
        public void GetAllOwners_AskForAllOwner_OwnerObject()
        {
            // Arrange
            // Build the mock data
            List<Owner> owners = new List<Owner>() {
                new Owner() { Id = 11, Name = "Noran" },
                new Owner() { Id = 12, Name = "Tag" },
                new Owner() { Id = 13, Name = "Emad" }
            };
            // setup called Dbsets
            contextMock.Setup(o => o.Owners).ReturnsDbSet(owners);

            // Act
            List<Owner> ownersList = ownerRepository.GetAllOwners();

            // Assert
            Assert.NotNull(ownersList);
            Assert.NotEmpty(ownersList);
        }
    }
}
