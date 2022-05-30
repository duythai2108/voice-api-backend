using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Payload.SubCategories
{
    public class SubCategoryCreatePayload
    {
        [MaxLength(100), Required]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
    }
}
