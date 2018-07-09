using System;
using System.IO;
using System.Collections.Generic;

namespace Document_Merger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Document Merger");
            do
            {
                List<string> documents = new List<string>();
                while (documents.Count < 2)
                {
                    string doc = GetValidDocument();
                    if (doc.Length == 0)
                    {
                        if (documents.Count < 2)
                        {
                            Console.WriteLine("You can't merge one document, silly!");
                        }
                    }
                    else
                    {
                        documents.Add(doc);
                    }
                }
                string mergedFileName = "";
                foreach (string doc in documents)
                {
                    mergedFileName += doc.Substring(0, doc.Length - 4);
                }
                mergedFileName += ".txt";
                Console.Write("Enter new file name (default: {0}): ", mergedFileName);
                string option = Console.ReadLine();
                mergedFileName = option.Length == 0 ? mergedFileName : option;
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(mergedFileName);
                    int count = 0;
                    foreach (string doc in documents)
                    {
                        count += WriteFileContents(writer, doc);
                    }
                    Console.WriteLine("{0} was successfully saved. The document contains {1} characters", mergedFileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing to {0}: {1}", mergedFileName, e.Message);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                Console.Write("\nWould you like to merge more documents? (y/n): ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        static string GetValidDocument()
        {
            Console.Write("Enter the name of a document: ");
            string doc;
            while ((doc = Console.ReadLine()).Length > 0 && !File.Exists(doc))
            {
                Console.Write("Document not found, please enter a valid document name: ");
            }
            return doc;
        }

        static int WriteFileContents(StreamWriter writer, string file)
        {
            StreamReader reader = null;
            int count = 0;
            try
            {
                reader = new StreamReader(file);
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    count += line.Length;
                    writer.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing {0} to new file: {1}", file, e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return count;
        }
    }
}
r