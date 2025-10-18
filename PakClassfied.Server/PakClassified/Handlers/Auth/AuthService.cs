using Microsoft.EntityFrameworkCore;
using PakClassified;
using PakClassified.Entities.UserEntities;
//using Project.Context.Entites.UserEntites;
using System;

namespace Project.Context.Handlers.Auth
{
    public class AuthServices
    {

        public User? GetUserByEmail(string email, string password)
        {
            using (var context = new PakClassifiedContext())
            {
                var user = context.Users
                                    .Include(u => u.Role)
                                    .FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return null;

                bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

                return isValid ? user : null;
            }

        }

        public User? AddUser(User user)
        {
            using (var context = new PakClassifiedContext())
            {
                context.Entry(user.Role).State = EntityState.Unchanged;
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }

        }
    }
}
