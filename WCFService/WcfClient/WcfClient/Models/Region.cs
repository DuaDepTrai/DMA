using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WcfClient.Models
{
    public class Region
    {
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
    }
}