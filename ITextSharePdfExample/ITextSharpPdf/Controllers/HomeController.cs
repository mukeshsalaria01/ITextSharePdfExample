using System;
using System.IO;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WPW.Web.UI;

namespace ITextSharpPdf.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Generate Pdf using ITextSharp using asp.net mvc c#";

            return View();
        }

        public ActionResult GeneratePdf()
        {
            //var userTickets = JsonConvert.DeserializeObject<List<TicketsByNameDetails>>(jsonString);

            var doc = new Document(PageSize.A4, 10f, 10f, 120f, 100f);
            var strFilePath = Server.MapPath("~/PdfUploads/");

            var fileName = "Pdf_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".pdf";

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.UNDERLINE, BaseColor.BLACK);
            var h1Font = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.NORMAL);
            var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.NORMAL, BaseColor.DARK_GRAY);

            try
            {
                var pdfWriter = PdfWriter.GetInstance(doc, new FileStream(strFilePath + fileName, FileMode.Create));
                pdfWriter.PageEvent = new ITextEvents();
                doc.Open();

                var tblContainer = new PdfPTable(5) { TotalWidth = 520f, LockedWidth = true };
                float[] widths = { 90f, 150f, 120f, 95f, 65f };
                tblContainer.SetWidths(widths);
                var heading = new Phrase("LearnShareCorner demo code to Genereate Pdf using ITextSharp.", h1Font);
                var titleEmployee = new Phrase("Employee", titleFont);
                var titleName = new Phrase("Name", titleFont);
                var titleOccupation = new Phrase("Occupation", titleFont);
                var titleLa = new Phrase("Lapse Action", titleFont);
                var titleExpiryDate = new Phrase("Expiry Date", titleFont);
                var cellTicketName = new PdfPCell(heading) { Colspan = 5, Border = 0 };
                var cellTitleEmployee = new PdfPCell(titleEmployee);
                var cellTitleName = new PdfPCell(titleName);
                var cellTitleOccupation = new PdfPCell(titleOccupation);
                var cellTitleLa = new PdfPCell(titleLa);
                var cellTitleExpiryDate = new PdfPCell(titleExpiryDate);

                cellTitleEmployee.Border = 0;
                cellTitleName.Border = 0;
                cellTitleOccupation.Border = 0;
                cellTitleLa.Border = 0;
                cellTitleExpiryDate.Border = 0;

                tblContainer.AddCell(cellTicketName);

                tblContainer.AddCell(cellTitleEmployee);
                tblContainer.AddCell(cellTitleName);
                tblContainer.AddCell(cellTitleOccupation);
                tblContainer.AddCell(cellTitleLa);
                tblContainer.AddCell(cellTitleExpiryDate);

                doc.Add(tblContainer);

                var tblResult = new PdfPTable(5) { TotalWidth = 520f, LockedWidth = true };
                tblResult.SetWidths(widths);
                var employee = new Phrase("WebTechSys.in", bodyFont);
                var name = new Phrase("Mukesh Salaria", bodyFont);
                var occupation = new Phrase("Software Engineer", bodyFont);
                var la = new Phrase("None",bodyFont);

                var expiryDate =  new Phrase("N/A", bodyFont);
                var cellEmployee = new PdfPCell(employee);
                var cellName = new PdfPCell(name);
                var cellOccupation = new PdfPCell(occupation);
                var cellLa = new PdfPCell(la);
                var cellExpiryDate = new PdfPCell(expiryDate);

                cellEmployee.Border = 0;
                cellName.Border = 0;
                cellOccupation.Border = 0;
                cellLa.Border = 0;
                cellExpiryDate.Border = 0;
                tblResult.AddCell(cellEmployee);
                tblResult.AddCell(cellName);
                tblResult.AddCell(cellOccupation);
                tblResult.AddCell(cellLa);
                tblResult.AddCell(cellExpiryDate);

                doc.Add(tblResult);

                doc.Close();
                //return Json(new { success = "true", link = strFilePath + fileName });
                byte[] contents = System.IO.File.ReadAllBytes(strFilePath + fileName);
                return File(contents, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return HandleErrorCondition(ex.Message);
            }
            finally
            {
                doc.Close();
            }
        }

        private ActionResult HandleErrorCondition(string message)
        {
            throw new NotImplementedException();
        }
    }
}
