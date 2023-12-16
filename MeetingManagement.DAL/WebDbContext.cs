using MeetingManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MeetingManagement.DAL
{
    
    public class WebDbContextOptions
    {
        public Dictionary<string, string>? ConnectionStrings { get; set; }
    }
    public class WebDbContext : DbContext
    {
        public WebDbContext(): base() { }
        public  DbSet<Meeting> Meetings { get; set; }
        public  DbSet<MeetingItem> MeetingItems { get; set; }
        public  DbSet<MeetingItems> XMeetingItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var result = JsonSerializer.Deserialize<WebDbContextOptions>(File.ReadAllText("./appsettings.json")) ?? new WebDbContextOptions();
            optionsBuilder.UseSqlServer(result?.ConnectionStrings?["Default"]);
        }

    }
}
