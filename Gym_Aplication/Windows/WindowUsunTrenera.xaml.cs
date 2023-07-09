using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Gym_Aplication.Windows
{
    /// <summary>
    /// Interaction logic for UsunTrenera.xaml
    /// </summary>
    public partial class WindowUsunTrenera : Window
    {
        ParametryFileManager fileManager = new ParametryFileManager();
        private string connection_string;

        MySqlConnection connection_name = new MySqlConnection();



        string id;
        string imie="";
        string nazwisko="";
        string telefon = "000000000";
        string email = "";
        string adres = "";
        string plec = "";


       string dataUrodzenia_Day = "1999-05-08";
        string dataZatrudnienia_Day = "";

        public WindowUsunTrenera()
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
                string sql0 = "SELECT COUNT(id_trenera) FROM `Trenerzy` WHERE  imie NOT LIKE \"\" AND id_trenera=" + IdTextBox.Text + ";";
                string sql1 =
                "UPDATE `Trenerzy` SET  `imie` = '" + imie + "'," + "`nazwisko` = '" + nazwisko + "'," + "`e-mail`= '" + email + "'," + " `telefon`= '" + telefon + "',  `plec`= '" + plec + "', `adres`= '" + adres + "'"+" WHERE `Trenerzy`.`id_trenera`=" + IdTextBox.Text + ";";


                MySqlCommand command0 = new MySqlCommand(sql0, connection_name);
                MySqlDataReader data_from_querry0 = command0.ExecuteReader();


                data_from_querry0.Read();
                int number = data_from_querry0.GetInt32(0);
                data_from_querry0.Close();

                if (number == 0)
                {
                    MessageBox.Show("Najwyraźniej brak trenera o podanym ID...  " );
                    connection_name.Close();
                }
                else
                {
                    MySqlCommand command1 = new MySqlCommand(sql1, connection_name);
                    command1.ExecuteNonQuery();
                    connection_name.Close();
                    MessageBox.Show("Z powodzeniem usunięto trenera " + imie + " ID: " + IdTextBox.Text + "!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas usuwaniu rekordu w bazie danych wystąpił błąd! \n" + ex.Message, "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }


}
    