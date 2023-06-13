using Gym_Aplication.Windows;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Data;



namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {

        //

        //// Declare a string to hold the entire document contents.
        //private string documentContents = "";

        //// Declare a variable to hold the portion of the document that
        //// is not printed.
        //private string stringToPrint = "";


        //void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    int charactersOnPage = 0;
        //    int linesPerPage = 0;

        //    // Sets the value of charactersOnPage to the number of characters
        //    // of stringToPrint that will fit within the bounds of the page.
        //    e.Graphics.MeasureString(stringToPrint, this.Font,
        //        e.MarginBounds.Size, StringFormat.GenericTypographic,
        //        out charactersOnPage, out linesPerPage);

        //    // Draws the string within the bounds of the page.
        //    e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
        //    e.MarginBounds, StringFormat.GenericTypographic);

        //    // Remove the portion of the string that has been printed.
        //    stringToPrint = stringToPrint.Substring(charactersOnPage);

        //    // Check to see if more pages are to be printed.
        //    e.HasMorePages = (stringToPrint.Length > 0);

        //    // If there are no more pages, reset the string to be printed.
        //    if (!e.HasMorePages)
        //        stringToPrint = documentContents;
       // }


        //

        private void Zarzadzanie_Click(object sender, RoutedEventArgs e)

        {
            ChangePageVisibility(Content_ZarządzanieTrenerami);

            try
            {
                if (user_privilege == 1)
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
                if (user_privilege == 1)
                {
                    WindowDodajTrenera window = new WindowDodajTrenera();
                    window.Show();
                }
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
                if (user_privilege == 1)
                {
                    WindowUsunTrenera window = new WindowUsunTrenera();
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania okna  usuwania trenera!");
            }
        }

        private void Export_to_CSV_Trainer(object sender, RoutedEventArgs e)
        {
            // SaveFileDialog saveFileDialog = new SaveFileDialog();
            // saveFileDialog.Filter = "Pliki CSV (*.csv)|*.csv";
            // saveFileDialog.Title = "Zapisz plik CSV";
            // string path = saveFileDialog.FileName = "trenerzy_raport_" +".csv";
            //// +DateTime.Now.Day.ToString("dd_MM_yyyy")
            // string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            // string finalFilePath = System.IO.Path.Combine(downloadsPath, path);

            // saveFileDialog.ShowDialog();


            // MessageBox.Show("Ścieżka: " +finalFilePath );


            //connection_name.Open();

            //string querry2 = "SELECT * FROM `Trenerzy` WHERE imie NOT LIKE '';";

            //MySqlCommand commend2 = new MySqlCommand(querry2, connection_name);
            //MySqlDataReader data_from_querry2 = commend2.ExecuteReader();

            //var listOfTrainers = new List<TrainerManagement>();

            //while (data_from_querry2.Read())
            //{
            //    TrainerManagement feld = new TrainerManagement
            //    {
            //        ID = int.Parse(data_from_querry2["id_trenera"].ToString()),
            //        Name = (data_from_querry2["imie"]).ToString(),
            //        Surname = (data_from_querry2["nazwisko"]).ToString(),
            //        Phone = data_from_querry2["telefon"].ToString(),
            //        E_Mail = data_from_querry2["e-mail"].ToString(),
            //        DateOfBirth = data_from_querry2["data_urodzenia"].ToString(),
            //        Address = data_from_querry2["adres"].ToString(),
            //        DateOfEmployment = data_from_querry2["data_zatrudnienia"].ToString()
            //    };

            //    listOfTrainers.Add(feld);
            //}
            //connection_name.Close();

            //string docName = "testPage.txt";
            //string docPath = @"C:\";
            //string fullPath = System.IO.Path.Combine(docPath, docName);
            //printDocument1.DocumentName = docName;
            //stringToPrint = System.IO.File.ReadAllText(fullPath);

            //printPreviewDialog1.Document = printDocument1;

            //printPreviewDialog1.ShowDialog();

        }
            //using (var writer = new StreamWriter(path,false, Encoding.UTF8))
            //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //{
            //    csv.WriteRecords(listOfTrainers);
            //}







    }

 
        
    }
