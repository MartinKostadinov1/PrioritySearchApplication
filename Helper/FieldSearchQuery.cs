using PrioritySearchProgram.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrioritySearchProgram.Helper
{
    public class FieldSearchQuery
    {
        public string FieldName { get; set; }

        public int Priority { get; set; }

        public string SearchValue { get; set; }


        public FieldSearchQuery(string fieldName, int priority, string searchValue)
        {
            FieldName = fieldName;
            Priority = priority;
            SearchValue = searchValue;
        }
        public (int priority, string fieldName, string searchValue) GetSearchFriendy()
        {
            return (Priority, FieldName, SearchValue);
        }

    }
}
