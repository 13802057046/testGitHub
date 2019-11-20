using BaseInfo;
using System;
using System.ServiceProcess;

namespace KssService
{
    public partial class KssService : ServiceBase
    {
        System.Timers.Timer _timer1;  //计时器

        public KssService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer1 = new System.Timers.Timer();
            _timer1.Interval = Convert.ToInt32(G_INI.ReadValue("Local", "Interval")) * 60 * 1000;
            _timer1.Elapsed += timer1_Elapsed;
            _timer1.Enabled = true;

            //1.windows服务模式，因为服务启动超时时间默认是30秒，立即执行同步数据，可能会造成服务启动失败。因此服务启动后，等1个Interval再执行数据同步吧。
            //2.交互模式，主要为了Debug，可以立即同步数据。
            if (Environment.UserInteractive)
            {
                timer1_Elapsed(null, null);
            }
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //数据同步
                _timer1.Enabled = false;
                SyncData syncData = new SyncData();
                syncData.MainProcess();
                _timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.ToString(), 0);
                _timer1.Enabled = true; //系统报错后，会按照Timer的Interval再次执行。如果不想继续按照Timer的Interval再次执行，需要把这行代码注释掉。
            }
        }

        protected override void OnStop()
        {
            _timer1.Enabled = false;
        }
    }
}
