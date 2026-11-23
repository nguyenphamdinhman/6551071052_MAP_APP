using ClosedXML.Excel;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.KhachHang;

public partial class FormDanhSachKhachHang : Form
{
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private readonly TextBox search = new() { Width = 260 };
    private readonly Button export = new() { Text = "Xuất file Excel", Width = 150, Height = 38 };
    private readonly Button add = new() { Text = "Thêm mới", Width = 150, Height = 38 };

    public FormDanhSachKhachHang()
    {
        Text = "Quản lý thông tin khách hàng";
        BackColor = Color.White;
        var top = new Panel { Dock = DockStyle.Top, Height = 58, Padding = new Padding(12) };
        var searchIcon = new PictureBox { Image = Properties.Resources.search, Size = new Size(32, 32), SizeMode = PictureBoxSizeMode.CenterImage };
        search.Location = new Point(50, 12); search.Font = new Font("Segoe UI", 11);
        export.Location = new Point(700, 10); export.BackColor = Color.DarkTurquoise;
        add.Location = new Point(860, 10); add.BackColor = Color.LightPink;
        top.Controls.AddRange(new Control[] { searchIcon, search, export, add });
        grid.Location = new Point(12, 68); grid.Dock = DockStyle.None;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grid.Size = new Size(1040, 620);
        Controls.Add(grid); Controls.Add(top);

        grid.Columns.Add("MaKH", "Mã KH");
        grid.Columns.Add("TenKH", "Họ và tên");
        grid.Columns.Add("SDT", "Số điện thoại");
        grid.Columns.Add("CCCD", "CCCD/Passport");
        grid.Columns.Add("QuocTich", "Quốc tịch");
        grid.Columns.Add("GioiTinh", "Giới tính");
        grid.Columns.Add("Sua", "Sửa");
        grid.Columns.Add("Xoa", "Xóa");
        grid.CellContentClick += Grid_CellContentClick;
        search.TextChanged += (_, _) => Filter();
        export.Click += Export_Click;
        add.Click += (_, _) => { using var f = new FormThemKhachHang(); f.ShowDialog(); LoadData(); };
        Load += (_, _) => LoadData();
    }

