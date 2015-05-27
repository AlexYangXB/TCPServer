using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using KyModel.Mapping;
using MySql.Data.MySqlClient;
using KyModel.Models;
using System.Data.Objects;
namespace KyBll.Context
{
    public partial class DeviceContext : DbContext
    {
        static DeviceContext()
        {
            Database.SetInitializer<DeviceContext>(null);
        }

        public DeviceContext(string connectString)
            : base(BuildConnection(connectString), true)
        {
        }
        static MySqlConnection BuildConnection(string connectString)
        {
            MySqlConnection mysqlConnection = new MySqlConnection(connectString);
            return mysqlConnection;
        }
        public DbSet<ky_atm> ky_atm { get; set; }
        public DbSet<ky_atmtype> ky_atmtype { get; set; }
        public DbSet<ky_branch> ky_branch { get; set; }
        public DbSet<ky_cashbox> ky_cashbox { get; set; }
        public DbSet<ky_cashstatus> ky_cashstatus { get; set; }
        public DbSet<ky_currency> ky_currency { get; set; }
        public DbSet<ky_factory> ky_factory { get; set; }
        public DbSet<ky_fakeregist> ky_fakeregist { get; set; }
        public DbSet<ky_fakesign> ky_fakesign { get; set; }
        public DbSet<ky_gzh_bundle> ky_gzh_bundle { get; set; }
        public DbSet<ky_gzh_package> ky_gzh_package { get; set; }
        public DbSet<ky_imgserver> ky_imgserver { get; set; }
        public DbSet<ky_import_file> ky_import_file { get; set; }
        public DbSet<ky_import_machine> ky_import_machine { get; set; }
        public DbSet<ky_machine> ky_machine { get; set; }
        public DbSet<ky_node> ky_node { get; set; }
        public DbSet<ky_package_bundle> ky_package_bundle { get; set; }
        public DbSet<ky_perm_user_node> ky_perm_user_node { get; set; }
        public DbSet<ky_user> ky_user { get; set; }
        public DbSet<ky_version> ky_version { get; set; }
        public DbSet<migration> migrations { get; set; }
        public DbSet<permission> permissions { get; set; }
        public DbSet<role> roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ky_atmMap());
            modelBuilder.Configurations.Add(new ky_atmtypeMap());
            modelBuilder.Configurations.Add(new ky_branchMap());
            modelBuilder.Configurations.Add(new ky_cashboxMap());
            modelBuilder.Configurations.Add(new ky_cashstatusMap());
            modelBuilder.Configurations.Add(new ky_currencyMap());
            modelBuilder.Configurations.Add(new ky_factoryMap());
            modelBuilder.Configurations.Add(new ky_fakeregistMap());
            modelBuilder.Configurations.Add(new ky_fakesignMap());
            modelBuilder.Configurations.Add(new ky_gzh_bundleMap());
            modelBuilder.Configurations.Add(new ky_gzh_packageMap());
            modelBuilder.Configurations.Add(new ky_imgserverMap());
            modelBuilder.Configurations.Add(new ky_import_fileMap());
            modelBuilder.Configurations.Add(new ky_import_machineMap());
            modelBuilder.Configurations.Add(new ky_machineMap());
            modelBuilder.Configurations.Add(new ky_nodeMap());
            modelBuilder.Configurations.Add(new ky_package_bundleMap());
            modelBuilder.Configurations.Add(new ky_perm_user_nodeMap());
            modelBuilder.Configurations.Add(new ky_userMap());
            modelBuilder.Configurations.Add(new ky_versionMap());
            modelBuilder.Configurations.Add(new migrationMap());
            modelBuilder.Configurations.Add(new permissionMap());
            modelBuilder.Configurations.Add(new roleMap());
        }
    }
}
