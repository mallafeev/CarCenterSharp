using CarCenterBusinessLogic.OfficePackage.HelperEnums;
using CarCenterBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWordWorker
    {
        public void CreateDoc(WordInfoWorker info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {
                    (info.Title, new WordTextProperties { Bold = true, Size = "24", })
                },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Both
                }
            });
            foreach (var report in info.carPresalesReport)
            {
                CreateNumberedParagraph(1, 0, report.PresaleId.ToString());
                foreach (var workshop in report.Cars)
                {
                    CreateNumberedParagraph(1, 1, workshop);
                }

            }

            SaveWord(info);
        }
        protected abstract void CreateWord(WordInfoWorker info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void CreateNumberedParagraph(int numId, int ilvl, string text);
        protected abstract void SaveWord(WordInfoWorker info);
    }
}
