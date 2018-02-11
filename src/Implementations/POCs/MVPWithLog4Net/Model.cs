namespace MVPWithLog4Net
{
    public class Model
    {
        public string Display
        {
            get { return id + " - " + name + " / " + value; }
        }

        public string id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}