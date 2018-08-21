using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClockWork.Model
{

    public class ClockWorkContext : DbContext
    {
        public ClockWorkContext(DbContextOptions<ClockWorkContext> options)
        : base(options)
        {
        }
        public DbSet<CurrentTimeQuery> CurrentTimeQueries { get; set; }
        public DbSet<TimeZoneRequest> TimeZoneRequests { get; set; }
    }

    public class CurrentTimeQuery
    {
        [Key]
        public int CurrentTimeQueryId { get; set; }
        public DateTime Time { get; set; }
        public string ClientIp { get; set; }
        public DateTime UTCTime { get; set; }
    }

    public class TimeZoneRequest
    {
        [Key]
        public int RequestId { get; set; }
        public string RequestedTimezone { get; set; }
        public DateTime RequestDate { get; set; }
        public string ClientIp { get; set; }
    }
}
