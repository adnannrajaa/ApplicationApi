using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
   public class ActionLinksRepository : Repository<ActionLinks>, IActionLinksRepository
    {
        private readonly ApplicationDbContext _db;

        public ActionLinksRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ActionLinks ActionLink)
        {
            var objFromDb = _db.ActionLinks.FirstOrDefault(s => s.ActionLinkId == ActionLink.ActionLinkId);
            objFromDb.ApiControllerName = ActionLink.ApiControllerName;
            objFromDb.ApiControllerUrl = ActionLink.ApiControllerUrl;
            objFromDb.PosLinkTitle = ActionLink.PosLinkTitle;
            objFromDb.PosLinkUrl = ActionLink.PosLinkUrl;
            objFromDb.PosLinkIcon = ActionLink.PosLinkIcon;
            objFromDb.IsActiveLink = ActionLink.IsActiveLink;
            _db.SaveChanges();
        }
    }
}
