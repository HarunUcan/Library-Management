namespace LibraryManagement
{
    internal class Program
    {

        static void Main(string[] args)
        {
            
            Library lib = new Library();
            int choose = 0;

            while (true)
            {
                Console.Clear();
                Console.Write(  "1-Kitap Ekle\n" +
                                "2-Tüm Kitapları Listele\n" +
                                "3-Kitap Ara\n" +
                                "4-Kitap Ödünç Al\n" +
                                "5-Kitap İade Et\n" +
                                "6-Süresi Geçmiş Kitapları Listele\n" +
                                "0-Çıkış\n\n" +
                                "Yapmak İstediğiniz İşlemin Numarasını Giriniz : ");
                try
                {
                    choose = Convert.ToInt32(Console.ReadLine());                    
                }
                catch
                {
                    Console.Write("\nLütfen Geçerli Bir Sayı Girin!!\n\nDevam Etmek İçin Herhangi Bir Tuşa Basın..");
                    Console.ReadKey();
                    continue; 
                }

                switch (choose)
                {
                    case 1: AddBook(); break;
                    case 2: PrintAllOfBooks(); break;
                    case 3: PrintSearchBooks(); break;
                    case 4: BorrowBook(); break;
                    case 5: ReturnBook();break;
                    case 6: PrintExpiredBooks();break;
                    case 0: return;
                }

                Console.Write("\nDevam Etmek İçin Herhangi Bir Tuşa Basın..");
                Console.ReadKey();

            }

        }

        private static void AddBook()
        {
            Console.Write("Kitap İsmi : ");
            string bookName = Console.ReadLine();
            Console.Write("Yazar İsmi : ");
            string bookWriter = Console.ReadLine();
            Console.Write("ISBN : ");
            string bookIsbn = Console.ReadLine();


            Library.AddNewBook(new Book(bookName, bookWriter, bookIsbn));
        }

        private static void PrintAllOfBooks()
        {
            List<Book> _books = Library.GetAllOfBooks();

            if (_books != null && _books.Count > 0)
            {
                _books.ForEach(x => Console.WriteLine("\nID : " + x.ID + "\nİsim : " + x.Title + "\nYazar : " + x.Writer + "\nISBN : " + x.ISBN + "\nKopya Sayısı : " + x.NumOfCopies + "\n\n-------------------------------------"));
            }
            else
            {
                Console.WriteLine("Kitap Bulunamadı!!");
            }
        }

        private static void PrintSearchBooks()
        {
            Console.Write("Aramak istediğiniz kitabın adını yada yazarını giriniz : ");
            string bookNameOrWriter = Console.ReadLine();
            Library.SearchBooks(bookNameOrWriter).ForEach(x => Console.WriteLine("\nID : " + x.ID + "\nİsim : " + x.Title + "\nYazar : " + x.Writer + "\nISBN : " + x.ISBN + "\nKopya Sayısı : " + x.NumOfCopies + "\n\n-------------------------------------"));
        }

        private static void BorrowBook()
        {
            try
            {
                Console.Write("\nÖdünç Almak İstediğiniz Kitabın ID'sini Girin : ");
                int bookId = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nÖdünç Alan Üyenin ID'sini Girin : ");
                int memberId = Convert.ToInt32(Console.ReadLine());
                Library.BorrowBook(bookId,memberId);
            }
            catch
            {
                Console.Write("\nLütfen Geçerli Bir ID Girin!!");
            }

        }

        private static void ReturnBook()
        {
            try
            {
                Console.Write("\nGeri Vermek İstediğiniz Kitabın ID'sini Girin : ");
                int bookId = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nKitabı İade Eden Üyenin ID'sini Girin : ");
                int memberId = Convert.ToInt32(Console.ReadLine());
                Library.ReturnBook(bookId,memberId);
            }
            catch
            {
                Console.Write("\nLütfen Geçerli Bir İade Girin!!");
            }
        }

        private static void PrintExpiredBooks()
        {
            Console.WriteLine("----------------------------------------------------");
            Library.GetExpiredBooks().ForEach(x => Console.WriteLine("\nKitap Adı : " + x.Title + "\nÜye No : " + x.BorrowerId + "\nGeciken Gün : " + x.ExpireDaysCount + "\n\n----------------------------------------------------"));

        }

    }
}