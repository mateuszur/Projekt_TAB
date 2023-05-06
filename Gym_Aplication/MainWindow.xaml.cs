using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {
        private string username = "";
        private string password = "";
        private int user_id = 0;
        private int user_privilege = 0;

        private string connection_string =
            "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";

        MySqlConnection connection_name = new MySqlConnection();

        public MainWindow()
        {
            InitializeComponent();
            //PopulateScheduleDataGrid();
        }

        private void EnableButtons()
        {
            Harmonogram.IsEnabled = true;
            Logowanie.IsEnabled = false;
            Informacje.IsEnabled = true;
            Rezerwacja.IsEnabled = true;
            Ocenianie.IsEnabled = true;
            Zarzadzanie.IsEnabled = true;
            RezerwacjaZajec.IsEnabled = true;
            Raportowanie.IsEnabled = true;
            Artykuly.IsEnabled = true;
            Czlonek.IsEnabled = true;
            Statystyki.IsEnabled = true;
            ObslugaKlienta.IsEnabled = true;
            UserContextMenu.IsEnabled = true;

            if (user_privilege == 1)
            {
                Zarzadzanie.IsEnabled = true;
                Raportowanie.IsEnabled = true;
            }
            else
            {
                Zarzadzanie.IsEnabled = false;
                Raportowanie.IsEnabled = false;
            }
        }

        private void DisableButtons()
        {
            Harmonogram.IsEnabled = false;
            Logowanie.IsEnabled = true;
            Informacje.IsEnabled = false;
            Rezerwacja.IsEnabled = false;
            Ocenianie.IsEnabled = false;
            Zarzadzanie.IsEnabled = false;
            RezerwacjaZajec.IsEnabled = false;
            Raportowanie.IsEnabled = false;
            Artykuly.IsEnabled = false;
            Czlonek.IsEnabled = false;
            Statystyki.IsEnabled = false;
            ObslugaKlienta.IsEnabled = false;
            UserContextMenu.IsEnabled = false;
        }

        private void ChangePageVisibility(Grid pageToShow)
        {
            Content_Logowanie.Visibility = Visibility.Collapsed;
            Content_Harmonogram.Visibility = Visibility.Collapsed;
            Content_ZarządzanieTrenerami.Visibility = Visibility.Collapsed;
            Content_ZarzadzanieCzlonkami.Visibility = Visibility.Collapsed;
            Content_RezerwacjaZajęć.Visibility = Visibility.Collapsed;
            Content_RezerwacjaSprzetu.Visibility = Visibility.Collapsed;
            Content_OcenianieTreningów.Visibility = Visibility.Collapsed;
            Content_ArtykułyIPorady.Visibility = Visibility.Collapsed;
            Content_StatystykiAnalizy.Visibility = Visibility.Collapsed;
            Content_ObslugaKlienta.Visibility = Visibility.Collapsed;
            Content_RaportowanieAwarii.Visibility = Visibility.Collapsed;
            Content_Informacje.Visibility = Visibility.Collapsed;

            pageToShow.Visibility = Visibility.Visible;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            username = UsernameTextBox.Text;
            password = PasswordTextBox.Password;
            NazwaUżytkownika.Content = username;

            Content_Logowanie.Visibility = Visibility.Visible;
            DisableButtons();

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                password = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            connection_name.ConnectionString = connection_string;

            connection_name.Open();

            string query1 = "SELECT * FROM `Uzytkownicy` WHERE Nazwa LIKE @username AND Hasło LIKE @password;";
            MySqlCommand command = new MySqlCommand(query1, connection_name);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            MySqlDataReader data_from_query1 = command.ExecuteReader();

            while (data_from_query1.Read())
            {
                user_id = (int)data_from_query1["ID"];
                user_privilege = (int)data_from_query1["Uprawnienia"];
            }

            if (user_id != 0)
            {
                MessageBox.Show("Logowanie zakończone pomyślnie. Witaj: " + username + " Twoje uprawnienia to: " +
                                user_privilege);
                EnableButtons();
                connection_name.Close();
            }
            else
            {
                MessageBox.Show("Niepoprawna nazwa użytkownika lub hasła.");
                connection_name.Close();
            }
        }

        //private void PopulateScheduleDataGrid()
        //{
        //    var sampleSchedule = new List<ScheduleEntry>
        //    {
        //        new ScheduleEntry { Time = "8:00-9:00", Activity = "Yoga", Trainer = "Jan Kowalski", Room = "1" },
        //        new ScheduleEntry { Time = "10:00-11:00", Activity = "Zumba", Trainer = "Anna Nowak", Room = "2" },
        //        new ScheduleEntry
        //            { Time = "12:00-13:00", Activity = "Kickboxing", Trainer = "Piotr Wiśniewski", Room = "3" },
        //        new ScheduleEntry
        //            { Time = "14:00-15:00", Activity = "Spinning", Trainer = "Marta Zielińska", Room = "4" },
        //    };

        //    ScheduleDataGrid.ItemsSource = sampleSchedule;
        //}

        private void UsernameButton_Click(object sender, RoutedEventArgs e)
        {
            UserContextMenu.IsOpen = true;
        }

        private void Logowanie_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_Logowanie);
        }

        private void Harmonogram_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_Harmonogram);

            if (user_privilege == 1)
            {
                connection_name.Open();

                string querry = "SELECT * FROM `rezerwacje2`;";

                MySqlCommand commend = new MySqlCommand(querry, connection_name);
                MySqlDataReader data_from_querry = commend.ExecuteReader();

                var listOfSchedule = new List<ScheduleEntry>();

                string ID = "";
                string Name = "";
                string Surname = "";
                string Phone = "";
                string Activity = "";

                string Room = "";

                string Date = "";

                string Start_time = "";
                string End_time = "";



                while (data_from_querry.Read())
                {



                    ScheduleEntry feld = new ScheduleEntry
                    {
                        ID = (data_from_querry["id"]).ToString(),
                        Name = (data_from_querry["imie"]).ToString(),
                        Surname = (data_from_querry["nazwisko"]).ToString(),
                        Phone = data_from_querry["telefon"].ToString(),
                        Activity = (data_from_querry["Temat"]).ToString(),
                        Room = (data_from_querry["Sala"]).ToString(),
                        Date = (data_from_querry["DataRezerwacji"]).ToString(),
                        Start_time = (data_from_querry["start_time"]).ToString(),
                        End_time = (data_from_querry["end_time"]).ToString(),
                    };
                



                listOfSchedule.Add(feld);
            }


                ScheduleDataGrid.ItemsSource = listOfSchedule;
                connection_name.Close();
         
}
            else
            {
                MessageBox.Show("Otwieranie zarządzania trenerami...");
            }



        
}


        private void Zarzadzanie_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_ZarządzanieTrenerami);

            if (user_privilege == 1)
            {
                connection_name.Open();

                string querry2 = "SELECT * FROM `Trenerzy`;";

                MySqlCommand commend2 = new MySqlCommand(querry2, connection_name);
                MySqlDataReader data_from_querry2 = commend2.ExecuteReader();

                var listOfTrainers = new List<TrainerManagement>();

                string name = "";

                string surname = "";
                string phone = "";
                string e_mail = "";
                string dateOfBirth = "";
                string address = "";
                string dateOfEmployment= "";

                while (data_from_querry2.Read())
                {
                     name = (data_from_querry2["imie"]).ToString();

                     surname = (data_from_querry2["nazwisko"]).ToString();
                     phone = data_from_querry2["telefon"].ToString();
                     e_mail = data_from_querry2["e-mail"].ToString();
                     dateOfBirth = data_from_querry2["data_urodzenia"].ToString();
                     address = data_from_querry2["adres"].ToString();
                     dateOfEmployment = data_from_querry2["data_zatrudenienia"].ToString();

                    TrainerManagement feld = new TrainerManagement
                    {
                        Name = name,
                        Surname = surname,
                        Phone = phone,
                        E_Mail = e_mail,
                        DateOfBirth = dateOfBirth,
                        Address = address,
                        DateOfEmployment = dateOfEmployment
                    };

                    listOfTrainers.Add(feld);
                }

                
                TrenersData.ItemsSource =listOfTrainers;
                connection_name.Close();
            }
            else
            {
                MessageBox.Show("Otwieranie zarządzania trenerami...");
            }
        }

        private void Czlonek_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_ZarzadzanieCzlonkami);
        }

        private void RezerwacjaZajec_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_RezerwacjaZajęć);
        }

        private void Rezerwacja_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_RezerwacjaSprzetu);
        }

        private void Ocenianie_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_OcenianieTreningów);
        }

        private void Artykuly_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_ArtykułyIPorady);
        }

        private void Statystyki_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Statystyki_Click fired.");
            ChangePageVisibility(Content_StatystykiAnalizy);
        }

        private void ObslugaKlienta_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_ObslugaKlienta);
        }

        private void Raportowanie_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_RaportowanieAwarii);
        }

        private void Informacje_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_Informacje);
        }

        private void MojProfil_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Page_MojProfil();
        }

        private void Ustawienia_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Page_Ustawienia();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Page_Pomoc();
        }


        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            user_privilege = 0;
            user_id = 0;
            UsernameTextBox.Text = "";
            PasswordTextBox.Password = "";
            MessageBox.Show("Wylogowywanie...");
        }

        private void AddTrainer_Click(object sender, RoutedEventArgs e)
        {
            string trainerName = "Nowy trener";
            MessageBox.Show($"Pomyślnie dodano trenera {trainerName}.");
        }

        private void RemoveTrainer_Click(object sender, RoutedEventArgs e)
        {
            string trainerName = "Trener do usunięcia";
            MessageBox.Show($"Trener {trainerName} został pomyślnie usunięty.");
        }

        private void ReserveClass_Click(object sender, RoutedEventArgs e)
        {
            string className = "Yoga";
            MessageBox.Show($"{className} klasa zarezerwowana pomyślnie.");
        }

        private void ReportIssue_Click(object sender, RoutedEventArgs e)
        {
            string equipmentName = "Treadmill";
            MessageBox.Show($"Pomyślnie zgłoszono problem z urządzeniem {equipmentName}.");
        }

        private void RateTraining_Click(object sender, RoutedEventArgs e)
        {
            int rating = 4;
            MessageBox.Show($"Szkolenie oceniono na {rating} na 5.");
        }

        //Rezerwuj
        private void ReserveEquipment_Click(object sender, RoutedEventArgs e)
        {
            bool isEquipmentAvailable = true;
            if (isEquipmentAvailable)
            {
                MessageBox.Show("Pomyślnie zarezerwowano sprzęt.");
            }
            else
            {
                MessageBox.Show("Sprzęt nie jest dostępny.");
            }
        }

        private void WyszukajKlientaTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WyszukajKlientaTextBox.Text == "Wpisz imię, nazwisko lub email klienta")
            {
                WyszukajKlientaTextBox.Text = "";
                WyszukajKlientaTextBox.Foreground = Brushes.Black;
            }
        }

        private void WyszukajKlientaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WyszukajKlientaTextBox.Text))
            {
                WyszukajKlientaTextBox.Text = "Wpisz imię, nazwisko lub email klienta";
                WyszukajKlientaTextBox.Foreground = Brushes.Gray;
            }
        }

        /*
        private void UsernameButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserContextMenu.IsOpen = true;
        }*/
    }
}