using System.Data;
using DataLayer.Models;
using DataLayer.Utilities;

namespace DataLayer.Repository
{
    public class ApiDataType : Enumeration
    {
        public static ApiDataType TeamResult => new(nameof(Models.TeamResult), "/teams/Results");
        public static ApiDataType Match => new(nameof(Models.Match), "/matches");
        public static ApiDataType Team => new(nameof(Models.Team), "/teams/");

        public static List<ApiDataType> endpoints = new List<ApiDataType>() { TeamResult, Match, Team };

        private ApiDataType(string name, string path) : base(name, path)
        {

        }
    }
    public class ApiRepo : IRepo
    {
        public Task<IEnumerable<T>> Get<T>(Categories category)
        {
            return Task.Run(() =>
            {
                string apiUrl = null;
                foreach (var endpint in ApiDataType.endpoints)
                {
                    if (endpint.Name == typeof(T).Name)
                    {
                        apiUrl = endpint.Path;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(apiUrl))
                {
                    throw new NoNullAllowedException("No such endpint");
                }
                else
                {
                    return ApiUtils.FetchJson<IEnumerable<T>>(category.ToString() + apiUrl);
                }
            });
        }

        public Task<IEnumerable<Match>> GetMatchByFifaCode(Categories category, string fifaCode)
        {
            return Task.Run(() =>
            {
                return ApiUtils.FetchJson<IEnumerable<Match>>(category + "/matches/country?fifa_code=" + fifaCode);
            });
        }
    }
}
