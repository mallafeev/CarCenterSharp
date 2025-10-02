using CarCenterBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        protected abstract void CreatePdf(PdfInfo info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfo info);
    }
}
