using Microsoft.AspNetCore.Localization;
using REPOSITORY.Data;
using REPOSITORY.Produto;
using SERVICE.Produto;
using System.Globalization;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var defaultCulture = new CultureInfo("pt-BR");
                var supportedCultures = new[] { defaultCulture };

                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            string? conexaoString = builder.Configuration.GetConnectionString("FirebirdConnection");

            if (string.IsNullOrEmpty(conexaoString))
            {
                throw new InvalidOperationException("A string de conexão 'FirebirdConnection' não foi encontrada nas configurações.");
            }
            FirebirdConnection.Inicializar(conexaoString);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<ProdutoService, ProdutoService>();
          
            var app = builder.Build();
            app.UseRequestLocalization();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Produto}/{action=ListarProdutos}/{id?}");

            app.Run();
        }
    }
}
