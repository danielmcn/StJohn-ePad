using System.Web.Security;
using StJohnEPAD.Models;
using WebMatrix.WebData;
using System.Data.Entity;

//http://kevin-junghans.blogspot.co.uk/2013/01/seeding-customizing-aspnet-mvc.html
 
namespace StJohnEPAD.Filters
{
    public class InitializeCustomMembership : DropCreateDatabaseIfModelChanges<UsersContext>
    {
        protected override void Seed(UsersContext context)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
 
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("AdminAccount", false) == null)
            {
                membership.CreateUserAndAccount("AdminAccount", "Admin1");
            }
            /*
            if (!roles.GetRolesForUser("AdminAccount").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "AdminAccount" }, new[] { "Admin" });
            } 
             */
 
        }
    }
}