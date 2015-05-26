using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using KyModel.Models.Mapping;
using MySql.Data.MySqlClient;

namespace KyModel.Models
{
    public partial class SphinxContext : DbContext
    {
        static SphinxContext()
        {
            Database.SetInitializer<SphinxContext>(null);
        }

        public SphinxContext(string connectString)
            : base(BuildConnection(connectString), true)
        {
        }
        static MySqlConnection BuildConnection(string connectString)
        {
            MySqlConnection mysqlConnection = new MySqlConnection(connectString);
            return mysqlConnection;
        }
        public DbSet<ky_batch> ky_batch { get; set; }
        public DbSet<ky_sign> ky_sign { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ky_batchMap());
            modelBuilder.Configurations.Add(new ky_signMap());
          
        }
    }
}
