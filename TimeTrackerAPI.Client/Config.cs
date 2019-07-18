using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackerAPI.Client
{
    public class Config
    {

        public const string ApiRootUrl = "https://localhost:44362/";

        public const string ApiResourceUrl = ApiRootUrl + "api/";

        public const string TokenUrl = ApiRootUrl + "get-token";

    }
}
