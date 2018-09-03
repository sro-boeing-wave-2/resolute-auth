using auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace auth.Context
{
    class UserCredentialContext: DbContext
    {

        public DbSet<UserCredentials> UserCredentials { get; set; }

        public UserCredentialContext(DbContextOptions opts) : base(opts)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
}
