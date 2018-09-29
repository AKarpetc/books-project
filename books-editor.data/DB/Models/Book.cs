using book_editor.data.DB.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.data.DB.Models
{
    public class Book: BaseTable
    {
        public Book()
        {
            Authors = new HashSet<Author>();
            Covers= new HashSet<Cover>();
        }
        [StringLength(30)]
        public string Header { get; set; }

        public int PageCount { get; set; }

        [StringLength(30)]
        public string PublishingOffice { get; set; }

        public int PublishYear { get; set; }

        public string ISBN { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Cover> Covers { get; set; }
    }

}
