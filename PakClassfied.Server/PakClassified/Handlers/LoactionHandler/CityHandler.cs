using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PakClassified.Entities.Location;
using PakClassified.Entities.Location;

namespace PakClassified.Handler.LocationHandler
{
    public interface ICityHandler
    {
        IEnumerable<City> GetAll();
        City? GetById(int id);
        City? Create(City request);
        City? Update(int id, City request);
        City? Delete(int id);
    }
    public class CityHandler : ICityHandler
    {
        public IEnumerable<City> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from city in dbContext.Cities
                        .Include(c => c.Province)
                        .ThenInclude(p => p.Country)
                            where !city.IsDeleted
                            select city).OrderByDescending(c => c.CreatedDate).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public City? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from city in dbContext.Cities
                       .Include(c => c.Province)
                       .ThenInclude(p => p.Country)
                            where city.Id == id && !city.IsDeleted
                            select city).FirstOrDefault();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public City Create(City request)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    //dbContext.Entry(request.CityAreas).State = EntityState.Unchanged;
                    //dbContext.Entry(request.Province).State = EntityState.Unchanged;
                    //dbContext.Entry(request.Country).State = EntityState.Unchanged;

                    
                    dbContext.Cities.Add(request);
                    dbContext.SaveChanges();
                    return request;
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public City? Delete(int id)
        {
            try
            {
                City? found = GetById(id);
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


        public City? Update(int id, City request)
        {
            try
            {
                City? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        //dbContext.Entry(request.CityAreas).State = EntityState.Unchanged;
                        //dbContext.Entry(request.Province).State = EntityState.Unchanged;
                        //dbContext.Entry(request.Country).State = EntityState.Unchanged;
                        found.Name = request.Name;
                        found.ProvinceId = request.ProvinceId;
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
    }
}
