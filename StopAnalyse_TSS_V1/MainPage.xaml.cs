using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StopAnalyse_TSS_V1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        
        private void textBox_Command_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                textBlockStatus.Text = textBoxCommand.Text.Substring(0, 5);
                parseCommand(textBoxCommand.Text);
                textBoxCommand.Text = "";

            }
        }

        private async void parseCommand(string cmd)
        {
            if (cmd.Length > 2)
            {
                if (cmd.Substring(0, 3).Equals("STO"))
                {
                    HttpBaseProtocolFilter filter;
                    HttpClient httpClient;
                    String resourceUri;
                    Uri uri;

                    resourceUri = "http://192.168.1.100/getBatch.php?id=12QW34as&batch_storage=" + cmd;

                    filter = new HttpBaseProtocolFilter();
                    httpClient = new HttpClient(filter);
                    Uri.TryCreate(resourceUri, UriKind.Absolute, out uri);

                    textBlockStatus.Text = "Connecting to server";

                    HttpResponseMessage response;
                    try
                    {
                        response = await httpClient.GetAsync(uri);
                        String responseBodyAsText = await response.Content.ReadAsStringAsync();


                        XElement result = XElement.Parse(await response.Content.ReadAsStringAsync());

                        XElement order = result.Element("order");

                        textBlockOrderCode.Text = order.Element("order_code").Value;
                        textBlockOrderProduct.Text = order.Element("order_product").Value;
                        textBlockOrderQuantity.Text = order.Element("order_quantity").Value;
                        textBlockOrderStart.Text = order.Element("order_start").Value;
                        textBlockOrderComplete.Text = order.Element("order_complete").Value;

                        listViewBatches.Items.Clear();


                        foreach (XElement batch in order.Elements("batch"))
                        {
                            ListViewItem li = new ListViewItem();

                            li.Content = batch.Element("batch_storage").Value;
                            li.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                            li.Margin = new Thickness(0.0);
                            li.Padding = new Thickness(12.0, 0.0, 0.0, 0.0);
                            li.Height = 30;
                            listViewBatches.Items.Add(li);



                            if (batch.Element("batch_focus").Value.Equals("1"))
                            {
                                //listViewBatches.SelectedItem = listViewBatches.Items.ElementAt(listViewBatches.Items.Count - 1);
                                listViewBatches.SelectedItem = li;
                                ListViewItem sli = listViewBatches.SelectedItem as ListViewItem;
                                sli.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

                                textBlockBatchStorage.Text = batch.Element("batch_storage").Value;
                                textBlockBatchId.Text = batch.Element("batch_id").Value;
                                textBlockBatchQuantity.Text = batch.Element("batch_quantity").Value;
                                textBlockBatchStart.Text = batch.Element("batch_start").Value;
                                textBlockBatchComplete.Text = batch.Element("batch_complete").Value;
                                textBlockBatchComment.Text = batch.Element("batch_comment").Value;
                            }



                        }


                    }
                    catch (Exception ex)
                    {
                        textBlockStatus.Text = "Error: " + ex.Message + "\n" + ex.GetType().ToString();
                    }
                }
            }
        }

        private void listViewBatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem sli = listViewBatches.SelectedItem as ListViewItem;
            sli.Background = new SolidColorBrush(Colors.White);
        }

        private void textBoxCommand_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Addcourse_Click(object sender, RoutedEventArgs e)
        {

        }
        */
    }

}

    /*

public class CustomListItem
{
    public string ID { get; set; }
    public string Description { get; set; }
}
*/