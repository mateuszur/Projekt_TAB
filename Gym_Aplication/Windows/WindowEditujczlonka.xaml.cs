using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowEditujczlonka.xaml
    /// </summary>
    public partial class WindowEditujczlonka : Window
    {

        ParametryFileManager fileManager = new ParametryFileManager();
        private string connection_string;

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
            connection_string = fileManager.OdczytajParametry();
            connection_name.ConnectionString = connection_string;
        }

        private void IdTextBox_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (IdTextBox.Text == "")
                    {
                        return;
                    }

                    connection_name.Open();
                    string sql0 = "SELECT COUNT(id_klienta) FROM `Klienci` WHERE  imie NOT LIKE \"\" AND id_klienta=" + IdTextBox.Text + ";";
                    string sql1 = "SELECT * FROM `Klienci` where `id_klienta` = " + IdTextBox.Text + ";";

                    MySqlCommand command0 = new MySqlCommand(sql0, connection_name);
                    MySqlDataReader data_from_querry0 = command0.ExecuteReader();


                    data_from_querry0.Read();
                    int number = data_from_querry0.GetInt32(0);

                    data_from_querry0.Close();

                    
                    if (number == 0)
                    {
                        MessageBox.Show("Najwyraźniej brak trenera o podanym ID...  ");
                        connection_name.Close();
                        ImieTextBox.Text = "";
                        NazwiskoTextBox.Text = "";
                        TelefonTextBox.Text = "";
                        EmailTextBox.Text = "";
                        AdresTextBox.Text = "";
                        PlecTextBox.Text = "";
                        DataPicker.Text = "";


                    }
                    else
                    {
                        MySqlCommand command1 = new MySqlCommand(sql1, connection_name);
                        MySqlDataReader data_from_querry1 = command1.ExecuteReader();


                        while (data_from_querry1.Read())
                        {
                            ImieTextBox.Text = (data_from_querry1["imie"]).ToString();
                            NazwiskoTextBox.Text = (data_from_querry1["nazwisko"]).ToString();
                            EmailTextBox.Text = data_from_querry1["e-mail"].ToString();
                            TelefonTextBox.Text = data_from_querry1["telefon"].ToString();
                            PlecTextBox.Text = data_from_querry1["plec"].ToString();
                            DataPicker.Text = data_from_querry1["data_utworzenia"].ToString();
                            AdresTextBox.Text = data_from_querry1["adres"].ToString();

                        }
                        connection_name.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Najwyraźniej brak członka o podanym ID...");
                    return;
                }
                connection_name.Close();
            }
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
