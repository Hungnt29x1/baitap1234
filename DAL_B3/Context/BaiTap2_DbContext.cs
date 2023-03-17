using DAL_B3.Configurations;
using DAL_B3.DomainClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Context
{
    public class BaiTap2_DbContext:DbContext
    {
        public BaiTap2_DbContext()
        {
            
        }
        public BaiTap2_DbContext(DbContextOptions<BaiTap2_DbContext> options) : base(options)
        {

        }


        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<SMS> SMSs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new GroupsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new SMSsConfiguration());
        }
    }
}
