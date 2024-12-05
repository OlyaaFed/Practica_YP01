using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CommunityBookLovers
{
    /// <summary>
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private readonly LibraryContext _context;
        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        private string _imagePath;
        private User currentUser;

        public AddBookWindow(LibraryContext context)
        {
            InitializeComponent();
            DataContext = this;
            _context = context ?? throw new ArgumentNullException(nameof(context), "LibraryContext не может быть null");
            LoadAuthors();
            LoadGenres();
        }

        private void LoadAuthors()
        {
            try
            {
                Authors = new ObservableCollection<Author>(_context.Authors.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить авторов книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                AuthorComboBox.IsEnabled = false;
            }
        }

        private void LoadGenres()
        {
            try
            {
                Genres = new ObservableCollection<Genre>(_context.Genres.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить жанры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                GenreComboBox.IsEnabled = false;
            }
        }
       
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _imagePath = openFileDialog.FileName;
                var bitmap = new BitmapImage(new Uri(_imagePath));
                CoverImage.Source = bitmap;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;
            try
            {
                var book = new Book
                {
                    Title = TitleTextBox.Text,
                    AuthorId = (int?)AuthorComboBox.SelectedValue,
                    GenreId = (int?)GenreComboBox.SelectedValue,
                    PublicationYear = int.Parse(PublicationYearTextBox.Text),
                    Pages = int.Parse(PagesTextBox.Text),
                    Description = DescriptionTextBox.Text,
                    ImagePath = _imagePath
                };
                _context.Books.Add(book); 
                _context.SaveChanges();
                MessageBox.Show("Книга успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                var bookWindow = new BookWindow(currentUser);
                bookWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (AuthorComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите автора книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (GenreComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите жанр книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(PublicationYearTextBox.Text, out int publicationYear) || publicationYear <= 0)
            {
                MessageBox.Show("Введите корректный год издания книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(PagesTextBox.Text, out int pages) || pages <= 0)
            {
                MessageBox.Show("Введите корректное количество страниц в книге.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(_imagePath))
            {
                MessageBox.Show("Пожалуйста, выберите обложку книги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

    }
}


