using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace LibraryManagement
{
    internal class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public string ISBN { get; set; }
        public int NumOfCopies { get; set; } = 1;
       
        public List<BorrowedBook> BorrowedCopies { get; set; } = new List<BorrowedBook>();

        public Book(string title,string writer,string isbn)
        {
            Title = title;
            Writer = writer;
            ISBN = isbn;
            ID = Library.GetAllOfBooks() == null ? 1 : Library.GetAllOfBooks().Count + 1;
        }

    }
}
