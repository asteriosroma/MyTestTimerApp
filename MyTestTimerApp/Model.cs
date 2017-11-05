using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MyTestTimerApp
{
    class Model : INotifyPropertyChanged, IDataErrorInfo
    {
        DispatcherTimer timer;
        TimeSpan start_time;
        string start_time_count;
        string chet_time;
        string nechet_time;
        bool edit_mode;
        bool has_error;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnMyPropertyChanged([CallerMemberName]string s = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(s));
            }
        }

        public Model()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            edit_mode = true;
            Start_time_count = Chet_time = Nechet_time = "00:00:00";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (start_time.TotalSeconds == 1)
            {
                Stop_timer();
            }

            start_time = start_time.Subtract(TimeSpan.FromSeconds(1));
            Start_time_count = start_time.ToString();

            bool is_chetne = (start_time.TotalSeconds % 2) == 0;
            if (is_chetne)
            {
                Chet_time = Start_time_count;
            }
            if (!is_chetne)
            {
                Nechet_time = Start_time_count;
            }
        }

        public void Start_timer()
        {
            if (!has_error)
            {
                if (start_time.ToString() != Start_time_count)
                {
                    int result;
                    int.TryParse(Start_time_count, out result);
                    start_time = TimeSpan.FromSeconds(result);
                    Nechet_time = Chet_time = "00:00:00";
                }

                timer.Start();
                edit_mode = false;
            }
        }

        public void Stop_timer()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            edit_mode = true;
        }

        public string Start_time_count
        {
            get
            {
                return start_time_count;
            }
            set
            {
                start_time_count = value;
                OnMyPropertyChanged("Start_time_count");
            }
        }

        public string Chet_time
        {
            get
            {
                return chet_time;
            }
            set
            {
                chet_time = value;
                OnMyPropertyChanged("Chet_time");
            }
        }

        public string Nechet_time
        {
            get
            {
                return nechet_time;
            }
            set
            {
                nechet_time = value;
                OnMyPropertyChanged("Nechet_time");
            }
        }

        public string Error
        {
            get
            {
                return "error";
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error_raised = string.Empty;
                int result;
                int.TryParse(Start_time_count, out result);
                has_error = false;
                switch (columnName)
                {
                    case "Start_time_count":
                        if (Start_time_count == string.Empty && edit_mode)
                        {
                            error_raised = "Значение не может быть пустым";
                            has_error = true;
                        }
                        if (Regex.IsMatch(Start_time_count, @"\D") && !(Start_time_count == string.Empty)
                             && edit_mode)
                        {
                            error_raised = "Значение может содержать только цифры";
                            has_error = true;
                        }
                        if (result > 72000 && edit_mode)
                        {
                            error_raised = "Значение должно быть не больше 72000";
                            has_error = true;
                        }
                        if (result < 0 && edit_mode)
                        {
                            error_raised = "Значение не может быть отрицательным числом";
                            has_error = true;
                        }
                        if (result == 0 && !Regex.IsMatch(Start_time_count, @"\D") 
                            && !(Start_time_count == string.Empty) && edit_mode)
                        {
                            error_raised = "Значение 0 неприемлемо";
                            has_error = true;
                        }
                        break;
                }
                return error_raised;
            }
        }
    }
}
