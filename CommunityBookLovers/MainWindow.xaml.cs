using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityBookLovers.MessegeBoxes;

namespace CommunityBookLovers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string Email = txtEmail.Text;
            string pass = psw.Password;

            
            User user = AuthenticateUser(Email, pass);

            if (user != null)
            {
                MessegeAvtoriz messegeAvtoriz = new MessegeAvtoriz();
                messegeAvtoriz.Show();

               
                MainCommunityBook mainCommunityBook = new MainCommunityBook(user);
                mainCommunityBook.Show();
                this.Close();
            }
            else
            {
                
                MessageBox.Show("Неправильная почта или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        private User AuthenticateUser(string Email, string pass)
        {
            using (LibraryContext db = new LibraryContext())
            {
                
                return db.Users.FirstOrDefault(e => e.Email == Email && e.Password == pass);
            }
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}