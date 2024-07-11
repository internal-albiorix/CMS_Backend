using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Model.Model
{
    [Table("ForgotPassword")]
    public class ForgotPasswordModel
    {
        [Key]
        public int Id {  get; set; }
        public int UserId {  get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpires { get; set; }

    }
}
