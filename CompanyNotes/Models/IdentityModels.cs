using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CompanyNotes.Models;

namespace CompanyNotes.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // one-to-one relationship between ApplicationUser and Employee (see the ApplicationDbContext class (in this namespace) OnModelCreating() method)
        public virtual Employee Employee { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Case> Cases { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; } // abstract class with two children (InternalEmployee, ExternalEmployee)
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Subcontractor> Subcontractors { get; set; }
        public DbSet<WorkNote> WorkNotes { get; set; }
        public DbSet<WorkTitle> WorkTitles { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Make one-to-one relationship between ApplicationUser and Employee
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasRequired<Employee>(a => a.Employee);

            base.OnModelCreating(modelBuilder);



            //modelBuilder.Entity<Employee>()
            //    .HasRequired<ApplicationUser>(e => e.ApplicationUser);

            //base.OnModelCreating(modelBuilder);
        }
    }
}