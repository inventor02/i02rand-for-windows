using MetroLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace i02rand
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ILogger logger = LogManagerFactory.DefaultLogManager.GetLogger<MainPage>();

        StoreContext context = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        public async void DisplayGenericDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                PrimaryButtonText = "Roger"
            };

            ContentDialogResult result = await dialog.ShowAsync();
        }

        public async void GetAddOnInfo()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }

            string[] productKinds = {"Durable", "Consumable", "UnmanagedConsumable"};
            List<String> filterList = new List<string>(productKinds);

            StoreProductQueryResult queryResult = await context.GetAssociatedStoreProductsAsync(filterList);

            if (queryResult.ExtendedError != null)
            {
                logger.Error("Could not get add-on info: " + queryResult.ExtendedError.Message);
                return;
            }

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products)
            {
                StoreProduct product = item.Value;
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSplitView.IsPaneOpen = !HamburgerSplitView.IsPaneOpen;

            DisplayGenericDialog("Hold up", "")
        }
    }
}
