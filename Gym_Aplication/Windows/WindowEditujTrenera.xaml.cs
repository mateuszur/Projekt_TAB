using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym_Aplication.Windows
{
    /// <summary>
    /// Interaction logic for EditujTrenera.xaml
    /// </summary>
    public partial class WindowEditujTrenera : Window
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
        int czy_grupowe;

        DateTime dataUrodzenia_Day;
        DateTime dataZatrudnienia_Day;

        public WindowEditujTrenera()
        {
            InitializeComponent();
            connection_string = fileManager.OdczytajParametry();
            connection_name.ConnectionString = connection_string;
        }


        //        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
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
                    string sql0 = "SELECT COUNT(id_trenera) FROM `Trenerzy` WHERE  imie NOT LIKE \"\" AND id_trenera=" + IdTextBox.Text + ";";
                    string sql1 = "SELECT * FROM `Trenerzy` where `id_trenera` = " + IdTextBox.Text + " AND imie NOT LIKE \"\";";


                    MySqlCommand command0 = new MySqlCommand(sql0, connection_name);
                    MySqlDataReader data_from_querry0 = command0.ExecuteReader();

                    data_from_querry0.Read();
                    int number = data_from_querry0.GetInt32(0);

                    data_from_querry0.Close();
                    if (number == 0)
                    {
                        MessageBox.Show("Najwyraźniej brak trenera o podanym ID...  " );
                        connection_name.Close();
                        ImieTextBox.Text = "";
                        NazwiskoTextBox.Text = "";
                        TelefonTextBox.Text = "";
                        EmailTextBox.Text = "";
                        AdresTextBox.Text = "";
                        DataUrodzeniaPicker.Text = "";
                        DataZatrudnieniaPicker.Text = "";
                    }
                    else
                    {
                        //  connection_name.Open();
                        MySqlCommand command1 = new MySqlCommand(sql1, connection_name);
                        MySqlDataReader data_from_querry1 = command1.ExecuteReader();

                        while (data_from_querry1.Read())
                        {
                            ImieTextBox.Text = (data_from_querry1["imie"]).ToString();
                            NazwiskoTextBox.Text = (data_from_querry1["nazwisko"]).ToString();
                            TelefonTextBox.Text = data_from_querry1["telefon"].ToString();
                            EmailTextBox.Text = data_from_querry1["e-mail"].ToString();
                            DataUrodzeniaPicker.Text = data_from_querry1["data_urodzenia"].ToString();
                            AdresTextBox.Text = data_from_querry1["adres"].ToString();
                            DataZatrudnieniaPicker.Text = data_from_querry1["data_zatrudnienia"].ToString();
                            czy_grupowe = int.Parse(data_from_querry1["czy_grupowe"].ToString());

                            if (czy_grupowe == 0)
                            {
                                ZajeciaComboBox.SelectedIndex = 1;
                            }
                            else
                            {
                                ZajeciaComboBox.SelectedIndex = 0;
                                czy_grupowe = 1;
                            }

                        }
                        connection_name.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd podczas edycji! \n" + ex.Message);
                    connection_name.Close();
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

                if (ZajeciaComboBox.SelectedItem != null)
                {
                    ComboBoxItem selectedOption = (ComboBoxItem)ZajeciaComboBox.SelectedItem;
                    string selectedValue = selectedOption.Content.ToString();

                    if (selectedValue == "Tak")
                    {
                        czy_grupowe = 1;
                    }
                    else
                    {
                        czy_grupowe = 0;
                    }
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
                "UPDATE `Trenerzy` SET  `imie` = '" + imie + "'," + "`nazwisko` = '" + nazwisko + "'," + "`e-mail`= '" + email + "'," + " `telefon`= '" + telefon + "', `data_urodzenia`= '" + dataUrodzenia_Day.ToString("yyyy-MM-dd") + "', `plec`= '" + plec + "', `adres`= '" + adres + "', `data_zatrudnienia`= '" + dataZatrudnienia_Day.ToString("yyyy-MM-dd") + "', `czy_grupowe`= "+ czy_grupowe + " WHERE `Trenerzy`.`id_trenera`=" + id + ";";

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
