using PrioritySearchProgram.Events;
using PrioritySearchProgram.Helper;
using PrioritySearchProgram.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PrioritySearchProgram.View.Control
{
    public partial class DynamicFields : UserControl {
        private Dictionary<string, FrameworkElement> _fieldControls = new Dictionary<string, FrameworkElement>();
        private List<int> priotityList = [];
        private List<ComboBox> dropdowns = [];
        Dictionary<int, int> selectedDropdownValues = [];

        //refactor
        Ticket displayedTicket;

        Action closeAction;

        public DynamicFields()
        {
            InitializeComponent();
        }

        public void DisplayFields(object obj, Action close)
        {
            if (obj == null)
                return;

            closeAction = close;
            if (obj is Ticket) {
                displayedTicket = (Ticket) obj;
            }

            var properties = obj.GetType().GetProperties();

            dropdowns.Clear();
            priotityList.Clear();

            int i = 1;
            foreach (var prop in properties)
            {
                priotityList.Add(i++);

                var label = new Label();
                label.Content = prop.Name;
                label.Margin = new Thickness(5);

                FrameworkElement inputControl;
                if (prop.PropertyType == typeof(bool))
                {
                    var checkBox = new CheckBox();
                    inputControl = checkBox;
                }
                else
                {
                    var textBox = new TextBox();
                    inputControl = textBox;
                }

                inputControl.Margin = new Thickness(5);

                inputControl.DataContext = prop.GetValue(obj)?.ToString() ?? "";

                var stackPanelHorizontal = new StackPanel();
                stackPanelHorizontal.HorizontalAlignment = HorizontalAlignment.Right;
                stackPanelHorizontal.Orientation = Orientation.Horizontal;

                inputControl.Width = 200;
                stackPanelHorizontal.Children.Add(label);
                stackPanelHorizontal.Children.Add(inputControl);

                var priorityDropDown = new ComboBox();
                priorityDropDown.SelectionChanged += Dropdown_SelectionChanged;
                priorityDropDown.ItemsSource = priotityList;
                priorityDropDown.MinWidth = 50;

                dropdowns.Add(priorityDropDown);
                stackPanelHorizontal.Children.Add(priorityDropDown);

                stackPanel.Children.Add(stackPanelHorizontal);

                _fieldControls.Add(prop.Name, inputControl);
            }
            
            var searchButton = new Button();
            searchButton.Click += Search;
            searchButton.Width = 200;
            searchButton.Height = 50;
            searchButton.Content = "Search";
            stackPanel.Children.Add(searchButton);

            //            <Button Width="200" Height="50" Click="Search">Search</Button>
        }

        public void Search(object sender, RoutedEventArgs e)
        {
            var fieldsSearchQueries = new List<FieldSearchQuery>();

            int i = 0;
            foreach (var fieldName in _fieldControls.Keys)
            {
                var priority = selectedDropdownValues.Count > i;
                var control = _fieldControls[fieldName];

                if (control is TextBox)
                {

                    fieldsSearchQueries.Add(new FieldSearchQuery(fieldName, selectedDropdownValues.ContainsKey(i) ? selectedDropdownValues[i] : int.MaxValue, ((TextBox)control).Text));
                }
                else if (control is CheckBox)
                {
                    fieldsSearchQueries.Add(new FieldSearchQuery(fieldName, selectedDropdownValues.ContainsKey(i) ? selectedDropdownValues[i] : int.MaxValue, ((CheckBox)control).IsChecked.ToString()));
                }

                i++;

            }

            GlobalEvents.RaiseFieldsSearchEvent(new FieldsSearchQueries(displayedTicket, fieldsSearchQueries ?? []));
            closeAction();
        }

        private void Dropdown_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox selectedDropdown = sender as ComboBox;
            if (selectedDropdown?.SelectedItem == null)
            {
                return;
            }

            int selectedValue = int.Parse(selectedDropdown.SelectedItem!.ToString());


            if (selectedDropdownValues.Values.Contains(selectedValue))
            {
                int key = dropdowns.IndexOf(selectedDropdown);
                if (selectedDropdownValues.ContainsKey(key))
                {
                    selectedDropdownValues.Remove(key);
                }

                selectedDropdown.SelectedIndex = -1;
                MessageBox.Show("Value already selected in another dropdown.");
            } else
            {
                int key = dropdowns.IndexOf(selectedDropdown);
                selectedDropdownValues[key] = selectedValue;

            }
        }
    }
}