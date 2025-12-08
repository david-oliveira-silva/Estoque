using REPOSITORY.Data;
using REPOSITORY.Produto;
using SERVICE.Produto;

namespace Estoque
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
