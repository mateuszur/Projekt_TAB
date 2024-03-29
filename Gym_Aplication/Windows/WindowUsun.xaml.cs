﻿using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowUsun.xaml
    /// </summary>
    public partial class WindowUsun : Window
    {
        string id;


        ParametryFileManager fileManager = new ParametryFileManager();
        private string connection_string;

        MySqlConnection connection_name = new MySqlConnection();

        public WindowUsun()
        {
            InitializeComponent();
            connection_string = fileManager.OdczytajParametry();
            connection_name.ConnectionString = connection_string;
        }


        private void Zapisz_ClickG()
        {
            try
            {
                if (IdTextBox.Text == "")
                {
                    return;
                }

                connection_name.Open();

                string sql =
                "UPDATE `Rezerwacje` SET  `czy_wazne` = 0, `StartRezerwacji` = '0', `KoniecRezerwacji` = '0'  WHERE `Rezerwacje`.`id`=" + IdTextBox.Text + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();
                connection_name.Close();


                MessageBox.Show("Z powodzeniem usunięto rezerwację " + " ID: " + IdTextBox.Text + "!");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas usuwaniu rekordu w bazie danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                connection_name.Close();
                return;
            }
        }
        private void Zapisz_Click_I()
        {
            try
            {
                if (IdTextBox.Text == "")
                {
                    return;
                }

                connection_name.Open();

                string sql =
                "UPDATE `Rezerwacje_indywidualne` SET  `czy_wazne` = 0, `StartRezerwacji` = '0', `Koniec_Rezerwacji` = '0'  WHERE id=" + IdTextBox.Text + ";";

                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();
                connection_name.Close();


                MessageBox.Show("Z powodzeniem usunięto rezerwację " + " ID: " + IdTextBox.Text + "!");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas usuwaniu rekordu w bazie danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                connection_name.Close();
                return;
            }
        }

        private void Zapisz_Click_Usun(object sender, RoutedEventArgs e)
        {
            

            if (radioButtonI.IsChecked == true)
            {
                Zapisz_Click_I();
                IdTextBox.Text = "";

            }
            if (radioButtonG.IsChecked == true)
            {
                Zapisz_ClickG();
                IdTextBox.Text = "";
            }

        }


        private void RadioButton_Checked_Usun(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton == radioButtonI || radioButton==radioButtonG)
            {
                // Wybrano ścieżkę 1 - wypełnij formularz zgodnie z tą ścieżką

                IdTextBox.IsEnabled = true;


            }
           

        }

    }
}
