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
using Microsoft.Phone.Controls;

namespace GReader
{
    public partial class ItemsPage : PhoneApplicationPage
    {
        public ItemsPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ItemsPage_Loaded);
            DataContext = App.ViewModel;
        }

        void ItemsPage_Loaded(object sender, RoutedEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            int index =  Convert.ToInt32(parameters["url"]);
            InterWeb.NavigateToString(App.ViewModel.Items[index].Content);
        }
    }
}
