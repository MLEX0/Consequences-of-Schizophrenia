using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using YariNovella.HelpClass;

namespace YariNovella
{
    /// <summary>
    /// Логика взаимодействия для RegistrationAuthorizationWin.xaml
    /// </summary>
    public partial class RegistrationAuthorizationWin : Window
    {
        List<HelpClass.PlayerClass> PlayerList = new List<HelpClass.PlayerClass>();

        int errorOfRead = 0;

        public RegistrationAuthorizationWin()
        {
            InitializeComponent();

            if (File.Exists("Data.txt") == true)
            {
                using (StreamReader sr = new StreamReader("Data.txt"))
                {
                    string[] words;
                    for (int i = 0; i < File.ReadLines("Data.txt").Count(); i++)
                    {
                        string str = sr.ReadLine();
                        words = str.Split(new char[] { '@' });
                        if (words[0] != "<DELETED>")
                        {
                            PlayerList.Add(new HelpClass.PlayerClass
                            {
                                Id = Convert.ToInt32(words[0]),
                                Name = words[1],
                                LastName = words[2],
                                Login = words[3],
                                Password = words[4],
                                PageNumber = Convert.ToInt32(words[5])
                            });
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Отсутствует лист, перезапустите программу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (File.Exists("Data.txt") == false)
            {
                File.Create("Data.txt");
                this.Close();
            }


            if (File.Exists("UserData.txt") == true)// защита от дауна
            {
                if (SaveFileClass.FileRead("UserData.txt") != null)
                {
                    try
                    {
                        Convert.ToInt32(SaveFileClass.FileRead("UserData.txt")); // проверка правильности данных в файле
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка сохранения пользователя, повторите вход!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        errorOfRead = 1;
                        File.Delete("UserData.txt");
                    }

                    if (errorOfRead == 0)
                    {
                        if (Convert.ToInt32(SaveFileClass.FileRead("UserData.txt")) > PlayerList.Count() || Convert.ToInt32(SaveFileClass.FileRead("UserData.txt")) < 0)// Проверка id 
                        {
                            MessageBox.Show("Сохранённый пользователь перестал существовать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            File.Delete("UserData.txt");
                        }
                        else
                        {
                            cbxRemind.IsChecked = true;
                            int saveId = Convert.ToInt32(SaveFileClass.FileRead("UserData.txt"));
                            var user = PlayerList.Where(u => u.Id == saveId).FirstOrDefault();
                            txtLogin.Text = user.Login;
                            pswPassword.Password = user.Password;
                        }
                    }
                }
            }
            else if (File.Exists("UserData.txt") == false)// Если файла не существует, создаёт файл
            {
                File.Create("UserData.txt");
            }
        }

        private void btnRegistration1_Click(object sender, RoutedEventArgs e)
        {
            Authorization.Visibility = Visibility.Hidden;
            brdAuthorization.Visibility = Visibility.Hidden;
            LoginAuthorization.Visibility = Visibility.Hidden;
            PasswordAuthorization.Visibility = Visibility.Hidden;
            SaveMeAutharization.Visibility = Visibility.Hidden;
            txtLogin.Visibility = Visibility.Hidden;
            pswPassword.Visibility = Visibility.Hidden;
            cbxRemind.Visibility = Visibility.Hidden;
            btnLogin1.Visibility = Visibility.Hidden;
            btnRegistration1.Visibility = Visibility.Hidden;
            btnClose1.Visibility = Visibility.Hidden;

            brdRegistration.Visibility = Visibility.Visible;
            Registration.Visibility = Visibility.Visible;
            RegistrationPass.Visibility = Visibility.Visible;
            FNRegistration.Visibility = Visibility.Visible;
            LNRegistration.Visibility = Visibility.Visible;
            txtFirstName.Visibility = Visibility.Visible;
            txtLastName.Visibility = Visibility.Visible;
            txtLogin1.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Visible;
            LoginRegistration.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Visible;
            btnEnter.Visibility = Visibility.Visible;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Authorization.Visibility = Visibility.Visible;
            brdAuthorization.Visibility = Visibility.Visible;
            LoginAuthorization.Visibility = Visibility.Visible;
            PasswordAuthorization.Visibility = Visibility.Visible;
            SaveMeAutharization.Visibility = Visibility.Visible;
            txtLogin.Visibility = Visibility.Visible;
            pswPassword.Visibility = Visibility.Visible;
            cbxRemind.Visibility = Visibility.Visible;
            btnLogin1.Visibility = Visibility.Visible;
            btnRegistration1.Visibility = Visibility.Visible;
            btnClose1.Visibility = Visibility.Visible;

            brdRegistration.Visibility = Visibility.Hidden;
            Registration.Visibility = Visibility.Hidden;
            RegistrationPass.Visibility = Visibility.Hidden;
            FNRegistration.Visibility = Visibility.Hidden;
            LNRegistration.Visibility = Visibility.Hidden;
            txtFirstName.Visibility = Visibility.Hidden;
            txtLastName.Visibility = Visibility.Hidden;
            txtLogin1.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Hidden;
            LoginRegistration.Visibility = Visibility.Hidden;
            btnLogin.Visibility = Visibility.Hidden;
            btnEnter.Visibility = Visibility.Hidden;
        }

        private void btnClose1_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin1_Click(object sender, RoutedEventArgs e)
        {
            var player = PlayerList.Where(u => u.Login == txtLogin.Text && u.Password == pswPassword.Password).FirstOrDefault();// Поиск по логину и паролю

            if (player != null)// проверка правильности ввода капчи и пароля
            {
                if (File.Exists("UserData.txt") == true)// Проверка существования файла!
                {
                    if (cbxRemind.IsChecked == true && SaveFileClass.FileRead("UserData.txt") == null && File.Exists("UserData.txt") == true)
                    {
                        SaveFileClass.FileReWrite(Convert.ToString(player.Id), "UserData.txt");// записывает id пользователя в файл
                    }
                    else if (cbxRemind.IsChecked == false)// удаление файла 
                    {
                        File.Delete("UserData.txt");
                    }
                }
                else
                {
                    if (cbxRemind.IsChecked == true && File.Exists("UserData.txt") == false)// Полная шляпа, когда пользователь трогает сраный файл!!!
                    {
                        MessageBox.Show("Внимание! \nИсполняемый файл занят системным процессом! " +
                            "\nПри следующей авторизации вам придётся ещё раз ввести ваши данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                MainWindow workwin = new MainWindow(player); // Переход на рабочее окно
                this.Hide();
                workwin.ShowDialog();
                this.Close();
            }
            else// при неправильном вводе пароля
            {
                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Data.txt") == true && txtFirstName.Text != "" && txtLastName.Text != "" && txtLogin1.Text != "" && txtPassword.Text != "")
            {
                SaveFileClass.FileWriteLine($"{(File.ReadLines("Data.txt").Count()) + 1}@{txtFirstName.Text}@{txtLastName.Text}@{txtLogin1.Text}@{txtPassword.Text}@0", "Data.txt");
                var currentuser = new PlayerClass { Id = (File.ReadLines("Data.txt").Count()) + 1, Name = txtFirstName.Text, LastName = txtLastName.Text, Login = txtLogin1.Text, Password = txtPassword.Text};

                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtPassword.Text = "";
                txtLogin.Text = "";

                MainWindow workwin = new MainWindow(currentuser);
                this.Hide();
                workwin.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
