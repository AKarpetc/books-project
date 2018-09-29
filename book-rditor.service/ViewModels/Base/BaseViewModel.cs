using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.ViewModels.Base
{
    public class BaseViewModel
    {
        public string Id { get; set; }

        public DateTime? AuditDateTime { get; set; }
    }
}
