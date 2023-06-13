using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowEditujczlonka.xaml
    /// </summary>
    public partial class WindowEditujczlonka : Window
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


        string dataUtworzenia_Day;


        public WindowEditujczlonka()
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
                string sql = "SELECT * FROM `Klienci` where `id_klienta` = " + IdTextBox.Text + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();

                MySqlDataReader data_from_querry = command.ExecuteReader();

                while (data_from_querry.Read())
                {
                    ImieTextBox.Text = (data_from_querry["imie"]).ToString();
                    NazwiskoTextBox.Text = (data_from_querry["nazwisko"]).ToString();
                    EmailTextBox.Text = data_from_querry["e-mail"].ToString();
                    TelefonTextBox.Text = data_from_querry["telefon"].ToString();
                    PlecTextBox.Text = data_from_querry["plec"].ToString();
                    DataPicker.Text = data_from_querry["data_utworzenia"].ToString();
                    AdresTextBox.Text = data_from_querry["adres"].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Najwyraźniej brak członka o podanym ID...");
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
                plec = PlecTextBox.Text;

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



                if (!Regex.IsMatch(plec, "^[MK]$"))
                {
                    MessageBox.Show("Płeć przyjmuje wartości K (kobieta) lub M (mężczyzna)! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
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
                "UPDATE `Klienci` SET  `imie` = '" + imie + "'," + "`nazwisko` = '" + nazwisko + "'," + "`e-mail`= '" + email + "'," + " `telefon`= '" + telefon + " ', `plec`= '" + plec + "', `adres`= '" + adres + "' WHERE `Klienci`.`id_klienta`=" + id + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();
                connection_name.Close();


                MessageBox.Show("Z powodzeniem edytowano klienta " + imie + " ID: " + id + "!");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas edytowania rekordu w bazie danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
