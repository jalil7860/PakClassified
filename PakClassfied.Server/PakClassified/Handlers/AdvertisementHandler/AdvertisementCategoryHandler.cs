using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakClassified.Entities.Location;
using PakClassified.Entities.PakClassified;

namespace PakClassified.Handlers.AdvertisementHandler
{
    public interface IAdvertisementCategoryHandler
    {
        IEnumerable<AdvertisementCategory> GetAll();
        AdvertisementCategory? GetById(int id);
        AdvertisementCategory? Create(AdvertisementCategory request);
        AdvertisementCategory? Update(int id, AdvertisementCategory request);
        AdvertisementCategory? Delete(int id);
    }
    public class AdvertisementCategoryHandler : IAdvertisementCategoryHandler
    {
        public IEnumerable<AdvertisementCategory> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from category in dbContext.AdvertisementCategories
                            where !category.IsDeleted
                            select category).OrderByDescending(c => c.CreatedDate).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public AdvertisementCategory? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from category in dbCobtext.AdvertisementCategories
                            where category.Id == id && !category.IsDeleted
                            select category).FirstOrDefault();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public AdvertisementCategory? Create(AdvertisementCategory request)
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
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public AdvertisementCategory? Update(int id, AdvertisementCategory request)
        {
            try
            {
                AdvertisementCategory? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        found.Name = request.Name;
                        found.Description = request.Description;
                        found.ModifiedDate = DateTime.Now;

                        dbContext.Update(found);
                        dbContext.SaveChanges();
                        return found;
                    }
                    return null;
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public AdvertisementCategory? Delete(int id)
        {
            try
            {
                AdvertisementCategory? found = GetById(id);
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
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
    }
}
