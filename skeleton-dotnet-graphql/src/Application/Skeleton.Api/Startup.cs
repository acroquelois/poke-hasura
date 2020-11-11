using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skeleton.Api.GraphQL.Mutation;
using Skeleton.Api.GraphQL.Query;
using Skeleton.Api.GraphQL.Schemas;
using Skeleton.Api.GraphQL.Type;
using Skeleton.Domain.Models;
using Skeleton.Domain.Repositories.Abstraction;
using Skeleton.Domain.Services.Core;
using Skeleton.Domain.UnitOfWork.Abstraction;
using Skeleton.Internal;
using Skeleton.Internal.Repositories.Core;
using Skeleton.Internal.UnitOfWork;

namespace Skeleton.Api
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

            //DbContext
            services.AddDbContext<SkeletonApiContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("SkeletonApiContext")));
            // Services
            services
                .AddScoped<ICrudService<Question, int>, CrudService<Question, int>>();

            services.AddControllers();
            
            // Repositories
            services
                .AddScoped<ICrudRepository<Question, int>, CrudRepository<Question, int>>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            // GraphQL
            services
                .AddSingleton<IDocumentExecuter, DocumentExecuter>()
                .AddSingleton<IDocumentWriter, DocumentWriter>()
                .AddSingleton<QuestionQuery>()
                .AddSingleton<QuestionMutation>()
                .AddSingleton<ISchema, QuestionSchema>()
                .AddGraphQL()
                // Add required services for de/serialization
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { }) // For .NET Core 3+
                .AddWebSockets();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseWebSockets();
            app.UseGraphQL<ISchema>();
            if (env.IsDevelopment())
            {
                app.UseGraphiQLServer();
            }
            UpdateDatabase(app);
        }
        
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SkeletonApiContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
