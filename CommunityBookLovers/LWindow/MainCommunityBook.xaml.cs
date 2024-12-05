using CommunityBookLovers.LWindow;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainCommunityBook.xaml
    /// </summary>
    public partial class MainCommunityBook : Window
    {
        private User currentUser;
        public MainCommunityBook(User user)
        {
            InitializeComponent();
            currentUser = user;
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


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var customMessageBox = new CustomWin();  
            customMessageBox.ShowDialog();  

            if (customMessageBox.Result)  
            {
                
                foreach (var window in Application.Current.Windows)
                {
                    if (window is MainCommunityBook || window is CustomWin)  
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





        private void MenuItem_Review_Click(object sender, RoutedEventArgs e)
        {
            ReviewWindow reviewWindow = new ReviewWindow(currentUser, new LibraryContext());
            reviewWindow.Show();
            this.Close();
        }
    }
}
