using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.QrCode.Generator.Web
{
    public static class BlazorServerConfiguration
    {
        public static void AddBlazorServer(this IServiceCollection services)
        {
            services.AddRazorPages(opt => opt.RootDirectory = "/Web/Pages");
            services.AddServerSideBlazor();
        }

        public static void UseBlazorServer(this WebApplication app)
        {
            if(app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
        }
    }
}
