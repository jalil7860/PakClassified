using System.IO.Pipes;
using System.Runtime.CompilerServices;
using PakClassified.Entities.Location;
using PakClassified.Entities.PakClassified;
using PakClassified.Entities.UserEntities;
using PakClassified.Models.Location;
using PakClassified.Models.PakClassified;
using PakClassified.Models.User;

namespace PakClassified.API.Helper
{
    public static class EntityModelHelper
    {
        #region CountryModels
        public static CountryModel ToModel(this Country entity)
        {
            if (entity != null)
            {
                CountryModel model = new CountryModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                model.IsDeleted = entity.IsDeleted;
                return model;
            }
            return null;
        }
        public static IEnumerable<CountryModel> ToModelList(this IEnumerable<Country> entities)
        {
            List<CountryModel> models = new List<CountryModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }

            }
            return models;
        }
        public static Country ToEntity(this CountryModel model)
        {
            Country entity = new Country();

            entity.Name = model.Name;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region ProvinceModels
        public static ProvinceModel? ToModel(this Province? entity)
        {
            if (entity != null)
            {
                ProvinceModel provinceModel = new ProvinceModel();

                provinceModel.Id = entity.Id;
                provinceModel.Name = entity.Name;
                provinceModel.Country = entity.Country.ToModel();
                provinceModel.IsDeleted = entity.IsDeleted;
                provinceModel.CreatedBy = entity.CreatedBy;
                provinceModel.CreatedDate = entity.CreatedDate;

                return provinceModel;
            }

            return null;
        }
        public static IEnumerable<ProvinceModel> ToModelList(this IEnumerable<Province> entities)
        {
            List<ProvinceModel> provinceModels = new List<ProvinceModel>();

            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    provinceModels.Add(entity.ToModel());
                }
            }

            return provinceModels;
        }

        public static Province ToEntity(this ProvinceModel provinceModel)
        {
            Province entity = new Province();

            entity.Name = provinceModel.Name;
            entity.CountryId = provinceModel.Country.Id;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region CityModels
        public static CityModel? ToModel(this City? entity)
        {
            if (entity != null)
            {
                CityModel model = new CityModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Province = entity.Province.ToModel();
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<CityModel> ToModelList(this IEnumerable<City> entities)
        {
            List<CityModel> models = new List<CityModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static City ToEntity(this CityModel model)
        {
            City entity = new City();
            entity.Name = model.Name;
            entity.ProvinceId = model.Province.Id;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region CityAreaModels
        public static CityAreaModel? ToModel(this CityArea? entity)
        {
            if (entity != null)
            {
                CityAreaModel model = new CityAreaModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.City = entity.City.ToModel();
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<CityAreaModel> ToModelList(this IEnumerable<CityArea> entities)
        {
            List<CityAreaModel> models = new List<CityAreaModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static CityArea ToEntity(this CityAreaModel model)
        {
            CityArea entity = new CityArea();
            entity.Name = model.Name;
            entity.CityId = model.City.Id;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region AdvertisementCategoryModels
        public static AdvertisementCategoryModel? ToModel(this AdvertisementCategory? entity)
        {
            if (entity != null)
            {
                AdvertisementCategoryModel model = new AdvertisementCategoryModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Description = entity.Description;
                model.Image = entity.Image;
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<AdvertisementCategoryModel> ToModelList(this IEnumerable<AdvertisementCategory> entities)
        {
            List<AdvertisementCategoryModel> models = new List<AdvertisementCategoryModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static AdvertisementCategory ToEntity(this AdvertisementCategoryModel model)
        {
            AdvertisementCategory entity = new AdvertisementCategory();
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Image = model.Image;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region AdvertisementSubCategoryModels
        public static AdvertisementSubCategoryModel? ToModel(this AdvertisementSubCategory? entity)
        {
            if (entity != null)
            {
                AdvertisementSubCategoryModel model = new AdvertisementSubCategoryModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Description = entity.Description;
                model.CategoryId = entity.CategoryId;
                model.Image = entity.Image;
                //model.CategoryName = entity.Category.Name;
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<AdvertisementSubCategoryModel> ToModelList(this IEnumerable<AdvertisementSubCategory> entities)
        {
            List<AdvertisementSubCategoryModel> models = new List<AdvertisementSubCategoryModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static AdvertisementSubCategory ToEntity(this AdvertisementSubCategoryModel model)
        {
            AdvertisementSubCategory entity = new AdvertisementSubCategory();
            entity.Name = model.Name;
            entity.CategoryId = model.CategoryId;
            entity.Description = model.Description;
            entity.Image = model.Image;
            entity.CreatedBy = 1;
            //entity.Category.Name = model.CategoryName;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region AdvertisementStatusModels
        public static AdvertisementStatusModel? ToModel(this AdvertisementStatus? entity)
        {
            if (entity != null)
            {
                AdvertisementStatusModel model = new AdvertisementStatusModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<AdvertisementStatusModel?> ToModelList(this IEnumerable<AdvertisementStatus> entities)
        {
            List<AdvertisementStatusModel> models = new List<AdvertisementStatusModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static AdvertisementStatus ToEntity(this AdvertisementStatusModel model)
        {
            AdvertisementStatus entity = new AdvertisementStatus();
            entity.Name = model.Name;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region AdvertisementTypeModels
        public static AdvertisementTypeModel? ToModel(this AdvertisementType? entity)
        {
            if (entity != null)
            {
                AdvertisementTypeModel model = new AdvertisementTypeModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<AdvertisementTypeModel> ToModelList(this IEnumerable<AdvertisementType> entities)
        {
            List<AdvertisementTypeModel> models = new List<AdvertisementTypeModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static AdvertisementType ToEntity(this AdvertisementTypeModel model)
        {
            AdvertisementType entity = new AdvertisementType();
            entity.Name = model.Name;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            return entity;
        }
        #endregion

        #region AdvertisementImageMoels
        public static AdvertisementImageModel? ToModel(this AdvertisementImage? entity)
        {
            if (entity != null)
            {
                AdvertisementImageModel model = new AdvertisementImageModel();
                model.Id = entity.Id;
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                model.Advertisement = entity.Advertisement.ToModel();
                return model;
            }
            return null;
        }
        public static IEnumerable<AdvertisementImageModel> ToModelList(this IEnumerable<AdvertisementImage> entities)
        {
            List<AdvertisementImageModel> models = new List<AdvertisementImageModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static AdvertisementImage ToEntity(this AdvertisementImageModel model)
        {
            AdvertisementImage entity = new AdvertisementImage();
            entity.Rank = model.Rank;
            entity.Caption = model.Caption;
            entity.Content = model.Content;
            entity.IsDeleted = false;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            entity.AdvertisementId = model.Advertisement.Id;
            return entity;
        }
        #endregion

        #region AdvertisementTagsModel
        public static AdvertisementTagsModel? ToModel(this AdvertisementTag? entity)
        {
            if (entity != null)
            {
                AdvertisementTagsModel model = new AdvertisementTagsModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<AdvertisementTagsModel> ToModelList(this IEnumerable<AdvertisementTag> entities)
        {
            List<AdvertisementTagsModel> models = new List<AdvertisementTagsModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static AdvertisementTag ToEntity(this AdvertisementTagsModel model)
        {
            AdvertisementTag entity = new AdvertisementTag();
            entity.Name = model.Name;
            entity.IsDeleted = false;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            return entity;
        }
        #endregion

        #region AdvertisementModels
        public static AdvertisementModel? ToModel(this Advertisement? entity)
        {
            if (entity != null)
            {
                AdvertisementModel model = new AdvertisementModel();
                model.Id = entity.Id;
                model.SubCategoryId = entity.SubCategoryId;
                model.StatusId = entity.StatusId;
                model.TypeId = entity.TypeId;
                model.PostedById = entity.PostedById;
                //model.PostedBy = entity.PostedBy.Name;
                model.CityAreaId = entity.CityAreaId;
                //model.CityArea = entity.CityArea.Name;
                model.Name = entity.Name;
                model.Price = entity.Price;
                model.Image = entity.Image;
                model.Description = entity.Description;
                model.Features = entity.Features;
                model.Hits = entity.Hits;
                model.EndsOn = entity.EndsOn;
                model.StartsOn = entity.StartsOn;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                model.IsDeleted = entity.IsDeleted;
                return model;
            }
            return null;
        }
        public static IEnumerable<AdvertisementModel> ToModelList(this IEnumerable<Advertisement> entities)
        {
            List<AdvertisementModel> models = new List<AdvertisementModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static Advertisement ToEntity(this AdvertisementModel model)
        {
            Advertisement entity = new Advertisement();
            entity.Id = model.Id;
            entity.SubCategoryId = model.SubCategoryId;
            entity.StatusId = model.StatusId;
            entity.PostedById = model.PostedById;
            entity.TypeId = model.TypeId;
            entity.CityAreaId = model.CityAreaId;
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Image = model.Image;
            entity.Description = model.Description;
            entity.Features = model.Features;
            entity.Hits = model.Hits;
            entity.EndsOn = model.EndsOn;
            entity.StartsOn = model.StartsOn;
            entity.CreatedBy = model.CreatedBy;
            entity.CreatedDate = model.CreatedDate;
            entity.IsDeleted = model.IsDeleted;
            return entity;
        }
        #endregion

        #region UserModels
        public static UserModel? ToModel(this User? entity)
        {
            if (entity == null) return null;
            return new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
                DateOfBirth = entity.DateOfBirth,
                Password = entity.Password,
                ContactNumber = entity.ContactNumber,
                Email = entity.Email,
                Image = entity.Image,
                SecurityQuestion = entity.SecurityQuestion,
                SecurityAnswer = entity.SecurityAnswer,
                RoleId = entity.RoleId,
                RoleName = entity.Role?.Name,
                //CreatedBy = GetCreatedBy(),
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Advertisements = entity.Advertisements?
                 .Select(ad => new AdvertisementModel
                 {
                     Id = ad.Id,
                     Name = ad.Name,
                     Description = ad.Description,
                     Price = ad.Price,
                     PostedById = ad.PostedById,
                     CreatedDate = ad.CreatedDate
                 }).ToList()
            };
        }
        public static IEnumerable<UserModel> ToModelList(this IEnumerable<User> entities)
        {
            List<UserModel> models = new List<UserModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static User ToEntity(this UserModel model)
        {
            return new User
            {
                Id = model.Id,
                Name = model.Name,
                DateOfBirth = model.DateOfBirth,
                Password = model.Password,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                Image = model.Image,
                SecurityQuestion = model.SecurityQuestion,
                SecurityAnswer = model.SecurityAnswer,
                RoleId = model.RoleId
            };
        }
        #endregion

        #region RoleModels
        public static RoleModel? ToModel(this Role? entity)
        {
            if (entity != null)
            {
                RoleModel model = new RoleModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Rank = entity.Rank;
                model.IsDeleted = entity.IsDeleted;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                return model;
            }
            return null;
        }
        public static IEnumerable<RoleModel> ToModelList(this IEnumerable<Role> entities)
        {
            List<RoleModel> models = new List<RoleModel>();
            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    models.Add(entity.ToModel());
                }
            }
            return models;
        }
        public static Role ToEntity(this RoleModel model)
        {
            Role entity = new Role();
            entity.Name = model.Name;
            entity.Rank = model.Rank;
            entity.IsDeleted = false;
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Now;
            return entity;
        }
        #endregion
    }
}
