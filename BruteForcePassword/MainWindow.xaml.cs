using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BruteForcePassword
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThreadSlider.ValueChanged += ThreadSlider_ValueChanged; // Event handler for slider value change
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Placeholder for text box text change event
        }

        byte[] key, IV, encryptedPass; // Fields to store encryption parameters

        private void ThreadSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Update label content based on slider value
            switch ((int)ThreadSlider.Value)
            {
                case 0:
                    ThreadLabel.Content = "0%";
                    break;
                case 1:
                    ThreadLabel.Content = "25%";
                    break;
                case 2:
                    ThreadLabel.Content = "50%";
                    break;
                case 3:
                    ThreadLabel.Content = "75%";
                    break;
                case 4:
                    ThreadLabel.Content = "100%";
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string content = Content.Text; // Get text from Content text box
            int maxThreads = CalculateMaxThreads((int)ThreadSlider.Value); // Calculate max threads based on slider value
            if (maxThreads <= 0)
            {
                MessageBox.Show("Please select a valid number of threads."); // Show error if invalid number of threads
                return;
            }

            Password pass = new Password(); // Create a Password object
            pass.password = content; // Set the password field
            (key, IV, encryptedPass) = pass.encryptPassword(content); // Encrypt the password and get encryption parameters
            string result = BruteForceAttack.GetResult(encryptedPass, key, IV, maxThreads); // Perform brute-force attack
            MessageBox.Show("Your Password is: " + result); // Show the result
        }

        private int CalculateMaxThreads(int sliderValue)
        {
            int totalThreads = Environment.ProcessorCount; // Get the number of processor cores
            // Determine number of threads based on slider value
            switch (sliderValue)
            {
                case 0:
                    return 0;
                case 1:
                    return totalThreads / 4;
                case 2:
                    return totalThreads / 2;
                case 3:
                    return (totalThreads * 3) / 4;
                case 4:
                    return totalThreads;
                default:
                    return 0;
            }
        }
    }
}
