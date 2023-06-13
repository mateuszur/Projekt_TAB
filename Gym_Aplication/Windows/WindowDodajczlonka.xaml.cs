using Intersoft.Crosslight.Mobile;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowDodajczlonka.xaml
    /// </summary>
    public partial class WindowDodajczlonka : Window
    {

        private string connection_string = "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";

        MySqlConnection connection_name = new MySqlConnection();

        string imie;
        string nazwisko;
        string telefon;
        string email;
        string adres;
        string plec;


        DateTime dataDodania_D =  DateTime.Now.Date;



        public WindowDodajczlonka()
        {
            InitializeComponent();
            
            DataPicker.Text = dataDodania_D.Date.ToString("dd-MM-yyyy");
            connection_name.ConnectionString = connection_string;
        }


        private void AddMember_Click_Zapisz_zmiany(object sender, RoutedEventArgs e)
        {

            try
            {
                imie = ImieTextBox.Text;
                nazwisko = NazwiskoTextBox.Text;
                telefon = TelefonTextBox.Text;
                email = EmailTextBox.Text;
                adres = AdresTextBox.Text;
                plec = PlecTextBox.Text;


                //  DateTime? data_Dodania = DateTime.Now; // DataPicker.SelectedDate; ;
                //;

               // MessageBox.Show("Wartośc daty " + DateTime.Now.Date.ToString("dd-MM-yyyy"), "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);

                string email_pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                string phone_pattern = @"^\d{9}$";



                if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko))
                {
                    MessageBox.Show("Imię i Nazwisko są wymagane.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (string.IsNullOrWhiteSpace(telefon) || !Regex.IsMatch(telefon, phone_pattern) || telefon.Length != 9)
                {
                    MessageBox.Show("Brak lub błąd w numerze telefonu! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (!Regex.IsMatch(email, email_pattern) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Brak lub błąd w podanym adresie e-mail! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(adres))
                {
                    MessageBox.Show("Brak adresu! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }



                if (!Regex.IsMatch(plec, "^[MK]$"))
                {
                    MessageBox.Show("Płeć przyjmuje wartości K (kobieta) lub M (mężczyzna)! Podaj poprawne dane!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


            }
            catch (Exception ex1)
            {
                MessageBox.Show("Podczas przetwarzania formularza wystąpił błąd!", "Błąd walidacji!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                connection_name.Open();

                string sql =
                "INSERT INTO `Klienci` (`id_klienta`, `imie`, `nazwisko`, `e-mail`, `telefon`, `data_utworzenia`, `plec`, `adres`) VALUES (NULL, '" + imie + "'," + "'" + nazwisko + "'," + "'" + email + "'," + "'" + telefon + "'," + "'" + dataDodania_D.Date.ToString("yyyy-MM-dd") + "'," + "'" + plec + "'," + "'" + adres + "' );";



                MySqlCommand command = new MySqlCommand(sql, connection_name);
                command.ExecuteNonQuery();
                connection_name.Close();


                MessageBox.Show("Z powodzeniem dodano klienta " + imie + " !");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Podczas dodawania rekordu do bazy danych wystąpił błąd!", "Błąd bazy!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
