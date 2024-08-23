using Microsoft.EntityFrameworkCore;
using rest_cap.Server.Models;

namespace rest_cap.Server.Entities
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {

        }

        #region DBSETS
        public DbSet<User> Users { get; set; }
        public DbSet<ApiAccess> ApiAccess { get; set; }
        #endregion
    }
}
