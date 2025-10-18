using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakClassified.Entities.Location;
using PakClassified.Entities.UserEntities;

namespace PakClassified.Handlers.UserHandler
{
    public interface IRoleHandler
    {
        IEnumerable<Role> GetAll();
        Role? GetById(int id);
        Role? Create(Role request);
        Role? Update(int id, Role request);
        Role? Delete(int id);
    }
    public class RoleHandler : IRoleHandler
    {
        public IEnumerable<Role> GetAll()
        {

            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from role in dbContext.Roles
                            where !role.IsDeleted
                            select role).OrderByDescending(c => c.CreatedDate).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Role? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from role in dbCobtext.Roles
                            where role.Id == id && !role.IsDeleted
                            select role).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Role? Create(Role request)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    request.CreatedDate = DateTime.Now;
                    request.CreatedBy = 1;
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

        public Role? Delete(int id)
        {
            try
            {
                Role? found = GetById(id);
                if (found != null)
                {
                    using (PakClassifiedContext dbContext = new PakClassifiedContext())
                    {
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
        public Role? Update(int id, Role request)
        {

            try
            {
                Role? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        found.Name = request.Name;
                        found.Rank = request.Rank;
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
