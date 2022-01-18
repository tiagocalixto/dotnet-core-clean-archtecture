using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyLittlePetShop.DataProvider.context;
using MyLittlePetShop.DataProvider.mapper;
using MyLittlePetShop.DataProvider.models;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;

namespace MyLittlePetShop.DataProvider.repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PostgreSqlContext _context;
        
        public UserRepository(PostgreSqlContext context)
        {
            this._context = context;
        }
        
        public User FindByEmail(string email)
        {
            var db = _context.User.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());

            return UserDbMapper.ConvertDbToEntity(db);
        }

        public User Save(User user)
        {
            var db = UserDbMapper.ConvertEntityToDb(user);
            db.Id = 0;
            
            this._context.User.Add(db);
            this._context.SaveChanges();

            return UserDbMapper.ConvertDbToEntity(db);
        }

        public User Update(User user)
        {
            var db = _context.User.Find(user.Id);
            _context.Entry(db).State = EntityState.Detached;
            PrepareToUpdate(ref db, user);
            this._context.User.Update(db);
            this._context.SaveChanges();

            return UserDbMapper.ConvertDbToEntity(db);
        }

        public void Delete(int id)
        {
            var db = _context.User.Find(id);
            this._context.User.Remove(db);
            this._context.SaveChanges();
        }

        private void PrepareToUpdate(ref UserDb old, User user)
        {
            old.Email = user.Email;
            old.Password = user.Password;
            old.Role = user.Role;
            old.IsDeleted = false;
        }
    }
}