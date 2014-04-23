using System;

namespace RestByDesign.Tests.TestHelpers
{
    public static class Run
    {
        private static bool AlreadyRun;
        private static readonly object padlock = new object();

        public static void Once(Action action)
        {
            var run = action;

            lock (padlock)
            {
                if (AlreadyRun)
                    return;

                run();
                AlreadyRun = true;
            }
        }
    } 
}
