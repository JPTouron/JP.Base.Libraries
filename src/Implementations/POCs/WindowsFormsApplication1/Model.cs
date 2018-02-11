namespace WindowsFormsApplication1
{
    public class Model
    {
        public string Display
        {
            get { return id + " - " + name + " / " + value; }
        }

        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}