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

        private string connection_string = "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";

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

                string sql =
                "UPDATE `Klienci` SET  `imie` = '" + imie + "'," + "`nazwisko` = '" + nazwisko + "'," + "`e-mail`= '" + email + "'," + " `telefon`= '" + telefon + "',  `plec`= '" + plec + "', `adres`= '" + adres + "'" + " WHERE `Klienci`.`id_klienta`=" + IdTextBox.Text + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();
                connection_name.Close();


                MessageBox.Show("Z powodzeniem usunięto trenera " + imie + " ID: " + IdTextBox.Text + "!");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas usuwaniu rekordu w bazie danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
