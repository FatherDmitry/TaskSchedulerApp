using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace TaskSchedulerApp.Models
{
    class TaskModel: INotifyPropertyChanged
    {
        private bool _isDone;
        private string _text;

        //[JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; } = DateTime.Now;


        public bool IsDone
        {
            get { return _isDone; }
            set 
            {
                if (_isDone ==  value)
                    return;
                _isDone = value;
                OnPropertyChange("IsDone");
            }
        }

        public string Text
        {
            get { return _text; }
            set 
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChange("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; //Для отслуживания, если было изменение в задаче

        protected virtual void OnPropertyChange(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
