using book_editor.data.DB.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.data.DB.Models
{
    public class Cover: BaseTable
    {
        public byte[] File { get; set; }

        public string FileName { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

    }
}
