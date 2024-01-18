using NoteTakingApp.Core;
using System;
using UIKit;

namespace NoteTakingApp.iOS
{
    public class Application
    {
        public static void Main(string[] args)
        {
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                throw;
            }
        }
    }
}
