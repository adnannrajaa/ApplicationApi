using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class AllowedLinksRepository : Repository<AllowedLinks>, IAllowedLinksRepository
    {
        private readonly ApplicationDbContext _db;

        public AllowedLinksRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AllowedLinks AllowedLinks)
        {
            var objFromDb = _db.AllowedLinks.FirstOrDefault(s => s.AllowedLinkId == AllowedLinks.AllowedLinkId);
            objFromDb.ActionLinkId = AllowedLinks.ActionLinkId;
            objFromDb.UserId = AllowedLinks.UserId;
            objFromDb.IsAssinged = AllowedLinks.IsAssinged;
            _db.SaveChanges();
        }
    }
}
