# Software Engineer Technical Test

## Implementation and execution notes

There are some dependencies on a few other packages, e.g. xUnit and Unity and Unity register-by-convention (DI).
There are xUnit tests provided which can be run in Visual Studio, e.g. by using the xUnit Visual Studio test runner. 
The application can be run from within the Visual Studio editor or by double clicking on the
ClaimsReserveCalculator.exe file in Windows (File) Explorer, after building the project in debug or release mode. 

The application depends on .NET Framework 4.7.2 and uses WPF to provide a very rudimentary GUI with two buttons 
to load an input file ("Select Input File") and to convert the source data into a cumulative claims data file 
("Convert Data"). Pressing the select input file option will result in a file dialog being displayed and the user 
can select an appropriate text file to load, which contains comma separated input data.

There are 3 example input files included in the project to be found within the 'input' subfolder of the project base directory.
These files are called 'incremental_claims_data1.txt', 'incremental_claims_data2.txt' and 'incremental_claims_data3.txt' 
It is possible to pick these files one by one and press the convert button to test out different scenarios with them.
Some information and error messages are shown on the status display, including the location of the output file.
The output file format is auto-generated from the input file name and uses the same location. The files are identical except that the output file is postfixed with
the characters '_out' just before the file extension (e.g. incremental_claims_data1_out.txt). 

There is significant scope for refactoring and extending the classes and tests if more time was available.
Data parsing and validation could be improved to cope with more edge cases, incomplete data or data with errors in it.  
Tests would also be improved and test coverage extended. Integration and end-to-end tests should be added.
Documentation should be improved e.g. methods should document all exceptions that can be thrown. 
Configurable logging of exception messages should be added, eg using NLog. 
It is assumed that this would ultimately be added hence why custom error messages are generated to be handled better later.
The classes representing product claims data, for example could benefit from being refactored and broken up into smaller pieces.
Also a number of collections and parameters could be abstracted and encapsulated into wrapper classes or structures.
Also the data item separators (delimiters) could be stored separately from the parsers and the user should be able to select the preferred separator type.

The application handles the parameters arriving in different orders of their category titles,
assuming they are consistent with the position of the following data item fields and are separated by commas. 
It is also assumed that the minimum valid year is 1500 during the data validation process and 
the parsing may be aborted if an earlier value is seen for a data e.g. for origin year.  
This was a conscious decision to have a small level of data validation.

Origin years with no claims may be left out of the input file and the application can cope with it.
I have also allowed for the scenario where there is additional white space around items. 
The string names are assumed to be the ones indicated in the assignment description
(Product, Incremental Value, Origin Year, Development Year) but they can appear in any order,
and are not case sensitive. The data fields are assumed to map onto the same position as their title in the source file.
So the order of the data is assumed to correspond to the order of the titles but that order can be in different arrangements
(from one file to the next).  Values are assumed to be zero if they are not explicitly specified, 
e.g. if a comma is followed by another comma, regardless of white space. 

