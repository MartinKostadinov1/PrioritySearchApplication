using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrioritySearchProgram.Model
{
    public class FootballGameTicket: Ticket
    {
        public DateTime DateTime { get; set; }

        public string Host { get; set; }

        public string Guest { get; set; }

        public string Location { get; set; }

        public FootballGameTicket() : base()
        {
        }


        public FootballGameTicket(string name, double price, string description, DateTime dateTime, string host, string guest, string location) : base(name, price, description)
        {
            this.DateTime = dateTime;
            this.Host = host;
            this.Guest = guest;
            this.Location = location;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Price: {Price}, Description: {Description}, CreationDate: {CreationDate}, DateTime: {DateTime}, Host: {Host}, Guest: {Guest}, Location: {Location}";
        }

        public override Dictionary<string, string> ToSearchMap()
        {
            Type type = typeof(FootballGameTicket);

            PropertyInfo[] properties = type.GetProperties();

            var map = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                map[property.Name] = property.GetValue(this).ToString().ToLower();
            }

            return map;
        }
    }
}
