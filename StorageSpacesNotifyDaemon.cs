using System;
using System.Management;
using System.ServiceProcess;
using System.Timers;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace StorageSpacesNotifyDaemon
{
    public partial class StorageSpacesNotifyDaemon : ServiceBase
    {
        private Timer timer;

        public StorageSpacesNotifyDaemon()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 60000; // Check every minute
            timer.Elapsed += CheckStoragePoolHealth;
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void CheckStoragePoolHealth(object sender, ElapsedEventArgs e)
        {
            string NamespacePath = "\\\\.\\ROOT\\Microsoft\\Windows\\Storage";
            string ClassName = "MSFT_StoragePool";

            ManagementClass oClass = new ManagementClass(NamespacePath + ":" + ClassName);

            foreach (ManagementObject oObject in oClass.GetInstances())
            {
                UInt16 healthStatus = (UInt16)oObject["HealthStatus"];

                if (healthStatus == 2) // Unhealthy status
                {
                    ShowNotification();
                }
            }
        }

        private void ShowNotification()
        {
            string xml = $@"
                <toast activationType='protocol' launch='ms-settings:storagespaces'>
                    <visual>
                        <binding template='ToastGeneric'>
                            <text>Storage Pool Warning</text>
                            <text>The storage pool is unhealthy. Click to open Storage Spaces.</text>
                        </binding>
                    </visual>
                </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier("StorageSpacesNotifyDaemon").Show(toast);
        }
    }
}
