using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using sChallenge.Models;
using System;

namespace sChallenge
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
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("ApiDatabase")); // naming the InMemory database ApiDatabase
            services.AddControllers().AddNewtonsoftJson(); // possible additional support for JSON operations
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = serviceProvider.GetService<ApiContext>();
            generateTemporaryData(context);

        }

        /*
         * Initializes InMemory Database with User and Patient data (for ease of testing)
         */
        private static void generateTemporaryData(ApiContext context)
        {
            // Creating test User data
            context.User.AddRange(
                new Models.User // test user (call agent)
                {
                    Username = "agent1",
                    Password = "password",
                    IsSuper = false
                },
                new Models.User // test user (supervisor)
                {
                    Username = "super1",
                    Password = "password",
                    IsSuper = true
                }
            );

            // Creating test Patient data
            context.Patient.AddRange(
                new Models.Patient
                {
                    FirstName = "John",
                    LastName = "Smith",
                    BirthDate = new System.DateTime(1990, 01, 02),
                    Street = "Brenwood Crescent",
                    City = "Toronto",
                    Province = "Ontario",
                    Country = "Canada",
                    HasCovid = false,
                    Phone = "905-111-2233",
                    Email = "test@email.com",
                    Health_Notes = "Feeling healthy.",
                    Call_Date = new System.DateTime(2021, 01, 02, 13, 30, 0),
                    AgentId = 1
                },

                new Models.Patient
                {
                    FirstName = "Jane",
                    LastName = "Peralta",
                    BirthDate = new System.DateTime(1985, 06, 10),
                    Street = "Apple Street",
                    City = "Markham",
                    Province = "Ontario",
                    Country = "Canada",
                    HasCovid = true,
                    Phone = "905-111-2233",
                    Email = "test@email.com",
                    Health_Notes = "Feeling sick. Not sure what precautions to take. Contact supervisor immediately.",
                    Call_Date = new System.DateTime(2021, 01, 02, 13, 30, 0),
                    AgentId = 2
                },

                new Models.Patient
                {
                    FirstName = "Jerome",
                    LastName = "Wallace",
                    BirthDate = new System.DateTime(1999, 12, 16),
                    Street = "Windows Lane",
                    City = "Montserrat",
                    Province = "Quebec",
                    Country = "Canada",
                    HasCovid = false,
                    Phone = "905-111-2233",
                    Email = "jwall@email.com",
                    Health_Notes = "Feeling healthy. Requires further information on vaccinations.",
                    Call_Date = new System.DateTime(2021, 01, 02, 13, 30, 0),
                    AgentId = 1
                },

                new Models.Patient
                {
                    FirstName = "Michael Scotch",
                    LastName = "Smith",
                    BirthDate = new System.DateTime(1990, 01, 02),
                    Street = "Linux Boulevard",
                    City = "Ajax",
                    Province = "Ontario",
                    Country = "Canada",
                    HasCovid = true,
                    Phone = "905-111-2233",
                    Email = "mscott@dmifflin.com",
                    Health_Notes = "Feeling sick. Great manager.",
                    Call_Date = new System.DateTime(2021, 01, 02, 13, 30, 0),
                    AgentId = 2
                }
            );

            context.SaveChanges();
        }
    }
}