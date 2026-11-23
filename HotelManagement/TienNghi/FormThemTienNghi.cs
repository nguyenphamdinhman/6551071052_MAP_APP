using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.TienNghi;

public partial class FormThemTienNghi : Form
{
    public FormThemTienNghi()
    {
        InitializeComponent();
        txtMaTN.MaxLength = 5;
        txtTenTN.MaxLength = 50;
        StartPosition = FormStartPosition.CenterParent;
        AcceptButton = btnThemMoi;
        CancelButton = btnThoat;
    }

    private void btnThemMoi_Click(object? sender, EventArgs e)
    {
        string ma = txtMaTN.Text.Trim().ToUpper();
        string ten = txtTenTN.Text.Trim();
        if (ma.Length == 0 || ten.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập đầy đủ mã và tên tiện nghi.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        try
        {
            using var db = new HotelManagementContext();
            var old = db.TienNghis.FirstOrDefault(x => x.MaTn == ma);
            if (old != null)
            {
                if (old.DaXoa != true)
                {
                    MessageBox.Show("Mã tiện nghi đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                old.TenTn = ten;
                old.DaXoa = false;
            }
            else db.TienNghis.Add(new Models.TienNghi { MaTn = ma, TenTn = ten, DaXoa = false });
            db.SaveChanges();
            MessageBox.Show("Thêm tiện nghi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (DbUpdateException ex) { MessageBox.Show("Lỗi thêm tiện nghi: " + (ex.InnerException?.Message ?? ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private void btnThoat_Click(object? sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }
}
