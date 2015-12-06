namespace WinServMgr
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvServicesList = new System.Windows.Forms.DataGridView();
            this.btnStopService = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnRestartService = new System.Windows.Forms.Button();
            this.tbxShowStopped = new System.Windows.Forms.CheckBox();
            this.tmrRefreshGrid = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvServicesList
            // 
            this.dgvServicesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServicesList.EnableHeadersVisualStyles = false;
            this.dgvServicesList.Location = new System.Drawing.Point(27, 60);
            this.dgvServicesList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvServicesList.Name = "dgvServicesList";
            this.dgvServicesList.Size = new System.Drawing.Size(417, 662);
            this.dgvServicesList.TabIndex = 0;
            this.dgvServicesList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvServicesList_CellMouseClick);
            // 
            // btnStopService
            // 
            this.btnStopService.Location = new System.Drawing.Point(27, 742);
            this.btnStopService.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(123, 46);
            this.btnStopService.TabIndex = 3;
            this.btnStopService.Text = "Stop selected";
            this.btnStopService.UseVisualStyleBackColor = true;
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter.Location = new System.Drawing.Point(27, 16);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(218, 22);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.Text = "Filter...";
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.Enter += new System.EventHandler(this.txtFilter_Enter);
            this.txtFilter.Leave += new System.EventHandler(this.txtFilter_Leave);
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(175, 742);
            this.btnStartService.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(123, 46);
            this.btnStartService.TabIndex = 5;
            this.btnStartService.Text = "Start selected";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // btnRestartService
            // 
            this.btnRestartService.Location = new System.Drawing.Point(321, 742);
            this.btnRestartService.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRestartService.Name = "btnRestartService";
            this.btnRestartService.Size = new System.Drawing.Size(123, 46);
            this.btnRestartService.TabIndex = 6;
            this.btnRestartService.Text = "Restart selected";
            this.btnRestartService.UseVisualStyleBackColor = true;
            this.btnRestartService.Click += new System.EventHandler(this.btnRestartService_Click);
            // 
            // tbxShowStopped
            // 
            this.tbxShowStopped.AutoSize = true;
            this.tbxShowStopped.Checked = true;
            this.tbxShowStopped.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tbxShowStopped.Location = new System.Drawing.Point(269, 16);
            this.tbxShowStopped.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxShowStopped.Name = "tbxShowStopped";
            this.tbxShowStopped.Size = new System.Drawing.Size(175, 21);
            this.tbxShowStopped.TabIndex = 7;
            this.tbxShowStopped.Text = "Show stopped services";
            this.tbxShowStopped.UseVisualStyleBackColor = true;
            this.tbxShowStopped.CheckedChanged += new System.EventHandler(this.tbxShowStopped_CheckedChanged);
            // 
            // tmrRefreshGrid
            // 
            this.tmrRefreshGrid.Interval = 200;
            this.tmrRefreshGrid.Tick += new System.EventHandler(this.tmrRefreshGrid_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 815);
            this.Controls.Add(this.tbxShowStopped);
            this.Controls.Add(this.btnRestartService);
            this.Controls.Add(this.btnStartService);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnStopService);
            this.Controls.Add(this.dgvServicesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "WinServiceManager";
            this.Load += new System.EventHandler(this.SMATestTool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServicesList;
        private System.Windows.Forms.Button btnStopService;
        //private System.Windows.Forms.DataGridViewTextBoxColumn serviceNameDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn serviceStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnRestartService;
        private System.Windows.Forms.CheckBox tbxShowStopped;
        private System.Windows.Forms.Timer tmrRefreshGrid;
    }
}

