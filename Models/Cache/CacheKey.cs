using System.Collections.Generic;

namespace Models.Cache
{
    public class CacheKey
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
