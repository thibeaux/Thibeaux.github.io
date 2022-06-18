# Senior Projects

## Overview
Going through the Computer Science program has been a tremendous help to me in my goals. It has taught me the fundamentals of programming, design, and troubleshooting in software. It has showed me many different paths available in Computer Science, and has allowed me to sample each area. As I’ve done the course work and projects in each class, I’ve built my confidence and strength in my ability to develop software. I’ve also found some areas of software development more intriguing than others.  

## Authentication Project (Zoo Dashboard)
For my senior capstone, I demonstrated 3 skills. Software Engineering/Design, Data Structures and Algorithms, and Databases. These gave me a chance to step a bit outside of my comfort zone to create something unique. I made a zoo dashboard program that was to show off my ability to design a login for a Zoo’s software system. I paired the database skill with this project because I thought it would be a nice touch to add a database to back up the login UI for the Zoo Dashboard. Implementing the database was not as easy as you may think. I had a lot of bugs, errors, and issues trying to get a SQL server database to connect using a connection string in my C# WPF application. The issue was visual studio generated the connection string for you but the format of that connection string created errors. I had to manually format the location of the database file and the insert that location in the middle of the connection string for the connection to be made from that string. Took reading many forum posts to process and debug such vague error messages, but I finally got it to work. After getting the connection established, all I had to do was query my data. 

## Online Auction Project (Binary Search Tree Prject)
For data structures and algorithms, there is a data source in a CSV file. Each row in the file was a item that was up for auction. The command line interface was to allow the auction company to review their inventory. This data structure that housed the inventory was a binary search tree. I had tom implement an in order traversal to allow the customer to see all the items inside of the list. This was much simpler than I expected but uncovered a lot of things in the process. I had a few aha moments with this project. One was with recursion. I had made the assumption that after we get out of a recursive function call, we simply stop executing and cascade all the way back to the first call. But what I did not realize was that when you leave a recursive function call, you just exit that recursive call, then move on to the next line to execute, so you can have multiple recursive function calls in the recursive function. This allowed me to explore every node that was on a right branch and left branch. Ensuring that there was no node left unturned.

## Final Thoughts
One tool that helped with making the projects done in the capstone able to collaborated on and expanded is the use of git and a remote git repository on GitHub. Throughout the course in school, I have had to analyze many of readme files. There is an art to these files. They can make a project useless or useful. Being able to communicate your ideas, designs, and implementation to others is extremely important. 

## Resources
Mijacobs, & EdKaim. (2021, May). What is Git? - azure DevOps. Azure DevOps | Microsoft Docs. Retrieved June 16, 2022, from https://docs.microsoft.com/en-us/devops/develop/git/what-is-git 

TerryGLee. (2022, April). What is WPF? - visual studio (windows). What is WPF? - Visual Studio (Windows) | Microsoft Docs. Retrieved June 16, 2022, from https://docs.microsoft.com/en-us/visualstudio/designers/getting-started-with-wpf?view=vs-2022 

Thibeaux, B. (2022, June). Brandon Thibeaux Github Page. Retrieved June 16, 2022, from https://thibeaux.github.io/ 

Thibeaux. (n.d.). Thibeaux/thibeaux.github.io. GitHub. Retrieved June 16, 2022, from https://github.com/thibeaux/thibeaux.github.io 
This reference is the github repository where you can see the source code referenced in this page.
