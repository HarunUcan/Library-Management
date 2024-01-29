using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class BorrowedBook
    {
        public string Title { get; set; }
        public DateTime BorrowedDate { get; set; }
        public int BorrowerId { get; set; }
        public int ExpireDaysCount { get; set; } = 0;

    }
}
