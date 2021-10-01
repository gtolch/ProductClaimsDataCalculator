# Software Engineer Technical Test

## Purpose and Instructions

### Purpose

As it is difficult to fully assess somebody’s abilities at an interview, particularly their programming skills, we give a small programming exercise to all potential recruits. The problem is a fairly simple one, which should be completed in the applicant’s own time.

This is an opportunity for the candidate to show their engineering knowledge and craft work.  We are looking for engineering best practice. The test will be scored accordingly. 

This is a very important part of our hiring process. Therefore we recommend that candidates give this adequate consideration and address this task as they would do for any other professional assignments in their current workplace.

### Instructions 

-   Programming language: Use any object-oriented language, however our preference is C#.  
-   We use GitHub to host these tests to create a modern practical engineering experience. Please complete this exercise and create a pull request containing your solution 
-   We recommend you create a development branch for your development and from this create a final pull request to the master branch for review
-   We would much prefer that you submit a complete software implementation that demonstrates modern engineering best practices.  However we also appreciate that to provide a complete solution we may be expecting too much of the candidates time. If you are pressed for time, we recommend you use the pull request to comment on areas that, given more time, you would address or have done differently. 
-   Time or other constraints: We are looking for a solution that is simple and well-structured. The suggested duration of this exercise is around 2-4 hours.  
-   Please update the RUNME.MD file with instructions how to run your application 

## Exercise

### Background

One of the major jobs that we do is claims reserving, which involves assessing how much money is likely to be paid in future in respect of the claims that arise from a set of insurance policies that have already been taken out. The statistical analysis of this is based around data triangles of payment figures for the sum of previous claims.
 
A very simple example triangle might look like this (the triangle is the numbers in central grid):

	                            ╔═════════════════╗
	  Incremental Claims Data   ║ Development Year║
	                            ║─────┬─────┬─────║
	                            ║  1  │  2  │  3  ║
	╔═══════════════════════════╬═════════════════╣
	║                │   1995   ║ 100 │  50 │ 200 ║ 
	║                │──────────║─────┼─────┼─────║ 
	║   Origin Year  │   1996   ║  80 │  40 │  ?  ║
	║                │──────────║─────┼─────┼─────║ 
	║                │   1997   ║ 120 │  ?  │  ?  ║ 
	╚═══════════════════════════╩═════════════════╝
	
The numbers in the triangle are amounts of payments paid out for claims on a particular block of insurance policies.  
The block of claims is be broken into origin years. For example, all claims originally happening in 1996 will go into the 1996 row of the triangle.  
For each origin year, we will see the development of payments over time as settlements are made.  
In the example above we see that payments of 100 were made in respect or origin year 1995 during the year 1995, 50 during the year 1996 (development year 2) and 200 during 1997 (development year 3).  
Summing the values in an origin year gives the total claim payments. In the example above, at the end of 1997 the total of claim payments in respect of origin year 1995 is 350 = 100 + 50 + 200.

The purpose of the triangle is to try to estimate what numbers should go in the empty boxes (the ones with question marks in them) by looking at the patterns in the triangle – the numbers in the empty boxes are the amounts we expect to have to pay in the future in respect of the claims that occurred in 1996 and 1997.

To do this in practice, it is easier to spot patterns if we 'accumulate' the data in the triangle. Showing the total amount paid in all development years inclusively achives this. 

The accumulated version of the above triangle is:

	                            ╔═══════════════════════════════════════╗
	  Cumulative  Claims Data   ║           Development Year            ║
	                            ║─────┬────────────────┬────────────────║
	                            ║  1  │        2       │        3       ║
	╔═══════════════════════════╬═══════════════════════════════════════╣
	║                │   1995   ║ 100 │  150 (=100+50) │ 350 (=150+200) ║ 
	║                │──────────║─────┼────────────────┼────────────────║ 
	║   Origin Year  │   1996   ║  80 │  120 (=80+40)  │        ?       ║
	║                │──────────║─────┼────────────────┼────────────────║ 
	║                │   1997   ║ 120 │        ?       │        ?       ║ 
	╚═══════════════════════════╩═══════════════════════════════════════╝

### The Problem

You are required to create a program to read in incremental claims data from a comma-separated text file, to accumulate the data and output the results to a different comma-separated text file. This process represents the creation of the cumulative data triangle from the incremental data triangle above.

#### Example Input Data

A short input file might contain the following:

	Product, Origin Year, Development Year, Incremental Value
	Comp, 1992, 1992, 110.0
	Comp, 1992, 1993, 170.0
	Comp, 1993, 1993, 200.0
	Non-Comp, 1990, 1990, 45.2
	Non-Comp, 1990, 1991, 64.8
	Non-Comp, 1990, 1993, 37.0
	Non-Comp, 1991, 1991, 50.0	
	Non-Comp, 1991, 1992, 75.0
	Non-Comp, 1991, 1993, 25.0
	Non-Comp, 1992, 1992, 55.0
	Non-Comp, 1992, 1993, 85.0
	Non-Comp, 1993, 1993, 100.0
	
This example file contains two triangles – one for a product called 'Comp' and one for a product called 'Non-Comp'.  
The first row contains column headings, and the subsequent rows contain the data. For example, accidents occurring on the Non-Comp product in 1990, 45.2 was paid in 1990, 64.8 was paid in 1991 and 37 was paid in 1993.

#### Example Output Data

The output file corresponding to the above input file would be:

	1990, 4
	Comp, 0, 0, 0, 0, 0, 0, 0, 110, 280, 200
	Non-Comp, 45.2, 110, 110, 147, 50, 125, 150, 55, 140, 100

The first line gives the earliest origin year (i.e. 1990) and the number of development years (in this case ranging from 1990 through to 1993 i.e. 4).  
Subsequently there is a line for each triangle. The first field in the line gives the name of the product. The subsequent fields are the accumulated triangle values.

### General Considerations

Below are some hints to help with this exercise. 
-   Demonstrate engineering best practice 
-   Use the pull request to provide a 'self review' to highlight any assumptions or potential future refactoring
-   The data may well contain more than two triangles and there may be many more than four origin years
-   The example input data file given is very ‘clean’. In practice data files may contain errors or the data may be in an unexpected format
-   Incremental values in the input data may have been left out of the input file if they are zero. Origin years with no claims may have been left out of the input file

### Implementation and execution notes

There are some dependencies on a few other packages, e.g. xUnit and Unity and Unity register-by-convention (DI).
There are xUnit tests provided which can be run in Visual Studio, e.g. by using the xUnit Visual Studio test runner. 
The application can be run from within the Visual Studio editor or by double clicking on the
ClaimsReserveCalculator.exe file in Windows (File) Explorer after building the project in debug or release mode. 

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
Tests would also be improved and test coverage extended.
The classes representing product claims data, for example could benefit from being refactored and broken up into smaller pieces.
Also a number of collections and parameters could be abstracted and encapsulated into wrapper classes or structures.
Also the data item separators (delimiters) could be stored separately from the parsers and the user should be able to select the preferred separator type.
It would be easy to implement but was not considered a core aspect of the system.

The application handles the parameters arriving in different orders of their category titles,
assuming they are consistent with the position of the following data item fields. 
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

