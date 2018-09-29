using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.data.DB.Models.Base
{
    public class BaseTable
    {
        public BaseTable()
        {
            AuditDateTime = DateTime.Now;

        }
        [Key]
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AuditDateTime { get; set; }

        /// <summary>
        /// Softly deleted
        /// </summary>
        public bool IsDelete { get; set; }

    }
}
