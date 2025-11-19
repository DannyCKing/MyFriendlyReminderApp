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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FriendlyReminder.Views
{
    /// <summary>
    /// Interaction logic for CreateReminderWindow.xaml
    /// </summary>
    public partial class CreateReminderWindow : Window
    {
        internal SingleReminder Reminder;

        private int CurrentMinutes = 0;

        private bool UseCustomTime = false;

        public CreateReminderWindow()
        {
            InitializeComponent();

            this.Loaded += CreateReminderWindow_Loaded;
        }

        private void CreateReminderWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetTimerButtons(null);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void SetTimerButtons(Button inputButton)
        {
            UseCustomTime = false;
            TimeErrorLabel.Visibility = Visibility.Hidden;

            Color defaultBackgroundColor = Color.FromArgb(255, 74, 144, 226);
            Color selectedBackgroundColor = Color.FromArgb(255, 24, 81, 149);
            OneMinuteButton.Background = new SolidColorBrush(defaultBackgroundColor);
            FiveMinuteButton.Background = new SolidColorBrush(defaultBackgroundColor);
            TenMinuteButton.Background = new SolidColorBrush(defaultBackgroundColor);
            FifteenMinuteButton.Background = new SolidColorBrush(defaultBackgroundColor);
            ThirtyMinuteButton.Background = new SolidColorBrush(defaultBackgroundColor);
            CustomTimeButton.Background = new SolidColorBrush(defaultBackgroundColor);

            if (inputButton != null)
            {
                inputButton.Background = new SolidColorBrush(selectedBackgroundColor);
                var inputValue = inputButton.CommandParameter;
                if(inputValue != null && int.TryParse(inputValue.ToString(), out int intResult))
                {
                    CustomTimeValueTextBox.Visibility = Visibility.Hidden;
                    CustomTimeUnitComboBox.Visibility = Visibility.Hidden;
                    CustomTimeLabel.Visibility = Visibility.Hidden;

                    CurrentMinutes = intResult;
                }
                else
                {
                    CustomTimeValueTextBox.Visibility = Visibility.Visible;
                    CustomTimeUnitComboBox.Visibility = Visibility.Visible;
                    CustomTimeLabel.Visibility = Visibility.Visible;
                    UseCustomTime = true;
                }
            }
            else
            {
                CustomTimeValueTextBox.Visibility = Visibility.Hidden;
                CustomTimeUnitComboBox.Visibility = Visibility.Hidden;
                CustomTimeLabel.Visibility = Visibility.Hidden;
            }
        }

        private void OneMinuteButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimerButtons(sender as Button);
        }

        private void FiveMinuteButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimerButtons(sender as Button);
        }

        private void TenMinuteButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimerButtons(sender as Button);
        }

        private void FifteenMinuteButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimerButtons(sender as Button);
        }

        private void ThirtyMinuteButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimerButtons(sender as Button);
        }

        private void CustomTimeButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimerButtons(sender as Button);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan currentTimeSpan = TimeSpan.Zero;

            if(UseCustomTime)
            {
                double customValue = 0;

                double.TryParse(CustomTimeValueTextBox.Text, out customValue);
                var customUnit = CustomTimeUnitComboBox.Text.ToString().ToLower();
                if(customUnit == "seconds")
                {
                    currentTimeSpan = TimeSpan.FromSeconds(customValue);
                }
                else if(customUnit == "minutes")
                {
                    currentTimeSpan = TimeSpan.FromMinutes(customValue);
                }
            }
            else
            {
                currentTimeSpan = TimeSpan.FromMinutes(CurrentMinutes);
            }

            if(currentTimeSpan == TimeSpan.Zero)
            {
                TimeErrorLabel.Visibility = Visibility.Visible;
                return;
            }

            SoundEnum soundToPlay = SoundEnum.none;
            var item = SoundComboBox.Text.ToString();
            var stringValue = item.ToString().Replace(" ", "").ToLower();
            Enum.TryParse(stringValue, true, out SoundEnum soundResult);

            Reminder = new SingleReminder(TitleTextBox.Text, DescriptionTextBox.Text, DateTime.Now.AddSeconds(currentTimeSpan.TotalSeconds), soundResult);
            DialogResult = true;
        }

        private void TestSoundButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SoundComboBox.Text.ToString();
            var stringValue = item.ToString().Replace(" ", "").ToLower();
            var parsed = Enum.TryParse(stringValue, true, out SoundEnum soundResult);
            if(parsed)
            {
                Sounds.PlaySound(soundResult);
            }

        }
    }
}
