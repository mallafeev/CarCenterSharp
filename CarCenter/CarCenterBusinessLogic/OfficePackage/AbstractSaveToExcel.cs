using CarCenterBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        protected abstract void CreateExcel(ExcelInfo info);
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
