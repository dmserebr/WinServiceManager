namespace WinServMgr
{
    partial class WinServiceManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinServiceManager));
            this.dgvServicesList = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnStopService = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnRestartService = new System.Windows.Forms.Button();
            this.tbxShowStopped = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvServicesList
            // 
            this.dgvServicesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServicesList.Location = new System.Drawing.Point(22, 67);
            this.dgvServicesList.Name = "dgvServicesList";
            this.dgvServicesList.Size = new System.Drawing.Size(299, 410);
            this.dgvServicesList.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(215, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(106, 27);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh (F5)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnStopService
            // 
            this.btnStopService.Location = new System.Drawing.Point(23, 490);
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(89, 31);
            this.btnStopService.TabIndex = 3;
            this.btnStopService.Text = "Stop service";
            this.btnStopService.UseVisualStyleBackColor = true;
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter.Location = new System.Drawing.Point(22, 13);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(165, 20);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.Text = "Filter...";
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.Enter += new System.EventHandler(this.txtFilter_Enter);
            this.txtFilter.Leave += new System.EventHandler(this.txtFilter_Leave);
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(128, 490);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(89, 31);
            this.btnStartService.TabIndex = 5;
            this.btnStartService.Text = "Start service";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // btnRestartService
            // 
            this.btnRestartService.Location = new System.Drawing.Point(232, 490);
            this.btnRestartService.Name = "btnRestartService";
            this.btnRestartService.Size = new System.Drawing.Size(89, 31);
            this.btnRestartService.TabIndex = 6;
            this.btnRestartService.Text = "Restart service";
            this.btnRestartService.UseVisualStyleBackColor = true;
            this.btnRestartService.Click += new System.EventHandler(this.btnRestartService_Click);
            // 
            // tbxShowStopped
            // 
            this.tbxShowStopped.AutoSize = true;
            this.tbxShowStopped.Location = new System.Drawing.Point(22, 40);
            this.tbxShowStopped.Name = "tbxShowStopped";
            this.tbxShowStopped.Size = new System.Drawing.Size(136, 17);
            this.tbxShowStopped.TabIndex = 7;
            this.tbxShowStopped.Text = "Show stopped services";
            this.tbxShowStopped.UseVisualStyleBackColor = true;
            this.tbxShowStopped.CheckedChanged += new System.EventHandler(this.tbxShowStopped_CheckedChanged);
            // 
            // WinServiceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 537);
            this.Controls.Add(this.tbxShowStopped);
            this.Controls.Add(this.btnRestartService);
            this.Controls.Add(this.btnStartService);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnStopService);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvServicesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "WinServiceManager";
            this.Text = "WinServiceManager";
            this.Load += new System.EventHandler(this.SMATestTool_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WinServiceManager_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServicesList;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnStopService;
        //private System.Windows.Forms.DataGridViewTextBoxColumn serviceNameDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn serviceStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnRestartService;
        private System.Windows.Forms.CheckBox tbxShowStopped;
    }
}

