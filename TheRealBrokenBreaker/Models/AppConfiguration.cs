using System;

namespace TheRealBrokenBreaker.Models
{
    class AppConfiguration
    {        
        public static string LinkFile
        {
            get
            {
                return Environment.GetEnvironmentVariable("TBB_LinkFile");
            }
            private set { }
        }
        public static string BrokenLinksFile
        {
            get
            {
                BrokenLinksFile = Environment.GetEnvironmentVariable("TBB_BrokenLinksFile");
                return BrokenLinksFile;
            }
            private set { }
        }
        public static string LogFile
        {
            get
            {
                LogFile = Environment.GetEnvironmentVariable("TBB_LogFile");
                return LogFile;
            }
            private set  { }
        }
        public static string JSONConfigs
        {
            get
            {
                JSONConfigs = Environment.GetEnvironmentVariable("TBB_JSONConfigs");
                return JSONConfigs;
            }
            private set { }
        }
    }
}
