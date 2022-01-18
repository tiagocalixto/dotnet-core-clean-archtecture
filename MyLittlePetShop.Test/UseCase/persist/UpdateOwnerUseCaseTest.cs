using System;
using System.Collections.Generic;
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
    public class UpdateOwnerUseCaseTest
    {

        [Fact]
        public void OwnerExistsAndPetAreValidAndContactAreValid_Called_ReturnUpdatedOwner()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(112, 34, 68);

            substituteRepository.ExistsById(Arg.Any<int>()).Returns(true);
            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).Returns(responseOwner);
            substituteRepository.FindByPetChip(Arg.Any<string>()).Returns(responseOwner);
            substituteRepository.Update(Arg.Any<Owner>()).Returns(responseOwner);

            //Act
            var update = new UpdateOwnerUseCase(substituteRepository);
            var result = update.Execute(Mocks.CreateMockOwner(112));

            //Assert
            Assert.NotNull(result);
            Assert.Equal(responseOwner.Id, result.Id);
            Assert.Equal(responseOwner.Name, result.Name);
            Assert.Equal(responseOwner.Contacts[0].Id, result.Contacts[0].Id);
            Assert.Equal(responseOwner.Contacts[0].Type, result.Contacts[0].Type);
            Assert.Equal(responseOwner.Contacts[0].Value, result.Contacts[0].Value);

            substituteRepository.Received().ExistsById(Arg.Any<int>());
            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
            substituteRepository.Received().FindByPetChip(Arg.Any<string>());
            substituteRepository.Received().Update(Arg.Any<Owner>());
        }

        [Fact]
        public void OwnerNotExistsAndPetAreValidAndContactAreValid_Called_TheThrowKeyNotFoundException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(112, 34, 68);

            substituteRepository.ExistsById(Arg.Any<int>()).Returns(false);

            //Act
            var update = new UpdateOwnerUseCase(substituteRepository);
            Assert.Throws<KeyNotFoundException>(() => update.Execute(Mocks.CreateMockOwner(112)));

            //Assert
            substituteRepository.Received().ExistsById(Arg.Any<int>());
            substituteRepository.DidNotReceive().FindByContact(Arg.Any<string>(), Arg.Any<string>());
            substituteRepository.DidNotReceive().FindByPetChip(Arg.Any<string>());
            substituteRepository.DidNotReceive().Update(Arg.Any<Owner>());
        }

        [Fact]
        public void OwnerExistsAndPetAreValidAndContactAreNotValid_Called_TheThrowDataException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(112, 34, 68);

            substituteRepository.ExistsById(Arg.Any<int>()).Returns(true);
            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).Returns(responseOwner);

            //Act
            var update = new UpdateOwnerUseCase(substituteRepository);
            Assert.Throws<DataException>(() => update.Execute(Mocks.CreateMockOwner(110)));

            //Assert
            substituteRepository.Received().ExistsById(Arg.Any<int>());
            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
            substituteRepository.DidNotReceive().FindByPetChip(Arg.Any<string>());
            substituteRepository.DidNotReceive().Update(Arg.Any<Owner>());
        }

        [Fact]
        public void OwnerExistsAndPetAreNotValidAndContactAreValid_Called_TheThrowDataException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(112, 34, 68);

            substituteRepository.ExistsById(Arg.Any<int>()).Returns(true);
            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();
            substituteRepository.FindByPetChip(Arg.Any<string>()).Returns(responseOwner);

            //Act
            var update = new UpdateOwnerUseCase(substituteRepository);
            Assert.Throws<DataException>(() => update.Execute(Mocks.CreateMockOwner(110)));

            //Assert
            substituteRepository.Received().ExistsById(Arg.Any<int>());
            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
            substituteRepository.Received().FindByPetChip(Arg.Any<string>());
            substituteRepository.DidNotReceive().Update(Arg.Any<Owner>());
        }
    }
}
