using Jobs_Ooffers_Websit.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jobs_Ooffers_Websit.Startup))]
namespace Jobs_Ooffers_Websit
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaltRolesAndUsers();
        }
        public void CreateDefaltRolesAndUsers()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManger.RoleExists("Admins"))
            {
                role.Name = "Admins";
                roleManger.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Ahmad";
                user.Email = "Ahamad@test.com";
                var Check = userManger.Create(user, "A662536a@");

                if (Check.Succeeded)
                {
                    userManger.AddToRole(user.Id, "Admins");
                }
            }
        }
    }
}
