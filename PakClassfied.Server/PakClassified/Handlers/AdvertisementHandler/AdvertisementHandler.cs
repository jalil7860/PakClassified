using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PakClassified.Entities.PakClassified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Handlers.AdvertisementHandler
{
    public interface IAdvertisementHandler
    {
        IEnumerable<Advertisement> GetAll();
        IEnumerable<Advertisement> GetPostByUserId(int userId);
        Advertisement? GetById(int id);
        IEnumerable<Advertisement> SearchByQuery(string? name, int? categoryId, int? cityAreaId);
        Advertisement Create(Advertisement request);
        Advertisement? Update(Advertisement request, int id);
        void Delete(int id);
    }
    public class AdvertisementHandler : IAdvertisementHandler
    {
        public IEnumerable<Advertisement> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return dbContext.Advertisements
                         .Include(a => a.CityArea)
                             .ThenInclude(ca => ca.City)
                         .Include(a => a.SubCategory)
                             .ThenInclude(sc => sc.Category)
                         .Include(a => a.Status)
                         .Include(a => a.Type)
                         .Include(a => a.PostedBy)
                             .ThenInclude(u => u.Role)
                         .Include(a => a.Images)
                         .Include(a => a.Tags)
                         .Where(a => !a.IsDeleted)
                         .OrderByDescending(a => a.CreatedDate)
                         .Take(10)
                         .ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }


        }

        public Advertisement? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from advertisement in dbContext.Advertisements
                           .Include(a => a.CityArea)
                           .ThenInclude(a => a.City)
                           .Include(a => a.SubCategory)
                           .ThenInclude(a => a.Category)
                           .Include(a => a.Status)
                           .Include(a => a.Type)
                           .Include(a => a.Images)
                           .Include(a => a.Tags)
                           .Include(a => a.PostedBy)
                           .ThenInclude(a => a.Role)
                            where advertisement.Id == id && !advertisement.IsDeleted
                            select advertisement).FirstOrDefault();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public IEnumerable<Advertisement> GetPostByUserId(int userId)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from advertise in dbContext.Advertisements
                            .Include(a => a.CityArea)
                           .ThenInclude(a => a.City)
                           .Include(a => a.SubCategory)
                           .ThenInclude(a => a.Category)
                           .Include(a => a.Status)
                           .Include(a => a.Type)
                           .Include(a => a.Images)
                           .Include(a => a.Tags)
                           .Include(a => a.PostedBy)
                           .ThenInclude(a => a.Role)
                            where advertise.PostedById == userId && !advertise.IsDeleted
                            select advertise).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public IEnumerable<Advertisement> SearchByQuery(string? name, int? categoryId, int? cityAreaId)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    var res = (from advertisement in dbContext.Advertisements
                            .Include(a => a.CityArea)
                             .ThenInclude(ca => ca.City)
                         .Include(a => a.SubCategory)
                             .ThenInclude(sc => sc.Category)
                         .Include(a => a.Status)
                         .Include(a => a.Type)
                         .Include(a => a.PostedBy)
                             .ThenInclude(u => u.Role)
                         .Include(a => a.Images)
                         .Include(a => a.Tags)
                         .Where(a => !a.IsDeleted)
                         .OrderByDescending(a => a.CreatedDate)
                               select advertisement);
                    if (name != null)
                    {
                        res = res.Where(a => a.Name.Contains(name));
                    }
                    if (categoryId != null)
                    {
                        res = res.Where(a => a.SubCategory.CategoryId == categoryId);
                    }
                    if (cityAreaId != null)
                    {
                        res = res.Where(a => a.CityAreaId == cityAreaId);
                    }
                    return res.OrderByDescending(a => a.CreatedDate).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }


        }

        public Advertisement Create(Advertisement request)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    //dbContext.Entry(request.CityArea).State = EntityState.Unchanged;
                    request.CreatedBy = 1;
                    request.CreatedDate = DateTime.Now;

                    dbContext.Advertisements.Add(request);
                    dbContext.SaveChanges();

                    return request;
                }

            }
            catch (DbUpdateException err)
            {
                var inner = err.InnerException?.Message;
                throw new Exception($"Save failed: {inner}", err);
                
            }
        }

        public void Delete(int id)
        {
            try
            {
                Advertisement? found = GetById(id);
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

        public Advertisement? Update(Advertisement request, int id)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    Advertisement? found = (from Advertisement in dbContext.Advertisements
                                            .Include(a => a.CityArea)
                                            .Include(a => a.SubCategory)
                                            .Include(a => a.Status)
                                            .Include(a => a.Type)
                                            .Include(a => a.PostedBy)
                                            .Include(a => a.Images)
                                            .Include(a => a.Tags)
                                            where Advertisement.Id == id
                                            select Advertisement).FirstOrDefault();

                    if (found != null)
                    {
                        found.Name = request.Name;
                        found.Price = request.Price;
                        found.Description = request.Description;
                        found.Hits = request.Hits;
                        found.StartsOn = request.StartsOn;
                        found.EndsOn = request.EndsOn;
                        found.PostedById = request.PostedById;
                        found.SubCategoryId = request.SubCategoryId;
                        found.StatusId = request.StatusId;
                        found.TypeId = request.TypeId;
                        found.CityAreaId = request.CityAreaId;
                        found.ModifiedDate = DateTime.Now;
                        found.ModifiedBy = 1;

                        dbContext.Update(request);
                        dbContext.SaveChanges();
                        return request;
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
