using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunityBookLovers.LWindow;
using Microsoft.EntityFrameworkCore;
using CommunityBookLovers.MessegeBoxes;
namespace CommunityBookLovers
{
    /// <summary>
    /// Логика взаимодействия для BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        private readonly User currentUser;
        private readonly LibraryContext _context;
        private LibraryContext? context;

        public BookWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            _context = new LibraryContext();
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                var books = _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .ToList();

                lstBooks.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке книг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LstBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                DisplayBookDetails(selectedBook);
            }
        }

        private void DisplayBookDetails(Book book)
        {
            if (book == null) return;

            txtBookTitle.Text = book.Title;
            txtBookAuthor.Text = book.Author?.Name ?? "Автор не указан";
            txtBookGenre.Text = book.Genre?.Name ?? "Жанр не указан";
            txtBookDescription.Text = book.Description ?? "Описание отсутствует";

            
            var bookshelfEntry = _context.Bookshelves
                .FirstOrDefault(bs => bs.BookId == book.BookId && bs.UserId == currentUser.UserId);

            if (bookshelfEntry != null)
            {
                cbBookStatus.SelectedItem = cbBookStatus.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == bookshelfEntry.State);
            }
            else
            {
                cbBookStatus.SelectedIndex = -1; 
            }
        }

        private void SaveStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                var selectedStatus = (cbBookStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (string.IsNullOrEmpty(selectedStatus))
                {
                    MessageBox.Show("Выберите статус книги!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    var bookshelfEntry = _context.Bookshelves
                        .FirstOrDefault(bs => bs.BookId == selectedBook.BookId && bs.UserId == currentUser.UserId);

                    if (bookshelfEntry == null)
                    {
                        bookshelfEntry = new Bookshelf
                        {
                            BookId = selectedBook.BookId,
                            UserId = currentUser.UserId,
                            DataAdded = DateOnly.FromDateTime(DateTime.Now),
                            State = selectedStatus
                        };
                        _context.Bookshelves.Add(bookshelfEntry);
                    }
                    else
                    {
                        bookshelfEntry.State = selectedStatus;
                    }

                    _context.SaveChanges();
                    MessageBox.Show("Статус книги обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении статуса книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_AboutMe_Click(object sender, RoutedEventArgs e)
        {
            AboutMeWindow aboutMeWindow = new AboutMeWindow(currentUser);
            aboutMeWindow.Show();
            this.Close();
        }

        private void MenuItem_MyShelf_Click(object sender, RoutedEventArgs e)
        {
            BookshelfWindow bookshelfWindow = new BookshelfWindow(currentUser);
            bookshelfWindow.Show();
            this.Close();
        }

        private void MenuItem_Books_Click(object sender, RoutedEventArgs e)
        {
            MessegeBook messegeBook = new MessegeBook();
            messegeBook.Show();
        }

        private void MenuItem_Friends_Click(object sender, RoutedEventArgs e)
        {
            var friendsWindow = new CommunityBook(currentUser);
            friendsWindow.Show();
            this.Close();
        }

        private void MenuItem_Review_Click(object sender, RoutedEventArgs e)
        {
            ReviewWindow reviewWindow = new ReviewWindow(currentUser, new LibraryContext());
            reviewWindow.Show();
            this.Close();
        }

       
            private void LogoutButton_Click(object sender, RoutedEventArgs e)
            {
                var customMessageBox = new CustomWin();
                customMessageBox.ShowDialog();

                if (customMessageBox.Result)
                {

                    foreach (var window in Application.Current.Windows)
                    {
                        if (window is BookWindow || window is CustomWin)
                        {
                            continue;
                        }


                        (window as Window)?.Close();
                    }

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
        

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            LibraryContext context = new LibraryContext();
            AddBookWindow addBookWindow = new AddBookWindow(context);
            addBookWindow.Show();
            this.Close();
        }

        private void ViewingReviewsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


