using Microsoft.SharePoint.Client;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.Primitives;

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

        private void LoadData_GodzinaGK()
        {
            DateTime? data_rezerwacji = DataPicker_Harmonogram.SelectedDate;
            DateTime data_rezerwacjiD;

            int count = 0;

            if (comboBoxTrener.SelectedItem != null)
            {
                GodzinaZakonczeniaTextBox.Items.Clear();
                try
                {

                    if (!data_rezerwacji.HasValue)
                    {
                        MessageBox.Show("Podaj poprawną datę!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        data_rezerwacjiD = data_rezerwacji.Value;


                    }


                    connection_name.Open();
                    string sql0 = "SELECT COUNT(id_trenera) AS ILE FROM `Widok_z_dwoch_rezerwacji`WHERE id_trenera= '" + selectedItemT.Id + "' AND DataRezerwacji= '" + data_rezerwacjiD.ToString("yyy-MM-dd") + "'; ";

                    string sql1 = "SELECT StartRezerwacji,KoniecRezerwacji FROM `Widok_z_dwoch_rezerwacji` where id_trenera= " + selectedItemT.Id + " AND DataRezerwacji=  '" + data_rezerwacjiD.ToString("yyy-MM-dd") + "';";

                    string sql2 = "SELECT end_time FROM `Przedział_czasu` where (id BETWEEN 25 AND 88) ";


                    MySqlCommand command0 = new MySqlCommand(sql0, connection_name);
                    MySqlDataReader reader0 = command0.ExecuteReader();




                    if (reader0.Read())
                    {
                        count = reader0.GetInt32(0);
                    }

                    reader0.Close();

                    if (count == 0)
                    {
                        sql2 = sql2 + ";";
                    }

                    MySqlCommand command1 = new MySqlCommand(sql1, connection_name);
                    MySqlDataReader reader1 = command1.ExecuteReader();

                    for (int i = 0; i < count; i++)
                    {
                        while (reader1.Read())
                        {
                            sql2 = sql2 + " AND ( id NOT BETWEEN " + reader1.GetString(0) + " AND " + reader1.GetString(1) + " )";
                        }
                    }
                    reader1.Close();
                    sql2 = sql2 + ";";


                    MySqlCommand command2 = new MySqlCommand(sql2, connection_name);
                    MySqlDataReader reader2 = command2.ExecuteReader();

                    while (reader2.Read())
                    {

                        string GodzinaGK = reader2.GetString(0);

                        GodzinaZakonczeniaTextBox.Items.Add(new ItemGK(GodzinaGK));
                    }

                    reader2.Close();

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

        private void LoadData_GodzinaS()
        {
            DateTime? data_rezerwacji = DataPicker_Harmonogram.SelectedDate;
            DateTime data_rezerwacjiD;

            int count=0;

            if (comboBoxTrener.SelectedItem != null)
            {
                GodzinarozpoczeciaTextBox.Items.Clear();
                try
                {
                   
                    if (!data_rezerwacji.HasValue)
                    {
                        MessageBox.Show("Podaj poprawną datę!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        data_rezerwacjiD = data_rezerwacji.Value;
                        

                    }
                    

                    connection_name.Open();
                    string sql0 = "SELECT COUNT(id_trenera) AS ILE FROM `Widok_z_dwoch_rezerwacji`WHERE id_trenera= '"+ selectedItemT.Id + "' AND DataRezerwacji= '"+ data_rezerwacjiD.ToString("yyy-MM-dd")+"'; ";
                    
                    string sql1 = "SELECT StartRezerwacji,KoniecRezerwacji FROM `Widok_z_dwoch_rezerwacji` where id_trenera= "+ selectedItemT.Id + " AND DataRezerwacji=  '"+ data_rezerwacjiD.ToString("yyy-MM-dd") + "';";
                    
                    string sql2 = "SELECT start_time FROM `Przedział_czasu` where (id BETWEEN 25 AND 88) ";


                    MySqlCommand command0 = new MySqlCommand(sql0, connection_name);
                    MySqlDataReader reader0 = command0.ExecuteReader();




                    if(reader0.Read())
                    {
                        count= reader0.GetInt32(0);
                    }

                    reader0.Close();

                    if(count == 0)
                    {
                        sql2= sql2 + ";";
                    }

                    MySqlCommand command1 = new MySqlCommand(sql1, connection_name);
                    MySqlDataReader reader1 = command1.ExecuteReader();

                    for (int i=0; i<count; i++)
                    {
                        while(reader1.Read())
                        {
                            sql2= sql2 + " AND ( id NOT BETWEEN " + reader1.GetString(0) + " AND "+reader1.GetString(1)+" )";
                        }
                    }
                    reader1.Close();
                    sql2=sql2 + ";";


                    MySqlCommand command2 = new MySqlCommand(sql2, connection_name);
                    MySqlDataReader reader2 = command2.ExecuteReader();

                    while (reader2.Read())
                    {

                        string GodzinaS = reader2.GetString(0);

                        GodzinarozpoczeciaTextBox.Items.Add(new ItemGS(GodzinaS));
                    }

                    reader2.Close();

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
           
                comboBoxZajecia.Items.Clear();
                try
                {

                    connection_name.Open();
                    string sql = "SELECT Temat FROM zajecia;";
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
            comboBoxTrener.Items.Clear();

            try
            {

                connection_name.Open();
                string sql = "SELECT DISTINCT id_trenera, imie, nazwisko FROM `Specjalnosci_trenerow`;";
                MySqlCommand command = new MySqlCommand(sql, connection_name);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
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

        private void LoadData_Treners_Group()
        {
            comboBoxTrener.Items.Clear();
            try
            {
                connection_name.Open();
                string sql = "SELECT id_trenera, imie, nazwisko FROM `Trenerzy` Where imie NOT LIKE \"\" AND czy_grupowe =1;";
                MySqlCommand command = new MySqlCommand(sql, connection_name);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
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
                if ( radioButtonTrener.IsChecked==true)
                {
                    LoadData_Zajecia_T();
                }
                if(radioButtonGroup.IsChecked == true) 
                {
                    LoadData_Zajecia_G();
                }
                DataPicker_Harmonogram.IsEnabled = true;
               
            }
        }


        private void comboBox_SelectionChanged_Sala(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxSala.SelectedItem != null)
            {
                selectedItemS = (ItemS)comboBoxSala.SelectedItem;
                
            }
        }

        private void comboBox_SelectionChanged_Klient(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxKlient.SelectedItem != null)
            {
                selectedItemK = (ItemK)comboBoxKlient.SelectedItem;
                
            }
        }

        private void comboBox_SelectionChanged_Zajecia(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (comboBoxZajecia.SelectedItem != null)
            {
                ItemZ selectedItem = (ItemZ)comboBoxZajecia.SelectedItem;
                
            }
        }


        private void DataPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
                LoadData_GodzinaS();
                LoadData_GodzinaGK();
            GodzinarozpoczeciaTextBox.IsEnabled = true;
            GodzinaZakonczeniaTextBox.IsEnabled = true;
             
            


        }

        private void comboBox_SelectionChanged_Godzina_Start(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GodzinarozpoczeciaTextBox.SelectedItem != null)
            {
                ItemGS selectedItem = (ItemGS)GodzinarozpoczeciaTextBox.SelectedItem;

               
            }
        }


        private void comboBox_SelectionChanged_Godzina_Stop(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GodzinaZakonczeniaTextBox.SelectedItem != null)
            {
                ItemGK selectedItem = (ItemGK)GodzinaZakonczeniaTextBox.SelectedItem;

                
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
                comboBoxTrener.IsEnabled = true;
                comboBoxKlient.IsEnabled = false;
                comboBoxZajecia.IsEnabled = true;
                LoadData_Sale();
                LoadData_Zajecia_G();
                LoadData_Treners_Group();


            }

        }


        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}