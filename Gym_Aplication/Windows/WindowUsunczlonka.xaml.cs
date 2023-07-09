using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowUsunczlonka.xaml
    /// </summary>
    public partial class WindowUsunczlonka : Window
    {

        ParametryFileManager fileManager = new ParametryFileManager();
        private string connection_string;

        MySqlConnection connection_name = new MySqlConnection();

        string id;
        string imie = "";
        string nazwisko = "";
        string telefon = "000000000";
        string email = "";
        string adres = "";
        string plec = "";

        public WindowUsunczlonka()
        {
            InitializeComponent();
            connection_string = fileManager.OdczytajParametry();
            connection_name.ConnectionString = connection_string;
        }
        private void Usun_Click_Zapisz_zmiany(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IdTextBox.Text == "")
                {
                    return;
                }

                connection_name.Open();
                string sql0 = "SELECT COUNT(id_klienta) FROM `Klienci` WHERE  imie NOT LIKE \"\" AND id_klienta=" + IdTextBox.Text + ";";
                string sql =
                "UPDATE `Klienci` SET  `imie` = '" + imie + "'," + "`nazwisko` = '" + nazwisko + "'," + "`e-mail`= '" + email + "'," + " `telefon`= '" + telefon + "',  `plec`= '" + plec + "', `adres`= '" + adres + "'" + " WHERE `Klienci`.`id_klienta`=" + IdTextBox.Text + ";";


                MySqlCommand command0 = new MySqlCommand(sql0, connection_name);
                MySqlDataReader data_from_querry0 = command0.ExecuteReader();

                data_from_querry0.Read();
                int number = data_from_querry0.GetInt32(0);
                data_from_querry0.Close();

                if (number == 0)
                {
                    MessageBox.Show("Najwyraźniej brak trenera o podanym ID...  ");
                    connection_name.Close();
                }
                else
                {

                    MySqlCommand command = new MySqlCommand(sql, connection_name);
                    command.ExecuteNonQuery();
                    connection_name.Close();


                    MessageBox.Show("Z powodzeniem usunięto trenera " + imie + " ID: " + IdTextBox.Text + "!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas usuwaniu rekordu w bazie danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
