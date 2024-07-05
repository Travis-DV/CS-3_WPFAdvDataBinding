using System.Collections.ObjectModel;
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
using System.Globalization;

namespace WPFAdvDataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<Person> people = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();

            PersonConverter personConverter = new PersonConverter();

            people.Add(new Person()
            {
                FirstName = "Travis",
                LastName = "Findley",
                Salary = 100000,
                BirthDate = DateTime.Parse("06-22-2007 09:09:09"),
                Converter = personConverter
            });
            people.Add(new Person()
            {
                FirstName = "Adrianna",
                LastName = "Findley",
                Salary = 100000,
                BirthDate = DateTime.Parse("01-05-2004 10:59:29"),
                Converter = personConverter
            });

            DataContext = this;

            LV.ItemsSource = people;
        }
    }

    record Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - BirthDate.Year;

                if (DateTime.Now < BirthDate.AddYears(age))
                {
                    age--;
                }

                return age;
            }

        }
        public double Salary { get; set; }
        public DateTime BirthDate { get; set; }

        public IMultiValueConverter Converter { get; set; }

        public override string ToString()
        {
            if (Converter == null)
            {
                return "ERROR";
            }
            return $"{LastName}, {FirstName} (Age: {Age}, Salary: {Salary}, Birth Date: {BirthDate}";
        }
    }

    public class PersonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string firstName &&
                values[1] is string lastName &&
                values[2] is int age &&
                values[3] is double salary &&
                values[4] is DateTime birthDate)
            {
                return $"{lastName}, {firstName} (Age: {age}, Salary: {salary}, Birth Date: {birthDate}"; ;
            }
            return "ERROR";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

