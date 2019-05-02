using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComScoreAssignment
{
    public class Options
    {
        // Boolean if select is used.
        [Option('s', "select", Separator = ':' , HelpText ="Select input parameter")]
        public IEnumerable<string> Select { get; set; }

        // Boolean for if orderby is used for queries
        [Option('o', "orderby", Default = null, Separator = ':', HelpText = "OrderBy input parameter")]
        public IEnumerable<string> OrderBy { get; set; }

        // Boolean for filter options
        [Option('f', "Filter", Default = null, HelpText = "Filter input parameter")]
        public string Filter { get; set; }

        // Boolean for loading the data.
        [Option('l', "Load", Default = null, HelpText = "Load Data input parameter")]
        public string LoadFile { get; set; }

        // Boolean for loading the datastore files
        [Option('d', "DataStore", Default = null, HelpText = "Load DataStore input parameter")]
        public string LoadDataStoreFile { get; set; }
    }
}
