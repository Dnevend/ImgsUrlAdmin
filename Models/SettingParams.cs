using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilesUrl.Models
{
    [Table("SettingParams")]
    public class SettingParams
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Param")]
        public string Param { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

    }

    public class SettingParamsViewModel
    {

        [Display(Name = "Param")]
        public string Param { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

    }

    public class SetValueModel
    {

        /// <summary>
        /// 参数
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

    }

}