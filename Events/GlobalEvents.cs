using PrioritySearchProgram.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrioritySearchProgram.Events
{
    public class GlobalEvents
    {
        public static readonly Window window = new Window();
        public static readonly RoutedEvent ProductsListSearchEvent = EventManager.RegisterRoutedEvent(
            "ProductsListSearchEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(GlobalEvents));

        public static void AddFieldsSearchHandler(RoutedEventHandler handler)
        {
            window.AddHandler(ProductsListSearchEvent, handler);
        }

        public static void RaiseFieldsSearchEvent(FieldsSearchQueries fieldSearchQueries)
        {
            RoutedEventArgs args = new RoutedEventArgs(ProductsListSearchEvent, fieldSearchQueries);
            window.RaiseEvent(args);
        }
    }
}