 
using System.Data.Entity;
 
namespace StarWarsWeb.Models
{
    public class UserDataContext : DbContext 
    {
        public UserDataContext()  
        { }

        public UserDataContext(string dbNameOrConnection) : base(dbNameOrConnection)
        {
            
        }
        public DbSet<User> Users { get; set; }
     
    }
}
