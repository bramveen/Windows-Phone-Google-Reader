using System;
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
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace GReader
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
			InitializeComponent();
            DataContext = App.ViewModel;
            this.Loaded +=new RoutedEventHandler(DetailsPage_Loaded);
        }
		
		private void DetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = MainListBox.SelectedIndex;
            string url = String.Format("/ItemsPage.xaml?url={0}",index.ToString());
            NavigationService.Navigate(new Uri(url, UriKind.Relative));
            
        }

    }
}