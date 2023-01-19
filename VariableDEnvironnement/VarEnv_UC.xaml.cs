using System;
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
    public partial class VarEnv_UC : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string _key
        {
            get { return key; }
            set
            {
                if (value == key) return;
                key = value;
                OnPropertyChanged("_key");
            }
        }
        string key;

        public string _val
        {
            get { return val; }
            set
            {
                if (value == val) return;
                val = value;
                OnPropertyChanged("_val");
            }
        }
        string val;

        public VarEnv_UC()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void _Init(string key, string val)
        {
            this._key = key;
            this._val = val;
        }

        private void Save_click(object sender, MouseButtonEventArgs e)
        {
            Environment.SetEnvironmentVariable(_key, _val, EnvironmentVariableTarget.Machine);
        }

        private void Load_click(object sender, MouseButtonEventArgs e)
        {
            _val = Environment.GetEnvironmentVariable(_key);
        }
    }
}
