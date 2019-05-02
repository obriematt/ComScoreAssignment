using CommandLine;
using ComScoreAssignment.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComScoreAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            DataImporter dataImporter = new DataImporter();
            DataQuery dataQuery = new DataQuery();
            

            Parser.Default.ParseArguments<Options>(args).WithParsed(options => 
            {
                foreach(string s in options.Select)
                {
                    Console.WriteLine(s);
                }
                if (options.LoadFile != null)
                {
                    dataImporter.ImportFromRawDataFile(options.LoadFile);
                }
                else if (options.LoadDataStoreFile != null)
                {
                    // Load the data store
                    dataImporter.ReadDataInFromStorage(options.LoadDataStoreFile);
                }
                if ((options.Select.Count() > 0) || options.Filter != null || options.OrderBy.Count() > 0)
                {
                    // Select the stuff
                    // Need to check for orderby and filter.
                    if(options.Select.Count() > 0 && options.Filter != null && options.OrderBy.Count() > 0)
                    {
                        var item = dataQuery.OrderData(options.Select, dataImporter.GetMovieList(), options.Filter, options.OrderBy);
                        foreach (dynamic piece in item)
                        {
                            Console.WriteLine(piece);
                        }
                    }
                    else if (options.Select.Count() > 0 && options.Filter != null && options.OrderBy.Count() == 0)
                    {
                        var item = dataQuery.SelectAndFilterData(options.Select, dataImporter.GetMovieList(), options.Filter);
                        foreach (dynamic piece in item)
                        {
                            Console.WriteLine(piece);
                        }
                    }
                    else if(options.Select.Count() > 0  && options.Filter == null && options.OrderBy.Count() == 0)
                    {
                        var item = dataQuery.SelectData(options.Select, dataImporter.GetMovieList());
                        foreach(dynamic piece in item)
                        {
                            Console.WriteLine(piece);
                        }
                    }

                }
            });
            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}
