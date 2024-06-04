using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class OtpManager
    {
        [Key]
        public int OtpId { get; set; }
        public string OtpText { get; set; }
        public DateTime Expired { get; set; }
        public DateTime CreateDate { get; set; }
        public string OtpType { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
