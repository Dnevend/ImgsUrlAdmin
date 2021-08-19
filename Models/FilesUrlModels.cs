using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilesUrl.Models
{

    public class FilesUrl
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "UpLoadTime")]
        public DateTime UpTime { get; set; }

        [Display(Name = "Uploader")]
        public string Uploader { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Size")]
        public float Size { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Deleted")]
        public bool Deleted { get; set; } = false;
    }

    public class FilesUrlModels
    {

    }

    public class FilesUrlFilterModel
    {
        [Required]
        public int page { get; set; }
        [Required]
        public int pageSize { get; set; }

        public DateTime? startDate { get; set; }
        public DateTime? stopDate { get; set; }
    }

    public class FilesUrlListModel
    {
        public FilesUrlListModel()
        {
        }

        public FilesUrlListModel(int total, Array list)
        {
            this.total = total;
            this.imgList = list;
        }

        public int total { get; set; }
        public Array imgList { get; set; }
    }

}