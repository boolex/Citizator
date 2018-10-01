namespace Citizator.Model
{
    public class Place
    {
        private readonly string name;
        public Place(
            string name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return name; }
        }
    }
}
