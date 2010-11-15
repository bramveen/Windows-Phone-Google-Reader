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
using System.ServiceModel.Syndication;

namespace GoogleReader
{
    public partial class View : PhoneApplicationPage
    {
        public View()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Page1_Loaded);
        }

        void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            string _url = string.Empty;
            this.NavigationContext.QueryString.TryGetValue("Url", out _url);
            FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
            SyndicationItem si = (SyndicationItem)root.DataContext;
            TextSyndicationContent sc = (TextSyndicationContent)si.Content;
            interwebs.NavigateToString(sc.Text);
        }
    }
}