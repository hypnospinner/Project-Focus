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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo.Models;
using ToDo.ModelsView;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DB_API db;

        public MainWindow()
        {
            InitializeComponent();
            db = new DB_API();
            db.start();
        }

        private void MakeNoteToDB_Button_Click(object sender, RoutedEventArgs e)
        {
            string s = "INSERT INTO task VALUES ('homework', 'matan')";
            db.insert(s);
        }
    }
}
