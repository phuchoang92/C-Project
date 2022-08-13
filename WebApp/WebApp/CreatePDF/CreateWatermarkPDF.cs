using iText.IO.Image;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;

using NhanVienAPI.Models;

namespace NhanVienAPI.CreatePDF
{
    public class CreateWatermarkPDF
    {
        public static readonly String FONT = "Resources/vuArial.ttf";
        public static readonly String IMG = "Resources/watermark2.png";
        public static readonly String SRC = "Resources/TLTVBQG_MoTaNghiepVu_2.4.pdf";

        public void CreatePDFFile(MemoryStream ms, NhanVien nhanVien)
        {
            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(SRC), writer);
            Document document = new Document(pdfDoc, PageSize.A4, false);
            PdfFont font = PdfFontFactory.CreateFont(FONT);
            writer.SetCloseStream(false);

            ImageData img = ImageDataFactory.Create(IMG);
            float widthImg = img.GetWidth();
            float heightImg = img.GetHeight();

            PdfExtGState gs1 = new PdfExtGState().SetFillOpacity(0.5f);

            int n = pdfDoc.GetNumberOfPages();
            
            for (int i = 1; i <= n; i++)
            {
                PdfPage pdfPage = pdfDoc.GetPage(i);
                Rectangle pageSize = pdfPage.GetPageSize();
                float x = (pageSize.GetLeft() + pageSize.GetRight()) / 2;
                float y = (pageSize.GetTop() + pageSize.GetBottom()) / 3;
                PdfCanvas over = new PdfCanvas(pdfPage);

                over.SaveState();
                over.SetExtGState(gs1);
                gs1.SetFillOpacity(0.2f);

                Paragraph paragraph = new Paragraph("Tên Nhân Viên: "+nhanVien.TenNhanVien).SetFont(font);
                Canvas canvasWatermark1 = new Canvas(over, pdfDoc.GetDefaultPageSize())
                    .ShowTextAligned(paragraph, x, y + 50, 1, TextAlignment.CENTER, VerticalAlignment.TOP, (float)Math.PI / 6);
                
                paragraph = new Paragraph("Tên Đăng Nhập: " + nhanVien.TenDangNhap).SetFont(font);
                canvasWatermark1.ShowTextAligned(paragraph, x, y + 25, 1, TextAlignment.CENTER, VerticalAlignment.TOP, (float)Math.PI / 6);

                paragraph = new Paragraph("Phòng Ban: " + nhanVien.PhongBan).SetFont(font);
                canvasWatermark1.ShowTextAligned(paragraph, x, y, 1, TextAlignment.CENTER, VerticalAlignment.TOP, (float)Math.PI/6);

                paragraph = new Paragraph(DateTime.Now.ToString("hh:mm:ss dd/mm/yyyy"))
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12);
                canvasWatermark1.ShowTextAligned(paragraph, x, y - 25, 1, TextAlignment.CENTER, VerticalAlignment.TOP, (float)Math.PI / 6);
                canvasWatermark1.Close();

                over.AddImageWithTransformationMatrix(img, widthImg, 0, 0, heightImg, x - (widthImg / 2), y - (heightImg / 2), true);
                over.RestoreState();
            }

            document.Close();
        }
    }
}
