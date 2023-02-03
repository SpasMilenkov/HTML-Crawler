# **HTML Crawler**
## Purpose
This program was created for educational purposes only as a course work for my "Algorithms and Data Structures" course. The program was created without using any external libraries or any of the integrated data structures. All string editing methods like (split) were implemented manually by me. All data structures used in the project apart from the standard array were implemented manually. This project was used to show understanding of common algorithms and data structures studied as part of this course.
## Features
- Loading HTML documents from file and creating a general tree in system memory
- Enabling the user to edit, copy and paste nodes and save the edited document as an HTML document
- Enable the user to visualize the document using the integrated picture box window
## Supported commands
### The sample HTML document used in the examples is in sample.html
- ### PRINT

	Prints part of the document or the entire document
  
	Example:
  
	 PRINT "//" -> prints the entire document
   
	 PRINT "//html/body/p" -> prints \<p> Text1 \</p> \<p> Text2 \</p> \<p id='p3'> Text3 \</p>
   
	 PRINT "//html/body/table/tr/td" -> prints \<td> 11 \</td> \<td> 22 \</td> \<td> 33 \</td> \<td> 44 \</td>
   
	 PRINT "//html/body/p[2]" -> prints \<p> Text2 \</p>
   
	 PRINT "//html/body/div/*" -> prints \<div>Text4\</div>\<p>Text5\</p>
   
	 PRINT "//html/body/div" -> prints \<div>\<div>Text4\</div>\<p>Text5\</p>\</div>
   
	 PRINT "//html/body/p[@id='p3']" -> prints \<p id='p3'> Text3 \</p>
   
	 PRINT "//html/body/table[@id='table2']/tr[2]/td" -> prints \<td> 22 \</td> 
   
- ### SET

	Replace the content of a node with what the user has entered
  
	Example:
  
	 SET "//html/body/p" "AAA" -> \<p> AAA \</p> \<p> AAA \</p> \<p id='p3'> AAA \</p>
   
	 SET "//html/body/div/div" "\<b>Text4\</b>" -> \<div>\<b>Text4\</b>\</div>
- ### COPY

	Copies one node to another
  Example:
  
	COPY "//html/body/div/div" "//html/body/table[@id='table2']/tr[2]/td"

- ### SAVE

	Saves the tree as an HTML document
- ### VISUALIZE

	Renders the content of the document using a BMP image and System.Drawing
## Limitations
- The HTML document should start with the \<html> tag
- The crawler does not support \<head>, <!DOCTYPE> or comments 
- The crawler does not support script tags and style tags
## Tech Stack
The HTML Crawler is written entirely in the C# programming language on .net 6. Windows Form is used for the user interface. 

![Main look of the program](https://github.com/Clash2453/HTML-Crawler/blob/main/HTML-Crawler/README%20Source/MainLook.PNG)
