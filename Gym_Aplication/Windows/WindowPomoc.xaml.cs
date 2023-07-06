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
                    new FAQ { Question = "Dlczego nie mogę wybrać sali w trakcie dodawania zajęć?", Answer = "Zapewne są to zajęcia indywidualne, dla tego typu zajęć nie można wybrać sali." },
                    new FAQ { Question = "Gdzie mogę zgłosić problem dot. aplikacji?", Answer = "Wszelkie błędy aplikacji należy zgłazać na silownia.kontakt@gmail.com" },
                    new FAQ { Question = "Jak korzystać z aplikacji?", Answer = "Oto jak korzystać z aplikacji..." },
                    // Add more FAQs here...
                };
            }
        }

        public void CollapseContent()
        {
            FAQListBox.Visibility = Visibility.Collapsed;
            ClassSchedule.Visibility = Visibility.Collapsed;
            PersonalSchedule.Visibility = Visibility.Collapsed;
            TrainersMenage.Visibility = Visibility.Collapsed;
            MembersMenage.Visibility = Visibility.Collapsed;
            Articles.Visibility = Visibility.Collapsed;
            Statistic.Visibility = Visibility.Collapsed;
            Reports.Visibility = Visibility.Collapsed;
            Info.Visibility = Visibility.Collapsed;

            ClassScheduleScroll.Visibility = Visibility.Collapsed;
            PersonalScheduleScroll.Visibility = Visibility.Collapsed;
            TrainersMenageScroll.Visibility = Visibility.Collapsed;
            MembersMenageScroll.Visibility = Visibility.Collapsed;
            ArticlesScroll.Visibility = Visibility.Collapsed;
            StatisticScroll.Visibility = Visibility.Collapsed;
            ReportsScroll.Visibility = Visibility.Collapsed;
            InfoScroll.Visibility = Visibility.Collapsed;
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
            ClassSchedule.Visibility = Visibility.Visible;
            ClassScheduleScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Harmonogram zajęć";
        }


        public void ShowPersonalSchedule(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            PersonalSchedule.Visibility = Visibility.Visible;
            PersonalScheduleScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Harmonogram indywidualny";
        }



        public void ShowTrainersMenage(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            TrainersMenage.Visibility = Visibility.Visible;
            TrainersMenageScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Zarządzanie trenerami";
        }



        public void ShowMembersMenage(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            MembersMenage.Visibility = Visibility.Visible;
            MembersMenageScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Zarządzanie członkami";
        }



        public void ShowArticles(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            Articles.Visibility = Visibility.Visible;
            ArticlesScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Artykuły i porady";
        }


        public void ShowStatistic(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            Statistic.Visibility = Visibility.Visible;
            StatisticScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Statystyki i analizy";
        }

        public void ShowReports(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            Reports.Visibility = Visibility.Visible;
            ReportsScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Raportowanie awarii";
        }

        public void ShowInfo(object sender, MouseButtonEventArgs arg)
        {
            CollapseContent();
            Info.Visibility = Visibility.Visible;
            InfoScroll.Visibility = Visibility.Visible;
            contentLabel.Content = "Informacje";
        }



    }

}