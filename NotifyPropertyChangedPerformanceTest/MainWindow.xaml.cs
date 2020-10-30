using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotifyPropertyChangedPerformanceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string NotifyChangedText { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for(int i = 0; i < 10000000; i++)
            {
                NotifyChangedText = i.ToString();
                NotifyPropertyChanged(nameof(NotifyChangedText));
            }

            double timeInNotify = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();

            for(int i = 0; i < 10000000; i++)
            {
                ManualTextBlock.Text = i.ToString();
            }

            double timeInManual = stopwatch.Elapsed.TotalSeconds;

            stopwatch.Stop();
            MessageBox.Show($"Notify: {timeInNotify}\nManual: {timeInManual}");
        }
    }
}
