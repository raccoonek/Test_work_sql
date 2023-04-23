using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using System;
using Microsoft.Win32;


namespace ConsoleApp_test_work
{
    internal class Program
    {
        const string UnwantedSigns = ".,?!-/*”“'\"";
        static void Main()
        {
            try
            {
                Console.WriteLine("Напишите полный путь к текстовому файлу:");
                string check_path = Console.ReadLine();

                string path = check_path;
                get_fail(path);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void get_fail(string path)
        {
            try
            {
                string FileContents = File.ReadAllText(path);
                foreach (char c in UnwantedSigns)
                {
                    FileContents = FileContents.Replace(c, ' ');
                    GC.Collect();
                }
                FileContents = FileContents.Replace(Environment.NewLine, " ");
                string[] Words = FileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                FileContents = string.Empty;
                GC.Collect();
                Dictionary<string, int> WordCounts = new Dictionary<string, int>();
                foreach (string w in Words.Select(x => x.ToLower()))
                    if (WordCounts.TryGetValue(w, out int c))
                        WordCounts[w] = c + 1;
                    else
                        WordCounts.Add(w, 1);
                Words = new string[1];
                GC.Collect();
                List<Tuple<int, string>> WordStats = WordCounts.Select(x => new Tuple<int, string>(x.Value, x.Key)).ToList();
                WordStats.Sort((x, y) => y.Item1.CompareTo(x.Item1));
                foreach (Tuple<int, string> t in WordStats)
                    File.AppendAllText(path + ".stats.txt", t.Item2 + " " + t.Item1 + Environment.NewLine);
                Console.ReadLine();
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine("Ошибка подключения введите путь заново.");
                Main();
            }
        }
    }
}