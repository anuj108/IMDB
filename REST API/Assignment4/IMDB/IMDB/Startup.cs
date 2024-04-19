

using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IMDB
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration =configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IActorService, ActorService>();
            services.AddSingleton<IActorRepository,ActorRepository>();
            services.AddSingleton<IProducerService,ProducerService>();
            services.AddSingleton<IProducerRepository, ProducerRepository>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<IGenreService, GenreService>();
            services.AddSingleton<IGenreRepository, GenreRepository>();
            services.AddSingleton<IReviewService, ReviewService>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();

            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
        }

        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
