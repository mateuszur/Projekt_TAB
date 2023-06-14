using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Windows;
using RazorEngine;
using System.Data;
using System.IO;
using RazorEngine.Templating;

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

                    string querry = "SELECT * FROM `rezerwacje2` where imie NOT LIKE '';";

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

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user_privilege == 1)
                {
                    WindowDodaj window = new WindowDodaj();
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  dodawania terminu!");
            }
        }

        private void Edituj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowEdituj window = new WindowEdituj();
            window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna edytowania terminu!");
            }
        }
        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user_privilege == 1)
                {
                    WindowUsun window = new WindowUsun();
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  usuwania terminu!");
            }
        }

        private void Export_to_Raport_H(object sender, EventArgs e)
        {

            try
            {
                connection_name.Open();

                string querry2 = "SELECT * FROM `rezerwacje2` WHERE imie NOT LIKE '';";

                MySqlCommand commend2 = new MySqlCommand(querry2, connection_name);



                MySqlDataAdapter adapter = new MySqlDataAdapter(commend2);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "NazwaTabeli");


                // Wczytaj zawartość szablonu HTML z pliku
                string templatePath = "../../../\\Paterns\\patern_Harmonogram_Raport.html";
                string template = File.ReadAllText(templatePath); // Wprowadź ścieżkę do swojego szablonu

                // Wygeneruj raport HTML z danymi
                string generatedHTML = Engine.Razor.RunCompile(template, "templateKey", null, dataSet.Tables["NazwaTabeli"]);

                string outputPath = "";
                // Configure save file dialog box
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.FileName = "raport_Rezerwacje_"; // Default file name
                dialog.DefaultExt = ".html"; // Default file extension
                dialog.Filter = "HTML documents (.html)|*.html"; // Filter files by extension

                // Show save file dialog box
                bool? result = dialog.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    outputPath = dialog.FileName;
                }

                // Zapisz raport HTML do wybranej lokalizacji
                if (!string.IsNullOrEmpty(outputPath))
                {
                    File.WriteAllText(outputPath, generatedHTML);
                    MessageBox.Show("Raport został zapisany.", "Sukces");
                }

                connection_name.Close();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Report could not be created");
            }

        }

    }
}
