using MySql.Data.MySqlClient;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {
        private void Czlonek_Click(object sender, RoutedEventArgs e)
        {
            ChangePageVisibility(Content_ZarzadzanieCzlonkami);

            try
            {
                if (user_privilege == 1 || user_privilege == 2)
                    {
                    connection_name.Open();

                    string querry = "SELECT * FROM `Klienci` WHERE imie NOT LIKE '';";

                    MySqlCommand commend = new MySqlCommand(querry, connection_name);
                    MySqlDataReader data_from_querry = commend.ExecuteReader();

                    var listOfMembers = new List<MembersManagement>();

                    while (data_from_querry.Read())
                    {
                        MembersManagement feld = new MembersManagement()
                        {
                            ID = int.Parse(data_from_querry["id_klienta"].ToString()),
                            FristName = (data_from_querry["imie"]).ToString(),
                            LastName = (data_from_querry["nazwisko"]).ToString(),
                            E_Mail = data_from_querry["e-mail"].ToString(),
                            Phone = data_from_querry["telefon"].ToString(),
                            DateOfRegistration = data_from_querry["data_utworzenia"].ToString(),
                            Sex = data_from_querry["plec"].ToString(),
                            Adress = data_from_querry["adres"].ToString(),
                        };

                        listOfMembers.Add(feld);
                    }

                    _membersView = CollectionViewSource.GetDefaultView(listOfMembers);
                    _membersView.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
                    _membersView.Filter = obj =>
                    {
                        MembersManagement member = obj as MembersManagement;
                        return member != null && member.FristName.StartsWith("");
                    };

                    MembersDataGrid.ItemsSource = _membersView;
                    connection_name.Close();
                }
                else
                {
                    MessageBox.Show("Brak uprawnień!");
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Otwieranie zarządzania klientami...");
                connection_name.Close();
            }
        }


        private void Dodajczlonka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowDodajczlonka window = new WindowDodajczlonka();
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  dodawania członka!");
            }
        }

        private void Editujczlonka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowEditujczlonka window = new WindowEditujczlonka();
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  edycji członka!");
            }
        }

        private void Usunczlonka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowUsunczlonka window = new WindowUsunczlonka();
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  usuwania członka!");
            }
        }

        private void Export_to_Raport_M(object sender, EventArgs e)
        {

            try
            {
                if (user_privilege == 1 || user_privilege == 2)
                {
                    connection_name.Open();

                    string querry2 = "SELECT * FROM `Klienci` WHERE imie NOT LIKE '';";

                    MySqlCommand commend2 = new MySqlCommand(querry2, connection_name);



                    MySqlDataAdapter adapter = new MySqlDataAdapter(commend2);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "NazwaTabeli");


                    // Wczytaj zawartość szablonu HTML z pliku
                    string templatePath = "Paterns\\patern_Members_Raport.html";
                    string template = File.ReadAllText(templatePath); // Wprowadź ścieżkę do swojego szablonu

                    // Wygeneruj raport HTML z danymi
                    string generatedHTML = Engine.Razor.RunCompile(template, "templateKey", null, dataSet.Tables["NazwaTabeli"]);

                    string outputPath = "";
                    // Configure save file dialog box
                    var dialog = new Microsoft.Win32.SaveFileDialog();
                    dialog.FileName = "raport_Członkowie_"; // Default file name
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
                connection_name.Close();
            }

        }

    }
}