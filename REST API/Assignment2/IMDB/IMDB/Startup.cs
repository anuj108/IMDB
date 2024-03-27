

using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services;
using IMDB.Services.Interfaces;

namespace IMDB
{
    public class Startup
    {
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
