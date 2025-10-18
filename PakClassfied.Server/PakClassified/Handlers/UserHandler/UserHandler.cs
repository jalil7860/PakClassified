using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCrypt.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PakClassified.Entities.Location;
using PakClassified.Entities.UserEntities;

namespace PakClassified.Handlers.UserHandler
{
    public interface IUserHandler
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User? Create(User request);
        User? Update(int id, User request);
        User? Delete(int id);
    }
    public class UserHandler : IUserHandler
    {
        public IEnumerable<User> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from user in dbContext.Users
                            .Include(c => c.Role)
                            .Include(c => c.Advertisements)
                            where !user.IsDeleted
                            select user).OrderByDescending(c => c.CreatedDate).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from user in dbCobtext.Users
                        .Include(c => c.Role)
                            where user.Id == id && !user.IsDeleted
                            select user).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public User? Create(User request)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {

                    //dbContext.Entry(request.Role).State = EntityState.Unchanged;
                    request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    //request.CreatedDate = DateTime.Now;
                    //request.CreatedBy = 1;
                    dbContext.Add(request);
                    dbContext.SaveChanges();
                    return request;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User? Delete(int id)
        {
            try
            {
                User? found = GetById(id);
                if (found != null)
                {
                    using (PakClassifiedContext dbContext = new PakClassifiedContext())
                    {
                        dbContext.Entry(found.Role).State = EntityState.Unchanged;
                        found.IsDeleted = true;
                        found.DeletedDate = DateTime.Now;
                        dbContext.Update(found);
                        dbContext.SaveChanges();
                        return found;
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public User? Update(int id, User request)
        {
            try
            {
                User? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        dbContext.Entry(request.Role).State = EntityState.Unchanged;
                        found.Name = request.Name;
                        found.Email = request.Email;
                        found.Password = request.Password;
                        found.RoleId = request.RoleId;
                        found.ModifiedDate = DateTime.Now;

                        dbContext.Update(found);
                        dbContext.SaveChanges();
                        return found;
                    }
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
