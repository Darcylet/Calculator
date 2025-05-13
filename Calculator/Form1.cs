using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculator
{
    public partial class frmCalculator : Form
    {
        bool isEvaluated = false;

        public frmCalculator()
        {
            InitializeComponent();
        }

         private void button_click(object sender, EventArgs e)
 {
     Button button = (Button)sender;

     if (isEvaluated)
     {
         txtInput.Text = "";
         isEvaluated = false;
     }

     string input = txtInput.Text;
     string value = button.Text;

     if (value == "-" && (input.Length == 0 || input == "0" || "+-*/%".Contains(input[input.Length - 1])))
     {
         txtInput.Text = "-"; // Allow negative sign at the beginning or after an operator
     }
     else
     {
         if (input == "0" && value != "-")
         {
             txtInput.Text = "";
         }

         if (value == ".")
         {
             string[] parts = Regex.Split(input, @"[\+\-\*/]");
             string lastPart = parts[parts.Length - 1];

             if (!lastPart.Contains("."))
                 txtInput.Text += value;
         }
         else
         {
             txtInput.Text += value;
         }
     }
 }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string op = button.Text;

            if (isEvaluated)
            {
                isEvaluated = false;
            }

            if (txtInput.Text.Length == 0)
                return;

            char lastChar = txtInput.Text[txtInput.Text.Length - 1];

            if ("+-*/%".Contains(lastChar))
            {
              
                MessageBox.Show("Two operators entered consecutively. Use Clear Entry to fix or press '=' to see error.");
                txtInput.Text += op; 
            }
            else
            {
                txtInput.Text += op;
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            string expression = txtInput.Text;

            if (expression.Length == 0 || "+-*/%".Contains(expression[txtInput.Text.Length - 1]))
            {
                MessageBox.Show("Invalid expression. Please fix or use Clear Entry.");
                return;
            }

            try
            {
                
                var result = new DataTable().Compute(expression, null);

                if (result != null)
                {
                    txtInput.Text = result.ToString();
                    isEvaluated = true;
                }
                else
                {
                    MessageBox.Show("Error evaluating expression.");
                    txtInput.Text = "0";
                    isEvaluated = true;
                }
            }
            catch
            {
                MessageBox.Show("Error evaluating expression.");
                txtInput.Text = "0";
                isEvaluated = true;
            }
        }


        private void clear_click(object sender, EventArgs e)
        {
            txtInput.Text = "0";
            isEvaluated = false;
        }

        

        private void clearentry_click(object sender, EventArgs e)
        {
            if (txtInput.Text.Length == 0 || isEvaluated)
            {
                txtInput.Text = "0"; 
                return;
            }

            string input = txtInput.Text;

           
            txtInput.Text = input.Substring(0, input.Length - 1);

           
            if (txtInput.Text.Length == 0)
            {
                txtInput.Text = "0";
            }
        }

        private void frmCalculator_Load(object sender, EventArgs e)
        {

        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



