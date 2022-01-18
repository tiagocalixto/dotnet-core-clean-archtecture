using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.Test.mocks;
using MyLittlePetShop.UseCase.owner.find;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyLittlePetShop.Test.UseCase.find
{
    public class FindOwnerByContactUseCaseTest
    {
        [Fact]
        public void ContactValidValueValid_Called_ReturnOwner()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(99, 12, 64);

            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).Returns(responseOwner);

            //Act
            var find = new FindOwnerByContactUseCase(substituteRepository);
            var result = find.Execute(Mocks.CreateMockOwner().Contacts[0].Type, Mocks.CreateMockOwner().Contacts[0].Value);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(responseOwner.Id, result.Id);
            Assert.Equal(responseOwner.Name, result.Name);
            Assert.Equal(responseOwner.Contacts[0].Id, result.Contacts[0].Id);
            Assert.Equal(responseOwner.Contacts[0].Type, result.Contacts[0].Type);
            Assert.Equal(responseOwner.Contacts[0].Value, result.Contacts[0].Value);

            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void ContactAndValueInexistent_Called_ThenThrowKeyNotFoundException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();

            substituteRepository.ExistsById(Arg.Any<int>()).Returns(false);
            substituteRepository.FindByContact(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            //Act
            var find = new FindOwnerByContactUseCase(substituteRepository);
            Assert.Throws<KeyNotFoundException>(() => find.Execute(
                Mocks.CreateMockOwner().Contacts[0].Type, Mocks.CreateMockOwner().Contacts[0].Value));

            //Assert
            substituteRepository.Received().FindByContact(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
