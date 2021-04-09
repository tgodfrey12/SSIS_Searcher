using System;
using System.IO;
using System.Collections.Generic;

namespace SSIS_Searcher
{
    class Program
    {
        //string SSISPackagePath = @"C:\PTS_Workspace\PTSApplication\SSIS_Packages";     //SSIS packages file location
        string SSISPackagePath = @"C:\PTS_Workspace\PTSApplication\SSRS_Packages";   //SSRS reports file location
        string tableListDoc = @"C:\Users\tgod904\source\repos\SSIS_Searcher\files\table_list.txt";


        List<string> tableNameList;

        static void Main(string[] args)
        {

            //Console.WriteLine("Hello World!");
            Program p = new Program();
            p.getStringsToSearchFor();
            p.SSISPackageLoop();
            
        }

        //Create a list of SSIS packages in a given
        //location.  Put them in a list of strings
        public void SSISPackageLoop()
        {
            //string[] files = System.IO.Directory.GetFiles(SSISPackagePath, "*.dtsx");   //SSIS packages
            string[] files = System.IO.Directory.GetFiles(SSISPackagePath, "*.rdl");    //SSRS reports
            string fullOutputPath = @"C:\Users\tgod904\source\repos\SSIS_Searcher\files\output.txt";
            StreamWriter writer = new StreamWriter(fullOutputPath);

            foreach (string file in files)
            {
                //Call the method that will search the file for all strings
                foreach (var strToSearchFor in tableNameList)
                {
                    foreach (var line in File.ReadAllLines(file))
                    {
                        if(line.Contains("strToSearchFor"))
                        {
                            //write out to a log - line was found in file
                            using ( writer )
                            {
                                writer.WriteLine(strToSearchFor + " was found in " + file);
                            }
                        }
                    }

                    
                }
                Console.WriteLine("Done searching " + file);

            }

        }


        //Gather a list of all the table names we want to search all the SSIS packages for
        public void getStringsToSearchFor()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(tableListDoc);
            string line = string.Empty;
            tableNameList = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                tableNameList.Add(line);
            }
        }


    }
}
