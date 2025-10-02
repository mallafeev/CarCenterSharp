using CarCenterBusinessLogic.OfficePackage.HelperEnums;
using CarCenterBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcelWorker
    {
        public void CreateReport(ExcelInfoWorker info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 2,
                Text = "Предпродажные",
                StyleInfo = ExcelStyleInfoType.Title
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "B",
                RowIndex = 2,
                Text = "Автомобили",
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "B2",
                CellToName = ColumnLetter(info.maxleng + 1) + "2"
            });
            uint rowIndex = 3;
            foreach (var pc in info.carPresalesReport)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = pc.PresaleId.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });
                int place = 2;
                foreach (var workshop in pc.Cars)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = ColumnLetter(place),
                        RowIndex = rowIndex,
                        Text = workshop,
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });

                    place++;
                }
                rowIndex++;
            }

            SaveExcel(info);
        }
        private static string ColumnLetter(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
        protected abstract void CreateExcel(ExcelInfoWorker info);
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        protected abstract void SaveExcel(ExcelInfoWorker info);
    }
}
