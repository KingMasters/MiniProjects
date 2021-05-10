using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Bookstore.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()
                    ));
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, ProdMailService>();
#endif
            var connectionString = Startup.Configuration["connectionStrings:bookstoreDbConnectionString"];
            services.AddDbContext<BookstoreDbContext>(o => 
                o.UseSqlServer(connectionString));

            services.AddScoped<IBookstoreRepository, BookstoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            BookstoreDbContext bookstoreDbContext)
        {
            loggerFactory.AddDebug(LogLevel.Information);

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Mapper.Initialize(config => {
                config.CreateMap<Author, AuthorWithoutBooksDto>();
                config.CreateMap<BookCreationDto, Book>();
                config.CreateMap<Book, BookDto>();
            });

            bookstoreDbContext.EnsureSeedData();

            app.UseStatusCodePages();

            app.UseMvc();

        }
    }
}
