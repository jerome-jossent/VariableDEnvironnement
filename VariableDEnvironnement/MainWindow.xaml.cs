using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VariableDEnvironnement
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string _filter
        {
            get { return filter; }
            set
            {
                if (filter == value) return;
                filter = value;
                DisplayVarEnv();
                OnPropertyChanged("_filter");
            }
        }
        string filter = "";

        Dictionary<string, string> vars;
        List<string> varenvs_keys;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadVarEnv();
        }
        void Load_click(object sender, MouseButtonEventArgs e)
        {
            LoadVarEnv();
        }


        void LoadVarEnv()
        {
            IDictionary varenvs_raw = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
            vars = new Dictionary<string, string>();
            foreach (DictionaryEntry varenv_raw in varenvs_raw)
                if (varenv_raw.Key != null && varenv_raw.Value != null)
                    vars.Add(varenv_raw.Key?.ToString(), varenv_raw.Value.ToString());

            varenvs_keys = vars.OrderBy(x => x.Key).Select(x => x.Key).ToList();
            DisplayVarEnv();
        }

        void DisplayVarEnv()
        {
            _lst.Items.Clear();
            string filter_lower = _filter.ToLower();

            foreach (string varenv_key in varenvs_keys)
            {
                if (filter == "" || varenv_key.ToLower().Contains(filter_lower))
                {
                    VarEnv_UC varEnv_UC = new VarEnv_UC();
                    varEnv_UC._Init(varenv_key, vars[varenv_key].ToString());
                    _lst.Items.Add(varEnv_UC);
                }
            }
        }

    }
}
