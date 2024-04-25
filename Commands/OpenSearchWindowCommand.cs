using PrioritySearchProgram.Model;
using PrioritySearchProgram.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrioritySearchProgram.Commands
{
    class OpenSearchWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter == null)
            {
                return;
            }

            string? command = parameter as string;

            if (command.Equals("FootballGameTicketInstance")) { 
                (new SearchWindow(new FootballGameTicket(), FootballGameTicket.DefaultPriorities)).Show(); 
            } 
            else
            {
                (new SearchWindow(new ConcertTicket(), ConcertTicket.DefaultPriorities)).Show();
            }

           
        }
    }
}
