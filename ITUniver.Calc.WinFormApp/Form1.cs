using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ITUniver.Calc.WinFormApp
{
    public partial class Form1 : Form
    {
        private ConsoleCalc.Calc calc { get; set; }
        public Form1()
        {
            InitializeComponent();

            #region Загрузка операций

            calc = new ConsoleCalc.Calc();

            var operations = calc.GetOperNames();

            cbOperation.DataSource = operations;
            
            //cbOperation.Items.Clear();
            //cbOperation.Items.AddRange(operations);

            #endregion
        }

        private void btnLuck_Click(object sender, EventArgs e)
        {
            tbResult.Text = "Успех!";
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            // получить операцию
            var oper = $"{cbOperation.SelectedItem}";

            // полуить данные
            var args = tbInput.Text
                .Trim()
                .Split(' ')
                .Select(str => Convert.ToDouble(str))
                .ToArray();

            //выислить результат
            var result = calc.Exec(oper, args);

            // показать результат
            tbResult.Text = $"{result}";
        }

        private void cbOperation_TextChanged(object sender, EventArgs e)
        {
            if(cbOperation.Text != "")
            {
                tbInput.Enabled = true;
            }
            else
            {
                tbInput.Enabled = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbInput.Clear();
            tbResult.Clear();
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            btnCalc.Enabled = true;
        }


        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 0x20 && number != '\u0016') //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }
    }
}
