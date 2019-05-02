using System;
using System.Collections.Generic;
using System.Text;

namespace ComScoreAssignment.Constants
{
    public class MovieKey 
    {
        // Unique identication for movies.
        private readonly string _stb;
        private readonly string _title;
        private readonly string _date;
        private readonly string _dataKey;

        public MovieKey(string stb, string title, string date)
        {
            _stb = stb;
            _title = title;
            _date = date;
            _dataKey = $"{stb}{title}{date}";
        }

        public override bool Equals(object obj)
        {
            var movieKey = obj as MovieKey;

            if(movieKey == null)
            {
                return false;
            }
            //return _date.Equals(movieKey._date) && _title.Equals(movieKey._date) && _stb.Equals(movieKey._stb);
            return _dataKey.Equals(movieKey._dataKey);
        }

        public override int GetHashCode()
        {
            return _dataKey.GetHashCode();
        }
    }
}
