using System;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.Test.mocks;
using MyLittlePetShop.UseCase.owner.find;
using NSubstitute;
using Xunit;

namespace MyLittlePetShop.Test.UseCase.find
{
    public class FindOwnerByNameUseCaseTest
    {

        [Fact]
        public void ExistentName_Called_ReturnOwner()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();
            var responseOwner = Mocks.CreateMockOwnerList(99, 12, 64);

            substituteRepository.FindByName(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(responseOwner);

            //Act
            var find = new FindOwnerByNameUseCase(substituteRepository);
            var result = find.Execute("john", 1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(responseOwner[0].Id, result[0].Id);
            Assert.Equal(responseOwner[0].Name, result[0].Name);
            Assert.Equal(responseOwner[0].Contacts[0].Id, result[0].Contacts[0].Id);
            Assert.Equal(responseOwner[0].Contacts[0].Type, result[0].Contacts[0].Type);
            Assert.Equal(responseOwner[0].Contacts[0].Value, result[0].Contacts[0].Value);

            substituteRepository.Received().FindByName(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());
        }
    }
}
