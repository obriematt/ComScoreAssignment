# ComScore Coding Challenge

 - A basic Datastore and Query tool for use on Movie Information.
 - A Command Prompt C# tool that extracts raw data, exports to JSON, and performs basic operations.
 - Outside Nuget Dependencies:
   *  Command Line Parser - https://github.com/commandlineparser/commandline
   *  Newtonsoft Json - https://www.newtonsoft.com/json
   *  Dynamic Linq - https://github.com/wilx2000/System.Linq.Dynamic


# Using the Query Tool
 - The solution will need to be built via Visual Studio or a .NET compiler.
 - Navigate to the solution bin, containing the newly built dll, ComScoreAssignment.dll
 - The Query tool must read in either an existing formatted Json file, or a raw data file.
   * There is an example file given ExampleData.txt for testing purposes.
 - The operations the tool can perform are as follows and are case sensitive:
   * The columns are STB, Title, Provider, Date, Rev, View_Time.
   * -s to select a return column. Multiple inputs allowed, and separated by  ':'
   * -o to order the return by a specific column.
   * -f to filter the return values. This only supports a single equals operation. Example input "STB=stb1".
   * -l to load raw data into the datastore. Required to perform operations with the query tool. This must be the exact path of the raw data file in question.
   * -d to load existing json formatted information. This must be the exact path of the json file.
  - An example input for the Query tool. "dotnet ComScoreAssignment.dll -l rawrdatalocation.txt -s STB:Title:Date:Rev -o Title". That command will select all of the STB, Title, Date, Rev of movies and order them by the Title.
 
# Notes
  - All of the inputs are case sensitive and rely on the user entering the correct inputs. Due to time constraints there is little error checking from user input.
  - The dotnet command is required to build/run the dll for the tool to work. 
  - The additional project was simply included for testing purposes to verify that the queries were behaving correctly.

