using MyLittlePetShop.Auth.handler.interfaces;
using MyLittlePetShop.Auth.user.auth.interfaces;
using MyLittlePetShop.Auth.user.persist.interfaces;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Auth.handler
{
    public class AuthHandler : IAuthHandler
    {
        #region Properties
        private readonly IGenerateAccessTokenByEmailAndPassword _token;
        private readonly ICreateUser _create;
        #endregion

        #region constructor
        public AuthHandler(IGenerateAccessTokenByEmailAndPassword token, ICreateUser create)
        {
            _token = token;
            _create = create;
        }
        #endregion
        
        
        public AuthenticationToken GenerateAccessTokenByEmailAndPassword(string email, string password)
        {
            return _token.Execute(email, password);
        }

        public void CreateUser(User user)
        {
            _create.Execute(user);
        }
    }
}