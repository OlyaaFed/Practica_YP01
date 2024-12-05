using ClassLibrary1;

namespace _2111
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestHighRatedBooks()
        {
            var book = new Book { BookId = 1, Title = "Война и Мир" };
            var review1 = new Review { ReviewId = 1, BookId = book.BookId, Rating = 5, Text = "Шедевр!" };
            var review2 = new Review { ReviewId = 2, BookId = book.BookId, Rating = 4, Text = "Очень хорошая книга." };
            var review3 = new Review { ReviewId = 3, BookId = book.BookId, Rating = 2, Text = "Не понравилось." };

            book.Reviews = new List<Review> { review1, review2, review3 };

            var threshold = 4;
            var highRatedReviews = book.Reviews.Where(r => r.Rating >= threshold).ToList();

            Console.WriteLine($"Book '{book.Title}' has {highRatedReviews.Count} reviews with rating >= {threshold}:");
            foreach (var review in highRatedReviews)
            {
                Console.WriteLine($"- Review ID: {review.ReviewId}, Rating: {review.Rating}, Text: '{review.Text}'");
            }
        }

        [TestMethod]
        public void TestBooksByGenre()
        {
            var genre1 = new Genre { GenreId = 1, Name = "Роман" };
            var genre2 = new Genre { GenreId = 2, Name = "Фантастика" };

            var book1 = new Book { BookId = 1, Title = "Война и Мир", Genre = genre1 };
            var book2 = new Book { BookId = 2, Title = "1984", Genre = genre2 };
            var book3 = new Book { BookId = 3, Title = "Гарри Поттер", Genre = genre2 };

            var allBooks = new List<Book> { book1, book2, book3 };
            var romanBooks = allBooks.Where(b => b.Genre?.Name == "Роман").ToList();

            Console.WriteLine("Books in 'Роман' genre:");
            foreach (var book in romanBooks)
            {
                Console.WriteLine($"- {book.Title}");
            }
        }

        [TestMethod]
        public void TestAddAndRemoveReview()
        {
            var book = new Book { BookId = 1, Title = "Война и Мир" };
            var user = new User { UserId = 1, Name = "Лев Толстой" };
            var review1 = new Review { ReviewId = 1, BookId = book.BookId, UserId = user.UserId, Rating = 5, Text = "Шедевр!" };
            var review2 = new Review { ReviewId = 2, BookId = book.BookId, UserId = user.UserId, Rating = 4, Text = "Отлично, но не без недостатков." };
            book.Reviews.Add(review1);
            book.Reviews.Add(review2);
            Console.WriteLine($"Book '{book.Title}' has {book.Reviews.Count} reviews.");
            book.Reviews.Remove(review1);
            Console.WriteLine($"After removal, the book '{book.Title}' has {book.Reviews.Count} reviews.");
        }
        [TestMethod]
        public void TestAddBookToBookshelf()
        {
            var book = new Book { BookId = 1, Title = "Война и Мир" };
            var user = new User { UserId = 1, Name = "Лев Толстой" };

            var shelf1 = new Bookshelf
            {
                Bookshelf1 = 1,
                Book = book,
                User = user,
                State = "Читаю",
                DataAdded = DateOnly.FromDateTime(DateTime.Now)
            };

            var shelf2 = new Bookshelf
            {
                Bookshelf1 = 2,
                Book = book,
                User = user,
                State = "Прочитано",
                DataAdded = DateOnly.FromDateTime(DateTime.Now)
            };

            book.Bookshelves.Add(shelf1);
            book.Bookshelves.Add(shelf2);

            Console.WriteLine($"Book '{book.Title}' is on {book.Bookshelves.Count} shelves.");
            foreach (var shelf in book.Bookshelves)
            {
                Console.WriteLine($"Shelf {shelf.Bookshelf1}: {shelf.State}");
            }
        }

    }
}
