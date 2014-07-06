using RateIt.Management.Controls.Map;
namespace RateIt.Management.Forms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.gbUserLogged = new System.Windows.Forms.GroupBox();
            this.btnCurrentUserLogout = new System.Windows.Forms.Button();
            this.tbLoggedUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbUserSelected = new System.Windows.Forms.GroupBox();
            this.btnUserLogout = new System.Windows.Forms.Button();
            this.btnUserLogin = new System.Windows.Forms.Button();
            this.gbUserList = new System.Windows.Forms.GroupBox();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ntbUsersCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserSearch = new System.Windows.Forms.TextBox();
            this.lblUserSearch = new System.Windows.Forms.Label();
            this.gbUserNew = new System.Windows.Forms.GroupBox();
            this.btnUserRegister = new System.Windows.Forms.Button();
            this.tpStores = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.gbMapMarker = new System.Windows.Forms.GroupBox();
            this.tbMapMarkerLongitude = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMapMarkerLatitude = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStoreRegister = new System.Windows.Forms.Button();
            this.gbMapNavigation = new System.Windows.Forms.GroupBox();
            this.trackMapZoom = new System.Windows.Forms.TrackBar();
            this.lblMapZoom = new System.Windows.Forms.Label();
            this.cbMapStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMapNavigate = new System.Windows.Forms.Button();
            this.tbMapLongitude = new System.Windows.Forms.TextBox();
            this.lblMapLongitude = new System.Windows.Forms.Label();
            this.tbMapLatitude = new System.Windows.Forms.TextBox();
            this.lblMapLatitude = new System.Windows.Forms.Label();
            this.pMap = new System.Windows.Forms.Panel();
            this.map = new RateIt.Management.Controls.Map.MapViewer();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpUsers.SuspendLayout();
            this.gbUserLogged.SuspendLayout();
            this.gbUserSelected.SuspendLayout();
            this.gbUserList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ntbUsersCount)).BeginInit();
            this.gbUserNew.SuspendLayout();
            this.tpStores.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbMapMarker.SuspendLayout();
            this.gbMapNavigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMapZoom)).BeginInit();
            this.pMap.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.Location = new System.Drawing.Point(12, 12);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tcMain);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.gbOutput);
            this.scMain.Size = new System.Drawing.Size(705, 600);
            this.scMain.SplitterDistance = 405;
            this.scMain.TabIndex = 8;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpUsers);
            this.tcMain.Controls.Add(this.tpStores);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(705, 405);
            this.tcMain.TabIndex = 1;
            // 
            // tpUsers
            // 
            this.tpUsers.BackColor = System.Drawing.Color.White;
            this.tpUsers.Controls.Add(this.gbUserLogged);
            this.tpUsers.Controls.Add(this.gbUserSelected);
            this.tpUsers.Controls.Add(this.gbUserList);
            this.tpUsers.Controls.Add(this.gbUserNew);
            this.tpUsers.Location = new System.Drawing.Point(4, 22);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(697, 379);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "Users";
            // 
            // gbUserLogged
            // 
            this.gbUserLogged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUserLogged.Controls.Add(this.btnCurrentUserLogout);
            this.gbUserLogged.Controls.Add(this.tbLoggedUserName);
            this.gbUserLogged.Controls.Add(this.label2);
            this.gbUserLogged.Enabled = false;
            this.gbUserLogged.Location = new System.Drawing.Point(473, 146);
            this.gbUserLogged.Name = "gbUserLogged";
            this.gbUserLogged.Size = new System.Drawing.Size(218, 74);
            this.gbUserLogged.TabIndex = 6;
            this.gbUserLogged.TabStop = false;
            this.gbUserLogged.Text = "Logged user";
            // 
            // btnCurrentUserLogout
            // 
            this.btnCurrentUserLogout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCurrentUserLogout.Location = new System.Drawing.Point(6, 44);
            this.btnCurrentUserLogout.Name = "btnCurrentUserLogout";
            this.btnCurrentUserLogout.Size = new System.Drawing.Size(206, 23);
            this.btnCurrentUserLogout.TabIndex = 2;
            this.btnCurrentUserLogout.Text = "Logout this user";
            this.btnCurrentUserLogout.UseVisualStyleBackColor = true;
            this.btnCurrentUserLogout.Click += new System.EventHandler(this.BtnCurrentUserLogoutClick);
            // 
            // tbLoggedUserName
            // 
            this.tbLoggedUserName.Location = new System.Drawing.Point(73, 18);
            this.tbLoggedUserName.Name = "tbLoggedUserName";
            this.tbLoggedUserName.ReadOnly = true;
            this.tbLoggedUserName.Size = new System.Drawing.Size(139, 20);
            this.tbLoggedUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "User name:";
            // 
            // gbUserSelected
            // 
            this.gbUserSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUserSelected.Controls.Add(this.btnUserLogout);
            this.gbUserSelected.Controls.Add(this.btnUserLogin);
            this.gbUserSelected.Location = new System.Drawing.Point(473, 62);
            this.gbUserSelected.Name = "gbUserSelected";
            this.gbUserSelected.Size = new System.Drawing.Size(218, 78);
            this.gbUserSelected.TabIndex = 5;
            this.gbUserSelected.TabStop = false;
            this.gbUserSelected.Text = "Selected user";
            // 
            // btnUserLogout
            // 
            this.btnUserLogout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUserLogout.Location = new System.Drawing.Point(6, 48);
            this.btnUserLogout.Name = "btnUserLogout";
            this.btnUserLogout.Size = new System.Drawing.Size(206, 23);
            this.btnUserLogout.TabIndex = 1;
            this.btnUserLogout.Text = "Logout selected user";
            this.btnUserLogout.UseVisualStyleBackColor = true;
            this.btnUserLogout.Click += new System.EventHandler(this.BtnUserLogoutClick);
            // 
            // btnUserLogin
            // 
            this.btnUserLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUserLogin.Location = new System.Drawing.Point(6, 19);
            this.btnUserLogin.Name = "btnUserLogin";
            this.btnUserLogin.Size = new System.Drawing.Size(206, 23);
            this.btnUserLogin.TabIndex = 0;
            this.btnUserLogin.Text = "Login selected user";
            this.btnUserLogin.UseVisualStyleBackColor = true;
            this.btnUserLogin.Click += new System.EventHandler(this.BtnUserLoginClick);
            // 
            // gbUserList
            // 
            this.gbUserList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUserList.Controls.Add(this.lvUsers);
            this.gbUserList.Controls.Add(this.ntbUsersCount);
            this.gbUserList.Controls.Add(this.label1);
            this.gbUserList.Controls.Add(this.tbUserSearch);
            this.gbUserList.Controls.Add(this.lblUserSearch);
            this.gbUserList.Location = new System.Drawing.Point(6, 6);
            this.gbUserList.Name = "gbUserList";
            this.gbUserList.Size = new System.Drawing.Size(461, 367);
            this.gbUserList.TabIndex = 4;
            this.gbUserList.TabStop = false;
            this.gbUserList.Text = "User list";
            // 
            // lvUsers
            // 
            this.lvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.GridLines = true;
            this.lvUsers.HideSelection = false;
            this.lvUsers.Location = new System.Drawing.Point(6, 19);
            this.lvUsers.MultiSelect = false;
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.ShowGroups = false;
            this.lvUsers.ShowItemToolTips = true;
            this.lvUsers.Size = new System.Drawing.Size(449, 318);
            this.lvUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvUsers.TabIndex = 3;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.SelectedIndexChanged += new System.EventHandler(this.LvUsersSelectedIndexChanged);
            this.lvUsers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvUsersMouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "User name";
            this.columnHeader1.Width = 128;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Email";
            this.columnHeader2.Width = 131;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Is user logged";
            this.columnHeader3.Width = 78;
            // 
            // ntbUsersCount
            // 
            this.ntbUsersCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ntbUsersCount.Location = new System.Drawing.Point(407, 341);
            this.ntbUsersCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ntbUsersCount.Name = "ntbUsersCount";
            this.ntbUsersCount.Size = new System.Drawing.Size(48, 20);
            this.ntbUsersCount.TabIndex = 7;
            this.ntbUsersCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ntbUsersCount.ValueChanged += new System.EventHandler(this.TbUserSearchTextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(374, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "limit:";
            // 
            // tbUserSearch
            // 
            this.tbUserSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserSearch.Location = new System.Drawing.Point(110, 341);
            this.tbUserSearch.Name = "tbUserSearch";
            this.tbUserSearch.Size = new System.Drawing.Size(258, 20);
            this.tbUserSearch.TabIndex = 5;
            this.tbUserSearch.TextChanged += new System.EventHandler(this.TbUserSearchTextChanged);
            // 
            // lblUserSearch
            // 
            this.lblUserSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUserSearch.AutoSize = true;
            this.lblUserSearch.Location = new System.Drawing.Point(6, 344);
            this.lblUserSearch.Name = "lblUserSearch";
            this.lblUserSearch.Size = new System.Drawing.Size(98, 13);
            this.lblUserSearch.TabIndex = 4;
            this.lblUserSearch.Text = "Filter by user name:";
            // 
            // gbUserNew
            // 
            this.gbUserNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUserNew.Controls.Add(this.btnUserRegister);
            this.gbUserNew.Location = new System.Drawing.Point(473, 6);
            this.gbUserNew.Name = "gbUserNew";
            this.gbUserNew.Size = new System.Drawing.Size(218, 50);
            this.gbUserNew.TabIndex = 3;
            this.gbUserNew.TabStop = false;
            this.gbUserNew.Text = "New user";
            // 
            // btnUserRegister
            // 
            this.btnUserRegister.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUserRegister.Location = new System.Drawing.Point(6, 19);
            this.btnUserRegister.Name = "btnUserRegister";
            this.btnUserRegister.Size = new System.Drawing.Size(206, 23);
            this.btnUserRegister.TabIndex = 0;
            this.btnUserRegister.Text = "Register new user";
            this.btnUserRegister.UseVisualStyleBackColor = true;
            this.btnUserRegister.Click += new System.EventHandler(this.BtnUserRegisterClick);
            // 
            // tpStores
            // 
            this.tpStores.BackColor = System.Drawing.Color.White;
            this.tpStores.Controls.Add(this.groupBox1);
            this.tpStores.Controls.Add(this.gbMapMarker);
            this.tpStores.Controls.Add(this.gbMapNavigation);
            this.tpStores.Controls.Add(this.pMap);
            this.tpStores.Location = new System.Drawing.Point(4, 22);
            this.tpStores.Name = "tpStores";
            this.tpStores.Padding = new System.Windows.Forms.Padding(3);
            this.tpStores.Size = new System.Drawing.Size(697, 379);
            this.tpStores.TabIndex = 1;
            this.tpStores.Text = "Stores";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(472, 289);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stores";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(206, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Search store";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // gbMapMarker
            // 
            this.gbMapMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMapMarker.Controls.Add(this.tbMapMarkerLongitude);
            this.gbMapMarker.Controls.Add(this.label3);
            this.gbMapMarker.Controls.Add(this.tbMapMarkerLatitude);
            this.gbMapMarker.Controls.Add(this.label4);
            this.gbMapMarker.Controls.Add(this.btnStoreRegister);
            this.gbMapMarker.Location = new System.Drawing.Point(473, 182);
            this.gbMapMarker.Name = "gbMapMarker";
            this.gbMapMarker.Size = new System.Drawing.Size(218, 101);
            this.gbMapMarker.TabIndex = 2;
            this.gbMapMarker.TabStop = false;
            this.gbMapMarker.Text = "Marker";
            // 
            // tbMapMarkerLongitude
            // 
            this.tbMapMarkerLongitude.Location = new System.Drawing.Point(72, 45);
            this.tbMapMarkerLongitude.Name = "tbMapMarkerLongitude";
            this.tbMapMarkerLongitude.ReadOnly = true;
            this.tbMapMarkerLongitude.Size = new System.Drawing.Size(140, 20);
            this.tbMapMarkerLongitude.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Longitude:";
            // 
            // tbMapMarkerLatitude
            // 
            this.tbMapMarkerLatitude.Location = new System.Drawing.Point(72, 19);
            this.tbMapMarkerLatitude.Name = "tbMapMarkerLatitude";
            this.tbMapMarkerLatitude.ReadOnly = true;
            this.tbMapMarkerLatitude.Size = new System.Drawing.Size(140, 20);
            this.tbMapMarkerLatitude.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Latitude:";
            // 
            // btnStoreRegister
            // 
            this.btnStoreRegister.Location = new System.Drawing.Point(6, 71);
            this.btnStoreRegister.Name = "btnStoreRegister";
            this.btnStoreRegister.Size = new System.Drawing.Size(206, 23);
            this.btnStoreRegister.TabIndex = 1;
            this.btnStoreRegister.Text = "Register new store";
            this.btnStoreRegister.UseVisualStyleBackColor = true;
            this.btnStoreRegister.Click += new System.EventHandler(this.BtnStoreRegisterClick);
            // 
            // gbMapNavigation
            // 
            this.gbMapNavigation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMapNavigation.Controls.Add(this.trackMapZoom);
            this.gbMapNavigation.Controls.Add(this.lblMapZoom);
            this.gbMapNavigation.Controls.Add(this.cbMapStyle);
            this.gbMapNavigation.Controls.Add(this.label5);
            this.gbMapNavigation.Controls.Add(this.panel1);
            this.gbMapNavigation.Controls.Add(this.btnMapNavigate);
            this.gbMapNavigation.Controls.Add(this.tbMapLongitude);
            this.gbMapNavigation.Controls.Add(this.lblMapLongitude);
            this.gbMapNavigation.Controls.Add(this.tbMapLatitude);
            this.gbMapNavigation.Controls.Add(this.lblMapLatitude);
            this.gbMapNavigation.Location = new System.Drawing.Point(473, 6);
            this.gbMapNavigation.Name = "gbMapNavigation";
            this.gbMapNavigation.Size = new System.Drawing.Size(218, 170);
            this.gbMapNavigation.TabIndex = 1;
            this.gbMapNavigation.TabStop = false;
            this.gbMapNavigation.Text = "Navigation";
            // 
            // trackMapZoom
            // 
            this.trackMapZoom.AutoSize = false;
            this.trackMapZoom.BackColor = System.Drawing.Color.White;
            this.trackMapZoom.Location = new System.Drawing.Point(72, 133);
            this.trackMapZoom.Maximum = 18;
            this.trackMapZoom.Minimum = 5;
            this.trackMapZoom.Name = "trackMapZoom";
            this.trackMapZoom.Size = new System.Drawing.Size(140, 30);
            this.trackMapZoom.TabIndex = 9;
            this.trackMapZoom.Value = 5;
            this.trackMapZoom.Scroll += new System.EventHandler(this.TbMapZoomScroll);
            // 
            // lblMapZoom
            // 
            this.lblMapZoom.AutoSize = true;
            this.lblMapZoom.Location = new System.Drawing.Point(6, 138);
            this.lblMapZoom.Name = "lblMapZoom";
            this.lblMapZoom.Size = new System.Drawing.Size(52, 13);
            this.lblMapZoom.TabIndex = 8;
            this.lblMapZoom.Text = "Zoom [0]:";
            // 
            // cbMapStyle
            // 
            this.cbMapStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMapStyle.FormattingEnabled = true;
            this.cbMapStyle.Items.AddRange(new object[] {
            "Yandex Map"});
            this.cbMapStyle.Location = new System.Drawing.Point(72, 106);
            this.cbMapStyle.Name = "cbMapStyle";
            this.cbMapStyle.Size = new System.Drawing.Size(140, 21);
            this.cbMapStyle.TabIndex = 7;
            this.cbMapStyle.SelectedIndexChanged += new System.EventHandler(this.BtnMapNavigateClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Map style:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(6, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 1);
            this.panel1.TabIndex = 5;
            // 
            // btnMapNavigate
            // 
            this.btnMapNavigate.Location = new System.Drawing.Point(137, 70);
            this.btnMapNavigate.Name = "btnMapNavigate";
            this.btnMapNavigate.Size = new System.Drawing.Size(75, 23);
            this.btnMapNavigate.TabIndex = 4;
            this.btnMapNavigate.Text = "Navigate";
            this.btnMapNavigate.UseVisualStyleBackColor = true;
            this.btnMapNavigate.Click += new System.EventHandler(this.BtnMapNavigateClick);
            // 
            // tbMapLongitude
            // 
            this.tbMapLongitude.Location = new System.Drawing.Point(72, 44);
            this.tbMapLongitude.Name = "tbMapLongitude";
            this.tbMapLongitude.Size = new System.Drawing.Size(140, 20);
            this.tbMapLongitude.TabIndex = 3;
            this.tbMapLongitude.Text = "36.260140681508";
            this.tbMapLongitude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMapLatitude_KeyDown);
            // 
            // lblMapLongitude
            // 
            this.lblMapLongitude.AutoSize = true;
            this.lblMapLongitude.Location = new System.Drawing.Point(6, 47);
            this.lblMapLongitude.Name = "lblMapLongitude";
            this.lblMapLongitude.Size = new System.Drawing.Size(60, 13);
            this.lblMapLongitude.TabIndex = 2;
            this.lblMapLongitude.Text = "Longitude:";
            // 
            // tbMapLatitude
            // 
            this.tbMapLatitude.Location = new System.Drawing.Point(72, 18);
            this.tbMapLatitude.Name = "tbMapLatitude";
            this.tbMapLatitude.Size = new System.Drawing.Size(140, 20);
            this.tbMapLatitude.TabIndex = 1;
            this.tbMapLatitude.Text = "49.9513903316245";
            this.tbMapLatitude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMapLatitude_KeyDown);
            // 
            // lblMapLatitude
            // 
            this.lblMapLatitude.AutoSize = true;
            this.lblMapLatitude.Location = new System.Drawing.Point(6, 21);
            this.lblMapLatitude.Name = "lblMapLatitude";
            this.lblMapLatitude.Size = new System.Drawing.Size(48, 13);
            this.lblMapLatitude.TabIndex = 0;
            this.lblMapLatitude.Text = "Latitude:";
            // 
            // pMap
            // 
            this.pMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMap.Controls.Add(this.map);
            this.pMap.Location = new System.Drawing.Point(6, 6);
            this.pMap.Name = "pMap";
            this.pMap.Size = new System.Drawing.Size(461, 367);
            this.pMap.TabIndex = 0;
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.GrayScaleMode = false;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(459, 365);
            this.map.TabIndex = 0;
            this.map.Zoom = 0D;
            this.map.OnPositionChanged += new GMap.NET.PositionChanged(this.map_OnPositionChanged);
            this.map.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.mapStores_OnMapZoomChanged);
            this.map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapMouseDown);
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapMouseMove);
            this.map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapMouseUp);
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.tbOutput);
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOutput.Location = new System.Drawing.Point(0, 0);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(705, 191);
            this.gbOutput.TabIndex = 3;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.BackColor = System.Drawing.SystemColors.Window;
            this.tbOutput.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbOutput.Location = new System.Drawing.Point(6, 19);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(693, 166);
            this.tbOutput.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 624);
            this.Controls.Add(this.scMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RateIt! Management Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMainFormClosed);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            this.gbUserLogged.ResumeLayout(false);
            this.gbUserLogged.PerformLayout();
            this.gbUserSelected.ResumeLayout(false);
            this.gbUserList.ResumeLayout(false);
            this.gbUserList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ntbUsersCount)).EndInit();
            this.gbUserNew.ResumeLayout(false);
            this.tpStores.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbMapMarker.ResumeLayout(false);
            this.gbMapMarker.PerformLayout();
            this.gbMapNavigation.ResumeLayout(false);
            this.gbMapNavigation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMapZoom)).EndInit();
            this.pMap.ResumeLayout(false);
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpUsers;
        private System.Windows.Forms.GroupBox gbUserLogged;
        private System.Windows.Forms.GroupBox gbUserSelected;
        private System.Windows.Forms.Button btnUserLogin;
        private System.Windows.Forms.GroupBox gbUserList;
        private System.Windows.Forms.TextBox tbUserSearch;
        private System.Windows.Forms.Label lblUserSearch;
        private System.Windows.Forms.ListView lvUsers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox gbUserNew;
        private System.Windows.Forms.Button btnUserRegister;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.NumericUpDown ntbUsersCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLoggedUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCurrentUserLogout;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnUserLogout;
        private System.Windows.Forms.TabPage tpStores;
        private System.Windows.Forms.Panel pMap;
        private System.Windows.Forms.GroupBox gbMapNavigation;
        private System.Windows.Forms.TextBox tbMapLongitude;
        private System.Windows.Forms.Label lblMapLongitude;
        private System.Windows.Forms.TextBox tbMapLatitude;
        private System.Windows.Forms.Label lblMapLatitude;
        private System.Windows.Forms.Button btnMapNavigate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMapStyle;
        private System.Windows.Forms.TrackBar trackMapZoom;
        private System.Windows.Forms.Label lblMapZoom;
        private System.Windows.Forms.GroupBox gbMapMarker;
        private System.Windows.Forms.Button btnStoreRegister;
        private MapViewer map;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbMapMarkerLongitude;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMapMarkerLatitude;
        private System.Windows.Forms.Label label4;

    }
}

