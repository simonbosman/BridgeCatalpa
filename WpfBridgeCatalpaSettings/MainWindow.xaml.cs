using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading;

namespace BridgeCatalpa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BridgeCatalpaReg bridgeCatalpaReg;
        private FolderBrowserDialog folderBrowserDialog1;
        private DialogResult result;

        public MainWindow()
        {
            InitializeComponent();
            bridgeCatalpaReg = new BridgeCatalpaReg();
            folderBrowserDialog1 = new FolderBrowserDialog();

            textBox2.Text = bridgeCatalpaReg.getRegValue("userId");
            string pass = bridgeCatalpaReg.getRegValue("password");
            if (pass.Length == 0)
                passwordBox1.Password = "";
            else
                passwordBox1.Password = EncryptPassword.Decrypt(pass, "cat777");
            textBox4.Text = bridgeCatalpaReg.getRegValue("IN");
            textBox5.Text = bridgeCatalpaReg.getRegValue("OUT");
            textBox6.Text = bridgeCatalpaReg.getRegValue("FOUT");
            textBox7.Text = bridgeCatalpaReg.getRegValue("LOG");
            checkBox1.IsChecked = bridgeCatalpaReg.getRegValue("CheckDebId").Equals("True");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bridgeCatalpaReg.setRegValue("userId", textBox2.Text);
                bridgeCatalpaReg.setRegValue("password", EncryptPassword.Encrypt(passwordBox1.Password, "cat777"));
                bridgeCatalpaReg.setRegValue("IN", textBox4.Text);
                bridgeCatalpaReg.setRegValue("OUT", textBox5.Text);
                bridgeCatalpaReg.setRegValue("FOUT", textBox6.Text);
                bridgeCatalpaReg.setRegValue("LOG", textBox7.Text);
                bridgeCatalpaReg.setRegValue("CheckDebId", checkBox1.IsChecked);

                ServiceController service;
                service = new ServiceController("BridgeCatalpa");

                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();

                    while (service.Status == ServiceControllerStatus.Stopped)
                    {
                        Thread.Sleep(1000);
                        service.Refresh();
                    }
                }
                else
                {
                    service.Stop();

                    while (service.Status != ServiceControllerStatus.Stopped)
                    {
                        Thread.Sleep(1000);
                        service.Refresh();
                    }

                    service.Start();

                    while (service.Status == ServiceControllerStatus.Stopped)
                    {
                        Thread.Sleep(1000);
                        service.Refresh();
                    }
                }
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Colors.Green;
                label8.Foreground = mySolidColorBrush;
                label8.Content = "Instellingen opgeslagen";
                label8.Visibility = Visibility.Visible;
                System.Windows.MessageBox.Show("Instellingen zijn opgeslagen", "Status");
            }
            catch (Exception excep)
            {
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Colors.Red;
                label8.Foreground = mySolidColorBrush;
                label8.Content = "Fout bij opslaan";
                label8.Visibility = Visibility.Visible;
                System.Windows.MessageBox.Show(excep.Message + e.Source, "Status");
            }



        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            result = folderBrowserDialog1.ShowDialog();
            string folderName = "";

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                textBox4.Text = folderName;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            result = folderBrowserDialog1.ShowDialog();
            string folderName = "";

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                textBox5.Text = folderName;
            }

        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            result = folderBrowserDialog1.ShowDialog();
            string folderName = "";

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                textBox6.Text = folderName;
            }

        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            result = folderBrowserDialog1.ShowDialog();
            string folderName = "";

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                textBox7.Text = folderName;
            }

        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
