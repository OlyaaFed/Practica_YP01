using CommunityBookLovers.LWindow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
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
using CommunityBookLovers.MessegeBoxes;
namespace CommunityBookLovers
{
    /// <summary>
    /// Логика взаимодействия для AboutMeWindow.xaml
    /// </summary>
    public partial class AboutMeWindow : Window
    {
        private User currentUser;
        private bool isEditing = false;
        private LibraryContext _context;
        public AboutMeWindow(User user)
        {
            InitializeComponent();
            currentUser = user; 
            DisplayUserInfo(); 
        }

        private void DisplayUserInfo()
        {
            if (!isEditing)
            {

                txtName.Text = currentUser.Name;
                txtEmail.Text = currentUser.Email;

                if (!string.IsNullOrEmpty(currentUser.ProfileImagePath))
            {
                try
                {
                    Uri imageUri = new Uri(currentUser.ProfileImagePath);  

                   
                    if (File.Exists(currentUser.ProfileImagePath))
                    {
                        BitmapImage bitmap = new BitmapImage(imageUri);
                        ProfileImage.Source = bitmap;  
                    }
                    else
                    {
                        MessageBox.Show("Файл изображения не найден.");
                    }
                }
                catch (Exception ex) when (
                    ex is ArgumentException ||
                    ex is NotSupportedException ||
                    ex is SecurityException ||
                    ex is IOException)
                {
                    MessageBox.Show($"Произошла ошибка при загрузке изображения: {ex.Message}");
                }
            }
                btnEdit.Visibility = Visibility.Visible;
                txtName.IsReadOnly = true;
                txtEmail.IsReadOnly = true;
                btnSave.Visibility = Visibility.Collapsed;
                btnUploadImage.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtName.IsReadOnly = false;
                txtEmail.IsReadOnly = false;
                btnSave.Visibility = Visibility.Visible;
                btnUploadImage.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Collapsed;
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            isEditing = true; 
            DisplayUserInfo(); 
        } 
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentUser.Name = txtName.Text;
            currentUser.Email = txtEmail.Text;
            SaveUserData();
            isEditing = false;
            DisplayUserInfo(); 
        }

       
        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                currentUser.ProfileImagePath = imagePath;
                Uri imageUri = new Uri(imagePath);
                BitmapImage bitmap = new BitmapImage(imageUri);
                ProfileImage.Source = bitmap;
            }
        }

        private void SaveUserData()
        {
            using (LibraryContext db = new LibraryContext())
            {
                db.Users.Update(currentUser);
                db.SaveChanges(); 
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainCommunityBook mainCommunityBook = new MainCommunityBook(currentUser); 
            mainCommunityBook.Show();
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
                    if (window is AboutMeWindow || window is CustomWin)
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
        private void MenuItem_AboutMe_Click(object sender, RoutedEventArgs e)
        {
            MessegeAboutme messegeAboutme = new MessegeAboutme();
            messegeAboutme.Show();

        }

        private void MenuItem_MyShelf_Click(object sender, RoutedEventArgs e)
        {
            BookshelfWindow bookshelfWindow = new BookshelfWindow(currentUser);
            bookshelfWindow.Show();
            this.Close();
        }

        private void MenuItem_Books_Click(object sender, RoutedEventArgs e)
        {
            BookWindow bookWindow = new BookWindow(currentUser);
            bookWindow.Show();
            this.Close();
        }

        private void MenuItem_Friends_Click(object sender, RoutedEventArgs e)
        {
            var friendsWindow = new CommunityBook(currentUser);
            friendsWindow.Show();
            this.Close();
        }

        private void MenuItem_Review_Click(object sender, RoutedEventArgs e)
        {
            ReviewWindow reviewWindow = new ReviewWindow(currentUser, _context);
            reviewWindow.Show();
            this.Close();
        }
    }
}
