using Microsoft.SharePoint.Client.UserProfiles;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowMojProfil.xaml
    /// </summary>
    public partial class WindowMojProfil : Window
    {
        public class UserProfileViewModel
        {
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string UserPassword { get; set; }
        }

        private UserProfileViewModel userProfile;

        public WindowMojProfil()
        {
            InitializeComponent();
            userProfile = new UserProfileViewModel();
            DataContext = userProfile;
        }

        public static class Database
        {
            public static void UpdateUserProfile(UserProfileViewModel userProfile)
            {
                // TODO: Implement this method to update the user profile in your data storage system
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string newUserName = userProfile.UserName;
            string newUserEmail = userProfile.UserEmail;
            string newUserPassword = userProfile.UserPassword;

            UserProfileViewModel updatedUserProfile = new UserProfileViewModel
            {
                UserName = newUserName,
                UserEmail = newUserEmail,
                UserPassword = newUserPassword
            };

            Database.UpdateUserProfile(updatedUserProfile);

            MessageBox.Show("Twój profil został zaktualizowany.");
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Add logic here to validate the email address as it's being typed
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            userProfile.UserPassword = PasswordBox.Password;
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordTextBox.Text = PasswordBox.Password;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordBox.Password = PasswordTextBox.Text;
        }
    }
}