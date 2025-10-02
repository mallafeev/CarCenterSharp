using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterContracts.BindingModels
{
    public class MailSendInfoModel
    {
        public string MailAddress { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public byte[] Pdf { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
