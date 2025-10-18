using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakClassified.Entities.Location;
using PakClassified.Entities.PakClassified;

namespace PakClassified.Handlers.AdvertisementHandler
{
    public interface IAdvertisementStatusHandler
    {
        IEnumerable<AdvertisementStatus> GetAll();
        AdvertisementStatus? GetById(int id);
        AdvertisementStatus? Create(AdvertisementStatus request);
        AdvertisementStatus? Update(int id, AdvertisementStatus request);
        AdvertisementStatus? Delete(int id);
    }
    public class AdvertisementStatusHandler : IAdvertisementStatusHandler
    {

        public IEnumerable<AdvertisementStatus> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from status in dbContext.AdvertisementStatuses
                            where !status.IsDeleted
                            select status).OrderByDescending(a => a.CreatedDate).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AdvertisementStatus? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from status in dbCobtext.AdvertisementStatuses
                            where status.Id == id && !status.IsDeleted
                            select status).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public AdvertisementStatus? Create(AdvertisementStatus request)
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

        public AdvertisementStatus? Delete(int id)
        {
            try
            {
                AdvertisementStatus? found = GetById(id);
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

        public AdvertisementStatus? Update(int id, AdvertisementStatus request)
        {
            try
            {
                AdvertisementStatus? found = GetById(id);
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
