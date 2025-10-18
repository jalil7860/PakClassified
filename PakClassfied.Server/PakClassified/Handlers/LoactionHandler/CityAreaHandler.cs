using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PakClassified.Entities.Location;
using PakClassified.Entities.Location;

namespace PakClassified.Handler.LocationHandler
{
    public interface ICityAreaHandler
    {
        IEnumerable<CityArea> GetAll();
        CityArea? GetById(int id);
        CityArea? Create(CityArea request);
        CityArea? Update(int id, CityArea request);
        CityArea? Delete(int id);

    }
    public class CityAreaHandler : ICityAreaHandler
    {
        public IEnumerable<CityArea> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from cityArea in dbContext.CityAreas
                            .Include(c => c.City)
                            where !cityArea.IsDeleted
                            select cityArea).OrderByDescending(c => c.CreatedDate).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public CityArea? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from cityArea in dbCobtext.CityAreas
                        .Include(c => c.City)
                            where cityArea.Id == id && !cityArea.IsDeleted
                            select cityArea).FirstOrDefault();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public CityArea Create(CityArea request)
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
        public CityArea Update(int id, CityArea request)
        {
            try
            {
                CityArea? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        //dbContext.Entry(request.City).State = EntityState.Unchanged;
                        //dbContext.Entry(request.City.Province).State = EntityState.Unchanged;
                        //dbContext.Entry(request.City.Province.Country).State = EntityState.Unchanged;
                        found.Name = request.Name;
                        found.CityId = request.CityId;
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

        public CityArea Delete(int id)
        {
            try
            {
                CityArea? found = GetById(id);
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
