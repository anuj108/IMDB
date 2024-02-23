using System;
using TechTalk.SpecFlow;


using System.Data;
using TechTalk.SpecFlow.Assist;
using System.Xml.Linq;
using IMDB.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using IMDB.Domain;

namespace IMDB.Tests.StepDefinitions
{
    [Binding]
    public class IMDBStepDefinitions
    {
        private string _title,_plot,_producer;
        private Exception _exception;
        private string _yearofrelease;
        List<string> actors=new List<string>();
        private IIMDBService _imdbService;
        private List<Movie> _movies;
        public IMDBStepDefinitions()
        {
            _imdbService = new IMDBService();
        }
        [Given(@"I have a movie with title ""([^""]*)""")]
        public void GivenIHaveAMovieWithTitle(string a)
        {
            _title=a;
        }

        [Given(@"the year of release is (.*)")]
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
            actors=table.CreateInstance<List<string>>();
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
                _imdbService.AddMovie(_title, _plot, _yearofrelease,actors,_producer);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"IMDB app would look like this")]
        public void ThenIMDBAppWouldLookLikeThis(Table table)
        {
            var movies = _imdbService.ListMovie();
            table.CompareToSet(movies);
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
            table.CompareToSet(_movies);
        }




    }
}
