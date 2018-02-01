namespace UlfIdentity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UlfIdentity.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UlfIdentity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "UlfIdentity.Models.ApplicationDbContext";
        }
        protected override void Seed(UlfIdentity.Models.ApplicationDbContext context)//"context" is connection to database
        {
            #region
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
#endregion
            //1**Role  part 
            //Ulf we will add manage roleStor to manage add new role for user or admin or support
            var roleStore = new RoleStore<IdentityRole>(context);//reference to roleStore
            var roleManager = new RoleManager<IdentityRole>(roleStore);//reference to roleManager
            
            //1 ***Ulf we create new user and password
            //we call first time this class "UserStore" then ctr . bring "name" space for indentity model
           
            var userStore = new UserStore<ApplicationUser>(context); // and we need again name space to add "ApplicationUser" 
            var userManager = new UserManager<ApplicationUser>(userStore);// and we need name space for "UserManager"
            
            // now we have "UserManager" then we can add users
            // this email for first time then we will use if for check if the admin not exist then do this 
            //If admin not exist then create admin

            ApplicationUser myAdmin;//reference
            ApplicationUser myFoo;

            if (context.Users.SingleOrDefault(u => u.Email == "admin@admin.se")==null)//we check to create new one fro first time 
            {
              // then create application user
                //here will not create password because all of thos data will stor in database it menas no hash for passowrd
                 myAdmin = new ApplicationUser() { Email = "admin@admin.se", UserName = "admin@admin.se" };
                //here will create password as this way 
                userManager.Create(myAdmin, "!23Qwe");
            }
            //2**new Role to added to table role it related by role we create here role part  We have if to add from first time when we run project first time then next time no new
            if (roleManager.FindByName("Admin") == null)
            {
                roleManager.Create(new IdentityRole("Admin"));//we create new role as a name 
            }
            //3 **new Role
            if (roleManager.FindByName("Commen") == null)
            {
                roleManager.Create(new IdentityRole("Commen"));//now we create this object "commen"
            }
            if (context.Users.SingleOrDefault(u => u.Email == "oo@foo.se") == null)//new user name and password myFoo is reference
            {
                 myFoo = new ApplicationUser() { Email = "foo@foo.se", UserName = "foo@foo.se" };//I create new object to myFoo
                 userManager.Create(myFoo, "!23Qwe"); //then now we save it in user table by userManager
            }
            //we be to check an add for user i do not use if for this things 

            context.SaveChanges();//we will save changes to db

             myAdmin = userManager.FindByName("admin@admin.se");

             myFoo = userManager.FindByName("foo@foo.se");//I Assing this name to myFoo
            
            userManager.AddToRole(myAdmin.Id, "Admin");// we give "Admin" role to myAdmin
            userManager.AddToRole(myFoo.Id, "Commen");// we give "Commen" role to myFoo
        }
    }
}
