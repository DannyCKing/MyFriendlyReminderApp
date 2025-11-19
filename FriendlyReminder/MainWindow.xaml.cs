using FriendlyReminder.Models;
using FriendlyReminder.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FriendlyReminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewReminderButton_Click(object sender, RoutedEventArgs e)
        {
            CreateReminderWindow createReminderWindow = new CreateReminderWindow();
            var result = createReminderWindow.ShowDialog();
            if(result.HasValue && result.Value == true)
            {
                ReminderMaster.AddReminder(createReminderWindow.Reminder);
            }
        }

        private void CurrentRemindersButon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateReminderFromTemplateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateTemplateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // hide this window
            //ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Minimized;
            //Hide();
            //this.Close();
        }
    }
}
