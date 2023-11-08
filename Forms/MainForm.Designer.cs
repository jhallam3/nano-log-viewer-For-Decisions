using System.Windows.Forms;

namespace NanoLogViewer.Forms
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.openLogFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.btSelectSourceFile = new System.Windows.Forms.Button();
            this.lvLogLines = new System.Windows.Forms.ListView();
            this.btUpdate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wbDetails = new System.Windows.Forms.WebBrowser();
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.cbTailMB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btClearFilter = new System.Windows.Forms.Button();
            this.timerUpdateAfterFilterChange = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbFilter
            // 
            this.cbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(55, 45);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(841, 23);
            this.cbFilter.TabIndex = 22;
            this.toolTip.SetToolTip(this.cbFilter, "Any text to search and filter");
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            this.cbFilter.TextUpdate += new System.EventHandler(this.cbFilter_TextUpdate);
            // 
            // openLogFileDialog
            // 
            this.openLogFileDialog.Filter = "Log files (*.log)|*.log";
            this.openLogFileDialog.Title = "Select a log file to open";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(13, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Source";
            // 
            // btSelectSourceFile
            // 
            this.btSelectSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectSourceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSelectSourceFile.Location = new System.Drawing.Point(708, 13);
            this.btSelectSourceFile.Name = "btSelectSourceFile";
            this.btSelectSourceFile.Size = new System.Drawing.Size(75, 23);
            this.btSelectSourceFile.TabIndex = 13;
            this.btSelectSourceFile.Text = "Browse...";
            this.btSelectSourceFile.UseVisualStyleBackColor = true;
            this.btSelectSourceFile.Click += new System.EventHandler(this.btSelectSourceFile_Click);
            // 
            // lvLogLines
            // 
            this.lvLogLines.AllowColumnReorder = true;
            this.lvLogLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogLines.FullRowSelect = true;
            this.lvLogLines.GridLines = true;
            this.lvLogLines.HideSelection = false;
            this.lvLogLines.Location = new System.Drawing.Point(0, 0);
            this.lvLogLines.MultiSelect = false;
            this.lvLogLines.Name = "lvLogLines";
            this.lvLogLines.Size = new System.Drawing.Size(944, 180);
            this.lvLogLines.TabIndex = 16;
            this.lvLogLines.UseCompatibleStateImageBehavior = false;
            this.lvLogLines.View = System.Windows.Forms.View.Details;
            this.lvLogLines.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.LvLogLines_ColumnReordered);
            this.lvLogLines.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.LvLogLines_ColumnWidthChanged);
            this.lvLogLines.SelectedIndexChanged += new System.EventHandler(this.lvLogLines_SelectedIndexChanged);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btUpdate.Location = new System.Drawing.Point(789, 7);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(75, 33);
            this.btUpdate.TabIndex = 17;
            this.btUpdate.Text = "UPDATE";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 74);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvLogLines);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wbDetails);
            this.splitContainer1.Size = new System.Drawing.Size(944, 361);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 18;
            // 
            // wbDetails
            // 
            this.wbDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbDetails.Location = new System.Drawing.Point(0, 0);
            this.wbDetails.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbDetails.Name = "wbDetails";
            this.wbDetails.Size = new System.Drawing.Size(944, 177);
            this.wbDetails.TabIndex = 0;
            // 
            // cbSource
            // 
            this.cbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Location = new System.Drawing.Point(65, 13);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(637, 23);
            this.cbSource.TabIndex = 17;
            this.cbSource.Text = "http://";
            this.cbSource.SelectedIndexChanged += new System.EventHandler(this.CbSource_SelectedIndexChanged);
            // 
            // cbTailMB
            // 
            this.cbTailMB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTailMB.AutoSize = true;
            this.cbTailMB.Checked = true;
            this.cbTailMB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTailMB.Location = new System.Drawing.Point(871, 16);
            this.cbTailMB.Name = "cbTailMB";
            this.cbTailMB.Size = new System.Drawing.Size(91, 17);
            this.cbTailMB.TabIndex = 19;
            this.cbTailMB.Text = "Load tail 1MB";
            this.cbTailMB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Filter";
            // 
            // btClearFilter
            // 
            this.btClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btClearFilter.Location = new System.Drawing.Point(902, 45);
            this.btClearFilter.Name = "btClearFilter";
            this.btClearFilter.Size = new System.Drawing.Size(55, 23);
            this.btClearFilter.TabIndex = 20;
            this.btClearFilter.Text = "Clear";
            this.btClearFilter.UseVisualStyleBackColor = true;
            this.btClearFilter.Click += new System.EventHandler(this.btClearFilter_Click);
            // 
            // timerUpdateAfterFilterChange
            // 
            this.timerUpdateAfterFilterChange.Interval = 500;
            this.timerUpdateAfterFilterChange.Tick += new System.EventHandler(this.timerUpdateAfterFilterChange_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 447);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btClearFilter);
            this.Controls.Add(this.cbTailMB);
            this.Controls.Add(this.cbSource);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btSelectSourceFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "NanoLogViewer 4 Decisions";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.OpenFileDialog openLogFileDialog;
		private Label label7;
		private Button btSelectSourceFile;
		private ListView lvLogLines;
		private Button btUpdate;
		private SplitContainer splitContainer1;
		private WebBrowser wbDetails;
        private ComboBox cbSource;
        private CheckBox cbTailMB;
        private ComboBox cbFilter;
        private Label label1;
        private Button btClearFilter;
        private Timer timerUpdateAfterFilterChange;
    }
}

