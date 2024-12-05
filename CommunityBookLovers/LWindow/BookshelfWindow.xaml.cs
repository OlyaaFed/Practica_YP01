using CommunityBookLovers.LWindow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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

namespace CommunityBookLovers
{
    /// <summary>
    /// Логика взаимодействия для BookshelfWindow.xaml
    /// </summary>
    public partial class BookshelfWindow : Window
    {
        private readonly User currentUser;
        private readonly LibraryContext _context;
        private ObservableCollection<Bookshelf> bookshelf;

        public BookshelfWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            _context = new LibraryContext();
            LoadBookshelf();
        }

        private void LoadBookshelf()
        {
            try
            {
                var bookshelfData = _context.Bookshelves
                    .Include(bs => bs.Book)
                    .ThenInclude(b => b.Author)
                    .Where(bs => bs.UserId == currentUser.UserId)
                    .ToList();

                if (bookshelfData == null || bookshelfData.Count == 0)
                {
                    MessageBox.Show("На вашей полке пока нет книг.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return; // если книг нет, выходим
                }

                bookshelf = new ObservableCollection<Bookshelf>(bookshelfData);

                FilterBooksByStatus("Все книги");
                lstBookshelf.ItemsSource = bookshelf;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке полки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StatusSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
 
            ComboBox comboBox = (ComboBox)sender;
            string selectedStatus = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            FilterBooksByStatus(selectedStatus);
        }

        private void FilterBooksByStatus(string status)
        {
            if (bookshelf == null)
            {
                MessageBox.Show("Полка не была загружена или пустая.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ObservableCollection<Bookshelf> filteredBookshelf;

            switch (status)
            {
                case "Все книги":
                    filteredBookshelf = new ObservableCollection<Bookshelf>(bookshelf);
                    break;
                case "Прочитал":
                    filteredBookshelf = new ObservableCollection<Bookshelf>(bookshelf.Where(b => b.State == "Прочитал"));
                    break;
                case "Читаю":
                    filteredBookshelf = new ObservableCollection<Bookshelf>(bookshelf.Where(b => b.State == "Читаю"));
                    break;
                case "Хочу прочитать":
                    filteredBookshelf = new ObservableCollection<Bookshelf>(bookshelf.Where(b => b.State == "Хочу прочитать"));
                    break;
                default:
                    filteredBookshelf = new ObservableCollection<Bookshelf>(bookshelf);
                    break;
            }

            lstBookshelf.ItemsSource = filteredBookshelf;
        }

        private void LstBookshelf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBookshelf.SelectedItem is Bookshelf selectedEntry)
            {
                
                MessageBox.Show($"Вы выбрали книгу: {selectedEntry.Book.Title}\nСтатус: {selectedEntry.State}",
                                "Информация о книге",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        private void MenuItem_AboutMe_Click(object sender, RoutedEventArgs e)
        {
            AboutMeWindow aboutMeWindow = new AboutMeWindow(currentUser);
            aboutMeWindow.Show();
            Close();
        }

        private void MenuItem_MyShelf_Click(object sender, RoutedEventArgs e)
        {
            BookshelfMessageBox bookshelfMessageBox = new BookshelfMessageBox();
            bookshelfMessageBox.Show();
        }

        private void MenuItem_Books_Click(object sender, RoutedEventArgs e)
        {
            BookWindow bookWindow = new BookWindow(currentUser);
            bookWindow.Show();
            Close();
        }

        private void MenuItem_Friends_Click(object sender, RoutedEventArgs e)
        {
            var friendsWindow = new CommunityBook(currentUser);
            friendsWindow.Show();
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
                        if (window is BookshelfWindow|| window is CustomWin)
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
        

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstBookshelf.SelectedItem is Bookshelf selectedBookshelf)
            {
                try
                {
                   
                    _context.Bookshelves.Remove(selectedBookshelf);
                    _context.SaveChanges();

                 
                    LoadBookshelf();

                    MessageBox.Show("Книга успешно удалена с полки!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Review_Click(object sender, RoutedEventArgs e)
        {
            ReviewWindow reviewWindow = new ReviewWindow(currentUser, new LibraryContext());
            reviewWindow.Show();
            this.Close();
        }
    }

}
