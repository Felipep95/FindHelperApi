using FindHelperApi.Data;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<UserAuthenticationService, UserAuthenticationService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FindHelperApi", Version = "v1" });
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
