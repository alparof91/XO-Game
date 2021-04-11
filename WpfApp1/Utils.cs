using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace WpfApp1
{
    public class MyComparer : IComparer<User>
    {
        public int Compare(User x, User y)
        {
            return y.score.CompareTo(x.score);
        }
    }

    public class Utils
    {
        public static bool CheckTextBox(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return false;
            }
            else
                return true;
        }

        public static string GetButtonName(int i, int j, int dim)
        {
            int name = i * dim + j;
            StringBuilder sb = new StringBuilder("_");
            sb.Append(name.ToString());
            return sb.ToString();
        }

        public static int GetButtonPosition(string name)
        {
            string temp = "";
            for (int i = 1; i < name.Length; i++)
                temp += name[i];
            return int.Parse(temp);
        }

        public static HighScore ReadHighScoreFromXML(string path)
        {
            HighScore highScore = new HighScore();

            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-read-object-data-from-an-xml-file
            System.Xml.Serialization.XmlSerializer reader =
        new System.Xml.Serialization.XmlSerializer(typeof(HighScore));
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                highScore = (HighScore)reader.Deserialize(file);
                file.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(
                    "{0}: The write operation could not " +
                    "be performed because the specified " +
                    "part of the file is locked.",
                    e.GetType().Name);
            }
            return highScore;
        }

        private static bool UpdateUserInUserList(User user, List<User> list)
        {
            foreach (var item in list)
            {
                if (user.name.Equals(item.name))
                {
                    item.score = user.score;
                    return true;
                }
            }
            return false;
        }

        public static void WriteToXML(HighScore highScore, string path)
        {
            highScore = SortHighScore(highScore);
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-write-object-data-to-an-xml-file
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(HighScore));

            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, highScore);
            file.Close();
        }

        public static void WriteResultsToXML(User user, string path)
        {
            HighScore highScore = ReadHighScoreFromXML(path);
            if (UpdateUserInUserList(user, highScore.users))
                Console.WriteLine("User score updated!");
            else
                highScore.users.Add(user);
            WriteToXML(highScore, path);
        }

        public static HighScore SortHighScore(HighScore highScore)
        {
            IComparer<User> comparer = new MyComparer();
            highScore.users.Sort(comparer);
            return highScore;
        }
    }
}
