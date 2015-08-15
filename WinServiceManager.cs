using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceProcess;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;

//TODO: add description of a service
//TODO: auto update service state
//TODO: create-delete a service
//TODO: show service properties

namespace WinServMgr
{
    public partial class WinServiceManager : Form
    {
        public List<ServiceEntry> mServiceEntries;
        public bool mFilterEmpty = true;
        private const string InitialFilterText = "Filter...";

        public WinServiceManager()
        {
            InitializeComponent();
        }

        private List<ServiceEntry> GetWindowsServiceEntries()
        {
            var services = ServiceController.GetServices();
            var entries = from s in services
                          select new ServiceEntry(s.ServiceName, s.Status);
            return entries.ToList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            mServiceEntries = GetWindowsServiceEntries();
            RefreshGrid();
            Text = string.Format("WinServiceManager [{0} active services]",
                mServiceEntries.Where(s => s.ServiceState != ServiceControllerStatus.Stopped).Count());
        }

        private void RefreshGrid()
        {
            // 1. If filter is not applied, we show all services, otherwise those starting with text (case independently)
            // 2. If checkbox "Show stopped services" in checked, we show them as well

            var filteredEntries = mServiceEntries.Where(s => 
                (mFilterEmpty || s.ServiceName.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase)) &&
                (tbxShowStopped.Checked || s.ServiceState != ServiceControllerStatus.Stopped))
                .ToList();
            dgvServicesList.DataSource = filteredEntries;

            AdjustColumnsWidth();
            foreach (DataGridViewRow row in dgvServicesList.Rows)
            {
                Color cellColor;
                switch ((ServiceControllerStatus)row.Cells[dgvServicesList.Columns["ServiceState"].Index].Value)
                {
                    case ServiceControllerStatus.Running:
                        cellColor = Color.Green;
                        break;
                    case ServiceControllerStatus.Stopped:
                        cellColor = Color.Red;
                        break;
                    default:
                        cellColor = Color.Gray;
                        break;
                }

                row.HeaderCell.Style.BackColor = cellColor;
                row.HeaderCell.Style.SelectionBackColor = cellColor;
            }
            dgvServicesList.Refresh();
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
                {
                    try
                    {
                        List<string> serviceNames = GetSelectedServices();
                        serviceNames.AsParallel().ForAll(name =>
                            {
                                ServiceController sc = new ServiceController(name);
                                sc.Stop();
                            });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception caught while performing controlled stop:\n" + ex.Message);
                    }
                });
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
                {
                    try
                    {
                        List<string> serviceNames = GetSelectedServices();
                        serviceNames.AsParallel().ForAll(name =>
                            {
                                ServiceController sc = new ServiceController(name);
                                sc.Start();
                            });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception caught while performing controlled start:\n" + ex.Message);
                    }
                });
        }

        private void btnRestartService_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
                {
                    try
                    {
                        List<string> serviceNames = GetSelectedServices();
                        serviceNames.AsParallel().ForAll(name =>
                            {
                                ServiceController sc = new ServiceController(name);
                                sc.Stop();
                                sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(60));
                                sc.Start();
                            });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception caught while performing restart:\n" + ex.Message);
                    }
                });
        }

        private List<string> GetSelectedServices()
        {
            var selectedServices = new List<string>();
            var selectedRows = dgvServicesList.SelectedRows;
            if (selectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    selectedServices.Add(GetServiceNameForRow(row));
                }
            }

            var selectedCells = dgvServicesList.SelectedCells;
            if (selectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in selectedCells)
                {
                    if (cell.ColumnIndex == dgvServicesList.Columns["ServiceName"].Index)
                    {
                        selectedServices.Add(cell.Value as string);
                    }
                }
            }
            return selectedServices;
        }

        private string GetServiceNameForRow(DataGridViewRow row)
        {
            return row.Cells[dgvServicesList.Columns["ServiceName"].Index].Value as string;
        }

        private void SMATestTool_Load(object sender, EventArgs e)
        {
            mServiceEntries = GetWindowsServiceEntries();
            RefreshGrid();
            Text = string.Format("WinServiceManager [{0} active services]", 
                mServiceEntries.Where(s => s.ServiceState != ServiceControllerStatus.Stopped).Count());
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == InitialFilterText)
            {
                return;
            }
            else if (txtFilter.Text == string.Empty)
            {
                mFilterEmpty = true;
            }
            else
            {
                mFilterEmpty = false;
            }
            RefreshGrid();
        }

        private void txtFilter_Enter(object sender, EventArgs e)
        {
            if (mFilterEmpty)
            {
                txtFilter.Text = "";
                txtFilter.ForeColor = Color.Black;
            }
        }

        private void txtFilter_Leave(object sender, EventArgs e)
        {
            if (mFilterEmpty)
            {
                txtFilter.Text = InitialFilterText;
                txtFilter.ForeColor = Color.Gray;
            }
        }

        private void AdjustColumnsWidth()
        {
            dgvServicesList.RowHeadersWidth = 12;
            dgvServicesList.Columns["ServiceState"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvServicesList.Columns["ServiceName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void tbxShowStopped_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void WinServiceManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                mServiceEntries = GetWindowsServiceEntries();
                RefreshGrid();
                Text = string.Format("WinServiceManager [{0} active services]",
                    mServiceEntries.Where(s => s.ServiceState != ServiceControllerStatus.Stopped).Count());
            }
        }

        private void dgvServicesList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvServicesList.CurrentCell = clickedCell;

                    string serviceName = GetServiceNameForRow(dgvServicesList.Rows[e.RowIndex]);
                    var state = mServiceEntries.First(s => s.ServiceName == serviceName).ServiceState;

                    ContextMenu cm = new ContextMenu();
                    var mi = new MenuItem("Service stop");
                    mi.Enabled = state != ServiceControllerStatus.Stopped;
                    mi.Click += btnStopService_Click;
                    cm.MenuItems.Add(mi);

                    mi = new MenuItem("Service start");
                    mi.Enabled = state != ServiceControllerStatus.Running;
                    mi.Click += btnStartService_Click;
                    cm.MenuItems.Add(mi);

                    mi = new MenuItem("Service restart");
                    mi.Enabled = state != ServiceControllerStatus.Stopped;
                    mi.Click += btnRestartService_Click;
                    cm.MenuItems.Add(mi);

                    var relativeMousePosition = dgvServicesList.PointToClient(Cursor.Position);
                    cm.Show(dgvServicesList, relativeMousePosition);
                }
            }
        }
    }

    public class ServiceEntry
    {
        [DisplayName("Service name")] 
        public string ServiceName {get; set;}

        [DisplayName("Service state")] 
        public ServiceControllerStatus ServiceState {get; set;}

        public ServiceEntry(string name, ServiceControllerStatus state)
        {
            ServiceName = name;
            ServiceState = state;
        }

        public override bool Equals(object obj)
        {
            return ServiceName == (obj as ServiceEntry).ServiceName
                && ServiceState == (obj as ServiceEntry).ServiceState;
        }

        public override int GetHashCode()
        {
            return ServiceName.GetHashCode() ^ ServiceState.GetHashCode();
        }
    }
}
