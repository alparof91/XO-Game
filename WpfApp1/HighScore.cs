using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    

    public class HighScore
    {
        //private string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/";
        private string path = "scores.xml";
        public List<User> users { set; get; }

        public HighScore()
        {

            this.users = new List<User>();
        }

        public HighScore(List<User> users)
        {
            this.users = users;
        }

        public void SetPath(string path)
        {
            this.path = path;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("file path: ");
            sb.Append(path);
            sb.Append("\n");
            foreach (var item in users)
            {
                sb.Append(item);
                sb.Append("\n");
            }
            return sb.ToString();
        }

        
    }
}
