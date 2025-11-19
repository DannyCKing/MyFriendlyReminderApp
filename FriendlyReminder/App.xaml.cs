using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FriendlyReminder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private NotifyIcon trayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //trayIcon = new NotifyIcon();
            //var iconPath = Path.Combine("Resources", "clock.ico");
            //trayIcon.Icon = new System.Drawing.Icon(iconPath);
            //trayIcon.Visible = true;
            //trayIcon.Text = "Friendly Reminder";

            //// Right-click menu
            //trayIcon.ContextMenuStrip = new ContextMenuStrip();
            //trayIcon.ContextMenuStrip.Items.Add("Show", null, (s, ev) => ShowWindow());
            //trayIcon.ContextMenuStrip.Items.Add("Exit", null, (s, ev) => ExitApp());

            //// Double-click
            //trayIcon.DoubleClick += (s, ev) => ShowWindow();
        }

        private void ShowWindow()
        {
            var window = Current.MainWindow;

            window.Show();
            window.WindowState = WindowState.Normal;
            window.Activate();
        }

        private void ExitApp()
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            Shutdown();
        }
    }
}
