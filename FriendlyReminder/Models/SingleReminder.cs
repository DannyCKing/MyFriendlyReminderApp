using FriendlyReminder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendlyReminder.Models
{
    internal class SingleReminder
    {
        public Guid Id { get; private set; }

        public string ReminderName { get; private set; }

        public string ReminderMessage { get; private set; }

        public DateTime ReminderExpiration {  get; set; }

        public bool Complete { get ; set; }

        public SoundEnum SoundToPlay { get; set; }

        public SingleReminder(string name, string message, DateTime expireDate, SoundEnum soundToPlay)
        {
            Id = Guid.NewGuid();
            ReminderName = name;
            ReminderMessage = message;
            ReminderExpiration = expireDate;
            Complete = false;
            SoundToPlay = soundToPlay;
        }
    }
}
