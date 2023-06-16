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
    /// Interaction logic for WindowDodaj.xaml
    /// </summary>
    public partial class WindowDodaj : Window
    {
        private ItemK selectedItemK;
        private ItemT selectedItemT;
        private ItemS selectedItemS;

        private string connection_string = "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";

        MySqlConnection connection_name = new MySqlConnection();

        public WindowDodaj()
        {
            InitializeComponent();
            connection_name.ConnectionString = connection_string;

        }
        private void LoadData_Zajecia_T()
        {
            if (comboBoxTrener.SelectedItem != null)
            {
                comboBoxZajecia.Items.Clear();
                try
            {
                
                    connection_name.Open();
                    string sql = "SELECT nazwa_specjalnosci FROM `Specjalnosci_trenerow` WHERE id_trenera=" + selectedItemT.Id + ";";
                    MySqlCommand command = new MySqlCommand(sql, connection_name);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        string Zajecia = reader.GetString(0);

                        comboBoxZajecia.Items.Add(new ItemZ(Zajecia));
                    }

                    reader.Close();

                }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection_name.Close();
            }
        }
        }

        private void LoadData_Zajecia_G()
        {
            if (comboBoxTrener.SelectedItem != null)
            {
                comboBoxZajecia.Items.Clear();
                try
                {

                    connection_name.Open();
                    string sql = "SELECT Temat FROM zajecia;;";
                    MySqlCommand command = new MySqlCommand(sql, connection_name);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        string Zajecia = reader.GetString(0);

                        comboBoxZajecia.Items.Add(new ItemZ(Zajecia));
                    }

                    reader.Close();

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection_name.Close();
                }
            }
        }

        private void LoadData_Klients()
        {
            try
            {
                connection_name.Open();
                string sql = "SELECT id_klienta, imie, nazwisko FROM `Klienci` WHERE imie NOT LIKE '';";
                MySqlCommand command = new MySqlCommand(sql, connection_name);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    comboBoxKlient.Items.Add(new ItemK(id, firstName, lastName));
                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection_name.Close();
            }
        }

        private void LoadData_Sale()
        {
            try
            {
                connection_name.Open();
                string sql = "SELECT id_sali, nazwa_sali FROM `Sala`;";
                MySqlCommand command = new MySqlCommand(sql, connection_name);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string salaName = reader.GetString(1);

                    comboBoxSala.Items.Add(new ItemS(id, salaName));
                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection_name.Close();
            }
        }

        private void LoadData_Treners()
        {
            try
            {
                connection_name.Open();
                string sql = "SELECT DISTINCT id_trenera, imie, nazwisko FROM `Specjalnosci_trenerow`;";
                MySqlCommand command= new MySqlCommand(sql, connection_name);
                MySqlDataReader reader = command.ExecuteReader();

               while(reader.Read())
{
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    comboBoxTrener.Items.Add(new ItemT(id, firstName, lastName));
                }

                reader.Close();
               
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection_name.Close();
                
            }
        }



        private void comboBox_SelectionChanged_Treners(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxTrener.SelectedItem != null)
            {
                selectedItemT = (ItemT)comboBoxTrener.SelectedItem;
                LoadData_Zajecia_T();
                MessageBox.Show("Selected ID: " + selectedItemT.Id + ", Name: " + selectedItemT.FullName);
            }
        }





        

        private void comboBox_SelectionChanged_Sala(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxSala.SelectedItem != null)
            {
                selectedItemS = (ItemS)comboBoxSala.SelectedItem;
                MessageBox.Show("Selected ID: " + selectedItemS.Id + ", Name: " + selectedItemS.SalaName);
            }
        }

        private void comboBox_SelectionChanged_Klient(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxKlient.SelectedItem != null)
            {
                 selectedItemK = (ItemK)comboBoxKlient.SelectedItem;
                MessageBox.Show("Selected ID: " + selectedItemK.Id + ", Name: " + selectedItemK.Name_Surname);
            }
        }

        private void comboBox_SelectionChanged_Zajecia(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            if (comboBoxZajecia.SelectedItem != null)
            {
                ItemZ selectedItem = (ItemZ)comboBoxZajecia.SelectedItem;
                MessageBox.Show(" Name: " + selectedItem.Zajecia);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton == radioButtonTrener)
            {
                // Wybrano ścieżkę 1 - wypełnij formularz zgodnie z tą ścieżką
                comboBoxTrener.IsEnabled = true;
                comboBoxSala.IsEnabled = false;

                comboBoxKlient.IsEnabled = true;
                comboBoxZajecia.IsEnabled = true;
                LoadData_Treners();
                LoadData_Klients();
              
               
                
            }
            else if (radioButton == radioButtonGroup)
            {
                // Wybrano ścieżkę 2 - wypełnij formularz zgodnie z tą ścieżką
                comboBoxSala.IsEnabled = true;
                comboBoxTrener.IsEnabled = false;
                comboBoxKlient.IsEnabled= false;
                comboBoxZajecia.IsEnabled= true;
                LoadData_Sale();
                LoadData_Zajecia_G();

                
            }

        }


        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}