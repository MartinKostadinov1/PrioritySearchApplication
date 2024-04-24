using PrioritySearchProgram.Helper;
using PrioritySearchProgram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrioritySearchProgram.Service
{
    public class SearchingService<T> where T : Ticket
    {
        public SearchingService() { }


        public List<T> ExecuteSearch(FieldsSearchQueries fieldsSearchQueries, List<T> tickets)
        {

            var searchFieldsOrdered = fieldsSearchQueries.FieldsSearchingQueries.Where(t => t.Priority != int.MaxValue).OrderByDescending(f => f.Priority).ToList();

            var topPrio = searchFieldsOrdered.LastOrDefault();
            if (!tickets.Any(t=> t.ToSearchMap()[topPrio.FieldName].Contains(topPrio.SearchValue.ToLower()))) {
                return [];
            }

            var ticketsResult = tickets;

            for(var i = 0; i < searchFieldsOrdered.Count;i++)
            {
                var res = ticketsResult;
                for (var j = i; j < searchFieldsOrdered.Count; j++)
                {
                    res = res.FindAll(t => t.ToSearchMap()[searchFieldsOrdered[j].FieldName].Contains(searchFieldsOrdered[j].SearchValue.ToLower()));
                }

                if (res.Count > 0)
                {
                    return res;
                }

            }

            return [];
        }
    }
}
