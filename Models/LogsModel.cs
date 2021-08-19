using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilesUrl.Models
{
    [Table("Log")]
    public class LogsModel
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "type")]
        public string type { get; set; }

        [Display(Name = "subject")]
        public string subject { get; set; }

        [Display(Name = "content")]
        public string content { get; set; }

        [Display(Name = "datetime")]
        public DateTime datetime { get; set; }

    }
}