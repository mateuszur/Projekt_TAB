using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Windows;

namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {
        private void Harmonogram_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_Harmonogram);
            try
            {
                if (user_privilege == 1)
                {
                    connection_name.Open();

                    string querry = "SELECT * FROM `rezerwacje2`;";

                    MySqlCommand commend = new MySqlCommand(querry, connection_name);
                    MySqlDataReader data_from_querry = commend.ExecuteReader();

                    var listOfSchedule = new List<ScheduleEntry>();


                    while (data_from_querry.Read())
                    {
                        ScheduleEntry feld = new ScheduleEntry
                        {
                            ID = (data_from_querry["id"]).ToString(),
                            Name = (data_from_querry["imie"]).ToString(),
                            Surname = (data_from_querry["nazwisko"]).ToString(),
                            Phone = data_from_querry["telefon"].ToString(),
                            Activity = (data_from_querry["Temat"]).ToString(),
                            Room = (data_from_querry["Sala"]).ToString(),
                            Date = (data_from_querry["DataRezerwacji"]).ToString(),
                            Start_time = (data_from_querry["start_time"]).ToString(),
                            End_time = (data_from_querry["end_time"]).ToString(),
                        };


                        listOfSchedule.Add(feld);
                    }


                    ScheduleDataGrid.ItemsSource = listOfSchedule;
                    connection_name.Close();
                }
                else
                {
                    MessageBox.Show("Brak uprawnień!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Otwieranie zarządzania hermonogramem...");
            }
        }
    }
}
