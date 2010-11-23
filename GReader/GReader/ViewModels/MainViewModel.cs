using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace GReader
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
			this.Labels = new ObservableCollection<LabelViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
		public ObservableCollection<LabelViewModel> Labels {get; private set;} 

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        { 
            get
            {
                return _sampleProperty;
            }
            set
            {
                _sampleProperty = value;
                NotifyPropertyChanged("SampleProperty");
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.Items.Add(new ItemViewModel() { Title = "runtime one", Feedname = "Maecenas praesent accumsan bibendum", Content="Baljwerlkwjrelkjerlejrkjrwekrj" });
            this.Items.Add(new ItemViewModel() { Title = "runtime one", Feedname = "Maecenas praesent accumsan bibendum", Content="Baljwerlkwjrelkjerlejrkjrwekrj" });
            this.Items.Add(new ItemViewModel() { Title = "runtime one", Feedname = "Maecenas praesent accumsan bibendum", Content="Baljwerlkwjrelkjerlejrkjrwekrj" });
            this.Items.Add(new ItemViewModel() { Title = "runtime one", Feedname = "Maecenas praesent accumsan bibendum", Content="Baljwerlkwjrelkjerlejrkjrwekrj" });
			
			this.Labels.Add(new LabelViewModel() {Id="bla", Label="All", Unread=5});
			this.Labels.Add(new LabelViewModel() {Id="bla", Label="Funnies", Unread=5});
			this.Labels.Add(new LabelViewModel() {Id="bla", Label="Entrepeneurs", Unread=5});			

            this.IsDataLoaded = true;
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