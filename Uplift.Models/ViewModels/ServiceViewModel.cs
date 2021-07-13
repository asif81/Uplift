using DevExpress.Xpo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Uplift.Models.ViewModels
{
    public class ServiceViewModel 
    {
        public ServiceViewModel() 
        {
        
        }

        public Service service { get; set; }
        public IEnumerable<SelectListItem> categoryList { get; set; }
        public IEnumerable<SelectListItem> frequecyList { get; set; }
    }

}