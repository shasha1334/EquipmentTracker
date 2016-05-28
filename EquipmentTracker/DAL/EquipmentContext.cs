using System.Data.Entity;
using EquipmentTracker.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EquipmentTracker.DAL
{
    public class EquipmentContext : DbContext
    {

        public EquipmentContext() : base("EquipmentContext")
        {

        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}