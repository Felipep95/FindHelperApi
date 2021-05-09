using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FindHelperApi.Data
{
    public class FindHelperApiContext : DbContext
    {
        public FindHelperApiContext(DbContextOptions<FindHelperApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<FriendList> FriendLists { get; set; }
        public DbSet<Publication> Publications { get; set; }
    }
}
