using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Log4Net.Winforms.Service
{
    public partial class ServiceTest : Form
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Timer timer = new Timer();
        private readonly RunServiceRun serviceRun;
        private CustomMemoryAppender appender;

        public ServiceTest()
        {
            InitializeComponent();
            serviceRun = new RunServiceRun();
            timer.Interval = 10000;
            timer.Tick+=timer_Tick;
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            XmlConfigurator.Configure();

            serviceRun.Start();
            appender = new CustomMemoryAppender();
            //Get the logger repository hierarchy.  
            var logRepository = (Hierarchy)LogManager.GetRepository();

            //and add the appender to the root level  
            //of the logging hierarchy  
            logRepository.Root.AddAppender(appender);

            //configure the logging at the root.  
            logRepository.Root.Level = Level.All;

            //mark repository as configured and  
            //notify that is has changed.  
            logRepository.Configured = true;
            logRepository.RaiseConfigurationChanged(EventArgs.Empty);  
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Debug.WriteLine("Service Stop Message Recieved");
            timer.Stop();
            
            serviceRun.Stop();

            base.OnClosing(e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
                string text = appender.ReadBuffer();
                if (!string.IsNullOrEmpty(text))
                {
                    textbox.AppendText(text);
                }

/*            LoggingEvent[] events = appender.GetEvents();

            foreach (LoggingEvent loggingEvent in events)
            {
                textbox.Text += loggingEvent.RenderedMessage;
            }*/
        }

        public class CustomMemoryAppender : AppenderSkeleton
        {
            private readonly StringBuilder logBuffer;
            readonly StringWriter stringWriter;
            private readonly object lockObject = new object();

            public CustomMemoryAppender()
            {
                logBuffer = new StringBuilder();
                stringWriter = new StringWriter(logBuffer);

            }

            protected override void Append(LoggingEvent loggingEvent)
            {
                lock (lockObject)
                {
                    if (Layout == null)
                    {
                        logBuffer.AppendLine(string.Format("{0} - {1}", loggingEvent.Level, loggingEvent.RenderedMessage));
                    }
                    else
                    {
                        Layout.Format(stringWriter, loggingEvent);
                    }
                }
            }

            public string ReadBuffer()
            {
                lock (lockObject)
                {
                    string retVal = logBuffer.ToString();
                    logBuffer.Clear();
                    return retVal;
                }
            }
        }


    }
}