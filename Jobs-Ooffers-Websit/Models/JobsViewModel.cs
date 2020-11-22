using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs_Ooffers_Websit.Models
{
    public class JobsViewModel
    {
        public string JobTitel { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}