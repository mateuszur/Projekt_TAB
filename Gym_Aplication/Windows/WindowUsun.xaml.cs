using Intersoft.Crosslight.Mobile;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowUsun.xaml
    /// </summary>
    public partial class WindowUsun : Window
    {
        string id;
        string imie = "";
        string nazwisko = "";
        //string telefon = "000000000";
        //string temat = "";
        //string sala = "";
        //string data_rezerwacji = "1999-05-08";
        

        private string connection_string = "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";

        MySqlConnection connection_name = new MySqlConnection();

        public WindowUsun()
        {
            InitializeComponent();
            connection_name.ConnectionString = connection_string;
        }
    

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IdTextBox.Text == "")
                {
                    return;
                }

                connection_name.Open();

                string sql =
                "UPDATE `rezerwacje2` SET  `imie` = '" + imie + "'," + "`nazwisko` = '" + nazwisko + "'"  + " WHERE `rezerwacje2`.`id`=" + IdTextBox.Text + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();
                connection_name.Close();


                MessageBox.Show("Z powodzeniem usunięto rezerwację "  + " ID: " + IdTextBox.Text + "!");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas usuwaniu rekordu w bazie danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
    }
