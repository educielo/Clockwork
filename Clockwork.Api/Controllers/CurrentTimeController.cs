using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClockWork.Model;
using Microsoft.AspNetCore.Mvc;

namespace Clockwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentTimeController : ControllerBase
    {
        private readonly ClockWorkContext _dbContext;
        public CurrentTimeController(ClockWorkContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now;
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime
            };
            _dbContext.CurrentTimeQueries.Add(returnVal);
            var count = _dbContext.SaveChanges();
            return Ok(returnVal);
        }
        // GET api/values/5
        [HttpGet("{tz}")]
        public ActionResult<string> Get(string tz)
        {
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now.ToUniversalTime();
            var requestedTime = serverTime.ConvertToTimezone(tz);
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            var timeZoneRequest = new TimeZoneRequest()
            {
                RequestedTimezone = tz,
                ClientIp = ip,
                RequestDate = serverTime,
            };
            _dbContext.TimeZoneRequests.Add(timeZoneRequest);
            _dbContext.SaveChanges();
            return new JsonResult(requestedTime.ToString("MM/dd/yyyy hh:mm tt"));
        }
    }
   
}

public static class DateConverter
{
    public static DateTime ConvertToTimezone(this DateTime date, string timezoneId)
    {
        if (string.IsNullOrEmpty(timezoneId))
            throw new ArgumentNullException("Timezone");
        TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
        if(timeInfo==null)
            throw new ArgumentNullException("Timezone Info");
        DateTime userTime = TimeZoneInfo.ConvertTimeFromUtc(date, timeInfo);
        return userTime;
    }
}