using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Jobs_Ooffers_Websit.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("اسم الوظيفة")]
        public string JobTitle { get; set; }
        [DisplayName("وصف الوظيفة")]
        public string JobContent { get; set; }
        [DisplayName("صورة الوظيفة")]
        public string JobImg { get; set; }
        [DisplayName("نوع الوظيفة")]
        public int CategoryId { get; set; }

        public string UserID { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

}