using Gym_Aplication.Windows;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Data;

using RazorEngine;
using RazorEngine.Templating;
using System.IO;

namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {

        private void Zarzadzanie_Click(object sender, RoutedEventArgs e)

        {
            ChangePageVisibility(Content_ZarządzanieTrenerami);

            try
            {
                if (user_privilege == 1 || user_privilege == 2)
                {
                    connection_name.Open();

                    string querry2 = "SELECT * FROM `Trenerzy` WHERE imie NOT LIKE '';";

                    MySqlCommand commend2 = new MySqlCommand(querry2, connection_name);
                    MySqlDataReader data_from_querry2 = commend2.ExecuteReader();

                    var listOfTrainers = new List<TrainerManagement>();

                    while (data_from_querry2.Read())
                    {
                        TrainerManagement feld = new TrainerManagement
                        {
                            ID = int.Parse(data_from_querry2["id_trenera"].ToString()),
                            Name = (data_from_querry2["imie"]).ToString(),
                            Surname = (data_from_querry2["nazwisko"]).ToString(),
                            Phone = data_from_querry2["telefon"].ToString(),
                            E_Mail = data_from_querry2["e-mail"].ToString(),
                            DateOfBirth = data_from_querry2["data_urodzenia"].ToString(),
                            Address = data_from_querry2["adres"].ToString(),
                            DateOfEmployment = data_from_querry2["data_zatrudnienia"].ToString()
                        };

                        listOfTrainers.Add(feld);
                    }
                    connection_name.Close();

                    _trainersView = CollectionViewSource.GetDefaultView(listOfTrainers);
                    _trainersView.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
                    _trainersView.Filter = obj =>
                    {
                        TrainerManagement trainer = obj as TrainerManagement;
                        return trainer != null && trainer.Name.StartsWith("");
                    };

                    TrenersData.ItemsSource = _trainersView;
                   
                }
                else
                {
                    MessageBox.Show("Brak uprawnień!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Otwieranie zarządzania trenerami...");
                connection_name.Close();
            }
        }

        private void AddTrainer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                    WindowDodajTrenera window = new WindowDodajTrenera();
                    window.Show();
                }
            
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  dodawania trenera!");
            }
        }


        private void EditTrainer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowEditujTrenera window = new WindowEditujTrenera();
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna edytowania trenera!");
            }
        }


        private void RemoveTrainer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    WindowUsunTrenera window = new WindowUsunTrenera();
                    window.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  usuwania trenera!");
            }
        }

       
        private void Export_to_Raport_T(object sender, EventArgs e)
        {

            try
            {
                if (user_privilege == 1 || user_privilege == 2)
                {
                    connection_name.Open();

                    string querry2 = "SELECT * FROM `Trenerzy` WHERE imie NOT LIKE '';";

                    MySqlCommand commend2 = new MySqlCommand(querry2, connection_name);


                    MySqlDataAdapter adapter = new MySqlDataAdapter(commend2);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "NazwaTabeli");

                    // Wczytaj zawartość szablonu HTML z pliku
                    string templatePath = "../../../\\Paterns\\patern_Treners_Raport.html";
                    string template = File.ReadAllText(templatePath); // Wprowadź ścieżkę do swojego szablonu

                    // Wygeneruj raport HTML z danymi
                    string generatedHTML = Engine.Razor.RunCompile(template, "templateKey", null, dataSet.Tables["NazwaTabeli"]);

                    string outputPath = "";
                    // Configure save file dialog box
                    var dialog = new Microsoft.Win32.SaveFileDialog();
                    dialog.FileName = "raport_Ternerzy_"; // Default file name
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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Report could not be created");
            }

        }



    }

}

