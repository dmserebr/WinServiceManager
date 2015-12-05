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
    public partial class MainForm : Form
    {
        #region Private fields

        private bool mFilterEmpty = true;
        private SrvController mSrvController;
        private const string InitialFilterText = "Filter...";
        private BindingList<ServiceEntry> servicesList;
        private BindingSource servicesListBindingSource;

        
        #endregion

        #region Construction

        public MainForm()
        {
            InitializeComponent();
            mSrvController = new SrvController(this);
            servicesList = new BindingList<ServiceEntry>();
            servicesListBindingSource = new BindingSource(servicesList, null);
            dgvServicesList.DataSource = servicesListBindingSource;
            dgvServicesList.RowHeadersWidth = 12;
            dgvServicesList.Columns["ServiceState"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvServicesList.Columns["ServiceName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        } 

        #endregion

        #region Public members

        /// <summary>
        /// Gets list of services that are selected in the grid view.
        /// The service is considered as selected if:
        /// 1. cell containing its name is selected
        /// 2. row of service is selected
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedServices()
        {
            var selectedServices = new List<string>();

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Exception caught while getting selected services:\n" + ex.Message);
            }

            return selectedServices;
        }

        /// <summary>
        /// Shows a message box with the specified error message
        /// </summary>
        /// <param name="message">Message to be shown</param>
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Private members
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            mSrvController.UpdateServiceEntries();

            if (RefreshGrid())
            {
                UpdateFormTitle();
            }
        }

        private bool RefreshGrid()
        {
            // 1. If filter is not applied, we show all services, otherwise those starting with text (case independently)
            // 2. If checkbox "Show stopped services" in checked, we show them as well

            var filteredEntries = mSrvController.ServiceEntries.Where(s =>
                (mFilterEmpty || s.ServiceName.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase)) &&
                (tbxShowStopped.Checked || s.ServiceState != ServiceControllerStatus.Stopped))
                .ToList();

            var entriesToRemove = servicesList.Except(filteredEntries).ToList();
            foreach (var entry in entriesToRemove)
            {
                servicesList.Remove(entry);
            }

            var newEntries = filteredEntries.Except(servicesList).ToList();
            foreach (var entry in newEntries)
            {
                // Insert an entry to its place so the binding list remains alphabetically sorted
                if (servicesList.Count == 0)
                {
                    servicesList.Add(entry);
                }
                else
                {
                    var elementToInsertBefore = servicesList.FirstOrDefault(e => e.ServiceName.CompareTo(entry.ServiceName) > 0);
                    if (elementToInsertBefore != null)
                    {
                        servicesList.Insert(servicesList.IndexOf(elementToInsertBefore), entry);
                    }
                    else
                    {
                        servicesList.Add(entry);
                    }
                }
            }

            UpdateStatusColors(newEntries);

            return entriesToRemove.Any() || newEntries.Any();  // true if something has changed
        }

        private void UpdateStatusColors(IEnumerable<ServiceEntry> changedEntries)
        {
            foreach (ServiceEntry entry in changedEntries)
            {
                DataGridViewRow row = dgvServicesList.Rows.Cast<DataGridViewRow>().First(r =>
                                        r.Cells["ServiceName"].Value.ToString() == entry.ServiceName);
                Color cellColor;
                switch (entry.ServiceState)
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
        }

        private void UpdateFormTitle()
        {
            Text = string.Format("WinServiceManager [{0} active services]",
                        mSrvController.ServiceEntries.Where(s => s.ServiceState != ServiceControllerStatus.Stopped).Count());
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            mSrvController.PerformForSelected(name =>
            {
                ServiceController sc = new ServiceController(name);
                sc.Stop();
            }, "controlled stop");
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            mSrvController.PerformForSelected(name =>
            {
                ServiceController sc = new ServiceController(name);
                sc.Start();
            }, "controlled start");
        }

        private void btnRestartService_Click(object sender, EventArgs e)
        {
            mSrvController.PerformForSelected(name =>
            {
                ServiceController sc = new ServiceController(name);
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(60));
                sc.Start();
            }, "controlled restart");
        }

        private string GetServiceNameForRow(DataGridViewRow row)
        {
            return row.Cells["ServiceName"].Value as string;
        }

        private void SMATestTool_Load(object sender, EventArgs e)
        {
            mSrvController.UpdateServiceEntries();
            if (RefreshGrid())
            {
                UpdateFormTitle();
            }
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

        private void tbxShowStopped_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void WinServiceManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                mSrvController.UpdateServiceEntries();
                if (RefreshGrid())
                {
                    UpdateFormTitle();
                }
            }
        }

        private void dgvServicesList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // -1 means header row / column
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvServicesList.CurrentCell = clickedCell;

                    string serviceName = GetServiceNameForRow(dgvServicesList.Rows[e.RowIndex]);
                    var state = mSrvController.ServiceEntries.First(s => s.ServiceName == serviceName).ServiceState;

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

        #endregion
    }
}
