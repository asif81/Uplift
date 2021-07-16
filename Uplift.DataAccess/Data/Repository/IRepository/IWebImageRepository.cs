﻿using System;
using System.Collections.Generic;
using System.Text;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IWebImageRepository : IRepository<WebImages>
    {
        void Update(WebImages webImage);
    }
}
