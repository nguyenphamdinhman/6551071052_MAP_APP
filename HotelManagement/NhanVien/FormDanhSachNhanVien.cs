using ClosedXML.Excel;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.NhanVien;

public partial class FormDanhSachNhanVien : Form
{
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private readonly TextBox search = new() { Width = 260 };
    private readonly Button export = new() { Text = "Xuất file Excel", Width = 150, Height = 38, BackColor = Color.DarkTurquoise };
    private readonly Button add = new() { Text = "Thêm mới", Width = 150, Height = 38, BackColor = Color.LightPink };

    public FormDanhSachNhanVien()
    {
        Text = "Quản lý thông tin nhân viên"; BackColor = Color.White;
        var top = new Panel { Dock = DockStyle.Top, Height = 58, Padding = new Padding(12) };
        var icon = new PictureBox { Image = Properties.Resources.search, Size = new Size(32, 32), SizeMode = PictureBoxSizeMode.CenterImage };
        search.Location = new Point(50, 12); search.Font = new Font("Segoe UI", 11);
        export.Location = new Point(700, 10); add.Location = new Point(860, 10);
        top.Controls.AddRange(new Control[] { icon, search, export, add });
        grid.Location = new Point(12, 68); grid.Dock = DockStyle.None;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grid.Size = new Size(1040, 620);
        Controls.Add(grid); Controls.Add(top);
        string[] cols = { "MaNV", "TenNV", "ChucVu", "Luong", "SDT", "CCCD", "NgaySinh", "GioiTinh", "DiaChi", "Email", "Sua", "Xoa" };
        string[] heads = { "Mã NV", "Họ tên", "Chức vụ", "Lương", "SĐT", "CCCD", "Ngày sinh", "Giới tính", "Địa chỉ", "Email", "Sửa", "Xóa" };
        for (int i = 0; i < cols.Length; i++) grid.Columns.Add(cols[i], heads[i]);
        grid.Columns["Luong"]!.DefaultCellStyle.Format = "#,##0";
        grid.Columns["NgaySinh"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
        grid.CellContentClick += Grid_CellContentClick;
        search.TextChanged += (_, _) => Filter();
        export.Click += Export_Click;
        add.Click += (_, _) => { using var f = new FormThemNhanVien(); f.ShowDialog(); LoadData(); };
        Load += (_, _) => LoadData();
    }

    private void LoadData()
    {
        try
        {
            using var db = new HotelManagementContext();
            var list = db.NhanViens.AsNoTracking().Where(x => x.DaXoa != true).OrderBy(x => x.MaNv).ToList();
            grid.Rows.Clear();
            foreach (var x in list) grid.Rows.Add(x.MaNv, x.TenNv, x.ChucVu, x.Luong, x.Sdt, x.Cccd, x.NgaySinh.ToDateTime(TimeOnly.MinValue), x.GioiTinh, x.DiaChi, x.Email, "Sửa", "Xóa");
            Filter();
        }
        catch (Exception ex) { MessageBox.Show("Lỗi tải nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    private void Filter()
    {
        foreach (DataGridViewRow r in grid.Rows)
        {
            string text = r.Cells.Cast<DataGridViewCell>().Take(10).Select(c => c.Value?.ToString() ?? "").Aggregate("", (a, b) => a + " " + b);
            r.Visible = text.Contains(search.Text.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        string ma = grid.Rows[e.RowIndex].Cells["MaNV"].Value?.ToString() ?? "";
        if (grid.Columns[e.ColumnIndex].Name == "Sua") { using var f = new FormSuaNhanVien(ma); f.ShowDialog(); LoadData(); }
        else if (grid.Columns[e.ColumnIndex].Name == "Xoa" && MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            using var db = new HotelManagementContext(); var x = db.NhanViens.FirstOrDefault(n => n.MaNv == ma);
            if (x != null) { x.DaXoa = true; db.SaveChanges(); LoadData(); }
        }
    }
    private void Export_Click(object? sender, EventArgs e)
    {
        using var dlg = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "DanhSachNhanVien.xlsx" };
        if (dlg.ShowDialog() != DialogResult.OK) return;
        using var wb = new XLWorkbook(); var ws = wb.AddWorksheet("Nhân viên");
        for (int c = 0; c < 10; c++) ws.Cell(1, c + 1).Value = grid.Columns[c].HeaderText;
        int r = 2;
        foreach (DataGridViewRow row in grid.Rows) if (row.Visible) { for (int c = 0; c < 10; c++) ws.Cell(r, c + 1).Value = row.Cells[c].Value?.ToString(); r++; }
        ws.Columns().AdjustToContents(); wb.SaveAs(dlg.FileName);
        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

public class FormThemNhanVien : Form
{
    private readonly TextBox ma = new(), ten = new(), chucvu = new(), luong = new(), sdt = new(), cccd = new(), diachi = new(), email = new();
    private readonly ComboBox gioitinh = new();
    private readonly DateTimePicker ngaysinh = new();
    public FormThemNhanVien()
    {
        Text = "Thêm thông tin nhân viên"; Size = new Size(620, 650); StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
        gioitinh.Items.AddRange(new object[] { "Nam", "Nữ" }); ngaysinh.Format = DateTimePickerFormat.Short;
        string[] labels = { "Mã NV", "Họ tên", "Chức vụ", "Lương", "SĐT", "CCCD", "Ngày sinh", "Giới tính", "Địa chỉ", "Email" };
        TextBox[] texts = { ma, ten, chucvu, luong, sdt, cccd, diachi, email };
        int textIndex = 0;
        for (int i = 0; i < labels.Length; i++)
        {
            Controls.Add(new Label { Text = labels[i], Location = new Point(40, 25 + i * 48), Size = new Size(140, 24) });
            Control field;
            if (i == 6) field = ngaysinh;
            else if (i == 7) field = gioitinh;
            else field = texts[textIndex++];
            field.Location = new Point(180, 22 + i * 48); field.Size = new Size(380, 28); Controls.Add(field);
        }
        ma.MaxLength = 5; ten.MaxLength = 50; chucvu.MaxLength = 50; luong.MaxLength = 12; sdt.MaxLength = 10; cccd.MaxLength = 12; diachi.MaxLength = 100; email.MaxLength = 100;
        gioitinh.DropDownStyle = ComboBoxStyle.DropDownList;
        var save = new Button { Text = "Thêm mới", Location = new Point(170, 510), Size = new Size(130, 42), BackColor = Color.Moccasin };
        var exit = new Button { Text = "Thoát", Location = new Point(330, 510), Size = new Size(130, 42), BackColor = Color.LightBlue };
        save.Click += Save_Click; exit.Click += (_, _) => Close(); Controls.AddRange(new Control[] { save, exit }); AcceptButton = save;
    }
    private void Save_Click(object? sender, EventArgs e)
    {
        if (ma.Text.Trim().Length == 0 || ten.Text.Trim().Length == 0 || chucvu.Text.Trim().Length == 0 || sdt.Text.Trim().Length == 0 || cccd.Text.Trim().Length == 0 || gioitinh.SelectedItem == null)
        { MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        if (!decimal.TryParse(luong.Text.Trim(), out decimal tienLuong) || tienLuong < 0)
        { MessageBox.Show("Lương không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        try
        {
            using var db = new HotelManagementContext();
            if (db.NhanViens.Any(x => x.MaNv == ma.Text.Trim().ToUpper()))
            { MessageBox.Show("Mã nhân viên đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            db.NhanViens.Add(new Models.NhanVien
            {
                MaNv = ma.Text.Trim().ToUpper(), TenNv = ten.Text.Trim(), ChucVu = chucvu.Text.Trim(), Luong = tienLuong,
                Sdt = sdt.Text.Trim(), Cccd = cccd.Text.Trim(), NgaySinh = DateOnly.FromDateTime(ngaysinh.Value),
                GioiTinh = gioitinh.SelectedItem!.ToString()!, DiaChi = diachi.Text.Trim(), Email = email.Text.Trim(), DaXoa = false
            });
            db.SaveChanges(); MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information); DialogResult = DialogResult.OK; Close();
        }
        catch (DbUpdateException ex) { MessageBox.Show("Lỗi thêm nhân viên: " + (ex.InnerException?.Message ?? ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}

public class FormSuaNhanVien : Form
{
    private readonly string ma;
    private readonly TextBox ten = new(), chucvu = new(), luong = new(), sdt = new(), cccd = new(), diachi = new(), email = new();
    private readonly ComboBox gioitinh = new(); private readonly DateTimePicker ngaysinh = new();
    public FormSuaNhanVien(string maNv)
    {
        ma = maNv; Text = "Cập nhật thông tin nhân viên"; Size = new Size(620, 590); StartPosition = FormStartPosition.CenterParent;
        gioitinh.Items.AddRange(new object[] { "Nam", "Nữ" }); gioitinh.DropDownStyle = ComboBoxStyle.DropDownList; ngaysinh.Format = DateTimePickerFormat.Short;
        string[] labels = { "Họ tên", "Chức vụ", "Lương", "SĐT", "CCCD", "Ngày sinh", "Giới tính", "Địa chỉ", "Email" };
        TextBox[] texts = { ten, chucvu, luong, sdt, cccd, diachi, email }; int ti = 0;
        for (int i = 0; i < labels.Length; i++)
        {
            Controls.Add(new Label { Text = labels[i], Location = new Point(40, 25 + i * 48), Size = new Size(140, 24) });
            Control field;
            if (i == 5) field = ngaysinh; else if (i == 6) field = gioitinh; else field = texts[ti++];
            field.Location = new Point(180, 22 + i * 48); field.Size = new Size(380, 28); Controls.Add(field);
        }
        var save = new Button { Text = "Cập nhật", Location = new Point(170, 470), Size = new Size(130, 42), BackColor = Color.Moccasin };
        var exit = new Button { Text = "Thoát", Location = new Point(330, 470), Size = new Size(130, 42), BackColor = Color.LightBlue };
        save.Click += Save_Click; exit.Click += (_, _) => Close(); Controls.AddRange(new Control[] { save, exit }); Load += LoadData;
    }
    private void LoadData(object? sender, EventArgs e)
    {
        using var db = new HotelManagementContext(); var x = db.NhanViens.FirstOrDefault(n => n.MaNv == ma && n.DaXoa != true);
        if (x == null) return;
        ten.Text = x.TenNv; chucvu.Text = x.ChucVu; luong.Text = x.Luong.ToString("0"); sdt.Text = x.Sdt; cccd.Text = x.Cccd;
        ngaysinh.Value = x.NgaySinh.ToDateTime(TimeOnly.MinValue); gioitinh.SelectedItem = x.GioiTinh; diachi.Text = x.DiaChi; email.Text = x.Email;
    }
    private void Save_Click(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(luong.Text.Trim(), out decimal tienLuong)) { MessageBox.Show("Lương không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        try
        {
            using var db = new HotelManagementContext(); var x = db.NhanViens.FirstOrDefault(n => n.MaNv == ma && n.DaXoa != true); if (x == null) return;
            x.TenNv = ten.Text.Trim(); x.ChucVu = chucvu.Text.Trim(); x.Luong = tienLuong; x.Sdt = sdt.Text.Trim(); x.Cccd = cccd.Text.Trim();
            x.NgaySinh = DateOnly.FromDateTime(ngaysinh.Value); x.GioiTinh = gioitinh.SelectedItem?.ToString() ?? ""; x.DiaChi = diachi.Text.Trim(); x.Email = email.Text.Trim();
            db.SaveChanges(); MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); DialogResult = DialogResult.OK; Close();
        }
        catch (DbUpdateException ex) { MessageBox.Show("Lỗi cập nhật: " + (ex.InnerException?.Message ?? ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
