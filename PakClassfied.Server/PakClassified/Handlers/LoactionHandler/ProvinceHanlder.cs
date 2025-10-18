using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PakClassified.Entities.Location;

namespace PakClassified.Handler.LocationHandler
{
    public interface IProvinceHanlder
    {
        IEnumerable<Province> GetAll();
        Province? GetById(int id);
        Province? Create(Province request);
        Province? Update(int id, Province request);
        Province? Delete(int id);

    }
    public class ProvinceHandler : IProvinceHanlder
    {
        public IEnumerable<Province> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from province in dbContext.Provinces
                            .Include(c => c.Country)
                            where !province.IsDeleted
                            select province).OrderByDescending(c => c.CreatedDate).ToList();
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
        public Province? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from province in dbCobtext.Provinces
                        .Include(c => c.Country)
                            where province.Id == id && !province.IsDeleted
                            select province).FirstOrDefault();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public Province Create(Province request)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {

                    //dbContext.Entry(request.Country).State = EntityState.Unchanged;

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
        public Province Update(int id, Province request)
        {
            try
            {
                Province? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        dbContext.Entry(request.Country).State = EntityState.Unchanged;
                        found.Name = request.Name;
                        found.CountryId = request.CountryId;
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
        public Province Delete(int id)
        {
            try
            {
                Province? found = GetById(id);
                if (found != null)
                {
                    using (PakClassifiedContext dbContext = new PakClassifiedContext())
                    {
                        dbContext.Entry(found.Country).State = EntityState.Unchanged;
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
