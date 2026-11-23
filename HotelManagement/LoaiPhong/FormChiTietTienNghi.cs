using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.LoaiPhong;

public class FormChiTietTienNghi : Form
{
    private readonly string maLoai;
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private readonly ComboBox cbTienNghi = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 240 };
    private readonly NumericUpDown soLuong = new() { Minimum = 1, Maximum = 100, Value = 1, Width = 100 };
    private readonly Button them = new() { Text = "Thêm", Width = 100 };

    public FormChiTietTienNghi(string maLoaiPhong)
    {
        maLoai = maLoaiPhong;
        Text = "Danh sách tiện nghi hiển có"; Size = new Size(700, 520); StartPosition = FormStartPosition.CenterParent;
        var top = new Panel { Dock = DockStyle.Top, Height = 70 };
        top.Controls.Add(new Label { Text = "Tiện nghi", Location = new Point(15, 22), AutoSize = true });
        cbTienNghi.Location = new Point(90, 18); soLuong.Location = new Point(345, 17);
        top.Controls.Add(new Label { Text = "Số lượng", Location = new Point(275, 22), AutoSize = true });
        top.Controls.Add(cbTienNghi); top.Controls.Add(soLuong);
        them.Location = new Point(460, 17); top.Controls.Add(them);
        grid.Dock = DockStyle.Fill;
        grid.Columns.Add("MaTN", "Mã TN"); grid.Columns.Add("TenTN", "Tên tiện nghi"); grid.Columns.Add("SoLuong", "Số lượng"); grid.Columns.Add("Sua", "Cập nhật"); grid.Columns.Add("Xoa", "Xóa");
        Controls.Add(grid); Controls.Add(top);
        Load += (_, _) => { LoadTienNghi(); LoadData(); };
        them.Click += Them_Click;
        grid.CellContentClick += Grid_CellContentClick;
        grid.CellEndEdit += Grid_CellEndEdit;
    }

    private void LoadTienNghi()
    {
        using var db = new HotelManagementContext();
        cbTienNghi.DataSource = db.TienNghis.AsNoTracking().Where(x => x.DaXoa != true).OrderBy(x => x.TenTn).ToList();
        cbTienNghi.DisplayMember = "TenTn"; cbTienNghi.ValueMember = "MaTn";
    }
    private void LoadData()
    {
        try
        {
            using var db = new HotelManagementContext();
            var list = db.Cttns.AsNoTracking().Where(x => x.MaLph == maLoai && x.DaXoa != true)
                .Include(x => x.MaTnNavigation).OrderBy(x => x.MaTn).ToList();
            grid.Rows.Clear();
            foreach (var x in list) grid.Rows.Add(x.MaTn, x.MaTnNavigation.TenTn, x.Sl ?? 1, "Cập nhật", "Xóa");
        }
        catch (Exception ex) { MessageBox.Show("Lỗi tải tiện nghi loại phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    private void Them_Click(object? sender, EventArgs e)
    {
        if (cbTienNghi.SelectedItem is not Models.TienNghi tn) return;
        try
        {
            using var db = new HotelManagementContext();
            var x = db.Cttns.FirstOrDefault(c => c.MaLph == maLoai && c.MaTn == tn.MaTn);
            if (x == null) db.Cttns.Add(new Cttn { MaLph = maLoai, MaTn = tn.MaTn, Sl = (int)soLuong.Value, DaXoa = false });
            else { x.Sl = (int)soLuong.Value; x.DaXoa = false; }
            db.SaveChanges(); LoadData();
        }
        catch (Exception ex) { MessageBox.Show("Lỗi cập nhật tiện nghi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        string maTn = grid.Rows[e.RowIndex].Cells["MaTN"].Value?.ToString() ?? "";
        if (grid.Columns[e.ColumnIndex].Name == "Sua")
        {
            grid.ReadOnly = false; grid.Columns["SoLuong"].ReadOnly = false;
            foreach (DataGridViewColumn c in grid.Columns) if (c.Name != "SoLuong") c.ReadOnly = true;
            grid.Rows[e.RowIndex].Cells["SoLuong"].Selected = true; grid.BeginEdit(true);
        }
        else if (grid.Columns[e.ColumnIndex].Name == "Xoa" && MessageBox.Show("Xóa tiện nghi khỏi loại phòng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            using var db = new HotelManagementContext();
            var x = db.Cttns.FirstOrDefault(c => c.MaLph == maLoai && c.MaTn == maTn);
            if (x != null) { x.DaXoa = true; db.SaveChanges(); LoadData(); }
        }
    }
    private void Grid_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Columns[e.ColumnIndex].Name != "SoLuong") return;
        string maTn = grid.Rows[e.RowIndex].Cells["MaTN"].Value?.ToString() ?? "";
        if (!int.TryParse(grid.Rows[e.RowIndex].Cells["SoLuong"].Value?.ToString(), out int sl) || sl < 1) { LoadData(); return; }
        using var db = new HotelManagementContext();
        var x = db.Cttns.FirstOrDefault(c => c.MaLph == maLoai && c.MaTn == maTn);
        if (x != null) { x.Sl = sl; db.SaveChanges(); }
        grid.ReadOnly = true;
    }
}
