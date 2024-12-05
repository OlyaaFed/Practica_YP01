using CommunityBookLovers.MessegeBoxes;
using Microsoft.EntityFrameworkCore;
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

namespace CommunityBookLovers.LWindow
{
    /// <summary>
    /// Логика взаимодействия для CommunityBook.xaml
    /// </summary>
    public partial class CommunityBook : Window
    {
        private User currentUser;
        private LibraryContext dbContext;

        public CommunityBook(User user)
        {
            InitializeComponent();
            currentUser = user;
            dbContext = new LibraryContext();
            LoadFriendshipRequests();
            LoadFriendsList();
        }

        private void LoadFriendsList()
        {
            var friends = dbContext.Friendships
                .Where(f =>
                    (f.UserId == currentUser.UserId || f.FriendId == currentUser.UserId) &&
                    f.IsAccepted) 
                .Select(f => f.UserId == currentUser.UserId ? f.Friend : f.User) 
                .Distinct() 
                .ToList();

            FriendsList.ItemsSource = friends;
        }


        private void LoadFriendshipRequests()
        {
            var pendingRequests = dbContext.Friendships
                .Where(f => f.FriendId == currentUser.UserId && !f.IsAccepted) 
                .Include(f => f.User) 
                .ToList();

            FriendshipRequestsList.ItemsSource = pendingRequests;
        }


        private void AcceptRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int friendshipId = (int)button.Tag;

            var friendshipRequest = dbContext.Friendships.FirstOrDefault(f => f.FriendshipId == friendshipId);

            if (friendshipRequest != null)
            {
                friendshipRequest.IsAccepted = true;
                dbContext.SaveChanges();
                LoadFriendshipRequests();
                LoadFriendsList();
            }
        }

        private void RemoveFriendButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int friendId = (int)button.Tag;

            var friendship = dbContext.Friendships.FirstOrDefault(f =>
                (f.UserId == currentUser.UserId && f.FriendId == friendId) ||
                (f.UserId == friendId && f.FriendId == currentUser.UserId));

            if (friendship != null)
            {
                dbContext.Friendships.Remove(friendship);
                dbContext.SaveChanges();
                LoadFriendsList();
                MessageBox.Show("Друг удалён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(query))
            {
                var results = dbContext.Users
                    .Where(u => u.Name.ToLower().Contains(query) && u.UserId != currentUser.UserId)
                    .ToList();

                SearchResultsList.ItemsSource = results;
            }
        }

        private void SendFriendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int friendId = (int)button.Tag;  
            var existingFriendship = dbContext.Friendships
                .Any(f =>
                    (f.UserId == currentUser.UserId && f.FriendId == friendId && f.IsAccepted) || 
                    (f.UserId == friendId && f.FriendId == currentUser.UserId && f.IsAccepted));  

            if (existingFriendship)
            {
                MessageBox.Show("Вы уже друзья с этим пользователем.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var existingRequest = dbContext.Friendships
                .Any(f =>
                    (f.UserId == currentUser.UserId && f.FriendId == friendId && !f.IsAccepted) || 
                    (f.UserId == friendId && f.FriendId == currentUser.UserId && !f.IsAccepted));  

            if (existingRequest)
            {
                
                MessageBox.Show("Запрос на дружбу уже отправлен или принят.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            
            var newFriendship = new Friendship
            {
                UserId = currentUser.UserId,  
                FriendId = friendId,          
                IsAccepted = false            
            };


            dbContext.Friendships.Add(newFriendship);
            dbContext.SaveChanges();
            LoadFriendshipRequests();
            MessageBox.Show("Заявка на дружбу успешно отправлена!", "Заявка на дружбу", MessageBoxButton.OK, MessageBoxImage.Information);
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
            MessegeBoxFriend messegeBoxFriend = new MessegeBoxFriend();
            messegeBoxFriend.Show();
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
                    if (window is CommunityBook || window is CustomWin)
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
            string query = SearchBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(query))
            {
                
                var results = dbContext.Users
                    .Where(u => u.Name.ToLower().Contains(query) && u.UserId != currentUser.UserId)
                    .ToList();

               
                SearchResultsList.ItemsSource = results;

                if (results.Count == 0)
                {
                    MessageBox.Show("Пользователи не найдены.", "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Введите текст для поиска.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}

