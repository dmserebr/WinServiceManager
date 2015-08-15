using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace WinServMgr
{
    public class SrvController
    {
        private MainForm mMainForm;

        public SrvController(MainForm mainForm)
        {
            ServiceEntries = new List<ServiceEntry>();
            mMainForm = mainForm;
        }

        public List<ServiceEntry> ServiceEntries { get; set; }

        public void UpdateServiceEntries()
        {
            ServiceEntries.Clear();
            var services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                ServiceEntries.Add(new ServiceEntry(sc.ServiceName, sc.Status));
            }
        }

        public void PerformForSelected(Action<string> action, string actionName)
        {
            Task task = new Task(() =>
            {
                List<string> serviceNames = mMainForm.GetSelectedServices();
                serviceNames.AsParallel().ForAll(name =>
                    {
                        try
                        {
                            action(name);
                        }
                        catch (Exception ex)
                        {
                            MainForm.ShowError(string.Format("Exception caught while performing {0} of {1}:\n{2}", actionName, name, ex.Message));
                        }
                    });
            });
            task.Start();
        }
    }
}
