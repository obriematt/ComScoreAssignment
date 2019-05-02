using ComScoreAssignment.Constants;
using ComScoreAssignment.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComScoreAssignment
{
    public class DataImporter
    {
        private Dictionary<MovieKey, Movies> _movieDictionary;

        public DataImporter()
        {
            _movieDictionary = new Dictionary<MovieKey, Movies>();
        }

        public List<Movies> GetMovieList()
        {
            return _movieDictionary.Values.ToList();
        }

        // Raw data file
        public void ImportFromRawDataFile(string fileName)
        {
            // null/empty filename to attempt to import.
            if(string.IsNullOrEmpty(fileName))
            {
                return;
            }

            // filename not found.
            //if (!File.Exists(fileName))
            //{
            //    return;
            //}

            // import the file information.
            var lines = File.ReadLines(fileName);
            foreach(var line in lines)
            {
                // Read in the line and split on bar. If the length isn't 6, the data is bad.
                string[] movieInformation = line.Split('|');
                if (movieInformation.Length != 6)
                {
                    break;
                }

                // Create the new key and movie for the dictionary.
                MovieKey movieKey = new MovieKey(movieInformation[0], movieInformation[1], movieInformation[3]);
                Movies newMovie = new Movies {
                    STB = movieInformation[0],
                    Title = movieInformation[1],
                    Provider = movieInformation[2],
                    Date = movieInformation[3],
                    Rev = movieInformation[4],
                    View_Time = movieInformation[5]
                };

                // Add to the in memory storage
                if (!_movieDictionary.ContainsKey(movieKey))
                {
                    _movieDictionary.Add(movieKey, newMovie);
                }
                // The key already exists
                else
                {
                    // Compare the movie objects, add the more recent one to the storage
                    Movies originalMovie = _movieDictionary[movieKey];
                    if(originalMovie.CompareTo(newMovie) < 0)
                    {
                        _movieDictionary[movieKey] = newMovie;
                    }
                }
            }
        }

        // Moves the data storage from memory to file.
        public void ExportDataToStorageFile()
        {
            List<Movies> movieList = _movieDictionary.Values.ToList();
            //string jsonObjects = JsonConvert.SerializeObject(movieList.ToArray());
            using(StreamWriter file = File.CreateText(@"test.json"))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(file, movieList);
            }
        }

        // Already converted Data file
        public void ReadDataInFromStorage(string fileName)
        {
            string json = File.ReadAllText(fileName);
            var movieList = JsonConvert.DeserializeObject<List<Movies>>(json);
            AddMoviesToMemoryStorage(movieList);
        }

        private void AddMoviesToMemoryStorage(List<Movies> movieList)
        {
            foreach (Movies movie in movieList)
            {
                MovieKey movieKey = new MovieKey(movie.STB, movie.Title, movie.Date);

                // Add to the in memory storage
                if (!_movieDictionary.ContainsKey(movieKey))
                {
                    _movieDictionary.Add(movieKey, movie);
                }
                // The key already exists
                else
                {
                    // Compare the movie objects, add the more recent one to the storage
                    Movies originalMovie = _movieDictionary[movieKey];
                    if (originalMovie.CompareTo(movie) < 0)
                    {
                        _movieDictionary[movieKey] = movie;
                    }
                }
            }
        }
    }
}
