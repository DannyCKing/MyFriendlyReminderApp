using FriendlyReminder.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace FriendlyReminder.Models
{
    internal static class ReminderMaster
    {
        private const int ONE_MINUTE = 60 * 1000;
        private const int ONE_SECOND = 1000;

        static System.Timers.Timer ReminderTimer = new System.Timers.Timer();

        static List<SingleReminder> AllReminders;

        static object LockObject = new object();
        static ReminderMaster()
        {
            AllReminders = new List<SingleReminder>();
            ReminderTimer = new System.Timers.Timer();
            ReminderTimer.Interval = 1000;
            ReminderTimer.AutoReset = true;
            ReminderTimer.Elapsed += ReminderTimer_Elapsed;
        }

        private static void ReminderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunIntervalLogic();
        }

        private static void RunIntervalLogic()
        {
            ReminderTimer.Stop();

            lock (LockObject)
            {
                foreach (var reminder in AllReminders)
                {
                    if (DateTime.Now > reminder.ReminderExpiration)
                    {
                        ShowAndUpdateReminder(reminder);
                        Thread.Sleep(ONE_SECOND);
                    }
                }

                // If Any are complete, purge them
                if(AllReminders.Any(x => x.Complete))
                {
                    // purge done reminders
                    AllReminders = AllReminders.Where(x => !x.Complete).ToList();
                }

                var intervalToUse = ONE_MINUTE;

                foreach (var reminder in AllReminders)
                {
                    if (DateTime.Now.AddMinutes(1) > reminder.ReminderExpiration)
                    {
                        // this will be expire soon, change interval to once per second
                        intervalToUse = ONE_SECOND;
                    }
                }

                ReminderTimer.Interval = intervalToUse;
            }

            ReminderTimer.Start();
        }

        private static void ShowAndUpdateReminder(SingleReminder reminder)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var newWindow = new ViewReminderWindow(reminder);
                var result = newWindow.ShowDialog();
                if (newWindow.DismissStatus == DimissStatusEnum.Dismissed)
                {
                    reminder.Complete = true;
                }
                else if(newWindow.DismissStatus == DimissStatusEnum.Snoozed)
                {
                    reminder.ReminderExpiration = DateTime.Now.AddSeconds(5);

                    //reminder.ReminderExpiration = DateTime.Now.AddMinutes(5);
                }
            });
        }

        internal static bool AddReminder(SingleReminder newReminder)
        {
            try
            {
                lock(LockObject)
                {
                    AllReminders.Add(newReminder);
                }
                RunIntervalLogic();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        static bool DeleteReminder(Guid id)
        {
            try
            {
                lock (LockObject)
                {
                    var toRemove = AllReminders.FirstOrDefault(x => x.Id == id);
                    AllReminders.Remove(toRemove);
                }
                RunIntervalLogic();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
