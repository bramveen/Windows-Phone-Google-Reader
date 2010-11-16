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
        SyndicationItem si = null;

        public View()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Page1_Loaded);
            FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
            si = (SyndicationItem)root.DataContext;
            PageTitle.DataContext = si.Title.Text;
            ApplicationTitle.DataContext = si.SourceFeed.Title.Text;
        }

        void Page1_Loaded(object sender, RoutedEventArgs e)
        {

            TextSyndicationContent sc = (TextSyndicationContent)si.Content;
            interwebs.NavigateToString(sc.Text);
        }
    }
}