using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class ConfirmUserDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string OtpText { get; set; }
    }
}
