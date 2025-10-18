using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculadora
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            btn0.Click += Numero_Click;
            btn1.Click += Numero_Click;
            btn2.Click += Numero_Click;
            btn3.Click += Numero_Click;
            btn4.Click += Numero_Click;
            btn5.Click += Numero_Click;
            btn6.Click += Numero_Click;
            btn7.Click += Numero_Click;
            btn8.Click += Numero_Click;
            btn9.Click += Numero_Click;
            btnPonto.Click += Numero_Click;

            btnSomar.Click += Operador_Click;
            btnSubtrair.Click += Operador_Click;
            btnMultiplicar.Click += Operador_Click;
            btnDividir.Click += Operador_Click;

            btnLimpar.Click += Limpar_Click;
            btnResultado.Click += Resultado_Click;
            btnBackSpace.Click += BackSpace_Click;

            this.KeyPreview = true; // Permite que o Form capture antes dos controles
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                Resultado_Click(btnResultado, EventArgs.Empty);
                return true; 
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                    Numero_Click(btn0, EventArgs.Empty);
                    break;
                case Keys.NumPad1:
                    Numero_Click(btn1, EventArgs.Empty);
                    break;
                case Keys.NumPad2:
                    Numero_Click(btn2, EventArgs.Empty);
                    break;
                case Keys.NumPad3:
                    Numero_Click(btn3, EventArgs.Empty);
                    break;
                case Keys.NumPad4:
                    Numero_Click(btn4, EventArgs.Empty);
                    break;
                case Keys.NumPad5:
                    Numero_Click(btn5, EventArgs.Empty);
                    break;
                case Keys.NumPad6:
                    Numero_Click(btn6, EventArgs.Empty);
                    break;
                case Keys.NumPad7:
                    Numero_Click(btn7, EventArgs.Empty);
                    break;
                case Keys.NumPad8:
                    Numero_Click(btn8, EventArgs.Empty);
                    break;
                case Keys.NumPad9:
                    Numero_Click(btn9, EventArgs.Empty);
                    break;
                case Keys.Decimal:
                    Numero_Click(btnPonto, EventArgs.Empty);
                    break;
                case Keys.None:   // ponto do teclado numérico
                    Numero_Click(btnPonto, EventArgs.Empty);
                    break;
                case Keys.Add:       // +
                    Operador_Click(btnSomar, EventArgs.Empty);
                    break;

                case Keys.Subtract:  // -
                    Operador_Click(btnSubtrair, EventArgs.Empty);
                    break;

                case Keys.Multiply:  // *
                    Operador_Click(btnMultiplicar, EventArgs.Empty);
                    break;

                case Keys.Divide:    // /
                    Operador_Click(btnDividir, EventArgs.Empty);
                    break;
            }
        }


        public void Numero_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            inserirDisplay(botao.Text);
            double result = Calculos();
            if (result > 0) { 
                pre_result.Text = result.ToString();
            }           
        }

        private void Operador_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;            
            if(operator_signal.Text != "")
            {
                inputA.Text = pre_result.Text;
                inputB.Text = "";
                operator_signal.Text = botao.Text;
                pre_result.Text = "";
            } else if(inputA.Text != "")
            {
                operator_signal.Text = botao.Text;
            }
        }

        private void Limpar_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            inputA.Text = "";
            inputB.Text = "";
            operator_signal.Text = "";
            pre_result.Text = "";
        }
        private void Resultado_Click(object sender, EventArgs e)
        {
            if(operator_signal.Text != "" && inputB.Text != "")
            {
                double resrult = Calculos();
                pre_result.Text = resrult.ToString();
                inputA.Text = resrult.ToString();
                operator_signal.Text = "";
                inputB.Text = "";
            }
        }
        private void BackSpace_Click(object sender, EventArgs e) 
        {
            if (operator_signal.Text == "")
            {
                inputA.Text = BackspaceInfo(inputA.Text);
                double resrult = Calculos();
                if(resrult > 0)
                {
                    pre_result.Text = resrult.ToString();
                } else
                {
                    pre_result.Text = "";
                }
            }
            else {
                inputB.Text = BackspaceInfo(inputB.Text);
                double resrult = Calculos();
                if (resrult > 0)
                {
                    pre_result.Text = resrult.ToString();
                }
                else
                {
                    pre_result.Text = "";
                }
            }
        }

        private String BackspaceInfo(String linha)
        {
            if(linha == "")
            {
                return "";
            }

            char[] caracteres = linha.ToCharArray(); // tranforma texto em array de caracteres
            Array.Resize(ref caracteres, caracteres.Length - 1); // remove o ultimo item do array
            string newInfo = string.Concat(caracteres); // tranforma em string o array de caracteres concatenando os itens do array
            return newInfo;
        }

        private void inserirDisplay(object value)
        {
            if (operator_signal.Text == "") {
                inputA.Text +=  value.ToString();
            } 
            else {
                inputB.Text += value.ToString();
            }
        }

        private double Calculos()
        {
            if(operator_signal.Text != "" && inputA.Text != "" && inputB.Text != "")
            {
                double a = Convert(inputA.Text);
                double b = Convert(inputB.Text);

                switch (operator_signal.Text)
                {
                    case "÷":
                        return a / b;
                    case "x":
                        return a * b;
                    case "+":
                        return a + b;
                    case "-":
                        return a - b;
                }           
            }
            return 0;            
        }

        private double Convert(string value) {
            if (value.Contains(".") || value.Contains(","))
            {
                return double.Parse(value.Replace(",", "."), CultureInfo.InvariantCulture);
            }
            return double.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}
