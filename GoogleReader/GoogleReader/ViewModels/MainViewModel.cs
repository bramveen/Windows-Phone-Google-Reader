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
using System.ServiceModel.Syndication;
using System.Windows.Threading;


namespace GoogleReader
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ReaderHandler _rh = null;
        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<SyndicationItem> Items { get; private set; }
        public ObservableCollection<Catagory> Catagories { get; private set; }

        public MainViewModel()
        {
            _rh = new ReaderHandler("bramveenhof@gmail.com", "G3h31m3n");
            _rh.ReadingListRefreshed += new ReaderHandler.ReadingListRefreshedHandler(_rh_FeedsRefreshed);
            _rh.Initiated += new ReaderHandler.InitiatedHandler(_rh_Initiated);

            this.Items = new ObservableCollection<SyndicationItem>();
            this.Catagories = new ObservableCollection<Catagory>();
        }

        void _rh_Initiated(object sender, EventArgs e)
        {
            _rh.GetReadingList();
            _rh.GetSubscriptions();
        }

        void _rh_FeedsRefreshed(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    LoadData();
                });
        }



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
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
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
            foreach (SyndicationItem i in _rh.ReadingList.Items)
            {
                this.Items.Add(i);
            }

            foreach(Catagory c in _rh.Catagories)
            {
                this.Catagories.Add(c);
            }

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}