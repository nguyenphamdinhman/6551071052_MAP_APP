using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace HotelManagement.Properties;

internal static class Resources
{
    private static Bitmap CreateGraphic(int width, int height, Action<Graphics, int, int> drawAction)
    {
        var bmp = new Bitmap(width, height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            drawAction(g, width, height);
        }
        return bmp;
    }

    // 1. Ảnh minh họa màn hình đăng nhập & Chào đón (Phong cách Modern UI Clean)
    public static Image pictureIcon => CreateGraphic(600, 700, (g, w, h) =>
    {
        // Gradient nền trang nhã, dịu mắt
        using (var br = new LinearGradientBrush(new Rectangle(0, 0, w, h),
            Color.FromArgb(240, 244, 248), Color.FromArgb(218, 228, 240), LinearGradientMode.ForwardDiagonal))
        {
            g.FillRectangle(br, 0, 0, w, h);
        }

        // Vòng tròn ánh sáng mặt trời dịu nhẹ
        using (var sunBr = new SolidBrush(Color.FromArgb(35, 30, 144, 255)))
        {
            g.FillEllipse(sunBr, w / 2 - 200, 60, 400, 400);
        }
        using (var sunBr2 = new SolidBrush(Color.FromArgb(60, 70, 130, 180)))
        {
            g.FillEllipse(sunBr2, w / 2 - 150, 110, 300, 300);
        }

        // Tòa nhà phụ bên trái (Xanh ghi hiện đại)
        using (var buildingBr = new SolidBrush(Color.FromArgb(144, 164, 174)))
        {
            g.FillRoundedRectangle(buildingBr, 90, 260, 130, 320, 8);
        }
        // Tòa nhà phụ bên phải
        using (var buildingBr2 = new SolidBrush(Color.FromArgb(120, 144, 156)))
        {
            g.FillRoundedRectangle(buildingBr2, 380, 290, 130, 290, 8);
        }

        // Tòa khách sạn cao cấp trung tâm (Navy Blue hiện đại)
        using (var hotelBr = new LinearGradientBrush(new Rectangle(190, 170, 220, 410),
            Color.FromArgb(26, 82, 118), Color.FromArgb(19, 58, 83), LinearGradientMode.Vertical))
        {
            g.FillRoundedRectangle(hotelBr, 190, 170, 220, 410, 12);
        }

        // Cửa sổ kính phát sáng tinh tế
        using (var winBr = new SolidBrush(Color.FromArgb(220, 245, 250)))
        {
            for (int y = 230; y < 510; y += 38)
            {
                g.FillRoundedRectangle(winBr, 220, y, 42, 22, 4);
                g.FillRoundedRectangle(winBr, 280, y, 42, 22, 4);
                g.FillRoundedRectangle(winBr, 340, y, 42, 22, 4);
            }
        }

        // Bảng tên Khách sạn màu Vàng Gold
        using (var signBr = new SolidBrush(Color.FromArgb(212, 175, 55)))
        {
            g.FillRoundedRectangle(signBr, 225, 185, 150, 32, 6);
        }
        using (var font = new Font("Segoe UI", 12f, FontStyle.Bold))
        using (var textBr = new SolidBrush(Color.FromArgb(20, 30, 45)))
        using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
        {
            g.DrawString("HOTEL RESORT", font, textBr, new RectangleF(225, 185, 150, 32), sf);
        }

        // 5 Ngôi sao vàng đẳng cấp ★★★★★
        using (var starFont = new Font("Segoe UI", 16f, FontStyle.Bold))
        using (var starBr = new SolidBrush(Color.FromArgb(255, 193, 7)))
        using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
        {
            g.DrawString("★ ★ ★ ★ ★", starFont, starBr, new RectangleF(190, 520, 220, 30), sf);
        }

        // Nền cỏ & lối đi thanh lịch
        using (var groundBr = new SolidBrush(Color.FromArgb(67, 160, 71)))
        {
            g.FillRectangle(groundBr, 0, 570, w, h - 570);
        }
        using (var roadBr = new SolidBrush(Color.FromArgb(176, 190, 197)))
        {
            g.FillRectangle(roadBr, 0, 610, w, h - 610);
        }
    });

