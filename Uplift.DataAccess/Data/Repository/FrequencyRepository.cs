using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public FrequencyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<SelectListItem> GetFrequencyListFromDropDown()
        {
            return _applicationDbContext.frequencies.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }); ;
        }

        public void Update(Frequency frequency)
        {
            var objFromDb = _applicationDbContext.frequencies.FirstOrDefault(s => s.Id == frequency.Id);
            objFromDb.Name = frequency.Name;
            objFromDb.FrequencyCount = frequency.FrequencyCount;

            _applicationDbContext.SaveChanges();
        }
    }
}
