using ComScoreAssignment.Enums;
using ComScoreAssignment.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq;

namespace ComScoreAssignment
{
    public class DataQuery
    {
        private readonly string defaultSelect = "new (STB, Title, Provider, Date, Rev, View_Time)";

        public dynamic SelectData(IEnumerable<string> queryParams, IEnumerable<Movies> movies)
        {
            string selectStatement = ConstructSelectString(queryParams);
            if(selectStatement == null)
            {
                selectStatement = defaultSelect;
            }
            var items = movies.Select(selectStatement);
            return items;
        }

        public dynamic SelectAndFilterData(IEnumerable<string> queryParams, IEnumerable<Movies> movies, string filterParams)
        {
            // Construct filter statement for Dynamic LINQ
            string selectStatement = ConstructSelectString(queryParams);
            List<string> filterStatement = ConstructFilterString(filterParams);

            // Select statement isn't null.
            if (selectStatement != null)
            {
                var items = movies.Where(filterStatement[0], filterStatement[1]).Select(selectStatement);
                return items;
            }
            // Select Statement is null, and only want to use WHERE clause.
            else
            {
                // Query the data set using Dynamic LINQ
                var items = movies.Where(filterStatement[0], filterStatement[1]).Select(defaultSelect);
                return items;
            }
        }

        public dynamic OrderData(IEnumerable<string> queryParams, IEnumerable<Movies> movies, string filterParams, IEnumerable<string> column)
        {
            // Construct filter statement for Dynamic LINQ
            string selectStatement = ConstructSelectString(queryParams);
            List<string> filterStatement = ConstructFilterString(filterParams);
            string orderByStatement = ConstructOrderByString(column);

            // Select, Order
            if(selectStatement != null && filterStatement == null)
            {
                var items = movies.Select(selectStatement).OrderBy(orderByStatement);
                return items;
            }
            // Select, Filter, Order
            if(selectStatement != null && filterStatement != null)
            {
                var items = movies.Select(selectStatement).Where(filterStatement[0], filterStatement[1]).OrderBy(orderByStatement);
                return items;
            }
            // Filter, Order
            if(selectStatement == null && filterStatement != null)
            {
                var items = movies.Where(filterStatement[0], filterStatement[1]).OrderBy(orderByStatement).Select(defaultSelect);
                return items;
            }
            else
            {
                var items = movies.OrderBy(orderByStatement);
                return items;
            }

        }

        private string ConstructOrderByString(IEnumerable<string> columns)
        {
            string constructedString = null;
            foreach(string column in columns)
            {
                constructedString += $"{column},";
            }
            constructedString = constructedString.Substring(0, constructedString.Length - 1);
            return constructedString;
        }

        private string ConstructSelectString(IEnumerable<string> queryParams)
        {
            if(queryParams == null)
            {
                return null;
            }
            string selectStatement = "new (";
            foreach (string parameter in queryParams)
            {
                selectStatement += ($"{parameter},");
            }
            selectStatement = selectStatement.Substring(0, selectStatement.Length - 1) + ")";
            return selectStatement;
        }

        private List<string> ConstructFilterString(string filterString)
        {
            if(filterString == null)
            {
                return null;
            }
            List<string> filterStringConstructed = new List<string>();
            string[] splitString = filterString.Split('=');
            filterStringConstructed.Add($"{splitString[0]} == @0");
            filterStringConstructed.Add(splitString[1]);
            return filterStringConstructed;
        }
    }
}
