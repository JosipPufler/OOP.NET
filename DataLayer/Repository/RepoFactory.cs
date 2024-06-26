using DataLayer.Utilities;

namespace DataLayer.Repository
{
    public static class RepoFactory
    {
        private static IRepo instance = makeInstance();

        public static IRepo getInstance()
        {
            return instance;
        }

        private static IRepo makeInstance()
        {
            IRepo instance;
            switch (ConfigService.type)
            {
                case "file":
                    instance = new FileRepo();
                    break;
                case "api":
                    instance = new ApiRepo();
                    break;
                default:
                    throw new Exception("Unknown repo type");
            }
            return instance;
        }
    }
}
