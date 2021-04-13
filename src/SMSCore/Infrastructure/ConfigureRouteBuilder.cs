using Microsoft.AspNetCore.Builder;

namespace SMSCore.Infrastructure
{
    public static class ConfigureRouteBuilder
    {
        public static IApplicationBuilder ConfigureRoutes(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "login",
                pattern: "login",
                defaults: new { controller = "Account", action = "Login" });

                endpoints.MapControllerRoute(
               name: "logout",
               pattern: "logout",
               defaults: new { controller = "Account", action = "Logout" });

                //admin
                endpoints.MapControllerRoute(
                 name: "removeuser",
                 pattern: "/removeuser/{id}",
                 defaults: new { controller = "UserMaster", action = "Delete" });

                endpoints.MapControllerRoute(
                name: "removedesignation",
                pattern: "/removedesignation/{id}",
                defaults: new { controller = "DesignationMaster", action = "Delete" });

                endpoints.MapControllerRoute(
                name: "removerole",
                pattern: "/removerole/{id}",
                defaults: new { controller = "RoleMaster", action = "Delete" });

                //front end
                endpoints.MapControllerRoute(
                name: "getquote",
                pattern: "getquote/{custId?}",
                defaults: new { controller = "GenerateQuotation", action = "GenarateQuote"});

                endpoints.MapControllerRoute(
                name: "home",
                 pattern: "",
                defaults: new { controller = "Home", action = "index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });
            return app;
        }
    }
}
