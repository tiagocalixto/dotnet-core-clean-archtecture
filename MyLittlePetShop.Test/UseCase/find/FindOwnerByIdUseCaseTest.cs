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
    public class FindOwnerByIdUseCaseTest
    {

        [Fact]
        public void ExistentId_Called_ReturnOwner()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(99, 12, 64);

            substituteRepository.FindById(Arg.Any<int>()).Returns(responseOwner);

            //Act
            var find = new FindOwnerByIdUseCase(substituteRepository);
            var result = find.Execute(99);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(responseOwner.Id, result.Id);
            Assert.Equal(responseOwner.Name, result.Name);
            Assert.Equal(responseOwner.Contacts[0].Id, result.Contacts[0].Id);
            Assert.Equal(responseOwner.Contacts[0].Type, result.Contacts[0].Type);
            Assert.Equal(responseOwner.Contacts[0].Value, result.Contacts[0].Value);

            substituteRepository.Received().FindById(Arg.Any<int>());
        }

        [Fact]
        public void NotExistentId_Called_ThenThrowKeyNotFoundException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwner(99, 12, 64);

            substituteRepository.FindById(Arg.Any<int>()).ReturnsNull();

            //Act
            var find = new FindOwnerByIdUseCase(substituteRepository);
            Assert.Throws<KeyNotFoundException>(() => find.Execute(38));

            substituteRepository.Received().FindById(Arg.Any<int>());
        }
    }
}
