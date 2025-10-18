using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakClassified.Entities.PakClassified;

namespace PakClassified.Handlers.AdvertisementHandler
{
    public interface IAdvertisementTypeHandler
    {
        IEnumerable<AdvertisementType> GetAll();
        AdvertisementType? GetById(int id);
        AdvertisementType? Create(AdvertisementType request);
        AdvertisementType? Update(int id, AdvertisementType request);
        AdvertisementType? Delete(int id);
    }
    public class AdvertisementTypeHandler : IAdvertisementTypeHandler
    {
        public IEnumerable<AdvertisementType> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from type in dbContext.AdvertisementTypes
                            where !type.IsDeleted
                            select type).OrderByDescending(a => a.CreatedDate).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AdvertisementType? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from type in dbCobtext.AdvertisementTypes
                            where type.Id == id && !type.IsDeleted
                            select type).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public AdvertisementType? Create(AdvertisementType request)
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

        public AdvertisementType? Delete(int id)
        {
            try
            {
                AdvertisementType? found = GetById(id);
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

        public AdvertisementType? Update(int id, AdvertisementType request)
        {
            try
            {
                AdvertisementType? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        found.Name = request.Name;
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
