

using PrioritySearchProgram.Commands;
using PrioritySearchProgram.Database;
using PrioritySearchProgram.Events;
using PrioritySearchProgram.Helper;
using PrioritySearchProgram.Model;
using PrioritySearchProgram.Service;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PrioritySearchProgram.ViewModel
{
    class TicketsViewModel
    {

        private readonly ObservableCollection<ConcertTicket> _concertTickets = new ObservableCollection<ConcertTicket>();
        private readonly ObservableCollection<FootballGameTicket> _footballTickets = new ObservableCollection<FootballGameTicket>();


        private List<ConcertTicket> _backupConcertTickets = [];
        private List<FootballGameTicket> _backupFootballTickets = [];
        //TODO use these
        public FootballGameTicket FootballGameTicketInstance { get; } = new FootballGameTicket();
        public ConcertTicket ConcertTicketInstance { get; } = new ConcertTicket();

        public OpenSearchWindowCommand OpenSearchWindowCommand { get; set; } = new OpenSearchWindowCommand();

        public bool showConcertTicketsClearButton = false;
        public bool showFootballTicketsClearButton = false;

        private SearchingService<ConcertTicket> SearchingServiceConcert { get; set; }
        private SearchingService<FootballGameTicket> SearchingServiceFootball { get; set; }
        public TicketsViewModel()
        {
            loadData();
            GlobalEvents.AddFieldsSearchHandler(FilterProducts);
            SearchingServiceConcert = new SearchingService<ConcertTicket>();
            SearchingServiceFootball = new SearchingService<FootballGameTicket>();
        }

        public void loadData()
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();
                var concertTicketsRecords = context.ConcertTickets.ToList();
                var footballTicketsRecords = context.FootballGameTickets.ToList();

                concertTicketsRecords.ForEach(_concertTickets.Add);
                footballTicketsRecords.ForEach(_footballTickets.Add);

                _backupFootballTickets = _footballTickets.ToList();
                _backupConcertTickets = _concertTickets.ToList();
            }
        }

        private void FilterProducts(object sender, RoutedEventArgs e)
        {
            FieldsSearchQueries fieldSearchQueries = e.OriginalSource as FieldsSearchQueries;

            if (fieldSearchQueries.TicketType is FootballGameTicket) 
            {
                var footballTickets = SearchingServiceFootball.ExecuteSearch(fieldSearchQueries, _backupFootballTickets.ToList());
                showFootballTicketsClearButton = true;

                _footballTickets.Clear();
                footballTickets.ForEach(_footballTickets.Add);
            } else if (fieldSearchQueries.TicketType is ConcertTicket)
            {
                var concertTickets = SearchingServiceConcert.ExecuteSearch(fieldSearchQueries, _concertTickets.ToList());
                showConcertTicketsClearButton = true;

                _concertTickets.Clear();
                concertTickets.ForEach(_concertTickets.Add);
            }
        }

        public IEnumerable<ConcertTicket> ConcertTickets
        {
            get { return _concertTickets; }
        }

        public IEnumerable<FootballGameTicket> FootballTickets
        {
            get { return _footballTickets; }
        }

        public ICommand ClearSearch
        {
            get
            {
                return new DelegateConverterCommand((object s) =>
                {
                    if (showFootballTicketsClearButton)
                    {
                        _footballTickets.Clear();
                        _backupFootballTickets.ForEach(_footballTickets.Add);
                        showFootballTicketsClearButton = false;
                    }


                    if (showConcertTicketsClearButton)
                    {
                        _concertTickets.Clear();
                        _backupConcertTickets.ForEach(_concertTickets.Add);
                        showConcertTicketsClearButton = false;
                    }
                });
            }
        }

    }
}