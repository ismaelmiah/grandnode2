﻿using Grand.Infrastructure.Models;

namespace Grand.Web.Models.Catalog
{
    public class CategorySimpleModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string Flag { get; set; }
        public string FlagStyle { get; set; }
        public string Icon { get; set; }
        public string ImageUrl { get; set; }
        public string SeName { get; set; }
        public int? NumberOfProducts { get; set; }
        public bool IncludeInMenu { get; set; }
        public List<CategorySimpleModel> SubCategories { get; set; } = new();
    }
}