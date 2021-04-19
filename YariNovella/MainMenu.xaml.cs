using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YariNovella.Frames;

namespace YariNovella
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<HelpClass.PlayerClass> PlayerList = new List<HelpClass.PlayerClass>();
        List<HelpClass.LoreClass> LoreList = new List<HelpClass.LoreClass>();
        SoundPlayer sp = new SoundPlayer();
        int playerSaves;
        int endingcount = 0;
        int daycounter = 0;
        int endingchoise = 0;
        HelpClass.PlayerClass thisplayerData;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(HelpClass.PlayerClass player)
        {
            InitializeComponent();

            thisplayerData = player;


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
                MessageBox.Show("Отсутствует лист пользователя, перезапустите программу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (File.Exists("Data.txt") == false)
            {
                File.Create("Data.txt");
                this.Close();
            }




            sp.SoundLocation = "Sounds/BEST-RIDING-ROCK-MUSIC-_NCS-SONG_.wav";
            sp.PlayLooping();

            if (File.Exists("Lore.txt") == true)
            {
                using (StreamReader sr = new StreamReader("Lore.txt"))
                {
                    string[] words;
                    for (int i = 0; i < File.ReadLines("Lore.txt").Count(); i++)
                    {
                        string str = sr.ReadLine();
                        words = str.Split(new char[] { '@' });
                        if (words[0] != "<DELETED>")
                        {
                            LoreList.Add(new HelpClass.LoreClass
                            {
                                TextDialog = words[0],
                                PageNumber = Convert.ToInt32(words[1]),
                                PagePath = words[2],
                                PersonageName = words[3]
                            });
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Отсутствует сюжет, игра не запустится, поместите в папку bin/debug или bin/release лист сюжета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            txtWelcomeName.Text = $"{player.Name}!";
            playerSaves = player.PageNumber;
            //FramePage.Navigate(new _1());
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            if (playerSaves != 0)
            {
                var result = MessageBox.Show("ВНИМАНИЕ!\nЕсли вы начнёте новую игру, то все ваши старые сохранения сотруться!", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    playerSaves = 0;
                    grdMenu.Visibility = Visibility.Hidden;
                    PageSelecter("Comix1");
                    this.ResizeMode = ResizeMode.CanResize;
                }
            }
            else
            {
                playerSaves = 0;
                grdMenu.Visibility = Visibility.Hidden;
                PageSelecter("Comix1");
                this.ResizeMode = ResizeMode.CanResize;
            }    
        }



        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (playerSaves != 0)
            {
                playerSaves = thisplayerData.PageNumber - 1;
                grdMenu.Visibility = Visibility.Hidden;
                Next();
                this.ResizeMode = ResizeMode.CanResize;
            }
            else
            {
                MessageBox.Show("Нет сохранённой игры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            PlayerList[thisplayerData.Id - 1].PageNumber = playerSaves;

            if (File.Exists("Data.txt") == true)
            {
                using (StreamWriter sw = new StreamWriter("Data.txt", false))
                {
                    for (int i = 0; i < PlayerList.Count(); i++)
                    {
                        sw.WriteLine($"{PlayerList[i].Id}@{PlayerList[i].Name}@{PlayerList[i].LastName}@{PlayerList[i].Login}@{PlayerList[i].Password}@{PlayerList[i].PageNumber}");
                    }
                }
            }
        }



        private void PageSelecter(string pagepath) 
        {

            switch (pagepath)
            {
                case "Comix1":
                    MainFrame.Navigate(new Page1());
                    break;

                case "Comix2":
                    MainFrame.Navigate(new Page2());
                    break;

                case "Logo":
                    MainFrame.Navigate(new Page3());
                    break;

                case "1":
                    MainFrame.Navigate(new Page4());
                    break;

                case "2C":
                    MainFrame.Navigate(new Page5());
                    break;

                case "3":
                    MainFrame.Navigate(new Page6());
                    break;

                case "4":
                    MainFrame.Navigate(new Page7());
                    break;

                case "5":
                    MainFrame.Navigate(new Page8());
                    break;

                case "6":
                    MainFrame.Navigate(new Page9());
                    break;

                case "7":
                    MainFrame.Navigate(new Page10());
                    break;

                case "8":
                    MainFrame.Navigate(new Page11());
                    break;

                case "9":
                    MainFrame.Navigate(new Page12());
                    break;

                case "10":
                    MainFrame.Navigate(new Page13());
                    break;

                case "11":
                    MainFrame.Navigate(new Page14());
                    break;

                case "12":
                    MainFrame.Navigate(new Page15());
                    break;

                case "13":
                    MainFrame.Navigate(new Page16());
                    break;

                case "14":
                    MainFrame.Navigate(new Page17());
                    break;

                case "15":
                    MainFrame.Navigate(new Page18());
                    break;

                case "16":
                    MainFrame.Navigate(new Page19());
                    break;

                case "17":
                    MainFrame.Navigate(new Page20());
                    break;

                case "18":
                    MainFrame.Navigate(new Page21());
                    break;

                case "19":
                    MainFrame.Navigate(new Page22());
                    break;

                case "20":
                    MainFrame.Navigate(new Page23());
                    break;

            }

        }

        private void Choise(int choisenumber)
        {
            if (choisenumber == 5)
            {
                btn.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;
                wpChoise5.Visibility = Visibility.Visible;
            }
            if (choisenumber == 141)
            {
                btn.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;
                wpChoise141.Visibility = Visibility.Visible;
            }
        }

        private void Next()
        {
            if (playerSaves == 310)
            {

                btn.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;
                btnEndClose.Visibility = Visibility.Visible;
                MainFrame.Navigate(new Page24());
                sp.Stop();
                sp.SoundLocation = "Sounds/EndSound.wav";
                sp.Play();
            }
            else
            {
                playerSaves++;
                var thispage = LoreList[playerSaves];
                PageSelecter(thispage.PagePath);

                //MessageBox.Show(Convert.ToString(thispage.PageNumber));



                if (thispage.PageNumber == 295 && endingchoise == 0)
                {
                    playerSaves = 310;
                }
                if (thispage.PageNumber == 303 && endingchoise == 1)
                {
                    playerSaves = 310;
                }
                if (thispage.PageNumber == 311 && endingchoise == 2)
                {
                    playerSaves = 310;
                }

                if (thispage.PageNumber == 5)
                {
                    Choise(5);
                }
                if (thispage.PageNumber == 141)
                {
                    Choise(141);
                }
                if (thispage.PageNumber == 276)
                {
                    if (endingchoise == 0)
                    {
                        playerSaves = 276;
                    }
                    if (endingchoise == 1)
                    {
                        playerSaves = 295;
                    }
                    if (endingchoise == 2)
                    {
                        playerSaves = 303;
                    }
                }

                txtNamePerson.Text = thispage.PersonageName;
                if (thispage.TextDialog != "NOTEXT")
                {
                    txtDialog.Text = thispage.TextDialog;
                }
                else
                {
                    txtDialog.Text = "";
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Next();
        }

        private void Choise5_1_Click(object sender, RoutedEventArgs e)
        {
            Next();
            wpChoise5.Visibility = Visibility.Hidden;
            btn.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Visible;
        }

        private void Choise5_2_Click(object sender, RoutedEventArgs e)
        {
            endingcount++;
            if (endingcount > 5)
            {
                MessageBox.Show("Вы не проснулись, это был правильный выбор, до новых встреч!", "Конец!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                btn.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;
                btnEndClose.Visibility = Visibility.Visible;
                wpChoise5.Visibility = Visibility.Hidden;
                MainFrame.Navigate(new Page24());
                sp.Stop();
                sp.SoundLocation = "Sounds/EndSound.wav";
                sp.Play();
            }
        }

        private void Choise141_1_Click(object sender, RoutedEventArgs e)
        {
            if (daycounter == 6)
            {
                btn.Visibility = Visibility.Visible;
                wpChoise141.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                Next();
            }
            if (daycounter == 2)
            {
                daycounter++;
                endingchoise = 0;
                Next();
            }
            else
            {
                daycounter++;
                Next();
            }
        }

        private void Choise141_2_Click(object sender, RoutedEventArgs e)
        {
            if (daycounter == 6)
            {
                btn.Visibility = Visibility.Visible;
                wpChoise141.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                Next();
            }
            if (daycounter == 2)
            {
                daycounter++;
                endingchoise = 1;
                Next();
            }
            else 
            {
                daycounter++;
                Next();
            }
        }

        private void Choise141_4_Click(object sender, RoutedEventArgs e)
        {
            if (daycounter == 6)
            {
                btn.Visibility = Visibility.Visible;
                wpChoise141.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                Next();
            }
            if (daycounter == 2)
            {
                daycounter++;
                endingchoise = 2;
                Next();
            }
            else
            {
                daycounter++;
                Next();
            }
        }

        private void btnEndClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainFrame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && playerSaves != 310)
            {
                Next();
            }
            
        }
    }
}
