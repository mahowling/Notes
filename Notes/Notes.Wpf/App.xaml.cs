using Microsoft.Extensions.DependencyInjection;
using Notes.Wpf.UI.Windows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Notes.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Services for DependencyInjection
        /// </summary>
        public static IServiceProvider Services { get; protected set; }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //Initialise the Dependancy Injection
            RegisterServices();

            //Register Global Events
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.GotFocusEvent, new RoutedEventHandler(GlobalSelectAll));
            EventManager.RegisterClassHandler(typeof(ListBox), ListBox.SelectionChangedEvent, new RoutedEventHandler(GlobalScrollIntoView));

            //Load the MainWindow
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }


        /// <summary>
        /// Register the services for Dependency Injection
        /// </summary>
        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            //Add a single Data class
            services.AddSingleton<Data.IDataCalls, Data.Calls>();

            Services = services.BuildServiceProvider();
        }


        /// <summary>
        /// Global handler for GotFocusEvent that will automatically select the contents.
        /// </summary>
        private  void GlobalSelectAll(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TextBox)sender).SelectAll();
            }
            catch
            {
                //Ignore errors
            }
        }

        /// <summary>
        /// Global handled for SelectionChangedEvent that will automatically scroll the listbox item into view
        /// </summary>
        private void GlobalScrollIntoView(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = sender as ListBox;
                if (list?.SelectedItem!=null)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                        new Action(() => list.ScrollIntoView(list.SelectedItem)));
                }
            }
            catch
            {
                //Ignore errors
            }
        }
    }
}
