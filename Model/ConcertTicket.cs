using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrioritySearchProgram.Model
{
    public class ConcertTicket : Ticket
    {
        public DateTime DateTime { get; set; }

        public string Performer { get; set; }

        public string Location { get; set; }

        public ConcertTicket() : base()
        {
        }


        public ConcertTicket(string name, double price, string description, DateTime dateTime, string performer, string location) : base(name, price, description)
        {
            this.DateTime = dateTime;
            this.Performer = performer;
            this.Location = location;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Price: {Price}, Description: {Description}, CreationDate: {CreationDate}, DateTime: {DateTime}, Performer: {Performer}, Location: {Location}";
        }

        public override Dictionary<string, string> ToSearchMap()
        {
            Type type = typeof(ConcertTicket);

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