using IMDB.Domain;
using IMDB.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text;

namespace IMDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            IMDBService services = new IMDBService();
            string title, plot, producer;
            string yearofrelease;
            List<string> allactors = new List<string>{"ksjd","hsdj" };
            List<string> allproducers = new List<string> { "ksaedeaefsjd", "hsdfaefaefa3ej" };
            List<string> actors = new List<string>();


            //SAMPLE MOVIES
            List<string> Actors1 = new List<string>() { "Leonardo DiCaprio", "Joseph Gordon-Levitt" };
            services.AddMovie("Inception", "Dom Cobb, a skilled thief, enters the subconscious of targets to steal their secrets, but his latest job involves planting an idea instead.", "2010", Actors1, "Christopher Nolan");

            List<string> Actors2 = new List<string>() { "Morgan Freeman", "Tim Robbins" };
            services.AddMovie("Avatar", "Andy Dufresne, a banker wrongly convicted of murder, forms a bond with fellow inmate Red while finding solace and redemption in Shawshank Prison.", "1994", Actors2, "Frank Darabont");

            List<string> Actors3 = new List<string>() { "John Travolta", "Uma Thurman" };
            services.AddMovie("Avatar 2 ", "A series of interconnected stories involving two hitmen, a boxer, a gangster, and his wife intertwine in the criminal underworld of Los Angeles.", "1994", Actors3, " Lawrence Bender");
           
           
            

            while(true)
            {
                Console.WriteLine("----------------------------------------");

                Console.WriteLine("WELCOME TO IMDB APP ... PLEASE SELECT YOUR OPTIONS ");
                Console.WriteLine("1. ADD MOVIE \n2. LIST MOVIES");

                Console.WriteLine("----------------------------------------");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("ENTER MOVIE DETAILS: ");
                        Console.Write("NAME : ");
                        title=Console.ReadLine();
                        Console.Write("YEAR OF RELEASE : ");
                        
                            yearofrelease=Console.ReadLine();
                        
                        
                        
                        Console.Write("PLOT : ");
                        plot=Console.ReadLine();
                        Console.WriteLine("SELECT THE ACTORS FROM THE LIST : ");
                        int k = 1;
                        foreach (string actor in allactors)
                        {
                            Console.Write(k+". "+actor+" ");
                            k++;
                        }
                        Console.WriteLine();
                        string choiceofactor = Console.ReadLine();
                        string[] choiceindex = choiceofactor.Split(',');
                        for (int j = 0; j<choiceindex.Length; j++)
                        {
                            actors.Add(allactors[Convert.ToInt32(choiceindex[j])-1]);
                        }
                        Console.WriteLine("SELECT THE PRODUCERS FROM THE LIST : ");
                        k = 1;
                        foreach (string x in allproducers)
                        {
                            Console.Write(k+". "+x+" ");
                            k++;
                        }
                        Console.WriteLine();
                        string choiceofproducer = Console.ReadLine();
                        producer= allproducers[Convert.ToInt32(choiceofproducer)-1];
                        try
                        {
                            services.AddMovie(title, plot, yearofrelease, actors, producer);
                            Console.WriteLine("OPERATION SUCCESSFUL");
                            Console.WriteLine("--------------------------------------");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("EXCEPTION ENCOUNTERED");
                        }
                        
                        
                        
                        break;
                    case 2:
                        for (int i = 0; i<services.ListMovie().Count; i++)
                        {
                            Console.Write("   "+(i+1)+".");
                            Console.Write("TITLE: "+services.ListMovie()[i].Title);
                            Console.WriteLine(" ("+services.ListMovie()[i].YearOfRelease+")");
                            Console.WriteLine("   PLOT: "+services.ListMovie()[i].Plot);
                            Console.WriteLine("   HERE IS THE LIST OF ACTORS: ");
                            for (int j = 0; j<services.ListMovie()[i].Actors.Count; j++)
                            {
                                Console.WriteLine("   "+(j+1)+". "+services.ListMovie()[i].Actors[j]+" ");
                            }

                            Console.WriteLine("   PRODUCER: "+ services.ListMovie()[i].Producer);
                            Console.WriteLine();
                            Console.WriteLine();

                        }
                        
                        break;
                    default: Console.WriteLine("PLEASE CHOOSE A CORRECT OPTION"); break;

                }
            }
        

            //LINQ QUERIES

            var listMoviesAfter2010 = from mov in services.ListMovie() where Convert.ToInt32(mov.YearOfRelease)>Convert.ToInt32("2010") select mov;
            var listNames = from mov in services.ListMovie() where mov.Producer=="James Cameron" select mov.Title;
            var nameandyear = from mov in services.ListMovie()
                              select new
                              {
                                  Name = mov.Title,
                                  Year = mov.YearOfRelease
                              };

            Console.WriteLine(nameandyear.ToList()[0].Name);

            var firstAvatarMovie = services.ListMovie()
                .Where(mov => mov.Title.Contains("Avatar"))
                .Select(mov => mov.Title)
                .FirstOrDefault();
            Console.WriteLine(firstAvatarMovie);

            var listOfMoviesWillSmith = services.ListMovie()
                .Where(mov => mov.Actors.Contains("Leonardo DiCaprio"))
                .Select(mov=>mov).ToList();

            Console.WriteLine(listOfMoviesWillSmith[0].Title);










        }
    }
}
