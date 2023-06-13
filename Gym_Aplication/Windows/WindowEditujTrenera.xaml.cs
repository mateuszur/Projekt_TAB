using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Gym_Aplication.Windows
{
    /// <summary>
    /// Interaction logic for EditujTrenera.xaml
    /// </summary>
    public partial class WindowEditujTrenera : Window
    {

        private string connection_string = "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";

        MySqlConnection connection_name = new MySqlConnection();
        string id;
        string imie;
        string nazwisko;
        string telefon;
        string email;
        string adres;
        string plec;


        DateTime dataUrodzenia_Day;
        DateTime dataZatrudnienia_Day;

        public WindowEditujTrenera()
        {
            InitializeComponent();
            connection_name.ConnectionString = connection_string;
        }


        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (IdTextBox.Text == "")
                {
                    return;
                }

                connection_name.Open();
                string sql = "SELECT * FROM `Trenerzy` where `id_trenera` = " + IdTextBox.Text + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();

                MySqlDataReader data_from_querry = command.ExecuteReader();

                while (data_from_querry.Read())
                {
                    ImieTextBox.Text = (data_from_querry["imie"]).ToString();
                    NazwiskoTextBox.Text = (data_from_querry["nazwisko"]).ToString();
                    TelefonTextBox.Text = data_from_querry["telefon"].ToString();
                    EmailTextBox.Text = data_from_querry["e-mail"].ToString();
                    DataUrodzeniaPicker.Text = data_from_querry["data_urodzenia"].ToString();
                    AdresTextBox.Text = data_from_querry["adres"].ToString();
                    DataZatrudnieniaPicker.Text = data_from_querry["data_zatrudenienia"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Najwyraźniej brak użytkownika o podanym ID...");
                return;
            }
            connection_name.Close();
        }

        private void Edit_Click_Zapisz_zmiany(object sender, RoutedEventArgs e)
        {
            try
            {
                id = IdTextBox.Text;
                imie = ImieTextBox.Text;
                nazwisko = NazwiskoTextBox.Text;
                telefon = TelefonTextBox.Text;
                email = EmailTextBox.Text;
                adres = AdresTextBox.Text;


                DateTime? dataUrodzenia = DataUrodzeniaPicker.SelectedDate; ;
                DateTime? dataZatrudnienia = DataZatrudnieniaPicker.SelectedDate; ;



                string email_pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                string phone_pattern = @"^\d{9}$";


                if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko))
                {
                    MessageBox.Show("Imię i Nazwisko są wymagane.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (string.IsNullOrWhiteSpace(telefon) || !Regex.IsMatch(telefon, phone_pattern) || telefon.Length != 9)
                {
                    MessageBox.Show("Brak lub błąd w numerze telefonu! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (!Regex.IsMatch(email, email_pattern) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Brak lub błąd w podanym adresie e-mail! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(adres))
                {
                    MessageBox.Show("Brak adresu! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!dataUrodzenia.HasValue || !dataZatrudnienia.HasValue)
                {
                    MessageBox.Show("Podaj poprawną datę urodzenia i/lub zatrudnienia!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    dataUrodzenia_Day = dataUrodzenia.Value;
                    dataZatrudnienia_Day = dataZatrudnienia.Value;

                }

                if (imie[imie.Length - 1] == 'a')
                {
                    plec = "K";
                }
                else
                {
                    plec = "M";
                }

            }
            catch (Exception ex1)
            {
                MessageBox.Show("Podczas przetwarzania formularza wystąpił błąd!", "Błąd walidacji!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                connection_name.Open();

                string sql =
                "UPDATE `Trenerzy` SET  `imie` = '" + imie + "'," + "`nazwisko` = '" + nazwisko + "'," + "`e-mail`= '" + email + "'," + " `telefon`= '" + telefon + "', `data_urodzenia`= '" + dataUrodzenia_Day.ToString("yyyy-MM-dd") + "', `plec`= '" + plec + "', `adres`= '" + adres + "', `data_zatrudnienia`= '" + dataZatrudnienia_Day.ToString("yyyy-MM-dd") + "' WHERE `Trenerzy`.`id_trenera`=" + id + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();
                connection_name.Close();


                MessageBox.Show("Z powodzeniem edytowano trenera " + imie + " ID: " + id + "!");
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas edytowania rekordu w bazie danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
