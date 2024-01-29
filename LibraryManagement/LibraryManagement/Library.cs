using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class Library
    {
        private static List<Book> books = new List<Book>();
        private static string path = "../../../books.json";

        public Library()
        {

            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path);
                string jsonData = streamReader.ReadToEnd();
                books = JsonConvert.DeserializeObject<List<Book>>(jsonData);
                streamReader.Close();
            }
            else
            {
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.Close();
            }
            
        }
        public static List<Book> GetAllOfBooks()
        {
            return books;
        }

        public static void AddNewBook(Book book)
        {

            string jsonData = "";
            StreamWriter streamWriter;

            if (books == null)
            {
                books = new List<Book>();
            }
            foreach (Book b in books)
            {
                if (book.ISBN == b.ISBN)
                {
                    AddNewCopy(b);
                    jsonData = JsonConvert.SerializeObject(books);
                    streamWriter = new StreamWriter(path);
                    streamWriter.WriteLine(jsonData);
                    streamWriter.Close();
                    Console.WriteLine("\nKopya Başarıyla Eklendi!!");
                    return;
                }
            }

            books.Add(book);
            jsonData = JsonConvert.SerializeObject(books);
            streamWriter = new StreamWriter(path);
            streamWriter.WriteLine(jsonData);
            streamWriter.Close();
            Console.WriteLine("\nKitap Başarıyla Oluşturuldu!!");
        }

        public static void AddNewCopy(Book book)
        {
            books.First(x => x.ISBN == book.ISBN).NumOfCopies++;
        }

        public static List<Book> SearchBooks(string bookNameOrWriter)
        {
            bookNameOrWriter = bookNameOrWriter.ToLower().Trim();
            try
            {
                return books.Where(x => x.Title.ToLower() == bookNameOrWriter || x.Writer.ToLower() == bookNameOrWriter).ToList();
            }
            catch
            {
                Console.WriteLine("\nKitap Bulunamadı!!");
            }
            List<Book> emptyList = new List<Book> ();
            return emptyList;
        }

        public static void BorrowBook(int bookId,int memberId)
        {

            foreach ( Book b in books)
            {
                if (b.ID == bookId && b.NumOfCopies > 0)
                {
                    BorrowedBook borrowedBook = new BorrowedBook();

                    borrowedBook.BorrowerId = memberId;
                    borrowedBook.BorrowedDate = DateTime.Now;
                    borrowedBook.Title = b.Title;

                    b.BorrowedCopies.Add(borrowedBook);
                    b.NumOfCopies--;

                    string jsonData = JsonConvert.SerializeObject(books);
                    StreamWriter streamWriter = new StreamWriter(path);
                    streamWriter.WriteLine(jsonData);
                    streamWriter.Close();

                    Console.WriteLine("\nKitap Başarıyla Ödünç Alındı!!");

                    return;

                }
                 
            }
            Console.WriteLine("\nKitap Kütüphanede Bulunamadı!!");

        }

        public static void ReturnBook(int bookId,int memberId)
        {

            BorrowedBook borrowedBook =  books.First(x => x.ID == bookId).BorrowedCopies.First(x => x.BorrowerId == memberId);
            books.First(x => x.ID == bookId).BorrowedCopies.Remove(borrowedBook);
            books.First(x => x.ID == bookId).NumOfCopies++;

            string jsonData = JsonConvert.SerializeObject(books);
            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.WriteLine(jsonData);
            streamWriter.Close();

            Console.WriteLine("\nKitap Başarıyla İade Alındı!!");
        }

        public static List<BorrowedBook> GetExpiredBooks()
        {
            List<BorrowedBook> expiredBooks = new List<BorrowedBook>();
            foreach (Book b in books)
            {
                foreach(BorrowedBook bb in b.BorrowedCopies)
                {
                    var diffOfDates = DateTime.Now.Subtract(bb.BorrowedDate);
                    if(diffOfDates.Days > 30)
                    {
                        bb.ExpireDaysCount = Math.Abs(30 - diffOfDates.Days);
                        expiredBooks.Add(bb);                        
                    }
                }
            }
            return expiredBooks;
        }

    }
}
