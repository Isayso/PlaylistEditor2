namespace PlaylistEditor
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kodiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLinkLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cms1Send2Clip = new System.Windows.Forms.ToolStripMenuItem();
            this.searchGoogletoolStriptem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadYTFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editF2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms1NewWIndow = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox_unix = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox_download = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsDeletePathItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox_video = new System.Windows.Forms.ComboBox();
            this.comboBox_audio = new System.Windows.Forms.ComboBox();
            this.btn_refind = new System.Windows.Forms.Button();
            this.button_download_start = new System.Windows.Forms.Button();
            this.button_path = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.RedoButton = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.button_check = new System.Windows.Forms.Button();
            this.button_vlc = new System.Windows.Forms.Button();
            this.button_revert = new System.Windows.Forms.Button();
            this.button_tag = new System.Windows.Forms.Button();
            this.button_download = new System.Windows.Forms.Button();
            this.button_dup = new System.Windows.Forms.Button();
            this.button_search = new System.Windows.Forms.Button();
            this.button_del_all = new System.Windows.Forms.Button();
            this.button_settings = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.button_Info = new System.Windows.Forms.Button();
            this.button_delLine = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button_open = new System.Windows.Forms.Button();
            this.btn_clearfind = new System.Windows.Forms.Button();
            this.buttonR_MoveDown = new RepeatingButton();
            this.buttonR_moveUp = new RepeatingButton();
            this.textBox_find = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_counter = new System.Windows.Forms.Label();
            this.label_progress = new System.Windows.Forms.Label();
            this.lbl8 = new System.Windows.Forms.Label();
            this.lbl7 = new System.Windows.Forms.Label();
            this.checkBox_rlink = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMRU = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_open = new System.Windows.Forms.Label();
            this.label_central = new System.Windows.Forms.Label();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plabel_Filename = new PathLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panelMRU.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.163636F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(-3, 56);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 47;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1078, 310);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellValidated);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kodiToolStripMenuItem,
            this.openLinkLocationToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyTSMenuItem,
            this.pasteTSMenuItem,
            this.cutTSMenuItem,
            this.toolStripSeparator2,
            this.cms1Send2Clip,
            this.searchGoogletoolStriptem,
            this.toolStripSeparator3,
            this.downloadYTFileToolStripMenuItem,
            this.editF2ToolStripMenuItem,
            this.cms1NewWIndow});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(282, 287);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // kodiToolStripMenuItem
            // 
            this.kodiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playTSMenuItem,
            this.queueTSMenuItem});
            this.kodiToolStripMenuItem.Name = "kodiToolStripMenuItem";
            this.kodiToolStripMenuItem.Size = new System.Drawing.Size(281, 24);
            this.kodiToolStripMenuItem.Text = "Kodi";
            // 
            // playTSMenuItem
            // 
            this.playTSMenuItem.Name = "playTSMenuItem";
            this.playTSMenuItem.ShortcutKeyDisplayString = "Ctrl+P";
            this.playTSMenuItem.Size = new System.Drawing.Size(179, 24);
            this.playTSMenuItem.Text = "Play";
            this.playTSMenuItem.Click += new System.EventHandler(this.playTSMenuItem_Click);
            // 
            // queueTSMenuItem
            // 
            this.queueTSMenuItem.Name = "queueTSMenuItem";
            this.queueTSMenuItem.ShortcutKeyDisplayString = "Ctrl+Q";
            this.queueTSMenuItem.Size = new System.Drawing.Size(179, 24);
            this.queueTSMenuItem.Text = "Queue";
            this.queueTSMenuItem.Click += new System.EventHandler(this.queueTSMenuItem_Click);
            // 
            // openLinkLocationToolStripMenuItem
            // 
            this.openLinkLocationToolStripMenuItem.Name = "openLinkLocationToolStripMenuItem";
            this.openLinkLocationToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+L";
            this.openLinkLocationToolStripMenuItem.Size = new System.Drawing.Size(281, 24);
            this.openLinkLocationToolStripMenuItem.Text = "Open video location";
            this.openLinkLocationToolStripMenuItem.Click += new System.EventHandler(this.openLinkLocationTSMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(278, 6);
            // 
            // copyTSMenuItem
            // 
            this.copyTSMenuItem.Name = "copyTSMenuItem";
            this.copyTSMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyTSMenuItem.Size = new System.Drawing.Size(281, 24);
            this.copyTSMenuItem.Text = "Copy row";
            this.copyTSMenuItem.Click += new System.EventHandler(this.copyTSMenuItem_Click);
            // 
            // pasteTSMenuItem
            // 
            this.pasteTSMenuItem.Name = "pasteTSMenuItem";
            this.pasteTSMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            this.pasteTSMenuItem.Size = new System.Drawing.Size(281, 24);
            this.pasteTSMenuItem.Text = "Insert row (add)";
            this.pasteTSMenuItem.Click += new System.EventHandler(this.pasteTSMenuItem_Click);
            // 
            // cutTSMenuItem
            // 
            this.cutTSMenuItem.Name = "cutTSMenuItem";
            this.cutTSMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.cutTSMenuItem.Size = new System.Drawing.Size(281, 24);
            this.cutTSMenuItem.Text = "Cut row";
            this.cutTSMenuItem.Click += new System.EventHandler(this.cutTSMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(278, 6);
            // 
            // cms1Send2Clip
            // 
            this.cms1Send2Clip.Name = "cms1Send2Clip";
            this.cms1Send2Clip.ShortcutKeyDisplayString = "Alt+C";
            this.cms1Send2Clip.Size = new System.Drawing.Size(281, 24);
            this.cms1Send2Clip.Text = "Send YT Link to Clipboard";
            this.cms1Send2Clip.Click += new System.EventHandler(this.sendToHtttpTSMenuItem_Click);
            // 
            // searchGoogletoolStriptem
            // 
            this.searchGoogletoolStriptem.Name = "searchGoogletoolStriptem";
            this.searchGoogletoolStriptem.ShortcutKeyDisplayString = "Ctrl+G";
            this.searchGoogletoolStriptem.Size = new System.Drawing.Size(281, 24);
            this.searchGoogletoolStriptem.Text = "Search Name with iNet";
            this.searchGoogletoolStriptem.Click += new System.EventHandler(this.SearchGoogletoolStriptem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(278, 6);
            // 
            // downloadYTFileToolStripMenuItem
            // 
            this.downloadYTFileToolStripMenuItem.Name = "downloadYTFileToolStripMenuItem";
            this.downloadYTFileToolStripMenuItem.Size = new System.Drawing.Size(281, 24);
            this.downloadYTFileToolStripMenuItem.Text = "Download YT video";
            this.downloadYTFileToolStripMenuItem.Click += new System.EventHandler(this.downloadYTFileTSMenuItem_Click);
            // 
            // editF2ToolStripMenuItem
            // 
            this.editF2ToolStripMenuItem.Name = "editF2ToolStripMenuItem";
            this.editF2ToolStripMenuItem.ShortcutKeyDisplayString = "F2";
            this.editF2ToolStripMenuItem.Size = new System.Drawing.Size(281, 24);
            this.editF2ToolStripMenuItem.Text = "Rename";
            this.editF2ToolStripMenuItem.Click += new System.EventHandler(this.editF2ToolStripMenuItem_Click);
            // 
            // cms1NewWIndow
            // 
            this.cms1NewWIndow.Name = "cms1NewWIndow";
            this.cms1NewWIndow.ShortcutKeyDisplayString = "Ctrl+N";
            this.cms1NewWIndow.Size = new System.Drawing.Size(281, 24);
            this.cms1NewWIndow.Text = "New Window";
            this.cms1NewWIndow.Click += new System.EventHandler(this.cms1NewWIndow_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "m3u";
            this.openFileDialog.Filter = "Playlist|*.m3u";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "m3u";
            this.saveFileDialog1.Filter = "Playlist|*.m3u";
            // 
            // checkBox_unix
            // 
            this.checkBox_unix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_unix.Checked = true;
            this.checkBox_unix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_unix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_unix.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_unix.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBox_unix.Location = new System.Drawing.Point(788, 3);
            this.checkBox_unix.Name = "checkBox_unix";
            this.checkBox_unix.Size = new System.Drawing.Size(59, 24);
            this.checkBox_unix.TabIndex = 30;
            this.checkBox_unix.Text = "Unix";
            this.toolTip1.SetToolTip(this.checkBox_unix, "playlist for unix systems");
            this.checkBox_unix.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.MidnightBlue;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 15;
            this.comboBox1.Items.AddRange(new object[] {
            "2160",
            "1440",
            "1080",
            "720",
            "480",
            "360"});
            this.comboBox1.Location = new System.Drawing.Point(782, 31);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(64, 23);
            this.comboBox1.TabIndex = 41;
            this.toolTip1.SetToolTip(this.comboBox1, "max resolution");
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Click += new System.EventHandler(this.ComboBox_Click);
            // 
            // comboBox_download
            // 
            this.comboBox_download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_download.BackColor = System.Drawing.Color.MidnightBlue;
            this.comboBox_download.ContextMenuStrip = this.contextMenuStrip4;
            this.comboBox_download.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_download.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_download.ForeColor = System.Drawing.Color.White;
            this.comboBox_download.FormattingEnabled = true;
            this.comboBox_download.ItemHeight = 20;
            this.comboBox_download.Items.AddRange(new object[] {
            "new path"});
            this.comboBox_download.Location = new System.Drawing.Point(14, 11);
            this.comboBox_download.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox_download.Name = "comboBox_download";
            this.comboBox_download.Size = new System.Drawing.Size(329, 28);
            this.comboBox_download.TabIndex = 43;
            this.toolTip1.SetToolTip(this.comboBox_download, "download path");
            this.comboBox_download.Click += new System.EventHandler(this.ComboBox_Click);
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsDeletePathItem});
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(150, 28);
            // 
            // cmsDeletePathItem
            // 
            this.cmsDeletePathItem.Name = "cmsDeletePathItem";
            this.cmsDeletePathItem.Size = new System.Drawing.Size(149, 24);
            this.cmsDeletePathItem.Text = "Delete path";
            this.cmsDeletePathItem.Click += new System.EventHandler(this.cmsDeletePathItem_Click);
            // 
            // comboBox_video
            // 
            this.comboBox_video.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_video.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_video.FormattingEnabled = true;
            this.comboBox_video.Items.AddRange(new object[] {
            "mp4",
            "webm",
            "audio"});
            this.comboBox_video.Location = new System.Drawing.Point(14, 70);
            this.comboBox_video.Name = "comboBox_video";
            this.comboBox_video.Size = new System.Drawing.Size(88, 28);
            this.comboBox_video.TabIndex = 53;
            this.toolTip1.SetToolTip(this.comboBox_video, "video type");
            this.comboBox_video.Click += new System.EventHandler(this.ComboBox_Click);
            // 
            // comboBox_audio
            // 
            this.comboBox_audio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_audio.Enabled = false;
            this.comboBox_audio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_audio.FormattingEnabled = true;
            this.comboBox_audio.Items.AddRange(new object[] {
            "auto",
            "m4a",
            "aac",
            "ogg"});
            this.comboBox_audio.Location = new System.Drawing.Point(123, 70);
            this.comboBox_audio.Name = "comboBox_audio";
            this.comboBox_audio.Size = new System.Drawing.Size(88, 28);
            this.comboBox_audio.TabIndex = 54;
            this.toolTip1.SetToolTip(this.comboBox_audio, "audio format");
            this.comboBox_audio.Click += new System.EventHandler(this.ComboBox_Click);
            // 
            // btn_refind
            // 
            this.btn_refind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refind.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_refind.BackgroundImage = global::PlaylistEditor.Properties.Resources.autorenew_black_24dp;
            this.btn_refind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_refind.FlatAppearance.BorderSize = 0;
            this.btn_refind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refind.Location = new System.Drawing.Point(1001, 68);
            this.btn_refind.Margin = new System.Windows.Forms.Padding(0);
            this.btn_refind.Name = "btn_refind";
            this.btn_refind.Size = new System.Drawing.Size(25, 27);
            this.btn_refind.TabIndex = 66;
            this.toolTip1.SetToolTip(this.btn_refind, "check for invalid links\r\n+ Ctrl select links");
            this.btn_refind.UseVisualStyleBackColor = false;
            this.btn_refind.Visible = false;
            this.btn_refind.Click += new System.EventHandler(this.btn_refind_Click);
            // 
            // button_download_start
            // 
            this.button_download_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_download_start.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_download_start.BackgroundImage = global::PlaylistEditor.Properties.Resources.download_outline_green;
            this.button_download_start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_download_start.FlatAppearance.BorderSize = 0;
            this.button_download_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_download_start.Location = new System.Drawing.Point(223, 119);
            this.button_download_start.Margin = new System.Windows.Forms.Padding(0);
            this.button_download_start.Name = "button_download_start";
            this.button_download_start.Size = new System.Drawing.Size(107, 46);
            this.button_download_start.TabIndex = 65;
            this.toolTip1.SetToolTip(this.button_download_start, "download YT video\r\n");
            this.button_download_start.UseVisualStyleBackColor = true;
            this.button_download_start.Click += new System.EventHandler(this.button_download_start_Click);
            // 
            // button_path
            // 
            this.button_path.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_path.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_path.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_path.BackgroundImage")));
            this.button_path.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_path.FlatAppearance.BorderSize = 0;
            this.button_path.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_path.Location = new System.Drawing.Point(356, 7);
            this.button_path.Margin = new System.Windows.Forms.Padding(0);
            this.button_path.Name = "button_path";
            this.button_path.Size = new System.Drawing.Size(38, 37);
            this.button_path.TabIndex = 69;
            this.toolTip1.SetToolTip(this.button_path, "new download path\r\ndelete path with right click");
            this.button_path.UseVisualStyleBackColor = false;
            this.button_path.Click += new System.EventHandler(this.button_path_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_cancel.BackgroundImage")));
            this.button_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_cancel.FlatAppearance.BorderSize = 0;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Location = new System.Drawing.Point(97, 119);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(0);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(114, 46);
            this.button_cancel.TabIndex = 59;
            this.toolTip1.SetToolTip(this.button_cancel, "cancel");
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.BackColor = System.Drawing.Color.MidnightBlue;
            this.RedoButton.BackgroundImage = global::PlaylistEditor.Properties.Resources.redo;
            this.RedoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RedoButton.FlatAppearance.BorderSize = 0;
            this.RedoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RedoButton.Location = new System.Drawing.Point(108, 32);
            this.RedoButton.Margin = new System.Windows.Forms.Padding(0);
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(33, 19);
            this.RedoButton.TabIndex = 62;
            this.toolTip1.SetToolTip(this.RedoButton, "redo");
            this.RedoButton.UseVisualStyleBackColor = true;
            this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.BackColor = System.Drawing.Color.MidnightBlue;
            this.UndoButton.BackgroundImage = global::PlaylistEditor.Properties.Resources.undo;
            this.UndoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.UndoButton.FlatAppearance.BorderSize = 0;
            this.UndoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UndoButton.Location = new System.Drawing.Point(108, 4);
            this.UndoButton.Margin = new System.Windows.Forms.Padding(0);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(33, 19);
            this.UndoButton.TabIndex = 61;
            this.toolTip1.SetToolTip(this.UndoButton, "undo");
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // button_check
            // 
            this.button_check.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_check.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_check.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_check.BackgroundImage")));
            this.button_check.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_check.FlatAppearance.BorderSize = 0;
            this.button_check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_check.Location = new System.Drawing.Point(896, 16);
            this.button_check.Margin = new System.Windows.Forms.Padding(0);
            this.button_check.Name = "button_check";
            this.button_check.Size = new System.Drawing.Size(25, 36);
            this.button_check.TabIndex = 60;
            this.toolTip1.SetToolTip(this.button_check, "check for invalid links\r\n+ Ctrl select links");
            this.button_check.UseVisualStyleBackColor = true;
            this.button_check.Click += new System.EventHandler(this.button_check_Click);
            // 
            // button_vlc
            // 
            this.button_vlc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_vlc.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_vlc.BackgroundImage = global::PlaylistEditor.Properties.Resources.arrow_right_drop_circle_outline;
            this.button_vlc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_vlc.FlatAppearance.BorderSize = 0;
            this.button_vlc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_vlc.Location = new System.Drawing.Point(684, 10);
            this.button_vlc.Margin = new System.Windows.Forms.Padding(0);
            this.button_vlc.Name = "button_vlc";
            this.button_vlc.Size = new System.Drawing.Size(40, 37);
            this.button_vlc.TabIndex = 38;
            this.toolTip1.SetToolTip(this.button_vlc, "play with vlc\r\ndouble click cell");
            this.button_vlc.UseVisualStyleBackColor = true;
            this.button_vlc.Click += new System.EventHandler(this.button_vlc_Click);
            // 
            // button_revert
            // 
            this.button_revert.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_revert.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_revert.BackgroundImage")));
            this.button_revert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_revert.FlatAppearance.BorderSize = 0;
            this.button_revert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_revert.Location = new System.Drawing.Point(336, 9);
            this.button_revert.Margin = new System.Windows.Forms.Padding(0);
            this.button_revert.Name = "button_revert";
            this.button_revert.Size = new System.Drawing.Size(25, 37);
            this.button_revert.TabIndex = 37;
            this.toolTip1.SetToolTip(this.button_revert, "reload file");
            this.button_revert.UseVisualStyleBackColor = true;
            this.button_revert.Visible = false;
            this.button_revert.Click += new System.EventHandler(this.button_revert_Click);
            // 
            // button_tag
            // 
            this.button_tag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_tag.BackgroundImage = global::PlaylistEditor.Properties.Resources.search_web;
            this.button_tag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_tag.FlatAppearance.BorderSize = 0;
            this.button_tag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_tag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_tag.Location = new System.Drawing.Point(853, 16);
            this.button_tag.Margin = new System.Windows.Forms.Padding(0);
            this.button_tag.Name = "button_tag";
            this.button_tag.Size = new System.Drawing.Size(38, 34);
            this.button_tag.TabIndex = 36;
            this.toolTip1.SetToolTip(this.button_tag, "select plugin links");
            this.button_tag.UseVisualStyleBackColor = true;
            this.button_tag.Click += new System.EventHandler(this.button_tag_Click);
            // 
            // button_download
            // 
            this.button_download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_download.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_download.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_download.BackgroundImage")));
            this.button_download.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_download.FlatAppearance.BorderSize = 0;
            this.button_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_download.Location = new System.Drawing.Point(732, 11);
            this.button_download.Margin = new System.Windows.Forms.Padding(0);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(37, 37);
            this.button_download.TabIndex = 40;
            this.toolTip1.SetToolTip(this.button_download, "download YT video\r\n+Ctrl start immidiately");
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.downloadYTFileTSMenuItem_Click);
            // 
            // button_dup
            // 
            this.button_dup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_dup.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_dup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_dup.BackgroundImage")));
            this.button_dup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_dup.FlatAppearance.BorderSize = 0;
            this.button_dup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_dup.Location = new System.Drawing.Point(932, 15);
            this.button_dup.Margin = new System.Windows.Forms.Padding(0);
            this.button_dup.Name = "button_dup";
            this.button_dup.Size = new System.Drawing.Size(25, 37);
            this.button_dup.TabIndex = 35;
            this.toolTip1.SetToolTip(this.button_dup, "find duplicates\r\n+shift remove duplicates");
            this.button_dup.UseVisualStyleBackColor = true;
            this.button_dup.Click += new System.EventHandler(this.button_dup_Click);
            // 
            // button_search
            // 
            this.button_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_search.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_search.BackgroundImage")));
            this.button_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_search.FlatAppearance.BorderSize = 0;
            this.button_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_search.Location = new System.Drawing.Point(966, 14);
            this.button_search.Margin = new System.Windows.Forms.Padding(0);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(25, 37);
            this.button_search.TabIndex = 33;
            this.toolTip1.SetToolTip(this.button_search, "search\r\nCtrl+F");
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // button_del_all
            // 
            this.button_del_all.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_del_all.BackgroundImage")));
            this.button_del_all.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_del_all.FlatAppearance.BorderSize = 0;
            this.button_del_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_del_all.Location = new System.Drawing.Point(292, 10);
            this.button_del_all.Margin = new System.Windows.Forms.Padding(2);
            this.button_del_all.Name = "button_del_all";
            this.button_del_all.Size = new System.Drawing.Size(30, 32);
            this.button_del_all.TabIndex = 29;
            this.button_del_all.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button_del_all, "delete list\r\nCtrl+N open new window");
            this.button_del_all.UseVisualStyleBackColor = true;
            this.button_del_all.Click += new System.EventHandler(this.button_del_all_Click);
            // 
            // button_settings
            // 
            this.button_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_settings.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_settings.BackgroundImage")));
            this.button_settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_settings.FlatAppearance.BorderSize = 0;
            this.button_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_settings.Location = new System.Drawing.Point(1001, 15);
            this.button_settings.Margin = new System.Windows.Forms.Padding(0);
            this.button_settings.Name = "button_settings";
            this.button_settings.Size = new System.Drawing.Size(25, 37);
            this.button_settings.TabIndex = 28;
            this.toolTip1.SetToolTip(this.button_settings, "settings");
            this.button_settings.UseVisualStyleBackColor = true;
            this.button_settings.Click += new System.EventHandler(this.button_settings_Click);
            // 
            // button_add
            // 
            this.button_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_add.BackgroundImage")));
            this.button_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_add.FlatAppearance.BorderSize = 0;
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_add.Location = new System.Drawing.Point(249, 11);
            this.button_add.Margin = new System.Windows.Forms.Padding(2);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(30, 32);
            this.button_add.TabIndex = 27;
            this.button_add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button_add, "add line");
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_Info
            // 
            this.button_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Info.BackColor = System.Drawing.Color.MidnightBlue;
            this.button_Info.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_Info.BackgroundImage")));
            this.button_Info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Info.FlatAppearance.BorderSize = 0;
            this.button_Info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Info.Location = new System.Drawing.Point(1039, 15);
            this.button_Info.Margin = new System.Windows.Forms.Padding(0);
            this.button_Info.Name = "button_Info";
            this.button_Info.Size = new System.Drawing.Size(25, 37);
            this.button_Info.TabIndex = 24;
            this.toolTip1.SetToolTip(this.button_Info, "info/Keyboard shortcuts");
            this.button_Info.UseVisualStyleBackColor = true;
            this.button_Info.Click += new System.EventHandler(this.button_Info_Click);
            // 
            // button_delLine
            // 
            this.button_delLine.BackgroundImage = global::PlaylistEditor.Properties.Resources.close_box_outline_1_;
            this.button_delLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_delLine.FlatAppearance.BorderSize = 0;
            this.button_delLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_delLine.Location = new System.Drawing.Point(146, 11);
            this.button_delLine.Margin = new System.Windows.Forms.Padding(2);
            this.button_delLine.Name = "button_delLine";
            this.button_delLine.Size = new System.Drawing.Size(30, 32);
            this.button_delLine.TabIndex = 2;
            this.button_delLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button_delLine, "delete line");
            this.button_delLine.UseVisualStyleBackColor = true;
            this.button_delLine.Click += new System.EventHandler(this.button_delLine_Click);
            // 
            // button_save
            // 
            this.button_save.BackgroundImage = global::PlaylistEditor.Properties.Resources.content_save_1_;
            this.button_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_save.FlatAppearance.BorderSize = 0;
            this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_save.Location = new System.Drawing.Point(58, 2);
            this.button_save.Margin = new System.Windows.Forms.Padding(2);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(45, 49);
            this.button_save.TabIndex = 1;
            this.button_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button_save, "save as\r\n+shift save overwrite \r\nCtrl+S save");
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_open
            // 
            this.button_open.BackgroundImage = global::PlaylistEditor.Properties.Resources.open_in_app_1_;
            this.button_open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_open.FlatAppearance.BorderSize = 0;
            this.button_open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_open.Location = new System.Drawing.Point(9, 2);
            this.button_open.Margin = new System.Windows.Forms.Padding(2);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(45, 49);
            this.button_open.TabIndex = 0;
            this.button_open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button_open, "open m3u\r\ndouble click empty background\r\nhint: drop m3u file with shift \r\nactivat" +
        "es merge function\r\n");
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // btn_clearfind
            // 
            this.btn_clearfind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clearfind.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_clearfind.BackgroundImage = global::PlaylistEditor.Properties.Resources.close;
            this.btn_clearfind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_clearfind.FlatAppearance.BorderSize = 0;
            this.btn_clearfind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clearfind.Location = new System.Drawing.Point(1027, 68);
            this.btn_clearfind.Margin = new System.Windows.Forms.Padding(0);
            this.btn_clearfind.Name = "btn_clearfind";
            this.btn_clearfind.Size = new System.Drawing.Size(25, 27);
            this.btn_clearfind.TabIndex = 65;
            this.toolTip1.SetToolTip(this.btn_clearfind, "check for invalid links\r\n+ Ctrl select links");
            this.btn_clearfind.UseVisualStyleBackColor = false;
            this.btn_clearfind.Visible = false;
            this.btn_clearfind.Click += new System.EventHandler(this.btn_clearfind_Click);
            // 
            // buttonR_MoveDown
            // 
            this.buttonR_MoveDown.BackgroundImage = global::PlaylistEditor.Properties.Resources.arrow_down_bold_1_;
            this.buttonR_MoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonR_MoveDown.FlatAppearance.BorderSize = 0;
            this.buttonR_MoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonR_MoveDown.Location = new System.Drawing.Point(213, 11);
            this.buttonR_MoveDown.Margin = new System.Windows.Forms.Padding(0);
            this.buttonR_MoveDown.Name = "buttonR_MoveDown";
            this.buttonR_MoveDown.Size = new System.Drawing.Size(30, 32);
            this.buttonR_MoveDown.TabIndex = 32;
            this.buttonR_MoveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonR_MoveDown, "move line down");
            this.buttonR_MoveDown.UseVisualStyleBackColor = true;
            this.buttonR_MoveDown.Click += new System.EventHandler(this.button_moveDown_Click);
            // 
            // buttonR_moveUp
            // 
            this.buttonR_moveUp.BackgroundImage = global::PlaylistEditor.Properties.Resources.arrow_up_bold_1_;
            this.buttonR_moveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonR_moveUp.FlatAppearance.BorderSize = 0;
            this.buttonR_moveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonR_moveUp.Location = new System.Drawing.Point(183, 9);
            this.buttonR_moveUp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonR_moveUp.Name = "buttonR_moveUp";
            this.buttonR_moveUp.Size = new System.Drawing.Size(30, 32);
            this.buttonR_moveUp.TabIndex = 31;
            this.buttonR_moveUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.buttonR_moveUp, "move line up");
            this.buttonR_moveUp.UseVisualStyleBackColor = true;
            this.buttonR_moveUp.Click += new System.EventHandler(this.button_moveUp_Click);
            // 
            // textBox_find
            // 
            this.textBox_find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_find.BackColor = System.Drawing.Color.LightGray;
            this.textBox_find.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_find.Location = new System.Drawing.Point(825, 66);
            this.textBox_find.Name = "textBox_find";
            this.textBox_find.Size = new System.Drawing.Size(228, 31);
            this.textBox_find.TabIndex = 34;
            this.textBox_find.Visible = false;
            this.textBox_find.TextChanged += new System.EventHandler(this.textBox_find_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.label_counter);
            this.panel1.Controls.Add(this.label_progress);
            this.panel1.Controls.Add(this.lbl8);
            this.panel1.Controls.Add(this.lbl7);
            this.panel1.Controls.Add(this.button_download_start);
            this.panel1.Controls.Add(this.button_path);
            this.panel1.Controls.Add(this.checkBox_rlink);
            this.panel1.Controls.Add(this.comboBox_audio);
            this.panel1.Controls.Add(this.button_cancel);
            this.panel1.Controls.Add(this.comboBox_video);
            this.panel1.Controls.Add(this.comboBox_download);
            this.panel1.Location = new System.Drawing.Point(673, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 169);
            this.panel1.TabIndex = 44;
            this.panel1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 142);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(75, 20);
            this.progressBar1.TabIndex = 78;
            this.progressBar1.Visible = false;
            // 
            // label_counter
            // 
            this.label_counter.AutoSize = true;
            this.label_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_counter.ForeColor = System.Drawing.SystemColors.Control;
            this.label_counter.Location = new System.Drawing.Point(20, 119);
            this.label_counter.Name = "label_counter";
            this.label_counter.Size = new System.Drawing.Size(42, 20);
            this.label_counter.TabIndex = 73;
            this.label_counter.Text = "1 / 2";
            this.label_counter.Visible = false;
            // 
            // label_progress
            // 
            this.label_progress.AutoSize = true;
            this.label_progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_progress.ForeColor = System.Drawing.SystemColors.Control;
            this.label_progress.Location = new System.Drawing.Point(20, 140);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(0, 24);
            this.label_progress.TabIndex = 72;
            // 
            // lbl8
            // 
            this.lbl8.AutoSize = true;
            this.lbl8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl8.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl8.Location = new System.Drawing.Point(125, 47);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(49, 20);
            this.lbl8.TabIndex = 71;
            this.lbl8.Text = "audio";
            // 
            // lbl7
            // 
            this.lbl7.AutoSize = true;
            this.lbl7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl7.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl7.Location = new System.Drawing.Point(18, 47);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(48, 20);
            this.lbl7.TabIndex = 70;
            this.lbl7.Text = "video";
            // 
            // checkBox_rlink
            // 
            this.checkBox_rlink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_rlink.Checked = true;
            this.checkBox_rlink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_rlink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_rlink.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_rlink.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBox_rlink.Location = new System.Drawing.Point(227, 70);
            this.checkBox_rlink.Name = "checkBox_rlink";
            this.checkBox_rlink.Size = new System.Drawing.Size(160, 24);
            this.checkBox_rlink.TabIndex = 58;
            this.checkBox_rlink.Text = "replace link";
            this.checkBox_rlink.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.cutToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(158, 76);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Crtl-C";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.editCellCopy_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl-V";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.editCellPaste_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl-X";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.editCellCut_Click);
            // 
            // panelMRU
            // 
            this.panelMRU.Controls.Add(this.label5);
            this.panelMRU.Controls.Add(this.label4);
            this.panelMRU.Controls.Add(this.label3);
            this.panelMRU.Controls.Add(this.label2);
            this.panelMRU.Controls.Add(this.label1);
            this.panelMRU.Controls.Add(this.label_open);
            this.panelMRU.Location = new System.Drawing.Point(-3, 56);
            this.panelMRU.Name = "panelMRU";
            this.panelMRU.Size = new System.Drawing.Size(213, 235);
            this.panelMRU.TabIndex = 63;
            this.panelMRU.Visible = false;
            this.panelMRU.VisibleChanged += new System.EventHandler(this.panel2_VisibleChanged);
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(14, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "label5";
            this.label5.Click += new System.EventHandler(this.labelMRU_Click);
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(14, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.labelMRU_Click);
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(14, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.labelMRU_Click);
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(14, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.labelMRU_Click);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(14, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.labelMRU_Click);
            // 
            // label_open
            // 
            this.label_open.AutoSize = true;
            this.label_open.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_open.ForeColor = System.Drawing.SystemColors.Control;
            this.label_open.Location = new System.Drawing.Point(18, 13);
            this.label_open.Name = "label_open";
            this.label_open.Size = new System.Drawing.Size(134, 24);
            this.label_open.TabIndex = 1;
            this.label_open.Text = "Open   Ctrl-O";
            this.label_open.Click += new System.EventHandler(this.label_open_Click);
            // 
            // label_central
            // 
            this.label_central.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_central.AutoSize = true;
            this.label_central.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_central.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_central.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_central.Location = new System.Drawing.Point(420, 149);
            this.label_central.Name = "label_central";
            this.label_central.Size = new System.Drawing.Size(202, 60);
            this.label_central.TabIndex = 64;
            this.label_central.Text = "Double Click to open file\r\nDrag \'n Drop video files\r\nCTRL-N Open new Window";
            this.label_central.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_central.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(154, 28);
            // 
            // deleteEntryToolStripMenuItem
            // 
            this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
            this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.deleteEntryToolStripMenuItem.Text = "Delete entry";
            this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.deleteEntryToolStripMenuItem_Click);
            // 
            // plabel_Filename
            // 
            this.plabel_Filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plabel_Filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plabel_Filename.ForeColor = System.Drawing.Color.Cyan;
            this.plabel_Filename.Location = new System.Drawing.Point(363, 16);
            this.plabel_Filename.Name = "plabel_Filename";
            this.plabel_Filename.Size = new System.Drawing.Size(309, 23);
            this.plabel_Filename.TabIndex = 26;
            this.plabel_Filename.Text = "pathLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1074, 364);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_central);
            this.Controls.Add(this.panelMRU);
            this.Controls.Add(this.RedoButton);
            this.Controls.Add(this.UndoButton);
            this.Controls.Add(this.button_check);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button_vlc);
            this.Controls.Add(this.button_revert);
            this.Controls.Add(this.button_tag);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.button_dup);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.buttonR_MoveDown);
            this.Controls.Add(this.buttonR_moveUp);
            this.Controls.Add(this.checkBox_unix);
            this.Controls.Add(this.button_del_all);
            this.Controls.Add(this.button_settings);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.plabel_Filename);
            this.Controls.Add(this.button_Info);
            this.Controls.Add(this.button_delLine);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_open);
            this.Controls.Add(this.textBox_find);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_refind);
            this.Controls.Add(this.btn_clearfind);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Playlist Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.panelMRU.ResumeLayout(false);
            this.panelMRU.PerformLayout();
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_delLine;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Info;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private PathLabel plabel_Filename;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_settings;
        private System.Windows.Forms.Button button_del_all;
        private System.Windows.Forms.CheckBox checkBox_unix;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cms1Send2Clip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private RepeatingButton buttonR_moveUp;
        private RepeatingButton buttonR_MoveDown;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.TextBox textBox_find;
        private System.Windows.Forms.Button button_dup;
        private System.Windows.Forms.Button button_tag;
        private System.Windows.Forms.ToolStripMenuItem kodiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queueTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutTSMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button button_revert;
        private System.Windows.Forms.Button button_vlc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem downloadYTFileToolStripMenuItem;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem openLinkLocationToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox comboBox_download;
        private System.Windows.Forms.ComboBox comboBox_video;
        private System.Windows.Forms.ComboBox comboBox_audio;
        private System.Windows.Forms.CheckBox checkBox_rlink;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_check;
        private System.Windows.Forms.ToolStripMenuItem searchGoogletoolStriptem;
        private System.Windows.Forms.Button UndoButton;
        private System.Windows.Forms.Button RedoButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.Panel panelMRU;
        private System.Windows.Forms.Label label_open;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_central;
        private System.Windows.Forms.ToolStripMenuItem editF2ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.Button button_download_start;
        private System.Windows.Forms.Button button_path;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.Label lbl7;
        public System.Windows.Forms.Label label_progress;
        private System.Windows.Forms.Label label_counter;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem cmsDeletePathItem;
        private System.Windows.Forms.ToolStripMenuItem cms1NewWIndow;
        private System.Windows.Forms.Button btn_clearfind;
        private System.Windows.Forms.Button btn_refind;
    }
}

