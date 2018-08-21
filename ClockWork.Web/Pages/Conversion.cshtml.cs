using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClockWork.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClockWork.Web.Pages
{
    public class ConversionModel : PageModel
    {
        #region VIEWMODELS
        public IEnumerable<CurrentTimeQuery> TimeQueries { get; set; }
        #endregion
        private readonly ClockWorkContext _dbContext;
        public ConversionModel(ClockWorkContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            TimeQueries = _dbContext.CurrentTimeQueries.ToList();
        }
    }
}
