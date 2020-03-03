using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using Turner.Challenge.App.Models;
using Turner.Challenge.App.Repository;

namespace Turner.Challenge.App
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            ConfigureMongoDb(services);
        }

        private void ConfigureMongoDb(IServiceCollection services)
        {
            var mb = new MongoUrlBuilder
            {
                Username = "readonly",
                Password = "turner",
                Server = new MongoServerAddress("ds043348.mongolab.com:43348"),
                DatabaseName = "dev-challenge"
            };

            var mongoClient =
                new MongoClient(mb.ToMongoUrl());
                            ;
            var mongoDatabase = mongoClient.GetDatabase(Configuration.GetValue<string>("MongoDatabase"));
            mongoClient.StartSession();
                
            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddTransient(context => mongoDatabase);
            services.AddTransient(context => mongoDatabase.GetCollection<Movie>("Titles"));
            services.AddTransient(context => mongoDatabase.GetCollection<MovieTitle>("Titles"));
            services.AddTransient<IMoviesRepository, MovieRepository>();
            services.AddTransient<IMovieTitleRepository, MovieTitleRepository>();


            if (!BsonClassMap.IsClassMapRegistered(typeof(MovieTitle)))
            {
                BsonClassMap.RegisterClassMap<MovieTitle>(mapper =>
                {
                    mapper
                        .AutoMap();

                    mapper
                       .SetIgnoreExtraElements(true);

                    mapper
                        .MapMember(map => map.Name)
                        .SetElementName("TitleName");

                    mapper
                        .MapMember(map => map.NameSortable)
                        .SetElementName("TitleNameSortable");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Movie)))
            {
                BsonClassMap.RegisterClassMap<Movie>(mapper =>
                {
                    mapper
                        .AutoMap();

                    mapper
                       .SetIgnoreExtraElements(true);

                    mapper
                        .MapMember(map => map.Name)
                        .SetElementName("TitleName");

                    mapper
                        .MapMember(map => map.NameSortable)
                        .SetElementName("TitleNameSortable");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Award)))
            {
                BsonClassMap.RegisterClassMap<Award>(mapper =>
                {
                    mapper
                    .AutoMap();

                    mapper
                        .MapMember(map => map.Name)
                        .SetElementName("Award");

                    mapper
                        .MapMember(map => map.Won)
                        .SetElementName("AwardWon");

                    mapper
                        .MapMember(map => map.Year)
                        .SetElementName("AwardYear");

                    mapper
                        .MapMember(map => map.Company)
                        .SetElementName("AwardCompany");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Title)))
            {
                BsonClassMap.RegisterClassMap<Title>(mapper =>
                {
                    mapper
                        .AutoMap();

                    mapper
                        .MapMember(map => map.Name)
                        .SetElementName("TitleName");

                    mapper
                        .MapMember(map => map.Type)
                        .SetElementName("TitleNameType");

                    mapper
                        .MapMember(map => map.NameSortable)
                        .SetElementName("TitleNameSortable");

                    mapper
                        .MapMember(map => map.Language)
                        .SetElementName("TitleNameLanguage");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Participant)))
            {
                BsonClassMap.RegisterClassMap<Participant>(mapper =>
                {
                    mapper
                        .AutoMap();

                    mapper
                        .SetIgnoreExtraElements(true);

                    mapper
                        .MapMember(map => map.Id)
                        .SetElementName("ParticipantId");


                    mapper
                        .MapMember(map => map.Name)
                        .SetElementName("Name");

                    mapper
                        .MapMember(map => map.Type)
                        .SetElementName("ParticipantType");

                    mapper
                        .MapMember(map => map.IsOnScreen)
                        .SetElementName("IsOnScreen");

                    mapper
                        .MapMember(map => map.RoleType)
                        .SetElementName("RoleType");

                    mapper
                        .MapMember(map => map.IsKey)
                        .SetElementName("IsKey");
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
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
