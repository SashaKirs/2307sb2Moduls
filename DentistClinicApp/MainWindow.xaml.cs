using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DentistClinicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public partial class MainWindow : Window
        {
            private ObservableCollection<Request> Requests { get; set; }

            public MainWindow()
            {
                InitializeComponent();
                LoadRequests();
            }

            private void LoadRequests()
            {
                var list = DatabaseHelper.GetRequests();
                Requests = new ObservableCollection<Request>(list);
                RequestsDataGrid.ItemsSource = Requests;
            }

            private void BtnAdd_Click(object sender, RoutedEventArgs e)
            {
                var window = new RequestWindow();
                if (window.ShowDialog() == true)
                {
                    DatabaseHelper.AddRequest(window.Request);
                    LoadRequests();
                }
            }

            private void BtnEdit_Click(object sender, RoutedEventArgs e)
            {
                if (RequestsDataGrid.SelectedItem is Request selectedRequest)
                {
                    var window = new RequestWindow(selectedRequest);
                    if (window.ShowDialog() == true)
                    {
                        DatabaseHelper.UpdateRequest(window.Request);
                        LoadRequests();
                    }
                }
            }

            private void BtnDelete_Click(object sender, RoutedEventArgs e)
            {
                if (RequestsDataGrid.SelectedItem is Request selectedRequest)
                {
                    DatabaseHelper.DeleteRequest(selectedRequest.Id);
                    LoadRequests();
                }
            }

            private void BtnChangeStatus_Click(object sender, RoutedEventArgs e)
            {
                if (RequestsDataGrid.SelectedItem is Request selectedRequest)
                {
                  
                    var statuses = new[] { "на рассмотрении", "в исполнении", "выполнено" };
                    var currentIndex = System.Array.IndexOf(statuses, selectedRequest.Status);
                    var newStatus = statuses[(currentIndex + 1) % statuses.Length];
                    selectedRequest.Status = newStatus;
                    DatabaseHelper.UpdateRequest(selectedRequest);
                    LoadRequests();
                }
            }
        }
    }