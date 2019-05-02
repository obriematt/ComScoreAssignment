using ComScoreAssignment;
using System;
using System.Collections.Generic;

namespace TestingEnvironment
{
    class Program
    {
        static void Main(string[] args)
        {
            DataImporter dataImporter = new DataImporter();
            DataQuery dataQuery = new DataQuery();

            dataImporter.ImportFromRawDataFile(@"C:\Users\obrie\Source\Repos\ComScoreAssignment\TestingEnvironment\TestFileTester.txt");

            List<string> queryParams = new List<string>();
            queryParams.Add("STB");
            queryParams.Add("Title");
            string filtered = "STB=stb1";
            List<string> orderedBy = new List<string>();
            orderedBy.Add("Title");

            var selectedmovies = dataQuery.SelectData(queryParams, dataImporter.GetMovieList());
            foreach (dynamic movie in selectedmovies)
            {
                Console.WriteLine(movie);
            }

            var selectedmovies2 = dataQuery.SelectAndFilterData(queryParams, dataImporter.GetMovieList(), filtered);
            foreach (dynamic movie in selectedmovies2)
            {
                Console.WriteLine(movie);
            }

            var selectedmovies3 = dataQuery.OrderData(queryParams, dataImporter.GetMovieList(), filtered, orderedBy);
            foreach (dynamic movie in selectedmovies3)
            {
                Console.WriteLine(movie);
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
