﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobs_Ooffers_Websit.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "نوع الوظيفة")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "وصف الوظيفة")]
        public string CategoryDesc { get; set; }


        public virtual ICollection<Job> Jobs { get; set; }
    }
}