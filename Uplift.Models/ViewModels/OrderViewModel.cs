using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderHeder OrderHeder { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
