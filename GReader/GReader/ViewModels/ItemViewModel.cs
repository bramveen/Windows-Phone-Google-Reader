using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GReader
{
    public class ItemViewModel : INotifyPropertyChanged 
    {
        private string _title;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Title 
        {
            get 
            {
                return _title;
            }
            set 
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        
        private string _feedname;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Feedname
        {
            get
            {
                return _feedname;
            }
            set
            {
                _feedname = value;
                NotifyPropertyChanged("Feedname");
            }
        }

        private string _content;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                NotifyPropertyChanged("LineThree");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) 
        {
            if (null != PropertyChanged) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}