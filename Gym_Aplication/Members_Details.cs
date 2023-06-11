using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (user_privilege == 1)
                {
                    connection_name.Open();

                    string querry = "SELECT * FROM `Klienci`;";

                    MySqlCommand commend = new MySqlCommand(querry, connection_name);
                    MySqlDataReader data_from_querry = commend.ExecuteReader();

                    var listOfMembers = new List<MembersManagement>();

                    while (data_from_querry.Read())
                    {
                        MembersManagement feld = new MembersManagement()
                        {
                            ID = (data_from_querry["id_klienta"]).ToString(),
                            FristName = (data_from_querry["imie"]).ToString(),
                            LastName = (data_from_querry["nazwisko"]).ToString(),
                            E_Mail = data_from_querry["e-mail"].ToString(),
                            Phone = data_from_querry["telefon"].ToString(),
                            DateOfRegistration = data_from_querry["data_utworzenia"].ToString(),
                            Sex = data_from_querry["płec"].ToString(),
                            Adress = data_from_querry["adres"].ToString(),
                        };

                        listOfMembers.Add(feld);
                    }

                    _membersView = CollectionViewSource.GetDefaultView(listOfMembers);
                    _membersView.SortDescriptions.Add(new SortDescription("FristName", ListSortDirection.Ascending));
                    _membersView.Filter = obj =>
                    {
                        MembersManagement member = obj as MembersManagement;
                        return member != null && member.FristName.StartsWith("A");
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
            }
        }


        private void Dodajczlonka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowDodajczlonka window = new WindowDodajczlonka();
                window.Show();
            }catch(Exception ex)
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
        private void Pobierzraport3_Click(object sender, RoutedEventArgs e)
        {
            WindowPobierzraport3 window = new WindowPobierzraport3();
            window.Show();
        }
    }




}
