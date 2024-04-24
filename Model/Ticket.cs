using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PrioritySearchProgram.Model
{
    public abstract class Ticket
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public Ticket()
        {
            CreationDate = DateTime.Now;
        }


        public Ticket(string name, double price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
            CreationDate = DateTime.Now;
        }

        public abstract Dictionary<string, string> ToSearchMap();

    }
}
