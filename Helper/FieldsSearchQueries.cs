using PrioritySearchProgram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrioritySearchProgram.Helper
{
    public class FieldsSearchQueries
    {
        public Ticket TicketType { get; set; }
        public List<FieldSearchQuery> FieldsSearchingQueries { get; set; }


        public FieldsSearchQueries(Ticket ticketType, List<FieldSearchQuery> fieldsSearchingQueries)
        {
            TicketType = ticketType;
            FieldsSearchingQueries = fieldsSearchingQueries;
        }
    }
}