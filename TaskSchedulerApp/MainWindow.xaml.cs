using System;
using System.ComponentModel; // для работы BindingList
using TaskSchedulerApp.Models; //для работы <TaskModel>
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using TaskSchedulerApp.Services;

namespace TaskSchedulerApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\_taskDateList.json";
        private BindingList<TaskModel> _taskDateList;
        private FeileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FeileIOService(PATH);

            try
            {
                _taskDateList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            

            dgToolList.ItemsSource = _taskDateList; //Привязка данных к полю "Data" в окне приложения

            //Отслеживание изменений списка задач
            _taskDateList.ListChanged += _taskDateList_ListChanged;
        }

        //Сохранение данных на жесткий диск компьютера
        private void _taskDateList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveDate(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
            }

        }
    }
}
