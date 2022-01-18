using System;
namespace OvaCodeTest
{
    public static class GlobalSettings
    {
        public static string SystemAuthenticationEndpoint
        {
            get
            {
                return "https://devsys.slide.com.sg/";
            }
        }

        public static string POSAuthenticationEndpoint
        {
            get
            {
                return "https://devpos.slide.com.sg/";
            }
        }
    }
}
