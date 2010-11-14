﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ServiceModel.Syndication;

namespace GoogleReader
{
    public partial class MainPage : PhoneApplicationPage
    {
        ReaderHandler _rh = null; 
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _rh = new ReaderHandler("bramveenhof@gmail.com", "G3h31m3n");
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
<<<<<<< HEAD
        {
            
=======
        {           
>>>>>>> ba3cdb6f474ac4a54a30566e4b9630f85eb31b40
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            toread.DataContext = _rh.Feed.Items;
        }
    }

    public class LinkFormatter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Collection<SyndicationLink>)value).FirstOrDefault().Uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

