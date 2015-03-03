using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PDFMaker.Models;

namespace PDFMaker.Infrastructure
{
    public class DocumentMaker
    {
        FileStream stream;
        UserViewModel model;
        Document doc = new Document(PageSize.A4, 15, 15, 15, 15);
        
        public DocumentMaker(string filePath, UserViewModel model)
        {
            this.model = model;
            stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);
            doc.Open();
        }

        public void MakePDF()
        {
            BaseFont baseFont = BaseFont.CreateFont
                (
                    @"C:\Windows\Fonts\arial.ttf", 
                    BaseFont.IDENTITY_H, 
                    BaseFont.NOT_EMBEDDED
                );
            
            Font font = new Font(baseFont, 10);

            Paragraph p1 = new Paragraph
                (
                "Статистика экзаменов для пользователя: " + model.User.UserId.ToString(), 
                new Font(baseFont, 10, Font.UNDERLINE)
                );
            p1.Alignment = Element.ALIGN_CENTER;
            p1.SpacingAfter = 20f;
            
            doc.Add(p1);
            PdfPTable table = new PdfPTable(4);
            
            table.AddCell(new Phrase("Название экзамена", font));
            table.AddCell(new Phrase("Результат экзамена", font));
            table.AddCell(new Phrase("Сдан/не сдан", font));
            table.AddCell(new Phrase("Дата сдачи", font));

            foreach (var item in model.Stats.CompletedExams)
            {
                table.AddCell(item.ExamName);
                table.AddCell(item.ExamResult.ToString() + "%");
                table.AddCell(item.IsPassed == true ? new Phrase("Да", font) : new Phrase("Нет", font));
                table.AddCell(item.ExamDate.ToShortDateString());
            }
            doc.Add(table);
            doc.Close();
            stream.Close();
            
    }


        
}

    
}