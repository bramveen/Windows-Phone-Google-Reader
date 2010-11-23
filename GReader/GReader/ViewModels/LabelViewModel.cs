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
	public class LabelViewModel : INotifyPropertyChanged 
	{
		public LabelViewModel()
		{
			// Insert code required on object creation below this point.
		}
		
		public string Id {get; set;}
		public string Label {get; set;}
		public int Unread {get; set;}
		
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