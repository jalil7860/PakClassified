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
    public interface ICountryHandler
    {
        IEnumerable<Country> GetAll();
        Country? GetById(int id);
        Country? Create(Country request);
        Country? Update(int id, Country request);
        Country? Delete(int id);
    }
    public class CountryHandler : ICountryHandler
    {
        public IEnumerable<Country> GetAll()
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    return (from country in dbContext.Countries
                            where !country.IsDeleted
                            select country).OrderByDescending(c => c.CreatedDate).ToList();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public Country? GetById(int id)
        {
            try
            {
                using (PakClassifiedContext dbCobtext = new PakClassifiedContext())
                {
                    return (from country in dbCobtext.Countries
                            where country.Id == id && !country.IsDeleted
                            select country).FirstOrDefault();
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        public Country Create(Country request)
        {
            try
            {
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
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
        public Country Update(int id, Country request)
        {
            try
            {
                Country? found = GetById(id);
                using (PakClassifiedContext dbContext = new PakClassifiedContext())
                {
                    if (found != null)
                    {
                        found.Name = request.Name;
                        //found. = request.CountryId;
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
        public Country Delete(int id)
        {
            try
            {
                Country? found = GetById(id);
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
