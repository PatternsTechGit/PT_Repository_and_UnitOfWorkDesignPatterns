using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestDTO
{
    public class AccountRequestDTO
    {
        [MinLength(4, ErrorMessage = "The AccountNumber value cannot less than 4 characters. ")]
        [MaxLength(9, ErrorMessage = "The AccountNumber value cannot exceed 9 characters. ")]
        [Required(ErrorMessage = "AccountNumber is required.")]
        public string AccountNumber { get; set; }

        [MinLength(4, ErrorMessage = "The AccountTitle value cannot less than 4 characters.")]
        [MaxLength(20, ErrorMessage = "The AccountTitle value cannot exceed 20 characters. ")]
        [Required(ErrorMessage = "AccountTitle is required.")]
        public string AccountTitle { get; set; }
        
        [Required(ErrorMessage = "CurrentBalance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "CurrentBalance cannot be negative")]
        public decimal CurrentBalance { get; set; }
        
        [Required(ErrorMessage = "AccountStatus is required.")]
        public AccountStatus AccountStatus { get; set; }
        
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }
        
        public virtual User User { get; set; }
    }
}
