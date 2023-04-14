using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectAnswersAndQuestions.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectAnswersAndQuestions.Services;
using ProjectAnswersAndQuestions.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Graph;

namespace ProjectAnswersAndQuestions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
      
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        "Server=DESKTOP-CKVJ0S3\\SQLEXPRESS;Database=AQDB;Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True; TrustServerCertificate=true"
        ));
            services.AddMvc();
            services.AddScoped<IUserRepository, UsersRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArticleRepository, ArticlesRepository>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICommentRepository, CommentsRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
                });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AQpage}/{action=AQpage}/{id?}");
            });
        }
    }
}
