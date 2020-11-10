using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Skeleton.Api.GraphQL.Query;
using Skeleton.Api.GraphQL.Schemas;
using Skeleton.Api.GraphQL.Type;
using Skeleton.Domain.Models;
using Skeleton.Domain.Models.Users;
using Skeleton.Domain.Options;
using Skeleton.Domain.Repositories.Abstraction;
using Skeleton.Domain.Services.AuthServices;
using Skeleton.Domain.Services.AuthServices.Abstractions;
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
            //Options
            services
                .AddOptions<AuthOptions>()
                .Bind(Configuration.GetSection(nameof(ApplicationOptions.Auth)))
                .ValidateDataAnnotations();
            services.AddScoped(x => x.GetRequiredService<IOptions<AuthOptions>>().Value);
            //DbContext
            services.AddDbContext<SkeletonApiContext>(
                options => options.UseMySQL(Configuration.GetConnectionString("SkeletonApiContext")));
            
            //Controllers
            services.AddControllers();
            // Services
            services
                .AddScoped<ICrudService<Question, int>, CrudService<Question, int>>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            
            // Repositories
            services
                .AddScoped<ICrudRepository<Question, int>, CrudRepository<Question, int>>()
                .AddScoped<ICrudRepository<User, int>, CrudRepository<User, int>>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            // Authentification
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "graphQLApi";
                });
            
            // GraphQL
            services
                .AddScoped<QuestionAnswerType>()
                .AddScoped<QuestionCategorieType>()
                .AddScoped<QuestionPropositionType>()
                .AddScoped<QuestionType>()
                .AddScoped<QuestionQuery>()
                .AddScoped<ISchema, QuestionSchema>()
                .AddScoped<IDocumentExecuter, DocumentExecuter>()
                .AddGraphQL()
                // Add required services for de/serialization
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { }) // For .NET Core 3+
                .AddWebSockets()
                .AddGraphTypes(typeof(QuestionSchema));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseAuthentication()
                .UseWebSockets();
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
