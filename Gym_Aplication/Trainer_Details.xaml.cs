using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Trainer_Details.xaml
    /// </summary>
    public partial class Trainer_Details : Window
    {
        public Trainer_Details()
        {
            InitializeComponent();
        }

        public void UpdateFields(TrainerManagement trainer)
        {
            NameLabel.Content = "Name: " + trainer.Name;
            SurnameLabel.Content = "Surname: " + trainer.Surname;
            // Update other fields
        }
    }
}