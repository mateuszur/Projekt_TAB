using System;
using System.Windows;
using System.Windows.Controls;


namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowUstawienia.xaml
    /// </summary>
    public partial class WindowUstawienia : Window
    {
        public WindowUstawienia()
        {
            InitializeComponent();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string newLanguage = LanguageComboBox.SelectedItem.ToString();
            bool newDarkMode = DarkModeCheckBox.IsChecked.Value;
            bool newWhiteMode = WhiteModeCheckBox.IsChecked.Value;

            // TODO: Update the settings in your settings system

            MessageBox.Show("Twoje ustawienia zostały uaktualnione.");
        }
    }
}