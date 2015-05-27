using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using KyModel.Mapping;
using MySql.Data.MySqlClient;
using KyModel.Models;
namespace KyBll.Context
{
    public partial class ImageContext : DbContext
    {
        static ImageContext()
        {
            Database.SetInitializer<ImageContext>(null);
        }

        public ImageContext(string connectString)
            : base(BuildConnection(connectString), true)
        {
        }
        static MySqlConnection BuildConnection(string connectString)
        {
            MySqlConnection mysqlConnection = new MySqlConnection(connectString);
            return mysqlConnection;
        }
        public DbSet<ky_picture> ky_picture { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ky_pictureMap());

        }
    }
}
