namespace HotelManagement
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

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
            panel1 = new Panel();
            labelTenKhachSan = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            lblTenNguoiDung = new Label();
            pictureBox2 = new PictureBox();
            linkLabel1 = new LinkLabel();
            panelMain = new Panel();
            panel4 = new Panel();
            button13 = new Button();
            btnTrangChu = new Button();
            button5 = new Button();
            button4 = new Button();
            button7 = new Button();
            button6 = new Button();
            btnQuanLyPhong = new Button();
            button12 = new Button();
            button11 = new Button();
            btnSoDoPhong = new Button();
            button9 = new Button();
            button10 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(24, 106, 59);
            panel1.Controls.Add(labelTenKhachSan);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1380, 75);
            panel1.TabIndex = 0;
            // 
            // labelTenKhachSan
            // 
            labelTenKhachSan.AutoSize = true;
            labelTenKhachSan.BackColor = Color.Transparent;
            labelTenKhachSan.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTenKhachSan.ForeColor = Color.White;
            labelTenKhachSan.Location = new Point(70, 16);
            labelTenKhachSan.Name = "labelTenKhachSan";
            labelTenKhachSan.Size = new Size(346, 41);
            labelTenKhachSan.TabIndex = 3;
            labelTenKhachSan.Text = "HOTEL MANAGEMENT";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Menu;
            pictureBox1.Location = new Point(14, 14);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(45, 48);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(30, 132, 73);
            panel2.Controls.Add(lblTenNguoiDung);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(linkLabel1);
            panel2.Location = new Point(0, 75);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(270, 165);
            panel2.TabIndex = 1;
            // 
            // lblTenNguoiDung
            // 
            lblTenNguoiDung.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenNguoiDung.ForeColor = Color.White;
            lblTenNguoiDung.Location = new Point(10, 100);
            lblTenNguoiDung.Name = "lblTenNguoiDung";
            lblTenNguoiDung.Size = new Size(250, 30);
            lblTenNguoiDung.TabIndex = 2;
            lblTenNguoiDung.Text = "Admin";
            lblTenNguoiDung.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImageLayout = ImageLayout.None;
            pictureBox2.Image = Properties.Resources.user;
            pictureBox2.Location = new Point(95, 12);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(80, 85);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = Color.FromArgb(249, 231, 159);
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel1.LinkColor = Color.FromArgb(235, 245, 251);
            linkLabel1.Location = new Point(98, 133);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(80, 21);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Đăng xuất";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel4.AutoScroll = true;
            panel4.BackColor = Color.FromArgb(30, 132, 73);
            panel4.Controls.Add(button13);
            panel4.Controls.Add(button5);
            panel4.Controls.Add(button7);
            panel4.Controls.Add(button6);
            panel4.Controls.Add(button12);
            panel4.Controls.Add(button11);
            panel4.Controls.Add(button10);
            panel4.Controls.Add(button9);
            panel4.Controls.Add(button4);
            panel4.Controls.Add(btnQuanLyPhong);
            panel4.Controls.Add(btnSoDoPhong);
            panel4.Controls.Add(btnTrangChu);
            panel4.Location = new Point(0, 240);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(270, 660);
            panel4.TabIndex = 4;
            // 
            // btnTrangChu
            // 
            SetupSidebarButton(btnTrangChu, "Trang chủ", Properties.Resources.MenuTrangChu, 0, btnTrangChu_Click);
            // 
            // btnSoDoPhong
            // 
            SetupSidebarButton(btnSoDoPhong, "Sơ đồ phòng", Properties.Resources.MenuSoDoPhong, 48, btnSoDoPhong_Click);
            // 
            // btnQuanLyPhong
            // 
            SetupSidebarButton(btnQuanLyPhong, "Phòng", Properties.Resources.MenuPhong, 96, btnQuanLyPhong_Click);
            // 
            // button4
            // 
            SetupSidebarButton(button4, "Loại phòng", Properties.Resources.MenuLoaiPhong, 144, button4_Click);
            // 
            // button9
            // 
            SetupSidebarButton(button9, "Danh sách đặt phòng", Properties.Resources.MenuDatPhong, 192, button9_Click);
            // 
            // button10
            // 
            SetupSidebarButton(button10, "Danh sách hóa đơn", Properties.Resources.MenuDanhSachHoaDon, 240, button10_Click);
            // 
            // button11
            // 
            SetupSidebarButton(button11, "Danh sách dịch vụ", Properties.Resources.MenuQuanLyDichVu, 288, button11_Click);
            // 
            // button12
            // 
            SetupSidebarButton(button12, "Danh sách tiện nghi", Properties.Resources.MenuTienNghi, 336, button12_Click);
            // 
            // button6
            // 
            SetupSidebarButton(button6, "Danh sách khách hàng", Properties.Resources.MenuQuanLyKhachHang, 384, button6_Click);
            // 
            // button7
            // 
            SetupSidebarButton(button7, "Danh sách nhân viên", Properties.Resources.MenuQuanLyNhanVien, 432, button7_Click);
            // 
            // button5
            // 
            SetupSidebarButton(button5, "Danh sách tài khoản", Properties.Resources.MenuQuanLyTaiKhoan, 480, button5_Click);
            // 
            // button13
            // 
            SetupSidebarButton(button13, "Thống kê", Properties.Resources.MenuThongKe, 528, button13_Click);
            // 
            // panelMain
            // 
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Location = new Point(270, 75);
            panelMain.Margin = new Padding(3, 4, 3, 4);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1110, 825);
            panelMain.TabIndex = 2;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1380, 900);
            Controls.Add(panelMain);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hotel Management - Quản Lý Khách Sạn";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void SetupSidebarButton(Button btn, string text, Image icon, int top, EventHandler onClick)
        {
            btn.BackColor = Color.FromArgb(30, 132, 73);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 90, 50);
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn.ForeColor = Color.White;
            btn.Image = icon;
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(18, 0, 0, 0);
            btn.Location = new Point(0, top);
            btn.Name = btn.Name ?? ("btn" + text);
            btn.Size = new Size(270, 48);
            btn.Text = "  " + text;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.UseVisualStyleBackColor = false;
            btn.Click += onClick;
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTenKhachSan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblTenNguoiDung;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnQuanLyPhong;
        private System.Windows.Forms.Button btnSoDoPhong;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button13;
    }
}
