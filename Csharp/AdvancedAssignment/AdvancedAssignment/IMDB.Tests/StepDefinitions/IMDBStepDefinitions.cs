using System;
using TechTalk.SpecFlow;


using System.Data;
using TechTalk.SpecFlow.Assist;
using System.Xml.Linq;
using IMDB.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using IMDB.Domain;
using TechTalk.SpecFlow.Infrastructure;

namespace IMDB.Tests.StepDefinitions
{
    [Binding]
    public class IMDBStepDefinitions
    {
        public string Title { get; set; }
        public string YearOfRelease { get; set; }
        public string Producer { get; set; }
        public List<string> Actors { get; set; }
        public string Plot { get; set; }


        private string _title,_plot,_producer;
        private Exception _exception;
        private string _yearofrelease;
        List<string> _actors = new List<string>();
        private IIMDBService _imdbService;
        private List<Movie> _movies;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        public IMDBStepDefinitions(ISpecFlowOutputHelper outputHelper)
        {
            _imdbService = new IMDBService();
                _specFlowOutputHelper = outputHelper; 
        }
        [Given(@"I have a movie with title ""([^""]*)""")]
        public void GivenIHaveAMovieWithTitle(string a)
        {
            _title=a;
        }

        [Given(@"the year of release is ""([^""]*)""")]
        public void GivenTheYearOfReleaseIs(string p0)
        {
            _yearofrelease=p0;
        }


        [Given(@"the plot is ""([^""]*)""")]
        public void GivenThePlotIs(string b)
        {
            _plot=b;
        }

        [Given(@"the actors are")]
        public void GivenTheActorsAre(Table table)
        {
            _actors = new List<string>(table.Rows[0]["Actors"].Split(','));
            _specFlowOutputHelper.WriteLine("pehla actor "+_actors[0]);
        }

        [Given(@"the producer is ""([^""]*)""")]
        public void GivenTheProducerIs(string abcd)
        {
            _producer=abcd;
        }

        [When(@"I add the movie in IMDB app")]
        public void WhenIAddTheMovieInIMDBApp()
        {
            try
            {
                _imdbService.AddMovie(_title, _plot, _yearofrelease, _actors, _producer);
                _specFlowOutputHelper.WriteLine("ADDED SUCCESSFULLY");
            }
            catch (Exception ex)
            {
                _exception = ex;
            }

        }

        [Then(@"IMDB app would look like this")]
        public void ThenIMDBAppWouldLookLikeThis(Table expectedTable)
        {
            var movies = _imdbService.ListMovie();
          
            var expectedMovies = expectedTable.Rows.Select(row => new Movie
            {
                Title = row["Title"],
                YearOfRelease = row["YearofRelease"],
                Producer = row["Producer"],
                Actors = row["Actors"].Split(',').Select(a => a.Trim()).ToList(),
                Plot = row["Plot"]
            }).ToList();

            for (int i = 0; i < expectedMovies.Count; i++)
            {
                if (expectedMovies.Count != movies.Count)
                {
                    throw new Exception("NOT MATCHED");
                }
                if (expectedMovies[i].Title != movies[i].Title || expectedMovies[i].YearOfRelease != movies[i].YearOfRelease||expectedMovies[i].Producer!=movies[i].Producer||expectedMovies[i].Plot!=movies[i].Plot)
                {
                    throw new Exception("NOT MATCHED");
                }
                if (!expectedMovies[i].Actors.SequenceEqual(movies[i].Actors))
                {
                    throw new Exception("NOT MATCHED");
                }
            }

        }


        [Then(@"Then I should have an error ""([^""]*)""")]
        public void ThenThenIShouldHaveAnError(string p0)
        {
            Assert.Equal(p0, _exception.Message);
        }

        [Given(@"I have collection of movies")]
        public void GivenIHaveCollectionOfMovies()
        {
            
        }

        [When(@"I fetch my movies")]
        public void WhenIFetchMyMovies()
        {
            _movies=_imdbService.ListMovie();
        }

        [Then(@"I should have the following movies")]
        public void ThenIShouldHaveTheFollowingMovies(Table table)
        {

            // table.CompareToSet(_movies);
            var expectedMovies = table.Rows.Select(row => new Movie
            {
                Title = row["Title"],
                YearOfRelease = row["YearofRelease"],
                Producer = row["Producer"],
                Actors = row["Actors"].Split(',').Select(a => a.Trim()).ToList(),
                Plot = row["Plot"]
            }).ToList();

            for (int i = 0; i < expectedMovies.Count; i++)
            {
                if (expectedMovies.Count != _movies.Count)
                {
                    throw new Exception("NOT MATCHED");
                }
                if (expectedMovies[i].Title != _movies[i].Title || expectedMovies[i].YearOfRelease != _movies[i].YearOfRelease||expectedMovies[i].Producer!=_movies[i].Producer||expectedMovies[i].Plot!=_movies[i].Plot)
                {
                    throw new Exception("NOT MATCHED");
                }
                if (!expectedMovies[i].Actors.SequenceEqual(_movies[i].Actors))
                {
                    throw new Exception("NOT MATCHED");
                }


            }
        }
            [BeforeScenario("addMovie")]
        public void AddSampleMovieForAdd()
        {
            _specFlowOutputHelper.WriteLine("CJKBHJGDJHGDJH GJDGC JDGXHJG JHGJHX");
            List<string> sample= new List<string> { "w","e"};
            _imdbService.AddMovie("d","c","2",sample,"skld");
        }

        [BeforeScenario("listMovie")]
        public void AddSampleMovieForList()
        {
            _specFlowOutputHelper.WriteLine("CJKBHJGDJHGDJH GJDGC JDGXHJG JHGJHX");
            List<string> sample = new List<string> { "w", "e" };
            _imdbService.AddMovie("d", "c", "2", sample, "skld");
            
        }

    }
}
