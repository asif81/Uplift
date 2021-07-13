using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models.ViewModels
{
    public class CartViewModel
    {        
        public IList<Service> serviceList { get; set; }
        public OrderHeder OrderHeder { get; set; }

    }
}
