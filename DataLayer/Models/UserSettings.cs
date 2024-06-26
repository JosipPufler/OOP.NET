namespace DataLayer.Models
{
    public class UserSettings
    {
        public Categories category;
        public string categoryName;
        public Languages language;
        public string languageName;
        public Resolutions? WpfResolution;
        public string? favCountry;
        public IList<Player>? favPlayers = new List<Player>();
    }
}
