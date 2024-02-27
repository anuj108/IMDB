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
            string title, plot, producer="";
            string yearofrelease;
            int result;
           
            //SAMPLE MOVIES
            List<string> Actors1 = new List<string>() { "Leonardo DiCaprio", "Joseph Gordon-Levitt" };
            services.AddMovie("Inception", "Dom Cobb, a skilled thief, enters the subconscious of targets to steal their secrets, but his latest job involves planting an idea instead.", "2010", Actors1, "Christopher Nolan");

            List<string> Actors2 = new List<string>() { "Morgan Freeman", "Tim Robbins" };
            services.AddMovie("Avatar", "Andy Dufresne, a banker wrongly convicted of murder, forms a bond with fellow inmate Red while finding solace and redemption in Shawshank Prison.", "1994", Actors2, "Frank Darabont");

            List<string> Actors3 = new List<string>() { "John Travolta", "Uma Thurman" };
            services.AddMovie("Avatar 2 ", "A series of interconnected stories involving two hitmen, a boxer, a gangster, and his wife intertwine in the criminal underworld of Los Angeles.", "1994", Actors3, " Lawrence Bender");


            //SAMPLE ACTORS
            List<Actor> allactors = services.ListActor();
            allactors.Add(new Actor("Leonardo DiCaprio", DateTime.Parse("1990-02-02")));
            allactors.Add(new Actor("Leonardo DiCapri", DateTime.Parse("1990-03-02")));
            allactors.Add(new Actor("Leonardo DiCapr", DateTime.Parse("1990-12-02")));

            List<Producer> allproducers = services.ListProducer();

            //SAMPLE PRODUCERS
            allproducers.Add(new Producer("Lawrence Bender", DateTime.Parse("1990-02-02")));
            allproducers.Add(new Producer("Lawrence Bender", DateTime.Parse("1990-02-02")));
            allproducers.Add(new Producer("Lawrence Bender", DateTime.Parse("1990-02-02")));



            while (true)
            {
                Console.WriteLine("----------------------------------------");

                Console.WriteLine("WELCOME TO IMDB APP ... PLEASE SELECT YOUR OPTIONS ");
                Console.WriteLine("1. ADD MOVIE \n2. LIST MOVIES \n3. ADD ACTOR \n4. ADD PRODUCER \n5. DELETE MOVIE");

                Console.WriteLine("----------------------------------------");

                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Console.WriteLine("INVALID FORMAT");
                    continue;
                }

                switch (result)
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
                       
                        foreach (Actor actor in allactors)
                        {
                            Console.Write(k+". "+actor.Name+" ");
                            k++;
                        }
                        Console.WriteLine();
                        string choiceofactor = Console.ReadLine();
                        string[] choiceindex;
                        var flag=false;
                        List<string> actors = new List<string>();
                        if (!string.IsNullOrEmpty(choiceofactor))
                        {
                            choiceindex = choiceofactor.Split(',');
                            for (int j = 0; j<choiceindex.Length; j++)
                            {
                                if (int.TryParse(choiceindex[j], out result))
                                {
                                    if (result-1<allactors.Count()&&result>=0)
                                        actors.Add(allactors[result-1].Name);
                                    else
                                    {
                                        Console.WriteLine("INVALID INDEX");
                                        flag=true;
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("WRONG FORMAT");
                                }

                               
                            }
                            if(flag)
                            {
                                continue;
                            }
                        }
                        
                       
                        Console.WriteLine("SELECT THE PRODUCERS FROM THE LIST : ");
                        k = 1;
                        foreach (Producer x in allproducers)
                        {
                            Console.Write(k+". "+x.Name+" ");
                            k++;
                        }
                        Console.WriteLine();
                        string choiceofproducer = Console.ReadLine();
                        if(!string.IsNullOrEmpty(choiceofproducer))
                        {
                            try
                            {
                                if(int.TryParse(choiceofproducer, out result))
                                {
                                    if (result-1<allproducers.Count()&&result>=0)
                                    {
                                        producer= allproducers[result-1].Name;
                                    }
                                    else
                                    {
                                        Console.WriteLine("INVALID INDEX");
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("WRONG FORMAT!!");
                                }
                                
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }
                        }
                        

                        try
                        {
                            services.AddMovie(title, plot, yearofrelease, actors, producer);
                            Console.WriteLine("OPERATION SUCCESSFUL");
                            Console.WriteLine("--------------------------------------");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
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
                    case 3:
                        Console.WriteLine("ENTER ACTOR NAME: ");
                        string Aname=Console.ReadLine();
                        Console.WriteLine("ENTER ACTOR's DOB (e.g., yyyy-MM-dd):");
                        DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

                        try
                        {
    
                            services.AddActor(Aname, dateOfBirth);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                     case 4:
                        Console.WriteLine("ENTER PRODUCER NAME: ");
                        string Pname = Console.ReadLine();
                        Console.WriteLine("ENTER PRODUCER's DOB (e.g., yyyy-MM-dd):");
                        dateOfBirth = DateTime.Parse(Console.ReadLine());

                        try
                        {
                            services.AddProducer(Pname, dateOfBirth);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;

                        case 5:
                        Console.WriteLine("WHICH MOVIE DO YOU WANT TO DELETE\n");
                        int p = 1;
                        foreach (Movie movie in services.ListMovie())
                        {
                            Console.WriteLine(p+". "+movie.Title);
                            p++;
                        }
                        string res=Console.ReadLine();
                        if(int.TryParse(res, out result))
                        {
                            if (result-1<services.ListMovie().Count()&&result>=0)
                            {
                                if (services.DeleteMovie(services.ListMovie()[result-1].Title))
                                {
                                    Console.WriteLine("DELETED SUCCESSFULLY");
                                }
                                else
                                {
                                    Console.WriteLine("NOT DELETED");
                                }
                            }
                            else
                            {
                                Console.WriteLine("INVALID INDEX");
                            }
                        }
                        else
                        {
                            Console.WriteLine("WRONG FORMAT!!");
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