    private void LoadData()
    {
        try
        {
            using var db = new HotelManagementContext();
            var list = db.KhachHangs.AsNoTracking().Where(x => x.DaXoa != true).OrderBy(x => x.MaKh).ToList();
            grid.Rows.Clear();
            foreach (var x in list) grid.Rows.Add(x.MaKh, x.TenKh, x.Sdt, x.CccdPassport, x.QuocTich, x.GioiTinh, "Sửa", "Xóa");
            Filter();
        }
        catch (Exception ex) { MessageBox.Show("Lỗi tải khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private void Filter()
    {
        foreach (DataGridViewRow r in grid.Rows)
        {
            string text = r.Cells.Cast<DataGridViewCell>().Take(6).Select(c => c.Value?.ToString() ?? "").Aggregate("", (a, b) => a + " " + b);
            r.Visible = text.Contains(search.Text.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        string ma = grid.Rows[e.RowIndex].Cells["MaKH"].Value?.ToString() ?? "";
        if (grid.Columns[e.ColumnIndex].Name == "Sua")
        {
            using var f = new FormSuaKhachHang(ma); f.ShowDialog(); LoadData();
        }
        else if (grid.Columns[e.ColumnIndex].Name == "Xoa"
            && MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            using var db = new HotelManagementContext();
            var x = db.KhachHangs.FirstOrDefault(k => k.MaKh == ma);
            if (x != null) { x.DaXoa = true; db.SaveChanges(); LoadData(); }
        }
    }

    private void Export_Click(object? sender, EventArgs e)
    {
        using var dlg = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "DanhSachKhachHang.xlsx" };
        if (dlg.ShowDialog() != DialogResult.OK) return;
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Khách hàng");
        string[] headers = { "Mã KH", "Họ tên", "SĐT", "CCCD/Passport", "Quốc tịch", "Giới tính" };
        for (int i = 0; i < headers.Length; i++) ws.Cell(1, i + 1).Value = headers[i];
        int r = 2;
        foreach (DataGridViewRow row in grid.Rows) if (row.Visible) { for (int c = 0; c < 6; c++) ws.Cell(r, c + 1).Value = row.Cells[c].Value?.ToString(); r++; }
        ws.Columns().AdjustToContents(); wb.SaveAs(dlg.FileName);
        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

public class FormThemKhachHang : Form
{
    private readonly TextBox ma = new(), ten = new(), sdt = new(), cccd = new(), quoctich = new();
    private readonly ComboBox gioitinh = new();
    public FormThemKhachHang()
    {
        Text = "Thêm thông tin khách hàng"; Size = new Size(560, 520); StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
        gioitinh.Items.AddRange(new object[] { "Nam", "Nữ" });
        string[] labels = { "Mã KH", "Họ tên", "Số điện thoại", "CCCD/Passport", "Quốc tịch", "Giới tính" };
        TextBox[] texts = { ma, ten, sdt, cccd, quoctich };
        for (int i = 0; i < labels.Length; i++)
        {
            Controls.Add(new Label { Text = labels[i], Location = new Point(40, 35 + i * 55), Size = new Size(150, 25), Font = new Font("Segoe UI", 10) });
            if (i < texts.Length) Controls.Add(texts[i]); else Controls.Add(gioitinh);
            if (i < texts.Length) { texts[i].Location = new Point(190, 32 + i * 55); texts[i].Size = new Size(300, 28); texts[i].Font = new Font("Segoe UI", 11); }
            else { gioitinh.Location = new Point(190, 32 + i * 55); gioitinh.Size = new Size(300, 28); gioitinh.DropDownStyle = ComboBoxStyle.DropDownList; }
        }
        ma.MaxLength = 5; ten.MaxLength = 50; sdt.MaxLength = 10; cccd.MaxLength = 12; quoctich.MaxLength = 30;
        var save = new Button { Text = "Thêm mới", Location = new Point(120, 390), Size = new Size(130, 42), BackColor = Color.Moccasin };
        var exit = new Button { Text = "Thoát", Location = new Point(280, 390), Size = new Size(130, 42), BackColor = Color.LightBlue };
        save.Click += Save_Click; exit.Click += (_, _) => Close();
        Controls.AddRange(new Control[] { save, exit }); AcceptButton = save; CancelButton = exit;
    }
    private void Save_Click(object? sender, EventArgs e)
    {
        if (ma.Text.Trim().Length == 0 || ten.Text.Trim().Length == 0 || sdt.Text.Trim().Length == 0 || cccd.Text.Trim().Length == 0 || gioitinh.SelectedItem == null)
        { MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        try
        {
            using var db = new HotelManagementContext();
            if (db.KhachHangs.Any(x => x.MaKh == ma.Text.Trim().ToUpper()))
            { MessageBox.Show("Mã khách hàng đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            db.KhachHangs.Add(new Models.KhachHang { MaKh = ma.Text.Trim().ToUpper(), TenKh = ten.Text.Trim(), Sdt = sdt.Text.Trim(), CccdPassport = cccd.Text.Trim(), QuocTich = quoctich.Text.Trim(), GioiTinh = gioitinh.SelectedItem!.ToString()!, DaXoa = false });
            db.SaveChanges(); MessageBox.Show("Thêm khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information); DialogResult = DialogResult.OK; Close();
        }
        catch (DbUpdateException ex) { MessageBox.Show("Lỗi thêm khách hàng: " + (ex.InnerException?.Message ?? ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}

public class FormSuaKhachHang : Form
{
    private readonly string ma;
    private readonly TextBox ten = new(), sdt = new(), cccd = new(), quoctich = new();
    private readonly ComboBox gioitinh = new();
    public FormSuaKhachHang(string maKh)
    {
        ma = maKh; Text = "Cập nhật thông tin khách hàng"; Size = new Size(560, 470); StartPosition = FormStartPosition.CenterParent;
        gioitinh.Items.AddRange(new object[] { "Nam", "Nữ" });
        string[] labels = { "Họ tên", "Số điện thoại", "CCCD/Passport", "Quốc tịch", "Giới tính" };
        TextBox[] texts = { ten, sdt, cccd, quoctich };
        for (int i = 0; i < labels.Length; i++)
        {
            Controls.Add(new Label { Text = labels[i], Location = new Point(40, 35 + i * 55), Size = new Size(150, 25) });
            if (i < texts.Length) { Controls.Add(texts[i]); texts[i].Location = new Point(190, 32 + i * 55); texts[i].Size = new Size(300, 28); }
            else { Controls.Add(gioitinh); gioitinh.Location = new Point(190, 32 + i * 55); gioitinh.Size = new Size(300, 28); gioitinh.DropDownStyle = ComboBoxStyle.DropDownList; }
        }
        var save = new Button { Text = "Cập nhật", Location = new Point(120, 340), Size = new Size(130, 42), BackColor = Color.Moccasin };
        var exit = new Button { Text = "Thoát", Location = new Point(280, 340), Size = new Size(130, 42), BackColor = Color.LightBlue };
        save.Click += Save_Click; exit.Click += (_, _) => Close(); Controls.AddRange(new Control[] { save, exit });
        Load += LoadData;
    }
    private void LoadData(object? sender, EventArgs e)
    {
        using var db = new HotelManagementContext();
        var x = db.KhachHangs.FirstOrDefault(k => k.MaKh == ma && k.DaXoa != true);
        if (x == null) return;
        ten.Text = x.TenKh; sdt.Text = x.Sdt; cccd.Text = x.CccdPassport; quoctich.Text = x.QuocTich; gioitinh.SelectedItem = x.GioiTinh;
    }
    private void Save_Click(object? sender, EventArgs e)
    {
        try
        {
            using var db = new HotelManagementContext();
            var x = db.KhachHangs.FirstOrDefault(k => k.MaKh == ma && k.DaXoa != true);
            if (x == null) return;
            x.TenKh = ten.Text.Trim(); x.Sdt = sdt.Text.Trim(); x.CccdPassport = cccd.Text.Trim(); x.QuocTich = quoctich.Text.Trim(); x.GioiTinh = gioitinh.SelectedItem?.ToString() ?? "";
            db.SaveChanges(); MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); DialogResult = DialogResult.OK; Close();
        }
        catch (DbUpdateException ex) { MessageBox.Show("Lỗi cập nhật: " + (ex.InnerException?.Message ?? ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
