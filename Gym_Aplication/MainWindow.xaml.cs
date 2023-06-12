using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using Gym_Aplication.Windows;
using Microsoft.ProjectServer.Client;
using LiveCharts.Wpf.Charts.Base;


namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {
        private string username = "";
        private string password = "";
        private int user_id = 0;
        private int user_privilege = 0;

        private ICollectionView _trainersView;
        private ICollectionView _membersView;

        private string connection_string =
        "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";

        MySqlConnection connection_name = new MySqlConnection();

        public double CurrentTemperature { get; set; }
        public double TomorrowTemperature { get; set; }

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
            Zarzadzanie.IsEnabled = true;
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
            Zarzadzanie.IsEnabled = false;
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
            Content_ArtykułyIPorady.Visibility = Visibility.Collapsed;
            Content_StatystykiAnalizy.Visibility = Visibility.Collapsed;
            Content_ObslugaKlienta.Visibility = Visibility.Collapsed;
            Content_RaportowanieAwarii.Visibility = Visibility.Collapsed;
            Content_Informacje.Visibility = Visibility.Collapsed;

            pageToShow.Visibility = Visibility.Visible;
        }

        public class ScaleConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double scale = (double)value;
                return Math.Min(scale, 1);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            username = UsernameTextBox.Text;
            password = PasswordBox.Visibility == Visibility.Visible ? PasswordBox.Password : PasswordTextBox.Text;
            NazwaUżytkownika.Content = username;

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                password = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            connection_name.ConnectionString = connection_string;

            try
            {
                connection_name.Open();
                {
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
                        MessageBox.Show("Logowanie zakończone pomyślnie. Witaj: " + username +
                                        " Twoje uprawnienia to: " + user_privilege);
                        EnableButtons();
                        connection_name.Close();

                        Content_Harmonogram.Visibility = Visibility.Visible;
                        Content_Logowanie.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Niepoprawna nazwa użytkownika lub hasła.");
                        connection_name.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia do bazy danych!");
            }
        }


        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Show the password
            PasswordTextBox.Text = PasswordBox.Password;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Hide the password
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
        }


        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            user_privilege = 0;
            user_id = 0;
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
            MessageBox.Show("Wylogowano.");

            ChangePageVisibility(Content_Logowanie);


            DisableButtons();
        }


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
            try
            {
                if (user_privilege == 1)
                {
                    connection_name.Open();

                    string querry = "SELECT * FROM `rezerwacje2`;";

                    MySqlCommand commend = new MySqlCommand(querry, connection_name);
                    MySqlDataReader data_from_querry = commend.ExecuteReader();

                    var listOfSchedule = new List<ScheduleEntry>();


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
                    MessageBox.Show("Brak uprawnień!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Otwieranie zarządzania hermonogramem...");
            }
        }





        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _trainersView.Filter = obj =>
            {
                TrainerManagement trainer = obj as TrainerManagement;
                return trainer != null && trainer.Name.StartsWith(FilterTextBox.Text);
            };
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _trainersView.SortDescriptions.Clear();
            if (SortComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _trainersView.SortDescriptions.Add(new SortDescription(selectedItem.Content.ToString(),
                    ListSortDirection.Ascending));
            }
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
            WindowMojProfil window = new WindowMojProfil();
            //window.Show(); // Zwykłe.
            window.ShowDialog(); // Blokowanie okna z czekaniem na rezultat.
        }

        private void Ustawienia_Click(object sender, RoutedEventArgs e)
        {
            WindowUstawienia window = new WindowUstawienia();
            window.ShowDialog();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            WindowPomoc window = new WindowPomoc();
            window.ShowDialog();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            WindowDodaj window = new WindowDodaj();
            window.Show();
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            WindowUsun window = new WindowUsun();
            window.Show();
        }

        private void Pobierz1_Click(object sender, RoutedEventArgs e)
        {
            WindowPobierz1 window = new WindowPobierz1();
            window.Show();
        }

        private void Edituj_Click(object sender, RoutedEventArgs e)
        {
            WindowEdituj window = new WindowEdituj();
            window.Show();
        }



        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
                "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                IssueImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
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




        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = ((System.Windows.Controls.Calendar)sender).SelectedDate.Value;
            // Get the information for the selected date
            string info = GetInfoForDate(selectedDate);
            MessageBox.Show(info, "Informacje dla " + selectedDate.ToShortDateString());
        }

        private string GetInfoForDate(DateTime date)
        {
            // TODO: Implement this method to return the information for the specified date
            return "Informacje dla " + date.ToShortDateString();
        }

        private void SaveNewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReminderDatePicker.SelectedDate.HasValue)
            {
                DateTime date = ReminderDatePicker.SelectedDate.Value;

                // If you don't have a time, you can default to the start of the day
                TimeSpan time = TimeSpan.Zero;

                DateTime reminderDateTime = date.Date + time;

                string note = NewsTextBox.Text;

                SaveReminder(reminderDateTime, note);

                NewsTextBox.Clear();
                ReminderDatePicker.SelectedDate = null;

                RefreshCalendar();
            }
            else
            {
                MessageBox.Show("Please select a date for the reminder.");
            }
        }


        private void SaveReminder(DateTime reminderDateTime, string note)
        {
        }

        private void RefreshCalendar()
        {
        }


        /*public async Task UpdateWeatherAsync()
        {
            string apiKey = "your_openweathermap_api_key";
            string city = "your_city_name";

            using (HttpClient client = new HttpClient())
            {
                // Get current weather
                string currentWeatherResponse =
                    await client.GetStringAsync(
                        $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric");
                dynamic currentWeather = JsonConvert.DeserializeObject(currentWeatherResponse);
                CurrentTemperature = currentWeather.main.temp;

                // Get weather forecast
                string forecastResponse =
                    await client.GetStringAsync(
                        $"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric");
                dynamic forecast = JsonConvert.DeserializeObject(forecastResponse);
                TomorrowTemperature =
                    forecast.list[8].main
                        .temp; // This is a simplified way to get tomorrow's temperature, it might not be accurate
            }

            return Task.CompletedTask;
        }*/
    }
}


/*Rezerwacja.IsEnabled = true;
Ocenianie.IsEnabled = true;
RezerwacjaZajec.IsEnabled = true;*/

/*Informacje.IsEnabled = false;
Rezerwacja.IsEnabled = false;
Ocenianie.IsEnabled = false;
RezerwacjaZajec.IsEnabled = false;*/


/*Content_RezerwacjaZajęć.Visibility = Visibility.Collapsed;
Content_RezerwacjaSprzetu.Visibility = Visibility.Collapsed;
Content_OcenianieTreningów.Visibility = Visibility.Collapsed;*/

/*
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
*/

/*
private void UsernameButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
    UserContextMenu.IsOpen = true;
}*/