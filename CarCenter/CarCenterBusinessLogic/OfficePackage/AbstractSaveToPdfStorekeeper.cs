using CarCenterBusinessLogic.OfficePackage.HelperEnums;
using CarCenterBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdfStorekeeper
    {
        public void CreateDoc(PdfInfoStorekeeper info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph { Text = info.Title, Style = "NormalTitle", ParagraphAlignment = PdfParagraphAlignmentType.Center });
            CreateParagraph(new PdfParagraph { Text = $"с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

            CreateTable(new List<string> { "5cm", "10cm" });

            foreach (var report in info.reportBundling)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { "Номер комплектации", "Покупка" },
                    Style = "NormalTitle",
                    ParagraphAlignment = PdfParagraphAlignmentType.Center
                });
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { report.BundlingId.ToString(), "" },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
                foreach (var product in report.Orders)
                    CreateRow(new PdfRowParameters
                    {
                        Texts = new List<string> { "", product },
                        Style = "Normal",
                        ParagraphAlignment = PdfParagraphAlignmentType.Left
                    });
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { "", "Особенности" },
                    Style = "NormalTitle",
                    ParagraphAlignment = PdfParagraphAlignmentType.Center
                });
                foreach (var production in report.Features)
                    CreateRow(new PdfRowParameters
                    {
                        Texts = new List<string> { "", production },
                        Style = "Normal",
                        ParagraphAlignment = PdfParagraphAlignmentType.Left
                    });
            }
            SavePdf(info);
        }
        protected abstract void CreatePdf(PdfInfoStorekeeper info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfoStorekeeper info);
    }
}
