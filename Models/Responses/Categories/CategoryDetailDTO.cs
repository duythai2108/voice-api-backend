using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Responses.SubCategories;

namespace VoiceAPI.Models.Responses.Categories
{
    public class CategoryDetailDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<SubCategoryDTO> SubCategories { get; set; }
    }
}
