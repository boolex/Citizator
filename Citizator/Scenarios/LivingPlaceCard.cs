namespace Citizator.Scenarios
{
    public class LivingPlaceCard
    {
        private readonly string key;
        private readonly string place;
        public LivingPlaceCard(string key, string place)
        {
            this.key = key;
            this.place = place;
        }
        public string Name
        {
            get { return "Place name"; }
        }
    }
}
