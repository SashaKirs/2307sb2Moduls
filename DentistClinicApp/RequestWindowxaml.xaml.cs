using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DentistClinicApp
{
    /// <summary>
    /// Логика взаимодействия для RequestWindowxaml.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        public Request Request { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public string[] Statuses { get; } = { "на рассмотрении", "в исполнении", "выполнено" };

        public RequestWindow()
        {
            InitializeComponent();
            LoadData();
            DataContext = this;
            Request = new Request { RegistrationDate = DateTime.Now, Status = "на рассмотрении" };
        }

        public RequestWindow(Request request)
        {
            InitializeComponent();
            LoadData();
            DataContext = this;
            Request = request;
            PopulateFields();
        }

        private void LoadData()
        {
            Users = new ObservableCollection<User>(DatabaseHelper.GetUsers());
        }

        private void PopulateFields()
        {
            ArticulTextBox.Text = Request.Articul;
            DescriptionTextBox.Text = Request.Description;
            TypeTextBox.Text = Request.Type;
            FullDescriptionTextBox.Text = Request.FullDescription;
            StatusComboBox.SelectedItem = Request.Status;
            UserComboBox.SelectedItem = Users.FirstOrDefault(u => u.Id == Request.UserId);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            
            Request.Articul = ArticulTextBox.Text;
            Request.Description = DescriptionTextBox.Text;
            Request.Type = TypeTextBox.Text;
            Request.FullDescription = FullDescriptionTextBox.Text;
            Request.Status = StatusComboBox.SelectedItem as string;
            if (UserComboBox.SelectedItem is User selectedUser)
            {
                Request.UserId = selectedUser.Id;
            }
            Request.RegistrationDate = DateTime.Now;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
