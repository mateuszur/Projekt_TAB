﻿using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowUsun.xaml
    /// </summary>
    public partial class WindowUsun : Window
    {
        string id;
      
      
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
                "UPDATE `Rezerwacje` SET  `czy_wazne` = 0  WHERE `Rezerwacje`.`id`=" + IdTextBox.Text + ";";

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
