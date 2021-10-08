using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure
{
    public class EnvironmentBlackListChecker : IBlackListChecker
    {
        private readonly string[] blackList;

        public EnvironmentBlackListChecker(string key = "URL_BLACKLIST")
        {
            string settingsValue = Environment.GetEnvironmentVariable(key);

            blackList = settingsValue != null ?
                settingsValue.Split(',') : Array.Empty<string>();
        }

        public Task<bool> Check(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return Task.FromResult(
                blackList.Any() ? blackList.Contains(value) : true);
        }
    }
}