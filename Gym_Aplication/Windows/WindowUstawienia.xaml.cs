using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowUstawienia.xaml
    /// </summary>
    public partial class WindowUstawienia : Window
    {
        private string connection_string = "Server=polsl.online;Uid=test;Pwd=Pa$$w0rd;Database=Baza_projekt;";
        private string login;
        private string haslo;
        private string haslo1;
        private string imie;
        private string nazwisko;
        private string uprawnienia;

        MySqlConnection connection_name = new MySqlConnection();
        int User_p;
        public WindowUstawienia(int user_p)
        {
            InitializeComponent();
            this.User_p = user_p;


            if (user_p == 1)
            {
                Login.IsEnabled = false;
                Haslo.IsEnabled = false;
                Haslo2.IsEnabled = false;
            }
            else { connection_name.ConnectionString = connection_string; }


        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (User_p == 2 && Check.IsChecked == true)
                {

                    login = Login.Text;
                    haslo = Haslo.Text;
                    haslo1 = Haslo2.Text;
                    imie = Imie.Text;
                    nazwisko = Nazwisko.Text;


                    if (string.IsNullOrWhiteSpace(imie))
                    {
                        MessageBox.Show("Imię nie może byc puste!");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(nazwisko))
                    {
                        MessageBox.Show("Nazwisko nie może byc puste!");
                        return;
                    }

                    if (login.Length < 4 || string.IsNullOrWhiteSpace(login))
                    {
                        MessageBox.Show("Login uzytkownika powienien składać się z co najmniej 4 znaków.");
                        return;
                    }


                    if (haslo.Length < 8 || string.IsNullOrWhiteSpace(haslo))
                    {
                        MessageBox.Show("Hasło uzytkownika powienien składać się z co najmniej 8 znaków.");
                        return;
                    }

                    if (haslo != haslo1)
                    {
                        MessageBox.Show("Hasła nie są jednakowe!");
                        return;
                    }

                    ComboBoxItem wybor = ComboBox_User.SelectedItem as ComboBoxItem;

                    if (wybor != null)
                    {
                        uprawnienia = wybor.Tag.ToString();
                    }

                    using (SHA1 sha1 = SHA1.Create())
                    {
                        byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(haslo));
                        haslo = BitConverter.ToString(hash).Replace("-", "").ToLower();
                    }

                    connection_name.Open();

                    string sql = "INSERT INTO `Uzytkownicy` (`ID`, `Nazwa`, `Hasło`, `Uprawnienia`, `Imie_Nazwisko`) VALUES (NULL, '" + login + "', '" + haslo + "', " + uprawnienia + ", '" + imie + " " + nazwisko + "')";

                    MySqlCommand command = new MySqlCommand(sql, connection_name);
                    MySqlDataReader reader0 = command.ExecuteReader();
                    connection_name.Close();

                    Imie.Text = "";
                    Nazwisko.Text = "";
                    Login.Text = "";
                    Haslo.Text = "";
                    Haslo2.Text = "";


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd" + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                connection_name.Close();
            }



            MessageBox.Show("Twoje ustawienia zostały uaktualnione.");
        }
        private void RadioButton_Checked_Check(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton == Check)
            {
                Imie.IsEnabled = true;
                Nazwisko.IsEnabled = true;
                Login.IsEnabled = true;
                Haslo.IsEnabled = true;
                Haslo2.IsEnabled = true;

            }
        }
    }
}
