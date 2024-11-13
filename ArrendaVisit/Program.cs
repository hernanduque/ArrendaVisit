using Blazorise;
using ArrendaVisit.Components;
using ArrendaVisit.Service;
using Repositorio;
using System.Data;
using Blazorise.Material;
using Blazorise.Icons.Material;
using Microsoft.AspNetCore.Authentication.Cookies;
using Radzen;
using Microsoft.Data.SqlClient;

//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;



internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //var builder = WebAssemblyHostBuilder.CreateDefault(args);
        //*************************************************************

        // Añadir servicios de autenticación
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x =>
            {
                x.LoginPath = "/login";
            });

        builder.Services.AddAuthorization();

        builder.Services.AddCascadingAuthenticationState();
        // Otras configuraciones
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        //*************************************************************


        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddSignalR();
        builder.Services.AddHostedService<TimeWorker>();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });


        //INYECTAMOS LA CONEXION
        builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(builder.Configuration.GetConnectionString("CONEXIONSQL")));

        var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
        builder.Services.AddSingleton(emailConfig);

        builder.Services.AddSignalR();
        builder.Services.AddHostedService<TimeWorker>();

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7044") });
        builder.Services.AddScoped<IgrabarClienteServicio, GrabarClienteServicio>();


        //builder.RootComponents.Add<App>("#app");
        //builder.RootComponents.Add<HeadOutlet>("head::after");

        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


        //se agrego al contenedor de dependencias el nuevo servicio
        builder.Services.AddScoped<IGrabarClientes, GrabarClientes>();
        builder.Services.AddScoped<IListasRepositorio, ListaRepositorio>();
        builder.Services.AddScoped<IServicioCliente, ServicioCliente>();
        builder.Services.AddScoped<IlistaServicio, listaServicio>();
        builder.Services.AddScoped<TooltipService>();


        //************************************************
        builder.Services.AddHttpContextAccessor();
        //************************************************

        //builder.Services.AddBlazoredSessionStorage();
        //builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
        //builder.Services.AddAuthorizationCore();


        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddTransient<IEmailSenderCancel, EmailSenderCancel>();

        //----------------------------------------------------
        builder.Services
            .AddBlazorise(options =>
            {
                options.Immediate = true;
                //options.ChangesTextOnkeyPress = true;
            })
            .AddMaterialProviders()
        .AddMaterialIcons();



        var app = builder.Build();




        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        //*************************************************************
        app.UseAuthentication(); // Asegúrate de que esto esté aquí
        app.UseAuthorization(); // Debe ir después de UseAuthentication
                                //*************************************************************

        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapHub<ReportHub>("/reports");

        app.Run(); await builder.Build().RunAsync();
        builder.Services
            .AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .AddMaterialProviders()
            .AddMaterialIcons();
    }
}