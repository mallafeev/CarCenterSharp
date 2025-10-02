using CarCenterDataModels.Enums;
using CarCenterDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.ViewModels
{
    // создан для БУДУЩЕГО получения списков предпродажных по авто
    public class StorekeeperReportViewModel
    {
        public int? Id;
        public Dictionary<int, IPresaleModel> PresaleCars; // список предпродажных у авто
        public int? Count;
    }
}
