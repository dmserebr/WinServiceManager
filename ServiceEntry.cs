using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ServiceProcess;

namespace WinServMgr
{
    public class ServiceEntry
    {
        [DisplayName("Service name")]
        public string ServiceName { get; set; }

        [DisplayName("Service state")]
        public ServiceControllerStatus ServiceState { get; set; }

        public bool StateChanged;

        public ServiceEntry(string name, ServiceControllerStatus state)
        {
            ServiceName = name;
            ServiceState = state;
            StateChanged = true;
        }

        public override bool Equals(object obj)
        {
            return ServiceName == (obj as ServiceEntry).ServiceName;
        }

        public override int GetHashCode()
        {
            return ServiceName.GetHashCode();
        }
    }
}
