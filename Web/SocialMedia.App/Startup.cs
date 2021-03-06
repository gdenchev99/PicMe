using System.Reflection;

using SocialMedia.Data;
using SocialMedia.Data.Common;
using SocialMedia.Data.Common.Repositories;
using SocialMedia.Data.Models;
using SocialMedia.Data.Repositories;
using SocialMedia.Data.Seeding;
using SocialMedia.Services.Data;
using SocialMedia.Services.Mapping;
using SocialMedia.Services.Messaging;
using SocialMedia.Web.ViewModels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using SocialMedia.Web.ViewModels.Posts;
using SocialMedia.Helpers;
using SocialMedia.Services;
using SocialMedia.Features.Chat;
using SocialMedia.Web.ViewModels.Messages;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using System;
using SocialMedia.Features.Notifications;

namespace SocialMedia.App
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // If the app is running in production, use the production database.
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<ApplicationDbContext>(
                options => options.UseLazyLoadingProxies()
                .UseSqlServer(this.configuration.GetConnectionString("MyDbConnection")));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(
                options => options.UseLazyLoadingProxies()
                .UseSqlServer(this.configuration.GetConnectionString("LocalDb")));
            }

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.Configure<IdentityOptions>(
                options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredUniqueChars = 0;
                    // options.SignIn.RequireConfirmedAccount = true;
                    // options.SignIn.RequireConfirmedEmail = true;
                });

            services.Configure<CloudinaryConfig>(this.configuration.GetSection("Cloudinary"));
            services.Configure<SendGridConfig>(this.configuration.GetSection("SendGrid"));

            services.AddSignalR(config => config.EnableDetailedErrors = true);

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddSigningCredentials();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddRazorPages();
            services.AddControllers()
                 .AddNewtonsoftJson();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            //Helper services
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IEncodeService, EncodeService>();

            // Application services
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<INotificationsService, NotificationsService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IProfilesService, ProfilesService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ILikesService, LikesService>();
            services.AddTransient<IMessagesService, MessagesService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly,
                    typeof(PostCreateModel).GetTypeInfo().Assembly
                );

            // Seed data on app launch
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var dbContextSeeder = new ApplicationDbContextSeeder();
                dbContextSeeder.SeedAsync(dbContext, scope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHub<NotificationsHub>("/notificationsHub");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
