using HotelManagement.Models;

namespace HotelManagement.TienNghi;

public partial class FormSuaTienNghi : Form
{
    private readonly string _ma;

    public FormSuaTienNghi(string ma)
    {
        InitializeComponent();
        _ma = ma;
        txtTenTN.MaxLength = 50;
        StartPosition = FormStartPosition.CenterParent;
        AcceptButton = btnCapNhat;
        CancelButton = btnThoat;
    }

    private void FormSuaTienNghi_Load(object? sender, EventArgs e)
    {
        try
        {
            using var db = new HotelManagementContext();
            var x = db.TienNghis.FirstOrDefault(t => t.MaTn == _ma && t.DaXoa != true);
            if (x == null) { MessageBox.Show("Không tìm thấy tiện nghi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); btnCapNhat.Enabled = false; return; }
            txtTenTN.Text = x.TenTn;
        }
        catch (Exception ex) { MessageBox.Show("Lỗi tải tiện nghi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private void btnCapNhat_Click(object? sender, EventArgs e)
    {
        string ten = txtTenTN.Text.Trim();
        if (ten.Length == 0) { MessageBox.Show("Vui lòng nhập tên tiện nghi.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        try
        {
            using var db = new HotelManagementContext();
            var x = db.TienNghis.FirstOrDefault(t => t.MaTn == _ma && t.DaXoa != true);
            if (x == null) return;
            x.TenTn = ten;
            db.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex) { MessageBox.Show("Lỗi cập nhật tiện nghi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private void btnThoat_Click(object? sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }
}
