using Gym_Aplication.Windows;
using iText.Layout.Element;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Gym_Aplication
{
    public partial class MainWindow : Window
    {
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
                    string id = "";
                    string name = "";
                    string surname = "";
                    string phone = "";
                    string e_mail = "";
                    string dateOfBirth = "";
                    string address = "";
                    string dateOfEmployment = "";

                    while (data_from_querry2.Read())
                    {
                        id = (data_from_querry2["id_trenera"]).ToString();
                        name = (data_from_querry2["imie"]).ToString();
                        surname = (data_from_querry2["nazwisko"]).ToString();
                        phone = data_from_querry2["telefon"].ToString();
                        e_mail = data_from_querry2["e-mail"].ToString();
                        dateOfBirth = data_from_querry2["data_urodzenia"].ToString();
                        address = data_from_querry2["adres"].ToString();
                        dateOfEmployment = data_from_querry2["data_zatrudnienia"].ToString();

                        TrainerManagement feld = new TrainerManagement
                        {
                            ID = id,
                            Name = name,
                            Surname = surname,
                            Phone = phone,
                            E_Mail = e_mail,
                            DateOfBirth = dateOfBirth,
                            Address = address,
                            DateOfEmployment = dateOfEmployment
                        };

                        listOfTrainers.Add(feld);
                    }

                    _trainersView = CollectionViewSource.GetDefaultView(listOfTrainers);
                    _trainersView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                    _trainersView.Filter = obj =>
                    {
                        TrainerManagement trainer = obj as TrainerManagement;
                        return trainer != null && trainer.Name.StartsWith("");
                    };

                    TrenersData.ItemsSource = _trainersView;
                    connection_name.Close();
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
            OpenFileDialog openFileDialog = new OpenFileDialog();



        }

        //private void ExportToPDF_Click(object sender, RoutedEventArgs e)
       // {
            //    try
            //    {
            //        // Create a PDF document
            //        string pdfFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "List_of_Trainers.pdf");
            //       // var writer = new PdfWriter(pdfFilePath);
            //        //var pdf = new PdfDocument(writer);
            //        var document = new iText.Layout.Document(pdf);

            //        // Create a table and add headers
            //        var table = new Table(TrenersData.Columns.Count);
            //        foreach (DataGridColumn column in TrenersData.Columns)
            //        {
            //            table.AddHeaderCell(new Cell().Add(new Paragraph(column.Header.ToString())));
            //        }

            //        // Add data to the table
            //        foreach (var item in TrenersData.ItemsSource)
            //        {
            //            foreach (DataGridColumn column in TrenersData.Columns)
            //            {
            //                if (column.GetCellContent(item) is TextBlock)
            //                {
            //                    table.AddCell(
            //                        new Cell().Add(new Paragraph((column.GetCellContent(item) as TextBlock).Text)));
            //                }
            //                else
            //                {
            //                    table.AddCell(new Cell().Add(new Paragraph("")));
            //                }
            //            }
            //        }

            //        // Add the table to the document and close it
            //        document.Add(table);
            //        document.Close();

            //        MessageBox.Show("List of trainers exported to PDF successfully.");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error exporting the list of trainers to PDF.");
            //    }
      //  }


    }
    }



    





