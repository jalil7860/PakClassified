using Microsoft.EntityFrameworkCore;
using PakClassified.Entities.PakClassified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Handlers.AdvertisementHandler.SubCategoryHandler
{
    public interface ISubCategoryHandler
    {
        IEnumerable<AdvertisementSubCategory> GetAll();
        AdvertisementSubCategory? GetById(int id);
        AdvertisementSubCategory Create(AdvertisementSubCategory request);
        AdvertisementSubCategory? Update(int id, AdvertisementSubCategory request);
        IEnumerable<AdvertisementSubCategory> GetByCategoryId(int categoryId);
        void Delete(int id);
    }
    public class SubCategoryHandler : ISubCategoryHandler
    {
        public IEnumerable<AdvertisementSubCategory> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from subCat in dbContext.AdvertisementSubCategories
                            .Include(a => a.Category)
                            where !subCat.IsDeleted
                            select subCat).OrderByDescending(a => a.CreatedDate).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }


        }

        public AdvertisementSubCategory? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from subCat in dbContext.AdvertisementSubCategories
                           .Include(a => a.Category)
                            where subCat.Id == id && !subCat.IsDeleted
                            select subCat).FirstOrDefault();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public IEnumerable<AdvertisementSubCategory> GetByCategoryId(int categoryId)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from subcategory in dbContext.AdvertisementSubCategories
                            .Include(a => a.Category)
                            .ThenInclude(a => a.SubCategories)
                            //.Include(a => a.Advertisements)
                            where subcategory.CategoryId == categoryId
                            select subcategory).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public AdvertisementSubCategory Create(AdvertisementSubCategory request)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    //dbContext.Entry(request.Category).State = EntityState.Unchanged;
                    request.CreatedDate = DateTime.UtcNow;
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

        public void Delete(int id)
        {
            try
            {

                AdvertisementSubCategory? found = GetById(id);
                if (found != null)
                {
                    using (PakClassifiedContext dbContext = new PakClassifiedContext())
                    {

                        dbContext.Remove(found);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public AdvertisementSubCategory? SoftDelete(int id)
        {
            try
            {
                AdvertisementSubCategory? found = GetById(id);
                if (found != null)
                {
                    using (PakClassifiedContext dbContext = new PakClassifiedContext())
                    {
                        dbContext.Entry(found.Category).State = EntityState.Unchanged;

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
        public AdvertisementSubCategory? Update(int id, AdvertisementSubCategory request)
        {
            try
            {
                AdvertisementSubCategory? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {

                        dbContext.Entry(found.Category).State = EntityState.Unchanged;
                        found.Name = request.Name;
                        found.CategoryId = request.CategoryId;

                        found.ModifiedDate = DateTime.Now;
                        //found.ModifiedBy = Modfied by user Id;

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
    }
}
