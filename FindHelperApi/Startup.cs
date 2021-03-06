using FindHelperApi.Data;
using FindHelperApi.Helper.CustomExceptions;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace FindHelperApi
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
            services.AddDbContext<FindHelperApiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FindHelperApi"), builder =>
                    builder.MigrationsAssembly("FindHelperApi")));

            services.AddScoped<FindHelperApiContext, FindHelperApiContext>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<PublicationService, PublicationService>();
            services.AddScoped<FriendRequestService, FriendRequestService>();
            services.AddScoped<FriendListService, FriendListService>();
            services.AddScoped<DoctorService, DoctorService>();
            services.AddScoped<AreaService, AreaService>();

            services.AddDirectoryBrowser();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FindHelperApi", Version = "v1" });
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod());//https://www.c-sharpcorner.com/article/enabling-cors-in-asp-net-core-api-application/
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FindHelperApi v1"));
            }

            app.UseCors(options => options.AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();


        }
    }
}
