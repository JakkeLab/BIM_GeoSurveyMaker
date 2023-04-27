namespace GeoSurveyMaker
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.새파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.다른이름으로저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.닫기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.편집ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.프로젝트명ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgBorings = new System.Windows.Forms.DataGridView();
            this.ColNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbBoringTable = new System.Windows.Forms.Label();
            this.listBorings = new System.Windows.Forms.ListBox();
            this.lbBoringList = new System.Windows.Forms.Label();
            this.lbEditBoring = new System.Windows.Forms.Label();
            this.dgLayers = new System.Windows.Forms.DataGridView();
            this.ColLayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDepth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddBoring = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lbLayers = new System.Windows.Forms.Label();
            this.lbBoringName = new System.Windows.Forms.Label();
            this.tbBoringName = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBorings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.편집ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1118, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.새파일ToolStripMenuItem,
            this.저장ToolStripMenuItem,
            this.다른이름으로저장ToolStripMenuItem,
            this.닫기ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // 새파일ToolStripMenuItem
            // 
            this.새파일ToolStripMenuItem.Name = "새파일ToolStripMenuItem";
            this.새파일ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.새파일ToolStripMenuItem.Text = "새 파일";
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.저장ToolStripMenuItem.Text = "저장";
            // 
            // 다른이름으로저장ToolStripMenuItem
            // 
            this.다른이름으로저장ToolStripMenuItem.Name = "다른이름으로저장ToolStripMenuItem";
            this.다른이름으로저장ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.다른이름으로저장ToolStripMenuItem.Text = "다른 이름으로 저장";
            // 
            // 닫기ToolStripMenuItem
            // 
            this.닫기ToolStripMenuItem.Name = "닫기ToolStripMenuItem";
            this.닫기ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.닫기ToolStripMenuItem.Text = "닫기";
            // 
            // 편집ToolStripMenuItem
            // 
            this.편집ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.프로젝트명ToolStripMenuItem});
            this.편집ToolStripMenuItem.Name = "편집ToolStripMenuItem";
            this.편집ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.편집ToolStripMenuItem.Text = "편집";
            // 
            // 프로젝트명ToolStripMenuItem
            // 
            this.프로젝트명ToolStripMenuItem.Name = "프로젝트명ToolStripMenuItem";
            this.프로젝트명ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.프로젝트명ToolStripMenuItem.Text = "프로젝트 명";
            // 
            // dgBorings
            // 
            this.dgBorings.AllowUserToAddRows = false;
            this.dgBorings.AllowUserToDeleteRows = false;
            this.dgBorings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBorings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNumber,
            this.ColName});
            this.dgBorings.Location = new System.Drawing.Point(12, 49);
            this.dgBorings.Name = "dgBorings";
            this.dgBorings.ReadOnly = true;
            this.dgBorings.RowHeadersVisible = false;
            this.dgBorings.RowTemplate.Height = 25;
            this.dgBorings.Size = new System.Drawing.Size(709, 394);
            this.dgBorings.TabIndex = 1;
            // 
            // ColNumber
            // 
            this.ColNumber.HeaderText = "순번";
            this.ColNumber.Name = "ColNumber";
            this.ColNumber.ReadOnly = true;
            this.ColNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColNumber.Width = 60;
            // 
            // ColName
            // 
            this.ColName.HeaderText = "보링 이름";
            this.ColName.Name = "ColName";
            this.ColName.ReadOnly = true;
            this.ColName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColName.Width = 85;
            // 
            // lbBoringTable
            // 
            this.lbBoringTable.AutoSize = true;
            this.lbBoringTable.Location = new System.Drawing.Point(12, 31);
            this.lbBoringTable.Name = "lbBoringTable";
            this.lbBoringTable.Size = new System.Drawing.Size(59, 15);
            this.lbBoringTable.TabIndex = 2;
            this.lbBoringTable.Text = "보링 정보";
            // 
            // listBorings
            // 
            this.listBorings.FormattingEnabled = true;
            this.listBorings.ItemHeight = 15;
            this.listBorings.Location = new System.Drawing.Point(727, 49);
            this.listBorings.Name = "listBorings";
            this.listBorings.Size = new System.Drawing.Size(120, 394);
            this.listBorings.TabIndex = 3;
            this.listBorings.SelectedIndexChanged += new System.EventHandler(this.listBorings_SelectedIndexChanged);
            this.listBorings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBorings_MouseDown);
            // 
            // lbBoringList
            // 
            this.lbBoringList.AutoSize = true;
            this.lbBoringList.Location = new System.Drawing.Point(727, 31);
            this.lbBoringList.Name = "lbBoringList";
            this.lbBoringList.Size = new System.Drawing.Size(71, 15);
            this.lbBoringList.TabIndex = 4;
            this.lbBoringList.Text = "보링 리스트";
            // 
            // lbEditBoring
            // 
            this.lbEditBoring.AutoSize = true;
            this.lbEditBoring.Location = new System.Drawing.Point(860, 31);
            this.lbEditBoring.Name = "lbEditBoring";
            this.lbEditBoring.Size = new System.Drawing.Size(59, 15);
            this.lbEditBoring.TabIndex = 5;
            this.lbEditBoring.Text = "보링 설정";
            // 
            // dgLayers
            // 
            this.dgLayers.AllowUserToResizeRows = false;
            this.dgLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLayerName,
            this.ColDepth});
            this.dgLayers.Location = new System.Drawing.Point(860, 118);
            this.dgLayers.Name = "dgLayers";
            this.dgLayers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgLayers.RowTemplate.Height = 25;
            this.dgLayers.Size = new System.Drawing.Size(244, 295);
            this.dgLayers.TabIndex = 6;
            // 
            // ColLayerName
            // 
            this.ColLayerName.HeaderText = "층이름";
            this.ColLayerName.Name = "ColLayerName";
            this.ColLayerName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColDepth
            // 
            this.ColDepth.HeaderText = "깊이";
            this.ColDepth.Name = "ColDepth";
            this.ColDepth.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btnAddBoring
            // 
            this.btnAddBoring.Location = new System.Drawing.Point(984, 420);
            this.btnAddBoring.Name = "btnAddBoring";
            this.btnAddBoring.Size = new System.Drawing.Size(55, 23);
            this.btnAddBoring.TabIndex = 7;
            this.btnAddBoring.Text = "추가";
            this.btnAddBoring.UseVisualStyleBackColor = true;
            this.btnAddBoring.Click += new System.EventHandler(this.btnAddBoring_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(1049, 419);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(55, 23);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "삭제";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lbLayers
            // 
            this.lbLayers.AutoSize = true;
            this.lbLayers.Location = new System.Drawing.Point(860, 100);
            this.lbLayers.Name = "lbLayers";
            this.lbLayers.Size = new System.Drawing.Size(59, 15);
            this.lbLayers.TabIndex = 10;
            this.lbLayers.Text = "층별 설정";
            // 
            // lbBoringName
            // 
            this.lbBoringName.AutoSize = true;
            this.lbBoringName.Location = new System.Drawing.Point(860, 61);
            this.lbBoringName.Name = "lbBoringName";
            this.lbBoringName.Size = new System.Drawing.Size(59, 15);
            this.lbBoringName.TabIndex = 11;
            this.lbBoringName.Text = "보링 이름";
            // 
            // tbBoringName
            // 
            this.tbBoringName.Location = new System.Drawing.Point(925, 58);
            this.tbBoringName.Name = "tbBoringName";
            this.tbBoringName.Size = new System.Drawing.Size(179, 23);
            this.tbBoringName.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 461);
            this.Controls.Add(this.tbBoringName);
            this.Controls.Add(this.lbBoringName);
            this.Controls.Add(this.lbLayers);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddBoring);
            this.Controls.Add(this.dgLayers);
            this.Controls.Add(this.lbEditBoring);
            this.Controls.Add(this.lbBoringList);
            this.Controls.Add(this.listBorings);
            this.Controls.Add(this.lbBoringTable);
            this.Controls.Add(this.dgBorings);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SurveyMaker";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBorings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 파일ToolStripMenuItem;
        private ToolStripMenuItem 새파일ToolStripMenuItem;
        private ToolStripMenuItem 저장ToolStripMenuItem;
        private ToolStripMenuItem 다른이름으로저장ToolStripMenuItem;
        private ToolStripMenuItem 닫기ToolStripMenuItem;
        private ToolStripMenuItem 편집ToolStripMenuItem;
        private ToolStripMenuItem 프로젝트명ToolStripMenuItem;
        private DataGridView dgBorings;
        private Label lbBoringTable;
        private DataGridViewTextBoxColumn ColNumber;
        private DataGridViewTextBoxColumn ColName;
        private ListBox listBorings;
        private Label lbBoringList;
        private Label lbEditBoring;
        private DataGridView dgLayers;
        private Button btnAddBoring;
        private Button btnRemove;
        private DataGridViewTextBoxColumn ColLayerName;
        private DataGridViewTextBoxColumn ColDepth;
        private Label lbLayers;
        private Label lbBoringName;
        private TextBox tbBoringName;
    }
}