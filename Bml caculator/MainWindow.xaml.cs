using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace Bml_caculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [XmlRoot ("BMI Calc", Namespace = "www.bmicalc.ninja")]
    public partial class MainWindow : Window
    {
        public string Filepath = "C:/Temp/";
        public string FileName = "yourBMI.xml";

    
    internal class Customer
        {
            [XmlAttribute("Last Name")]
            public string LastName { get; set; }
            [XmlAttribute("First Name")]
            public string FirstName { get; set; }
            [XmlAttribute("Phone Name")]
            public string PhoneNumber { get; set; }
            [XmlAttribute("Height")]
            public int heightInches { get; set; }
            [XmlAttribute("Weight")]
            public int weightLbs { get; set; }
            [XmlAttribute("Customer BMI")]
            public int customerBMI { get; set; }
            [XmlAttribute("status")]
            public string statusTitle { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            xPhoneBox.Text = "";
            xFirstNameBox.Text = "";
            xLastNameBox.Text = "";
            xHeightInchesBox.Text = "";
            xWeightLbsBox.Text = "";
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);

        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer customer1 = new Customer();

            customer1.FirstName = xFirstNameBox.Text;
            customer1.LastName = xLastNameBox.Text;
            customer1.PhoneNumber = xPhoneBox.Text;

            int currentWeight = 0;
            int currentHeight = 0;
            Int32.TryParse(xWeightLbsBox.Text, out currentWeight);
            Int32.TryParse(xHeightInchesBox.Text, out currentHeight);
            customer1.weightLbs = currentWeight;
            customer1.heightInches = currentHeight;

            int bmi;
            bmi = 703 * customer1.weightLbs / (customer1.heightInches * customer1.heightInches);
            customer1.customerBMI = bmi;

            string yourBMIstatus = "NA";
            customer1.statusTitle = yourBMIstatus;

            MessageBox.Show($"The Customer's name is {customer1.FirstName} {customer1.LastName} and they can be reached at {customer1.PhoneNumber}. They are {customer1.heightInches} inches tall. Their weight is {customer1.weightLbs} Their BMI is {bmi}");

          
            if (bmi < 18.5)
            {
                Console.WriteLine("According to CDC.gov BMI Calculator \nyou are underweight.");
                customer1.statusTitle = "Underweight";
            }
            else if (bmi < 24.9)
            {
                Console.WriteLine("According to CDC.gov BMI Calculator \nyou have a normal body weight.");
                customer1.statusTitle = "Normal";
            }
            else if (bmi < 29.9)
            {
                Console.WriteLine("According to CDC.gov BMI Calculator \nyou are overweight.");
                customer1.statusTitle = "Overweight";
            }
            else
            {
                Console.WriteLine("According to CDC.gov BMI Calculator \nyou are obese.");
                customer1.statusTitle = "Obese";
            }
            xYourBMIResults.Text = customer1.customerBMI.ToString();
            xBMIMessage.Text = yourBMIstatus;


            TextWriter writer = new StreamWriter(Filepath + FileName);
            XmlSerializer ser = new XmlSerializer(typeof(Customer));

            ser.Serialize(writer, customer1);
            writer.Close();

        }
    }
}
  ///still on pt3