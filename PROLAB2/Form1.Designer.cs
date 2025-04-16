namespace PROLAB2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblStartLat;
        private System.Windows.Forms.Label lblStartLon;
        private System.Windows.Forms.Label lblDestLat;
        private System.Windows.Forms.Label lblDestLon;
        private System.Windows.Forms.TextBox txtStartLat;
        private System.Windows.Forms.TextBox txtStartLon;
        private System.Windows.Forms.TextBox txtDestLat;
        private System.Windows.Forms.TextBox txtDestLon;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.TextBox txtOutput;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.ComboBox cmbPassengerType;
        private System.Windows.Forms.Label lblPassengerType;
        private System.Windows.Forms.ComboBox cmbRouteType;
        private System.Windows.Forms.Label lblRouteType;
        private System.Windows.Forms.Label lblKrediKarti;
        private System.Windows.Forms.TextBox txtKrediKarti;
        private System.Windows.Forms.Label lblNakit;
        private System.Windows.Forms.TextBox txtNakit;
        private System.Windows.Forms.Label lblKentKart;
        private System.Windows.Forms.TextBox txtKentKart;
        private System.Windows.Forms.Button btnSetStartFromMap;
        private System.Windows.Forms.Button btnSetDestFromMap;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblStartLat = new System.Windows.Forms.Label();
            this.lblStartLon = new System.Windows.Forms.Label();
            this.lblDestLat = new System.Windows.Forms.Label();
            this.lblDestLon = new System.Windows.Forms.Label();
            this.txtStartLat = new System.Windows.Forms.TextBox();
            this.txtStartLon = new System.Windows.Forms.TextBox();
            this.txtDestLat = new System.Windows.Forms.TextBox();
            this.txtDestLon = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.cmbPassengerType = new System.Windows.Forms.ComboBox();
            this.lblPassengerType = new System.Windows.Forms.Label();
            this.cmbRouteType = new System.Windows.Forms.ComboBox();
            this.lblRouteType = new System.Windows.Forms.Label();
            this.lblKrediKarti = new System.Windows.Forms.Label();
            this.txtKrediKarti = new System.Windows.Forms.TextBox();
            this.lblNakit = new System.Windows.Forms.Label();
            this.txtNakit = new System.Windows.Forms.TextBox();
            this.lblKentKart = new System.Windows.Forms.Label();
            this.txtKentKart = new System.Windows.Forms.TextBox();
            this.btnSetStartFromMap = new System.Windows.Forms.Button();
            this.btnSetDestFromMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStartLat
            // 
            this.lblStartLat.AutoSize = true;
            this.lblStartLat.Location = new System.Drawing.Point(12, 15);
            this.lblStartLat.Name = "lblStartLat";
            this.lblStartLat.Size = new System.Drawing.Size(94, 13);
            this.lblStartLat.TabIndex = 0;
            this.lblStartLat.Text = "Başlangıç Enlemi:";
            // 
            // txtStartLat
            // 
            this.txtStartLat.Location = new System.Drawing.Point(120, 12);
            this.txtStartLat.Name = "txtStartLat";
            this.txtStartLat.Size = new System.Drawing.Size(100, 20);
            this.txtStartLat.TabIndex = 1;
            // 
            // lblStartLon
            // 
            this.lblStartLon.AutoSize = true;
            this.lblStartLon.Location = new System.Drawing.Point(12, 45);
            this.lblStartLon.Name = "lblStartLon";
            this.lblStartLon.Size = new System.Drawing.Size(100, 13);
            this.lblStartLon.TabIndex = 2;
            this.lblStartLon.Text = "Başlangıç Boylamı:";
            // 
            // txtStartLon
            // 
            this.txtStartLon.Location = new System.Drawing.Point(120, 42);
            this.txtStartLon.Name = "txtStartLon";
            this.txtStartLon.Size = new System.Drawing.Size(100, 20);
            this.txtStartLon.TabIndex = 3;
            // 
            // lblDestLat
            // 
            this.lblDestLat.AutoSize = true;
            this.lblDestLat.Location = new System.Drawing.Point(12, 75);
            this.lblDestLat.Name = "lblDestLat";
            this.lblDestLat.Size = new System.Drawing.Size(83, 13);
            this.lblDestLat.TabIndex = 4;
            this.lblDestLat.Text = "Hedef Enlemi:";
            // 
            // txtDestLat
            // 
            this.txtDestLat.Location = new System.Drawing.Point(120, 72);
            this.txtDestLat.Name = "txtDestLat";
            this.txtDestLat.Size = new System.Drawing.Size(100, 20);
            this.txtDestLat.TabIndex = 5;
            // 
            // lblDestLon
            // 
            this.lblDestLon.AutoSize = true;
            this.lblDestLon.Location = new System.Drawing.Point(12, 105);
            this.lblDestLon.Name = "lblDestLon";
            this.lblDestLon.Size = new System.Drawing.Size(89, 13);
            this.lblDestLon.TabIndex = 6;
            this.lblDestLon.Text = "Hedef Boylamı:";
            // 
            // txtDestLon
            // 
            this.txtDestLon.Location = new System.Drawing.Point(120, 102);
            this.txtDestLon.Name = "txtDestLon";
            this.txtDestLon.Size = new System.Drawing.Size(100, 20);
            this.txtDestLon.TabIndex = 7;
            // 
            // lblPassengerType
            // 
            this.lblPassengerType.AutoSize = true;
            this.lblPassengerType.Location = new System.Drawing.Point(12, 135);
            this.lblPassengerType.Name = "lblPassengerType";
            this.lblPassengerType.Size = new System.Drawing.Size(85, 13);
            this.lblPassengerType.TabIndex = 8;
            this.lblPassengerType.Text = "Yolcu Tipi Seçin:";
            // 
            // cmbPassengerType
            // 
            this.cmbPassengerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPassengerType.FormattingEnabled = true;
            this.cmbPassengerType.Items.AddRange(new object[] {
            "Genel Yolcu",
            "Öğrenci",
            "Yaşlı"});
            this.cmbPassengerType.Location = new System.Drawing.Point(120, 132);
            this.cmbPassengerType.Name = "cmbPassengerType";
            this.cmbPassengerType.Size = new System.Drawing.Size(100, 21);
            this.cmbPassengerType.TabIndex = 9;
            // 
            // lblRouteType
            // 
            this.lblRouteType.AutoSize = true;
            this.lblRouteType.Location = new System.Drawing.Point(12, 165);
            this.lblRouteType.Name = "lblRouteType";
            this.lblRouteType.Size = new System.Drawing.Size(96, 13);
            this.lblRouteType.TabIndex = 10;
            this.lblRouteType.Text = "Rota Stratejisi Seçin:";
            // 
            // cmbRouteType
            // 
            this.cmbRouteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRouteType.FormattingEnabled = true;
            this.cmbRouteType.Location = new System.Drawing.Point(120, 162);
            this.cmbRouteType.Name = "cmbRouteType";
            this.cmbRouteType.Size = new System.Drawing.Size(100, 21);
            this.cmbRouteType.TabIndex = 11;
            // 
            // lblKrediKarti
            // 
            this.lblKrediKarti.AutoSize = true;
            this.lblKrediKarti.Location = new System.Drawing.Point(12, 195);
            this.lblKrediKarti.Name = "lblKrediKarti";
            this.lblKrediKarti.Size = new System.Drawing.Size(87, 13);
            this.lblKrediKarti.TabIndex = 12;
            this.lblKrediKarti.Text = "Kredi Kartı Bakiye:";
            // 
            // txtKrediKarti
            // 
            this.txtKrediKarti.Location = new System.Drawing.Point(120, 192);
            this.txtKrediKarti.Name = "txtKrediKarti";
            this.txtKrediKarti.Size = new System.Drawing.Size(100, 20);
            this.txtKrediKarti.TabIndex = 13;
            // 
            // lblNakit
            // 
            this.lblNakit.AutoSize = true;
            this.lblNakit.Location = new System.Drawing.Point(12, 225);
            this.lblNakit.Name = "lblNakit";
            this.lblNakit.Size = new System.Drawing.Size(70, 13);
            this.lblNakit.TabIndex = 14;
            this.lblNakit.Text = "Nakit Bakiye:";
            // 
            // txtNakit
            // 
            this.txtNakit.Location = new System.Drawing.Point(120, 222);
            this.txtNakit.Name = "txtNakit";
            this.txtNakit.Size = new System.Drawing.Size(100, 20);
            this.txtNakit.TabIndex = 15;
            // 
            // lblKentKart
            // 
            this.lblKentKart.AutoSize = true;
            this.lblKentKart.Location = new System.Drawing.Point(12, 255);
            this.lblKentKart.Name = "lblKentKart";
            this.lblKentKart.Size = new System.Drawing.Size(80, 13);
            this.lblKentKart.TabIndex = 16;
            this.lblKentKart.Text = "KentKart Bakiye:";
            // 
            // txtKentKart
            // 
            this.txtKentKart.Location = new System.Drawing.Point(120, 252);
            this.txtKentKart.Name = "txtKentKart";
            this.txtKentKart.Size = new System.Drawing.Size(100, 20);
            this.txtKentKart.TabIndex = 17;
            // 
            // btnSetStartFromMap
            // 
            this.btnSetStartFromMap.Location = new System.Drawing.Point(240, 10);
            this.btnSetStartFromMap.Name = "btnSetStartFromMap";
            this.btnSetStartFromMap.Size = new System.Drawing.Size(130, 23);
            this.btnSetStartFromMap.TabIndex = 18;
            this.btnSetStartFromMap.Text = "Haritadan Başlangıç Seç";
            this.btnSetStartFromMap.UseVisualStyleBackColor = true;
            this.btnSetStartFromMap.Click += new System.EventHandler(this.btnSetStartFromMap_Click);
            // 
            // btnSetDestFromMap
            // 
            this.btnSetDestFromMap.Location = new System.Drawing.Point(240, 70);
            this.btnSetDestFromMap.Name = "btnSetDestFromMap";
            this.btnSetDestFromMap.Size = new System.Drawing.Size(130, 23);
            this.btnSetDestFromMap.TabIndex = 19;
            this.btnSetDestFromMap.Text = "Haritadan Hedef Seç";
            this.btnSetDestFromMap.UseVisualStyleBackColor = true;
            this.btnSetDestFromMap.Click += new System.EventHandler(this.btnSetDestFromMap_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(12, 285);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(205, 23);
            this.btnCalculate.TabIndex = 20;
            this.btnCalculate.Text = "Rotayı Hesapla";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 315);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(400, 180);
            this.txtOutput.TabIndex = 21;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(430, 12);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 20;
            this.gMapControl1.MinZoom = 1;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(33, 65, 105, 225);
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(600, 400);
            this.gMapControl1.TabIndex = 22;
            this.gMapControl1.Zoom = 14D;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1044, 540);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnSetDestFromMap);
            this.Controls.Add(this.btnSetStartFromMap);
            this.Controls.Add(this.txtKentKart);
            this.Controls.Add(this.lblKentKart);
            this.Controls.Add(this.txtNakit);
            this.Controls.Add(this.lblNakit);
            this.Controls.Add(this.txtKrediKarti);
            this.Controls.Add(this.lblKrediKarti);
            this.Controls.Add(this.cmbRouteType);
            this.Controls.Add(this.lblRouteType);
            this.Controls.Add(this.cmbPassengerType);
            this.Controls.Add(this.lblPassengerType);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.txtDestLon);
            this.Controls.Add(this.lblDestLon);
            this.Controls.Add(this.txtDestLat);
            this.Controls.Add(this.lblDestLat);
            this.Controls.Add(this.txtStartLon);
            this.Controls.Add(this.lblStartLon);
            this.Controls.Add(this.txtStartLat);
            this.Controls.Add(this.lblStartLat);
            this.Name = "Form1";
            this.Text = "Rota Hesaplama";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
