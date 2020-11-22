using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Jobs_Ooffers_Websit.Models
{
    public class roleViewModel

    {
        public string Id { get; set; }
        [DisplayName("الاسم")]
        public string Name { get; set; }
    }
}