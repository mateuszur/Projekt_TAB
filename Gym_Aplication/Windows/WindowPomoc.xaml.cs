using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


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
                    new FAQ { Question = "Jak korzystać z aplikacji?", Answer = "Oto jak korzystać z aplikacji..." },
                    // Add more FAQs here...
                };
            }
        }
    }
}