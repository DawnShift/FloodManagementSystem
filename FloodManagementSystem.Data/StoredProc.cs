using System;
using System.Collections.Generic;
using System.Text;

namespace FloodManagementSystem.Data
{
   public static class StoredProc
    {
        public static string GetDistributerrequests { get { return "GetRegionalRequests @regionId "; } }
        public static string GetCityRequests { get { return "GetCityRequests @cityId"; } }

    }
}
