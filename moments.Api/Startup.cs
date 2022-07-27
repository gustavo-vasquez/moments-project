using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using moments.Core;
using moments.Data;
using moments.Core.Services;
using moments.Services;
using moments.Core.Models;
using Microsoft.AspNetCore.Identity;
using moments.Api.Resources;
using moments.Api.Extensions;

namespace moments.Api
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
            var jwtResource = Configuration.GetSection("Jwt").Get<JwtResource>();

            services.AddControllers();

            // Inyección de dependencias
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IStoryService, StoryService>();
            services.AddTransient<INotificationService, NotificationService>();

            services.AddDbContext<MomentsDbContext>
            (
                options => options.UseSqlServer
                (
                    Configuration.GetConnectionString("MomentsDbConnection"),
                    x => x.MigrationsAssembly("moments.Data")
                )
            );
			
			// AddIdentity<User, Role> -> utiliza las implementaciones propias para el usuario y rol
			// AddEntityFrameworkStores<MomentsDbContext> -> indica el contexto donde Identity almacenará la información
			// AddDefaultTokenProviders -> agrega los proveedores predeterminados para generar tokens (para cambio de contraseña, email, teléfono y verificación en 2 pasos)
			services.AddIdentity<User, Role>()
			.AddEntityFrameworkStores<MomentsDbContext>()
			.AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<JwtResource>(Configuration.GetSection("Jwt"));
            services.AddAuth(jwtResource);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
