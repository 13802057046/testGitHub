using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KssService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //Json全局序列化设置
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = () =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                //空值处理
                setting.NullValueHandling = NullValueHandling.Ignore;

                ////默认值处理
                //setting.DefaultValueHandling = DefaultValueHandling.Ignore;

                return setting;
            };

            //运行service
            var servicesToRun = new ServiceBase[] { new KssService() };
            if (Environment.UserInteractive)
            {
                Console.Title = "Data synchronization service console";
                Console.WriteLine("Service starting...");

                Type type = typeof(ServiceBase);
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                MethodInfo method = type.GetMethod("OnStart", flags);

                foreach (ServiceBase service in servicesToRun)
                {
                    method.Invoke(service, new object[] { args });
                }

                Console.WriteLine("Service started.");
                Console.WriteLine("Press any key to exit");

                while (!(Console.KeyAvailable))
                {
                    Thread.Sleep(1000);
                }

                if (Console.KeyAvailable)
                {
                    Console.ReadLine();
                }

                foreach (ServiceBase service in servicesToRun)
                {
                    service.Stop();
                }
                Console.WriteLine("Service shutdown.");
                Console.ReadLine();
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}