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
     
 
        public List<string> Actors { get; set; }


        private string _title,_plot,_producer,_actorName,_producerName;
        private string _movietodelete;
        private Exception _exception;
        private string _yearofrelease,_actors;
        
        private IIMDBService _imdbService;
        private List<Movie> _movies;
        DateTime dateOfBirth;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        public IMDBStepDefinitions(ISpecFlowOutputHelper outputHelper)
        {
            _imdbService = new IMDBService();
                _specFlowOutputHelper = outputHelper; 
        }
        [Given(@"I have a movie with title ""([^""]*)""")]
        public void GivenIHaveAMovieWithTitle(string title)
        {
            _title=title;
        }

        [Given(@"the year of release is ""([^""]*)""")]
        public void GivenTheYearOfReleaseIs(string yor)
        {
            _yearofrelease=yor;
        }


        [Given(@"the plot is ""([^""]*)""")]
        public void GivenThePlotIs(string plot)
        {
            _plot=plot;
        }

        [Given(@"the actors are ""([^""]*)""")]
        public void GivenTheActorsAre(string actors)
        {
            _actors=actors;
        }


        [Given(@"the producer is ""([^""]*)""")]
        public void GivenTheProducerIs(string producer)
        {
            _producer=producer;
        }

        [When(@"I add the movie in IMDB app")]
        public void WhenIAddTheMovieInIMDBApp()
        {
            try
            {
                _imdbService.AddMovie(_title, _yearofrelease, _plot, _actors, _producer);
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
                Producer = _imdbService.ListProducer()[Convert.ToInt32(row["Producer"])-1],
                Actors = row["Actors"].Split(',').Select(index => _imdbService.ListActor()[Convert.ToInt32(index)-1]).ToList(),
                Plot = row["Plot"]
            }).ToList();



            for (int i = 0; i < expectedMovies.Count; i++)
            {
                if (expectedMovies.Count != movies.Count)
                {
                    throw new Exception("NOT MATCHE");
                }
                if (expectedMovies[i].Title != movies[i].Title || expectedMovies[i].YearOfRelease != movies[i].YearOfRelease||expectedMovies[i].Producer!=movies[i].Producer||expectedMovies[i].Plot!=movies[i].Plot)
                {
                    throw new Exception("NOT MATCHED");
                }
                if (!expectedMovies[i].Actors.SequenceEqual(movies[i].Actors))
                {
                    throw new Exception("NOT MATCH");
                }
            }
            

            //Assert.Equal(movies, expectedMovies);

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
        public void ThenIShouldHaveTheFollowingMovies(Table expectedTable)
        {

            // table.CompareToSet(_movies);

            var expectedMovies = expectedTable.Rows.Select(row => new Movie
            {
                Title = row["Title"],
                YearOfRelease = row["YearofRelease"],
                Producer = _imdbService.ListProducer()[Convert.ToInt32(row["Producer"])-1],
                Actors = row["Actors"].Split(',').Select(index => _imdbService.ListActor()[Convert.ToInt32(index)-1]).ToList(),
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

        [Given(@"I have Actor Name ""([^""]*)""")]
        public void GivenIHaveActorName(string name)
        {
            _actorName=name;
        }

        [Given(@"I have Actor's DOB ""([^""]*)""")]
        public void GivenIHaveActorsDOB(string dob)
        {
            dateOfBirth = DateTime.Parse(dob);
        }

        [When(@"I add actor in IMDB app")]
        public void WhenIAddActorInIMDBApp()
        {
            _imdbService.AddActor(_actorName,dateOfBirth);
        }

        [Then(@"Actor would be added in app")]
        public void ThenActorWouldBeAddedInApp(Table table)
        {
            var actors=_imdbService.ListActor();


            var expectedTable = table.Rows.Select(row => new Actor{
                Name=row["Name"],
                DOB=DateTime.Parse(row["DOB"])
            }).ToList();

            for (int i = 0; i < expectedTable.Count; i++)
            {
                if (expectedTable.Count != actors.Count)
                {
                    throw new Exception("NOT MATCHED");
                }
                if (expectedTable[i].Name != actors[i].Name || expectedTable[i].DOB != actors[i].DOB)
                {
                    throw new Exception("NOT MATCHED");
                }
                


            }

        }


        [Given(@"I have Producer Name ""([^""]*)""")]
        public void GivenIHaveProducerName(string aV)
        {
            _producerName= aV;
        }

        [Given(@"I have Producer's DOB ""([^""]*)""")]
        public void GivenIHaveProducersDOB(string p0)
        {
            dateOfBirth= DateTime.Parse(p0);
        }

        [When(@"I add producer in IMDB app")]
        public void WhenIAddProducerInIMDBApp()
        {
            _imdbService.AddProducer(_producerName,dateOfBirth);
        }

        [Then(@"Producer would be added in app")]
        public void ThenProducerWouldBeAddedInApp(Table table)
        {
            var producers = _imdbService.ListProducer();
            var expectedTable = table.Rows.Select(row=>new Producer
            {
                Name=row["Name"],
                DOB=DateTime.Parse(row["DOB"])
            }).ToList();

            if(producers.Count()==expectedTable.Count())
            {
                for(int i =0; i<expectedTable.Count(); i++) 
                {
                    if (expectedTable[i].Name!=producers[i].Name||expectedTable[i].DOB!=producers[i].DOB)
                    {
                        
                        
                        throw new Exception("UNMATCHED");
                    }
                }
            }

        }


        [Given(@"I have movie name ""([^""]*)""")]
        public void GivenIHaveMovieName(string d)
        {
            _movietodelete=d;

        }

        [When(@"I delete movie in IMDB app")]
        public void WhenIDeleteMovieInIMDBApp()
        {
            _imdbService.DeleteMovie(_movietodelete);
        }

        [Then(@"movie gets deleted and shows the result")]
        public void ThenMovieGetsDeletedAndShowsTheResult(Table expectedTable)
        {
            var movies = _imdbService.ListMovie();
            var expectedMovies = expectedTable.Rows.Select(row => new Movie
            {
                Title = row["Title"],
                YearOfRelease = row["YearofRelease"],
                Producer = _imdbService.ListProducer()[Convert.ToInt32(row["Producer"])-1],
                Actors = row["Actors"].Split(',').Select(index => _imdbService.ListActor()[Convert.ToInt32(index)-1]).ToList(),
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


        [BeforeScenario("addMovie")]

        public void AddSampleActorsForAdd()
        {
            _imdbService.AddActor("Tom Hanks", new DateTime(1956, 7, 9));
            _imdbService.AddActor("Meryl Streep", new DateTime(1949, 6, 22));
            _imdbService.AddActor("Leonardo DiCaprio", new DateTime(1974, 11, 11));
            _imdbService.AddActor("Emma Watson", new DateTime(1990, 4, 15));
            _imdbService.AddActor("Joseph Gordon-Levitt", new DateTime(1981, 2, 17));
            _imdbService.AddActor("Tim Robbins", new DateTime(1958, 10, 16));
            _imdbService.AddActor("Morgan Freeman", new DateTime(1937, 6, 1));
        }
        [BeforeScenario("addMovie")]
        public void AddSampleProducersForAdd()
        {
            _imdbService.AddProducer("Christopher Nolan", new DateTime(1970, 7, 30));
            _imdbService.AddProducer("Steven Spielberg", new DateTime(1946, 12, 18));
            _imdbService.AddProducer("Niki Marvin", new DateTime(1956, 2, 18));

        }
        [BeforeScenario("addMovie")]
        public void AddSampleMovieForAdd()
        {

            _imdbService.AddMovie("Inception", "2010", "A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home.", "3,5","1");
        }

        [BeforeScenario("listMovie")]
        public void AddSampleActorsToList()
        {
            _imdbService.AddActor("Tom Hanks", new DateTime(1956, 7, 9));
            _imdbService.AddActor("Meryl Streep", new DateTime(1949, 6, 22));
            _imdbService.AddActor("Leonardo DiCaprio", new DateTime(1974, 11, 11));
            _imdbService.AddActor("Emma Watson", new DateTime(1990, 4, 15));
            _imdbService.AddActor("Joseph Gordon-Levitt", new DateTime(1981, 2, 17));
            _imdbService.AddActor("Tim Robbins", new DateTime(1958, 10, 16));
            _imdbService.AddActor("Morgan Freeman", new DateTime(1937, 6, 1));
        }
        [BeforeScenario("listMovie")]
        public void AddSampleProducersToList()
        {
            _imdbService.AddProducer("Christopher Nolan", new DateTime(1970, 7, 30));
            _imdbService.AddProducer("Steven Spielberg", new DateTime(1946, 12, 18));
            _imdbService.AddProducer("Niki Marvin", new DateTime(1956, 2, 18));

        }
        [BeforeScenario("listMovie")]
        public void AddSampleMovieForList()
        {
            _imdbService.AddMovie("Inception", "2010", "A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home.", "3,5", "1");

        }

        [BeforeScenario("deleteMovie")]
        public void AddSampleActorsToDelete()
        {
            _imdbService.AddActor("Tom Hanks", new DateTime(1956, 7, 9));
            _imdbService.AddActor("Meryl Streep", new DateTime(1949, 6, 22));
            _imdbService.AddActor("Leonardo DiCaprio", new DateTime(1974, 11, 11));
            _imdbService.AddActor("Emma Watson", new DateTime(1990, 4, 15));
            _imdbService.AddActor("Joseph Gordon-Levitt", new DateTime(1981, 2, 17));
            _imdbService.AddActor("Tim Robbins", new DateTime(1958, 10, 16));
            _imdbService.AddActor("Morgan Freeman", new DateTime(1937, 6, 1));
        }
        [BeforeScenario("deleteMovie")]
        public void AddSampleProducersToDelete()
        {
            _imdbService.AddProducer("Christopher Nolan", new DateTime(1970, 7, 30));
            _imdbService.AddProducer("Steven Spielberg", new DateTime(1946, 12, 18));
            _imdbService.AddProducer("Niki Marvin", new DateTime(1956, 2, 18));

        }
        [BeforeScenario("deleteMovie")]
        public void AddSampleMovieForDelete()
        {
            
            
            _imdbService.AddMovie("Inception", "2010", "A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home.", "3,5", "1");
        }

    }
}
