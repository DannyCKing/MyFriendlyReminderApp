using FriendlyReminder.Models;
using FriendlyReminder.Utilities;
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

namespace FriendlyReminder.Views
{
    /// <summary>
    /// Interaction logic for ViewReminderWindow.xaml
    /// </summary>
    public partial class ViewReminderWindow : Window
    {
        public DimissStatusEnum DismissStatus;

        private SingleReminder Reminder;

        internal ViewReminderWindow(SingleReminder reminder)
        {
            Reminder = reminder;
            DismissStatus = DimissStatusEnum.Snoozed;
            this.Loaded += ViewReminderWindow_Loaded;
            InitializeComponent();
        }

        private void ViewReminderWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = true;
            WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Normal;
            Topmost = true;
            Activate();

            ReminderTitle.Text = Reminder.ReminderName;
            ReminderDescription.Text = Reminder.ReminderMessage;
            Sounds.PlaySound(Reminder.SoundToPlay);
        }

        private void SnoozeButton_Click(object sender, RoutedEventArgs e)
        {
            DismissStatus = DimissStatusEnum.Snoozed;
            this.Close();
        }

        private void DismissButton_Click(object sender, RoutedEventArgs e)
        {
            DismissStatus = DimissStatusEnum.Dismissed;
            this.Close();
        }
    }
}
