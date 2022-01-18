using System;
using System.Data;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.Test.mocks;
using MyLittlePetShop.UseCase.owner.persist;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyLittlePetShop.Test.UseCase.persist
{
    public class SaveOwnerUseCaseTest
    {


        [Fact]
        public void OwnerAndPetAreValidAndContactAreValid_Called_ReturnCreatedOwner()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(99, 12, 64);

            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();
            substituteRepository.FindByPetChip(Arg.Any<string>()).ReturnsNull();
            substituteRepository.Save(Arg.Any<Owner>()).Returns(responseOwner);

            //Act
            var save = new SaveOwnerUseCase(substituteRepository);
            var result = save.Execute(Mocks.CreateMockOwner());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(responseOwner.Id, result.Id);
            Assert.Equal(responseOwner.Name, result.Name);
            Assert.Equal(responseOwner.Contacts[0].Id, result.Contacts[0].Id);
            Assert.Equal(responseOwner.Contacts[0].Type, result.Contacts[0].Type);
            Assert.Equal(responseOwner.Contacts[0].Value, result.Contacts[0].Value);

            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
            substituteRepository.Received().FindByPetChip(Arg.Any<string>());
            substituteRepository.Received().Save(Arg.Any<Owner>());
        }

        [Fact]
        public void OwnerAndPetAreValidAndContactAreNotValid_Called_ThenThrowsDataException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();

            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).Returns(Mocks.CreateMockOwner(47));

            //Act
            var save = new SaveOwnerUseCase(substituteRepository);
            Assert.Throws<DataException>(() => save.Execute(Mocks.CreateMockOwner()));

            //Assert
            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
            substituteRepository.DidNotReceive().FindByPetChip(Arg.Any<string>());
            substituteRepository.DidNotReceive().Save(Arg.Any<Owner>());
        }

        [Fact]
        public void OwnerAndPetAreNotValidAndContactAreValid_Called_ThenThrowsDataException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();

            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();
            substituteRepository.FindByPetChip(Arg.Any<string>()).Returns(Mocks.CreateMockOwner(16));

            //Act
            var save = new SaveOwnerUseCase(substituteRepository);
            Assert.Throws<DataException>(() => save.Execute(Mocks.CreateMockOwner()));         

            //Assert
            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
            substituteRepository.Received().FindByPetChip(Arg.Any<string>());
            substituteRepository.DidNotReceive().Save(Arg.Any<Owner>());
        }

    }
}
