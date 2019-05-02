using ComScoreAssignment.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComScoreAssignment.Utilities
{
    public class Movies : IComparable<Movies>
    {
        public string STB { get; set; }
        public string Title { get; set; }
        public string Provider { get; set; }
        public string Date { get; set; }
        public string Rev { get; set; }
        public string View_Time { get; set; }


        // Only returns the most recent movie information between two movie sets
        public int CompareTo(Movies other)
        {
            // Convert strings to DateTime for comparison
            var thisDate = Convert.ToDateTime(Date);
            var otherDate = Convert.ToDateTime(other.Date);

            // Use the built in comparer from DateTime
            return thisDate.CompareTo(otherDate);
        }
    }
}
