﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Model.Model
{
    public class EmailLogModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Recipient { get; set; }
        public DateTime SentDate { get; set; }
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }
      
    }
}
