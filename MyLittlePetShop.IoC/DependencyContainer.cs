using System;
using Microsoft.Extensions.DependencyInjection;
using MyLittlePetShop.Auth.handler;
using MyLittlePetShop.Auth.handler.interfaces;
using MyLittlePetShop.Auth.user.auth;
using MyLittlePetShop.Auth.user.auth.interfaces;
using MyLittlePetShop.Auth.user.persist;
using MyLittlePetShop.Auth.user.persist.interfaces;
using MyLittlePetShop.DataProvider.repositories;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.UseCase.handler;
using MyLittlePetShop.UseCase.handler.interfaces;
using MyLittlePetShop.UseCase.owner.delete;
using MyLittlePetShop.UseCase.owner.delete.interfaces;
using MyLittlePetShop.UseCase.owner.find;
using MyLittlePetShop.UseCase.owner.find.interfaces;
using MyLittlePetShop.UseCase.owner.persist;
using MyLittlePetShop.UseCase.owner.persist.interfaces;


namespace MyLittlePetShop.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //repositories
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //use cases
            services.AddScoped<IUseCaseHandler, UseCaseHandler>();
            services.AddScoped<IDeleteOwnerUseCase, DeleteOwnerUseCase> ();
            services.AddScoped<IFindOwnerByContactUseCase, FindOwnerByContactUseCase> ();
            services.AddScoped<IFindOwnerByIdUseCase, FindOwnerByIdUseCase> ();
            services.AddScoped<IFindOwnerByNameUseCase, FindOwnerByNameUseCase> ();
            services.AddScoped<ISaveOwnerUseCase, SaveOwnerUseCase> ();
            services.AddScoped<IUpdateOwnerUseCase, UpdateOwnerUseCase> ();
            //Auth
            services.AddScoped<IAuthHandler, AuthHandler>();
            services.AddScoped<IGenerateAccessTokenByEmailAndPassword, GenerateAccessTokenByEmailAndPassword> ();
            services.AddScoped<ICreateUser, CreateUser>();
        }
    }
}
