using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {
        public class ScheduleEntry
        {
            public string Time { get; set; }
            public string Activity { get; set; }
            public string Trainer { get; set; }
            public string Room { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            PopulateScheduleDataGrid();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if (username == "admin" && password == "password") 
            {
                MessageBox.Show("Logged in successfully.");
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void PopulateScheduleDataGrid()
        {
            var sampleSchedule = new List<ScheduleEntry>
            {
                new ScheduleEntry { Time = "8:00-9:00", Activity = "Yoga", Trainer = "Jan Kowalski", Room = "1" },
                new ScheduleEntry { Time = "10:00-11:00", Activity = "Zumba", Trainer = "Anna Nowak", Room = "2" },
                new ScheduleEntry { Time = "12:00-13:00", Activity = "Kickboxing", Trainer = "Piotr Wiśniewski", Room = "3" },
                new ScheduleEntry { Time = "14:00-15:00", Activity = "Spinning", Trainer = "Marta Zielińska", Room = "4" },
            };

            ScheduleDataGrid.ItemsSource = sampleSchedule;
        }

        
        private void SwitchTab(int tabIndex)
        {
            MainTabControl.SelectedIndex = tabIndex;
        }

        private void ReserveEquipment_Click(object sender, RoutedEventArgs e)
        {
            
            bool isEquipmentAvailable = true; 
            if (isEquipmentAvailable)
            {
                MessageBox.Show("Equipment reserved successfully.");
            }
            else
            {
                MessageBox.Show("Equipment is not available.");
            }
        }

        private void RateTraining_Click(object sender, RoutedEventArgs e)
        {
            
            int rating = 4; 
            MessageBox.Show($"Training rated {rating} out of 5.");
        }

        private void AddTrainer_Click(object sender, RoutedEventArgs e)
        {
            
            string trainerName = "New Trainer"; 
            MessageBox.Show($"Trainer {trainerName} added successfully.");
        }

        private void RemoveTrainer_Click(object sender, RoutedEventArgs e)
        {
            
            string trainerName = "Trainer to Remove"; 
            MessageBox.Show($"Trainer {trainerName} removed successfully.");
        }

        private void ReserveClass_Click(object sender, RoutedEventArgs e)
        {
            
            string className = "Yoga"; 
            MessageBox.Show($"{className} class reserved successfully.");
        }

        private void ReportIssue_Click(object sender, RoutedEventArgs e)
        {
            
            string equipmentName = "Treadmill"; 
            MessageBox.Show($"Issue with {equipmentName} reported successfully.");
        }


        private void Harmonogram_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(0);
            MessageBox.Show("Otwieranie harmonogramu...");
        }

        private void Logowanie_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(1);
            MessageBox.Show("Otwieranie okna logowania...");
        }

        private void Informacje_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(2);
            MessageBox.Show("Otwieranie informacji o siłowni...");
        }

        private void Rezerwacja_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(3);
            MessageBox.Show("Otwieranie rezerwacji sprzętu...");
        }

        private void Ocenianie_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(4);
            MessageBox.Show("Otwieranie oceniania treningów...");
        }

        private void Zarzadzanie_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(5);
            MessageBox.Show("Otwieranie zarządzania trenerami...");
        }

        private void RezerwacjaZajec_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(6);
            MessageBox.Show("Otwieranie rezerwacji zajęć...");
        }

        private void Raportowanie_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(7);
            MessageBox.Show("Otwieranie raportowania awarii...");
        }

        private void Artykuly_Click(object sender, RoutedEventArgs e)
        {
            SwitchTab(8);
            MessageBox.Show("Otwieranie artykułów i porad...");
        }

        private void Ustawienia_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Otwieranie ustawień aplikacji...");
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Otwieranie pomocy...");
        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wylogowywanie...");
        }
    }
}
