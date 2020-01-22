 
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ApplicationApi.DataAccess.Data.Repository
{
    public class ShopesDetailsRepository : Repository<ShopsDetails>, IShopesDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public ShopesDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShopsDetails shopDetails)
        {
            var objFromDb = _db.ShopsDetail.FirstOrDefault(s => s.ShopId == shopDetails.ShopId);
            objFromDb.ShopName = shopDetails.ShopName;
            objFromDb.City = shopDetails.City;
            objFromDb.Section = shopDetails.Section;
            objFromDb.Route = shopDetails.Route;
            objFromDb.ShopOwnerName = shopDetails.ShopOwnerName;
            objFromDb.FullAddress = shopDetails.FullAddress;
            objFromDb.ContactNumber = shopDetails.ContactNumber;
            objFromDb.RecentDelivery = shopDetails.RecentDelivery;
            objFromDb.IsActive = shopDetails.IsActive;
            _db.SaveChanges();
        }
    }
}
