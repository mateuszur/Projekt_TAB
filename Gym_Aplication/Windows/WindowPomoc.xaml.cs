using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym_Aplication
{
    /// <summary>
    /// Interaction logic for WindowPomoc.xaml
    /// </summary>
    public partial class WindowPomoc : Window
    {
        public WindowPomoc()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        public class FAQ
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }

        public class ViewModel
        {
            public List<FAQ> FAQs { get; set; }

            public ViewModel()
            {
                FAQs = new List<FAQ>
                {
                    new FAQ { Question = "Jak mogę zmienić hasło do mojego konta?", Answer = "Aby mienić hasło należy skontaktować się z administartorem." },
                    new FAQ { Question = "Jak korzystać z aplikacji?", Answer = "Oto jak korzystać z aplikacji..." },
                    new FAQ { Question = "Jak korzystać z aplikacji?", Answer = "Oto jak korzystać z aplikacji..." },
                    new FAQ { Question = "Jak korzystać z aplikacji?", Answer = "Oto jak korzystać z aplikacji..." },
                    // Add more FAQs here...
                };
            }
        }

        public void CollapseContent () 
        {
            FAQListBox.Visibility = Visibility.Collapsed;

        }

        public void ShowFAQ(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Najczęściej zadawane pytania (FAQ)";
        }

        public void ShowClassSchedule(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Harmonogram zajęć";
        }


        public void ShowPersonalSchedule(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Harmonogram indywidualny";
        }



        public void ShowTrainersMenage(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Zarządzanie trenerami";
        }



        public void ShowMembersMenage(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Zarządzanie członkami";
        }



        public void ShowArticles(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Artykuły i porady";
        }


        public void ShowStatistic(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Statystyki i analizy";
        }

        public void ShowReports(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Raportowanie awarii";
        }

        public void ShowInfo(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            FAQListBox.Visibility = Visibility.Visible;
            contentLabel.Content = "Informacje";
        }



    }

}