    // 2. Chữ nghệ thuật Đăng nhập
    public static Image DangNhap => CreateGraphic(203, 75, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var font = new Font("Segoe UI", 24f, FontStyle.Bold);
        using var shadowBr = new SolidBrush(Color.FromArgb(30, 0, 0, 0));
        using var textBr = new SolidBrush(Color.FromArgb(30, 130, 76));
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("Đăng Nhập", font, shadowBr, new RectangleF(2, 2, w, h), sf);
        g.DrawString("Đăng Nhập", font, textBr, new RectangleF(0, 0, w, h), sf);
    });

    // 3. Avatar Người dùng (User profile tròn bóng bẩy)
    public static Image user => CreateGraphic(87, 108, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using (var bgCircle = new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
        {
            g.FillEllipse(bgCircle, 6, 6, 75, 75);
        }
        using (var circleBr = new SolidBrush(Color.FromArgb(240, 243, 246)))
        {
            g.FillEllipse(circleBr, 11, 11, 65, 65);
        }
        using (var headBr = new SolidBrush(Color.FromArgb(44, 62, 80)))
        {
            g.FillEllipse(headBr, 30, 22, 27, 27);
        }
        using (var bodyBr = new SolidBrush(Color.FromArgb(44, 62, 80)))
        {
            g.FillPie(bodyBr, 18, 46, 51, 51, 180, 180);
        }
        using (var tieBr = new SolidBrush(Color.FromArgb(231, 76, 60)))
        {
            Point[] tie = { new Point(43, 48), new Point(46, 60), new Point(43, 66), new Point(40, 60) };
            g.FillPolygon(tieBr, tie);
        }
    });

    // 4. Hamburger Menu Icon
    public static Image Menu => CreateGraphic(48, 78, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var pen = new Pen(Color.FromArgb(230, 245, 240), 3.2f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
        g.DrawLine(pen, 12, 28, 36, 28);
        g.DrawLine(pen, 12, 38, 36, 38);
        g.DrawLine(pen, 12, 48, 36, 48);
    });

    // 5. Sidebar Menu Icons
    private static Bitmap CreateMenuIcon(string emoji)
    {
        return CreateGraphic(34, 34, (g, w, h) =>
        {
            g.Clear(Color.Transparent);
            using var font = new Font("Segoe UI Emoji", 13.5f);
            using var brush = new SolidBrush(Color.White);
            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(emoji, font, brush, new RectangleF(0, 0, w, h), sf);
        });
    }

    public static Image MenuTrangChu => CreateMenuIcon("🏠");
    public static Image MenuSoDoPhong => CreateMenuIcon("🗺️");
    public static Image MenuPhong => CreateMenuIcon("🚪");
    public static Image MenuLoaiPhong => CreateMenuIcon("🏷️");
    public static Image MenuDatPhong => CreateMenuIcon("📅");
    public static Image MenuDanhSachHoaDon => CreateMenuIcon("🧾");
    public static Image MenuQuanLyDichVu => CreateMenuIcon("☕");
    public static Image MenuTienNghi => CreateMenuIcon("🛋️");
    public static Image MenuQuanLyKhachHang => CreateMenuIcon("🛎️");
    public static Image MenuQuanLyNhanVien => CreateMenuIcon("👥");
    public static Image MenuQuanLyTaiKhoan => CreateMenuIcon("👤");
    public static Image MenuThongKe => CreateMenuIcon("📊");

    // 6. Action Icons cho Bảng & Thanh công cụ
    public static Image search => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var pen = new Pen(Color.FromArgb(52, 73, 94), 2.6f);
        g.DrawEllipse(pen, 6, 6, 14, 14);
        g.DrawLine(pen, 17, 17, 26, 26);
    });

    public static Image Phong => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var brush = new SolidBrush(Color.FromArgb(41, 128, 185));
        g.FillRoundedRectangle(brush, 4, 4, 24, 24, 6);
        using var font = new Font("Segoe UI Emoji", 11f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("🚪", font, Brushes.White, new RectangleF(0, 0, w, h), sf);
    });

    public static Image edit => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(234, 250, 234));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var pen = new Pen(Color.FromArgb(39, 174, 96), 1.5f);
        g.DrawEllipse(pen, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 11.5f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("✏️", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static Image delete => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(253, 237, 236));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var pen = new Pen(Color.FromArgb(231, 76, 60), 1.5f);
        g.DrawEllipse(pen, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 11.5f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("🗑️", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static Image Giuong => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(254, 249, 231));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 12f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("🛌", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static Image details => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(235, 245, 251));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 11.5f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("🛋️", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static Image Add => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(230, 247, 235));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var pen = new Pen(Color.FromArgb(39, 174, 96), 2f);
        g.DrawEllipse(pen, 2, 2, 28, 28);
        using var p = new Pen(Color.FromArgb(39, 174, 96), 2.5f);
        g.DrawLine(p, 16, 8, 16, 24);
        g.DrawLine(p, 8, 16, 24, 16);
    });

    public static Image Huy => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(253, 237, 236));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var pen = new Pen(Color.FromArgb(231, 76, 60), 2f);
        g.DrawEllipse(pen, 2, 2, 28, 28);
        using var p = new Pen(Color.FromArgb(231, 76, 60), 2.5f);
        g.DrawLine(p, 10, 10, 22, 22);
        g.DrawLine(p, 22, 10, 10, 22);
    });

    public static Image delete_icon => delete;

    public static Image DichVu => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(254, 243, 199));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 11.5f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("☕", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static Image TienNghi => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(235, 245, 251));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 11.5f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("🛋️", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static Image KhachHang => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(236, 253, 245));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 11.5f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("🛎️", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static Image NhanVien => CreateGraphic(32, 32, (g, w, h) =>
    {
        g.Clear(Color.Transparent);
        using var bgBr = new SolidBrush(Color.FromArgb(243, 232, 255));
        g.FillEllipse(bgBr, 2, 2, 28, 28);
        using var font = new Font("Segoe UI Emoji", 11.5f);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString("👥", font, Brushes.Black, new RectangleF(0, 0, w, h), sf);
    });

    public static void FillRoundedRectangle(this Graphics g, Brush brush, int x, int y, int width, int height, int radius)
    {
        using var path = new GraphicsPath();
        path.AddArc(x, y, radius, radius, 180, 90);
        path.AddArc(x + width - radius, y, radius, radius, 270, 90);
        path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
        path.AddArc(x, y + height - radius, radius, radius, 90, 90);
        path.CloseFigure();
        g.FillPath(brush, path);
    }
}
