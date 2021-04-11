using System.Text;

namespace WpfApp1
{
    public class User
    {
        public string name { set; get; }
        public int score { set; get; }

        public User() { }

        public User(string name)
        {
            this.name = name;
            this.score = 0;
        }

        public User(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public override string ToString()
        {
            return name.ToString() + ": " + score.ToString();
        }

        public string Welcome()
        {
            StringBuilder sb = new StringBuilder("Hello, ");
            sb.Append(this.name);
            sb.Append("!");
            return sb.ToString();
        }
    }
}
