using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface IProductCategoryRepository: IRepository<ProductCategory>
    {
            IEnumerable<SelectListItem> GetCategoryListForDropDown();
            void Update(ProductCategory category);
    }
}
