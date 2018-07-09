using System;
using System.IO;

namespace Document_Merger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Document Merger");
            do
            {
                string doc1 = GetValidDocument();
                string doc2 = GetValidDocument();
                string mergedFileName = doc1.Substring(0, doc1.Length - 4) + doc2;
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(mergedFileName);
                    int count = WriteFileContents(writer, doc1);
                    count += WriteFileContents(writer, doc2);
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
                Console.Write("\nWould you like to merge two more documents? (y/n): ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        static string GetValidDocument()
        {
            Console.Write("Enter the name of a document: ");
            string doc;
            while ((doc = Console.ReadLine()).Length == 0 || !File.Exists(doc))
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
