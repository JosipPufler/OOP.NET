using DataLayer.Models;
using DataLayer.Utilities;

namespace DataLayer.Repository
{
    class FileDataType : Enumeration
    {
        public static FileDataType TeamResult => new(nameof(Models.TeamResult), @"\results.json");
        public static FileDataType Match => new(nameof(Models.Match), @"\matches.json");
        public static FileDataType Team => new(nameof(Models.Team), @"\teams.json");

        public static List<FileDataType> Files => new List<FileDataType> { TeamResult, Match, Team };

        private FileDataType(string name, string path) : base(name, path) { }
    }

    public class FileRepo : IRepo
    {
        public Task<IEnumerable<T>> Get<T>(Categories category)
        {
            return Task.Run(() =>
            {
                string filePath = null;
                foreach (var type in FileDataType.Files)
                {
                    if (type.Name == typeof(T).Name)
                    {
                        filePath = type.Path;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new NullReferenceException("No such data type");
                }
                else
                {
                    return FileUtils.ReadJson<IEnumerable<T>>(ConfigService.filePath + "\\" + category + filePath);
                }
            });
        }

        public async Task<IEnumerable<Match>> GetMatchByFifaCode(Categories category, string fifaCode)
        {
            IEnumerable<Match> allMatches = await Get<Match>(category);
            IList<Match> matches = new List<Match>();
            foreach (Match match in allMatches)
            {
                if (match.AwayTeam.Code == fifaCode || match.HomeTeam.Code == fifaCode)
                {
                    matches.Add(match);
                }
            }
            if (matches.Count > 0)
            {
                return matches;
            }
            throw new Exception($"No team with code {fifaCode}");

        }
    }
}
