using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakClassified.Models.Location;
using PakClassified.Models.User;

namespace PakClassified.Models.PakClassified
{
    public class AdvertisementModel : BaseDTO<int>
    {
        public int SubCategoryId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public int PostedById { get; set; }
        public int CityAreaId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public int Hits { get; set; }
        public DateTime EndsOn { get; set; }
        public string Image { get; set; }
        public DateTime StartsOn { get; set; }
        //public IEnumerable<AdvertisementImageModel> Images { get; set; }
        //public IEnumerable<AdvertisementTagsModel> Tags { get; set; }
        //public string CityArea { get; set; }
        //public CityModel City
        //{
        //    get
        //    {
        //        return CityArea.City;
        //    }
        //    set
        //    {
        //        CityArea.City = value;
        //    }
        //}
        //public AdvertisementStatusModel Status { get; set; }
        //public AdvertisementSubCategoryModel SubCategory { get; set; }
        //public AdvertisementTypeModel Type { get; set; }
        //public string PostedBy { get; set; }

        //public AdvertisementModel()
        //{
        //    Images = new List<AdvertisementImageModel>();
        //    Tags = new List<AdvertisementTagsModel>();
        //}
    }
}
