using ClosedXML.Excel;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.TienNghi;

public partial class FormDanhSachTienNghi : Form
{
    public FormDanhSachTienNghi()
    {
        InitializeComponent();
        dtgDanhSachTienNghi.CellContentClick += dtgDanhSachTienNghi_CellClick;
        txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        btnXuatExcel.Click += btnXuatExcel_Click;
        btnThemMoi.Click += btnThemMoi_Click;
        Load += FormDanhSachTienNghi_Load;
    }

    private void FormDanhSachTienNghi_Load(object? sender, EventArgs e)
    {
        CauHinhLuoi();
        TaiDuLieu();
    }

    private void CauHinhLuoi()
    {
        if (dtgDanhSachTienNghi.Columns.Count > 0) return;
        dtgDanhSachTienNghi.Columns.Add("MaTN", "Mã tiện nghi");
        dtgDanhSachTienNghi.Columns.Add("TenTN", "Tên tiện nghi");
        dtgDanhSachTienNghi.Columns.Add("Sua", "Sửa");
        dtgDanhSachTienNghi.Columns.Add("Xoa", "Xóa");
        dtgDanhSachTienNghi.ReadOnly = true;
        dtgDanhSachTienNghi.AllowUserToAddRows = false;
        dtgDanhSachTienNghi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dtgDanhSachTienNghi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dtgDanhSachTienNghi.RowTemplate.Height = 38;
    }

    private void TaiDuLieu()
    {
        try
        {
            using var db = new HotelManagementContext();
            var ds = db.TienNghis.AsNoTracking().Where(x => x.DaXoa != true).OrderBy(x => x.MaTn).ToList();
            dtgDanhSachTienNghi.Rows.Clear();
            foreach (var x in ds) dtgDanhSachTienNghi.Rows.Add(x.MaTn, x.TenTn, "Sửa", "Xóa");
        }
        catch (Exception ex) { MessageBox.Show("Lỗi tải tiện nghi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private void txtTimKiem_TextChanged(object? sender, EventArgs e)
    {
        foreach (DataGridViewRow r in dtgDanhSachTienNghi.Rows)
        {
            r.Visible = r.Cells["MaTN"].Value?.ToString()?.Contains(txtTimKiem.Text.Trim(), StringComparison.OrdinalIgnoreCase) == true
                || r.Cells["TenTN"].Value?.ToString()?.Contains(txtTimKiem.Text.Trim(), StringComparison.OrdinalIgnoreCase) == true;
        }
    }

    private void btnThemMoi_Click(object? sender, EventArgs e)
    {
        using var f = new FormThemTienNghi();
        f.ShowDialog();
        TaiDuLieu();
    }

    private void dtgDanhSachTienNghi_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        string ma = dtgDanhSachTienNghi.Rows[e.RowIndex].Cells["MaTN"].Value?.ToString() ?? "";
        if (dtgDanhSachTienNghi.Columns[e.ColumnIndex].Name == "Sua")
        {
            using var f = new FormSuaTienNghi(ma);
            f.ShowDialog();
            TaiDuLieu();
        }
        else if (dtgDanhSachTienNghi.Columns[e.ColumnIndex].Name == "Xoa"
            && MessageBox.Show("Bạn có chắc muốn xóa tiện nghi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            try
            {
                using var db = new HotelManagementContext();
                var x = db.TienNghis.FirstOrDefault(t => t.MaTn == ma);
                if (x == null) return;
                x.DaXoa = true;
                db.SaveChanges();
                TaiDuLieu();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xóa tiện nghi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }

    private void btnXuatExcel_Click(object? sender, EventArgs e)
    {
        using var dlg = new SaveFileDialog { Filter = "Excel Workbook (*.xlsx)|*.xlsx", FileName = "DanhSachTienNghi.xlsx" };
        if (dlg.ShowDialog() != DialogResult.OK) return;
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Tiện nghi");
        ws.Cell(1, 1).Value = "Mã tiện nghi";
        ws.Cell(1, 2).Value = "Tên tiện nghi";
        int row = 2;
        foreach (DataGridViewRow r in dtgDanhSachTienNghi.Rows)
            if (r.Visible) { ws.Cell(row, 1).Value = r.Cells["MaTN"].Value?.ToString(); ws.Cell(row, 2).Value = r.Cells["TenTN"].Value?.ToString(); row++; }
        ws.Columns().AdjustToContents();
        wb.SaveAs(dlg.FileName);
        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
