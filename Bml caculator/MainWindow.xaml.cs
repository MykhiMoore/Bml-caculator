using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
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
using System.Xml;
using System.Xml.Serialization;

namespace Bml_caculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [XmlRoot ("BMI Calc", Namespace = "www.bmicalc.ninja")]
    public partial class MainWindow : Window
    {
        public string FilePath = "C:\\Users\\mykhi\\source\\repos\\Bml-caculator\\Bml caculator";
        public string FileName = "yourBMI.xml";
        public class Customer

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


            if (customer1.customerBMI < 18.5)
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you are underweight.";
                customer1.statusTitle = "Underweight";
            }
            else if (customer1.customerBMI < 24.9)
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you have a normal body weight.";
                customer1.statusTitle = "Normal";
            }
            else if (customer1.customerBMI < 29.9)
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you are overweight.";
                customer1.statusTitle = "Overweight";
            }
            else
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you are obese.";
                customer1.statusTitle = "Obese";

            }
            MessageBox.Show($"BMI: {customer1.customerBMI}\nStatus: {customer1.statusTitle}");

            xYourBMIResults.Text = customer1.customerBMI.ToString();
            xBMIMessage.Text = yourBMIstatus;

            TextWriter writer = new StreamWriter(FilePath + FileName);
            XmlSerializer ser = new XmlSerializer(typeof(Customer));

            ser.Serialize(writer, customer1);
            writer.Close();


        }

        private void OnLoadStats() 
        {
            Customer cust = new Customer();
            
            XmlSerializer des = new XmlSerializer(typeof(Customer));
            using (XmlReader reader = XmlReader.Create(FilePath+FileName)) 
            {
                cust = (Customer) des.Deserialize(reader);



                xLastNameBox.Text = cust.LastName;
                xFirstNameBox.Text = cust.FirstName;
                xPhoneBox.Text = cust.PhoneNumber;
            }

            DataSet xmlData = new DataSet();
            xmlData.ReadXml(FilePath + FileName, XmlReadMode.Auto);
            xDataGrid.ItemsSource = xmlData.Tables[0].DefaultView;
        }
    }
}