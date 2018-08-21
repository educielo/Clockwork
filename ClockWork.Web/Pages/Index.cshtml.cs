using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClockWork.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockWork.Web.Pages
{
    public class TimeQueriesModel : PageModel
    {
        #region VIEWMODELS
        public string RequestedTimezone { get; set; }

        public IEnumerable<CurrentTimeQuery> TimeQueries { get; set; }

        public IEnumerable<SelectListItem> TimeZoneInfos { get; set; }
        #endregion
        private readonly ClockWorkContext _dbContext;
        public TimeQueriesModel(ClockWorkContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            var tz = TimeZoneInfo.GetSystemTimeZones();
            TimeZoneInfos = tz.Select(a => new SelectListItem()
            {
                Text = a.StandardName,
                Value = a.Id
            });

            TimeQueries = _dbContext.CurrentTimeQueries.ToList();
        }
    }
}
