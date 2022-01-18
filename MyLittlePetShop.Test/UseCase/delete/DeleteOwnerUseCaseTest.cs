using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.Test.mocks;
using MyLittlePetShop.UseCase.owner.delete;
using NSubstitute;
using Xunit;

namespace MyLittlePetShop.Test.UseCase.delete
{
    public class DeleteOwnerUseCaseTest
    {

        [Fact]
        public void OwnerExists_Called_ThenDeleteOwner()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();

            substituteRepository.ExistsById(Arg.Any<int>()).Returns(true);
            substituteRepository.DeleteById(Arg.Any<int>());

            //Act
            var delete = new DeleteOwnerUseCase(substituteRepository);
            delete.Execute(112);

            //Assert
            substituteRepository.Received().ExistsById(Arg.Any<int>());
            substituteRepository.Received().DeleteById(Arg.Any<int>());
        }

        [Fact]
        public void OwnerNotExists_Called_ThenThrowKeyNotFoundException()
        {
            //Arrange
            IOwnerRepository substituteRepository = Substitute.For<IOwnerRepository>();

            substituteRepository.ExistsById(Arg.Any<int>()).Returns(false); 

            //Act
            var delete = new DeleteOwnerUseCase(substituteRepository);
            Assert.Throws<KeyNotFoundException>(() => delete.Execute(99));

            //Assert
            substituteRepository.Received().ExistsById(Arg.Any<int>());
            substituteRepository.DidNotReceive().DeleteById(Arg.Any<int>());
        }
    }
}
