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

            string title, plot,yearofrelease;
            int result;
            var exit = false;
            //SAMPLE ACTORS
            services.AddActor("Tom Hanks", new DateTime(1956, 7, 9));
            services.AddActor("Meryl Streep", new DateTime(1949, 6, 22));
            services.AddActor("Leonardo DiCaprio", new DateTime(1974, 11, 11));
            services.AddActor("Emma Watson", new DateTime(1990, 4, 15));
            services.AddActor("Joseph Gordon-Levitt", new DateTime(1981, 2, 17));
            services.AddActor("Tim Robbins", new DateTime(1958, 10, 16));
            services.AddActor("Morgan Freeman", new DateTime(1937, 6, 1));

            List<Producer> allproducers = services.ListProducer();

            //SAMPLE PRODUCERS
            services.AddProducer("Christopher Nolan", new DateTime(1970, 7, 30));
            services.AddProducer("Steven Spielberg", new DateTime(1946, 12, 18));
            services.AddProducer("Niki Marvin", new DateTime(1956, 2, 18));

            Console.WriteLine("----------------------------------------");

            Console.WriteLine("WELCOME TO IMDB APP");
            Console.WriteLine("1. ADD MOVIE \n2. LIST MOVIES \n3. ADD ACTOR \n4. ADD PRODUCER \n5. DELETE MOVIE \n6. EXIT");

            Console.WriteLine("----------------------------------------");

            while (true)
            {
                Console.WriteLine("PLEASE ENTER YOUR CHOICE ");
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Console.WriteLine("INVALID FORMAT");
                    continue;
                }

                switch (result)
                {
                    case 1:
                        //ENTER TITLE
                        Console.WriteLine("ENTER MOVIE DETAILS: ");
                        Console.Write("NAME : ");                       
                        title=Console.ReadLine();
                     
                        //ENTER YEAROFRELEASE
                        Console.Write("YEAR OF RELEASE : ");
                        yearofrelease=Console.ReadLine();
                       
                        //ENTER PLOT
                        Console.Write("PLOT : ");
                        plot=Console.ReadLine();

                        //ENTER ACTORS
                        Console.WriteLine("SELECT THE ACTORS FROM THE LIST : ");
                        var k = 1;
                        foreach (Actor actor in services.ListActor())
                        {
                            Console.Write(k+". "+actor.Name+" ");
                            k++;
                        }
                        Console.WriteLine();
                        string choiceofactor = Console.ReadLine();
                        
                        
                       
                        Console.WriteLine("SELECT THE PRODUCERS FROM THE LIST : ");
                        k = 1;
                        foreach (Producer producer in services.ListProducer())
                        {
                            Console.Write(k+". "+producer.Name+" ");
                            k++;
                        }
                        Console.WriteLine();
                        string choiceofproducer = Console.ReadLine();
                        
                        

                        try
                        {
                            services.AddMovie(title, yearofrelease,plot, choiceofactor, choiceofproducer);
                            Console.WriteLine("OPERATION SUCCESSFUL");
                            Console.WriteLine("--------------------------------------");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                        
                        
                        break;
                    case 2:
                        if(services.ListMovie().Count==0)
                        {
                            Console.WriteLine("NO MOVIES TO DISPLAY ");
                            Console.WriteLine("--------------------------------------");
                        }
                        for (int i = 0; i<services.ListMovie().Count; i++)
                        {
                            Console.Write("   "+(i+1)+".");
                            Console.Write("TITLE: "+services.ListMovie()[i].Title);
                            Console.WriteLine(" ("+services.ListMovie()[i].YearOfRelease+")");
                            Console.WriteLine("   PLOT: "+services.ListMovie()[i].Plot);
                            Console.WriteLine("   HERE IS THE LIST OF ACTORS: ");
                            for (int j = 0; j<services.ListMovie()[i].Actors.Count; j++)
                            {
                                Console.WriteLine("   "+(j+1)+". "+services.ListMovie()[i].Actors[j].Name+" ");
                            }

                            Console.WriteLine("   PRODUCER: "+ services.ListMovie()[i].Producer.Name);
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
                            Console.WriteLine("OPERATION SUCCESSFUL");
                            Console.WriteLine("--------------------------------------");
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
                            Console.WriteLine("OPERATION SUCCESSFUL");
                            Console.WriteLine("--------------------------------------");
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

                        try
                        {
                            services.DeleteMovie(res);
                            Console.WriteLine("OPERATION SUCCESSFUL");
                            Console.WriteLine("--------------------------------------");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                       
                        break;
                    case 6:
                        Console.WriteLine("THANK YOU FOR USING OUR APP\n");
                        exit= true;
                        break;
                    default: Console.WriteLine("PLEASE CHOOSE A CORRECT OPTION"); break;

                }
                if (exit)
                    break;
            }
        

            //LINQ QUERIES

            /*
            var listMoviesAfter2010 = from mov in services.ListMovie() where Convert.ToInt32(mov.YearOfRelease)>Convert.ToInt32("2010") select mov;
            //var listNames = from mov in services.ListMovie() where mov.Producer=="James Cameron" select mov.Title;
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

           // var listOfMoviesWillSmith = services.ListMovie()
               // .Where(mov => mov.Actors.Contains("Leonardo DiCaprio"))
               // .Select(mov=>mov).ToList();

           // Console.WriteLine(listOfMoviesWillSmith[0].Title);
            */









        }
    }
}
