using System.Reflection;
using System.Timers;
using log4net;

namespace Log4Net.Winforms.Service
{
    internal class RunServiceRun
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly object locker = new object();
        private int i;

        private Timer timer;

        public void Start()
        {
            timer = new Timer {Interval = 5000};

            timer.Elapsed += TimerElapsed;

            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (locker)
            {
                if (i % 2 == 0)
                {
                    log.Info("Timer has ticked you have the info.");
                }
                else
                {
                    log.Warn("Time has ticked you have been warned.");
                }

                i++;
            }
        }

        public void Stop()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}