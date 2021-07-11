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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate double Calculate(double n1, double n2);
        public event Calculate CalcEvent;
        double? temp;

        void ClearEvent()
        {
            try
            {
                foreach (Delegate d in CalcEvent.GetInvocationList())
                {
                    CalcEvent -= (Calculate)d;
                }
            }
            catch (Exception)
            {

            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CalculateBox.Text = CalculateBox.Text == "0" || CalculateBox.Text == "" ? ((sender as Button).Content as TextBlock).Text : CalculateBox.Text + ((sender as Button).Content as TextBlock).Text;
        }

        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            CalculateBox.Text = CalculateBox.Text.Length > 0 ? CalculateBox.Text.Substring(0, CalculateBox.Text.Length - 1) : "";
        }

        double Plus(double n1, double n2)
        {
            return n1 + n2;
        }

        double Minus(double n1, double n2)
        {
            return n1 - n2;
        }

        double Mult(double n1, double n2)
        {
            return n1 * n2;
        }

        double Div(double n1, double n2)
        {
            try
            {
                return n1 / n2;
            }
            catch (DivideByZeroException)
            {
                CalculateBox.Text = "ERROR! Division by zero!";
                System.Threading.Thread.Sleep(2000);
                return 0;
            }
        }

        private void Button_Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                char ch = CalculationsBox.Text[CalculationsBox.Text.Length - 1];
                CalculationsBox.Text = temp.ToString() + ch + CalculateBox.Text;
                CalculateBox.Text = CalculateBox.Text == "" ? CalculateBox.Text : (CalcEvent.Invoke(temp ?? 0, Double.Parse(CalculateBox.Text))).ToString();
                ClearEvent();
            }
            catch (Exception) { }
        }

        private void Button_ClearLast_Click(object sender, RoutedEventArgs e)
        {
            CalculateBox.Text = "";
        }

        private void Button_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            CalculateBox.Text = "";
            CalculationsBox.Text = "";
        }

        private void Button_Negative_Click(object sender, RoutedEventArgs e)
        {
            CalculateBox.Text = (-(Double.Parse(CalculateBox.Text))).ToString();
        }

        private void Button_Period_Click(object sender, RoutedEventArgs e)
        {
            if (!CalculateBox.Text.Contains(',') && CalculateBox.Text.Length > 0)
            {
                CalculateBox.Text += ',';
            }
        }

        private void Button_Calc_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            TextBlock textBlock = button.Content as TextBlock;
            ClearEvent();
            switch (textBlock.Text)
            {
                case "+":
                    CalcEvent += Plus;
                    break;
                case "-":
                    CalcEvent += Minus;
                    break;
                case "*":
                    CalcEvent += Mult;
                    break;
                case "/":
                    CalcEvent += Div;
                    break;
            }
            try
            {
                double tmptmptmp = Double.Parse(CalculateBox.Text);
                temp = tmptmptmp;
            }
            catch (Exception) { }
            finally
            {
                if (CalculationsBox.Text.Length > 0)
                {
                    if (CalculationsBox.Text[CalculationsBox.Text.Length - 1] == '+' ||
                        CalculationsBox.Text[CalculationsBox.Text.Length - 1] == '-' ||
                        CalculationsBox.Text[CalculationsBox.Text.Length - 1] == '*' ||
                        CalculationsBox.Text[CalculationsBox.Text.Length - 1] == '/')
                    {
                        if (CalculateBox.Text == "")
                        {
                            CalculateBox.Text = CalculationsBox.Text.Substring(0, CalculationsBox.Text.Length - 1);
                        }
                    }
                }
                CalculationsBox.Text = CalculateBox.Text + textBlock.Text;
                CalculateBox.Text = "";
            }
        }
    }
}
