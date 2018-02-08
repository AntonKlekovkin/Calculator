using ITUniver.Calc.DB.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace ITUniver.Calc.WinFormApp
{
    public partial class Form1 : Form
    {
        private double CalcTime = 0;
        private static IHistoryRepository History = new MemoryRepository();
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
                
        private void btnCalc_Click(object sender, EventArgs e)
        {
            tbInput.Focus();
            tbInput_Click(sender, e);

            timer1.Enabled = false;
            CalcTime = 0;

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

            // добавить в историю в БД
            MyHelper.AddToHistory(oper, args, result);
            
            // добавить в историю на форму
            //lbHistory.Items.Add($"{result}");
            var items = MyHelper.GetAll();
            lbHistory.Items.Clear();
                        
            lbHistory.Items.AddRange(items.Select(it => 
                it.Operation + 
                "(" + 
                it.Args.Replace(' ', ',') + 
                ")=" 
                + it.Result.ToString())
                .ToArray() );

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

            timer1.Enabled = true;
            CalcTime = 0;


        }


        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 0x20 && number != '\u0016') //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                //btnCalc.PerformClick();
            }
        }

        private void tbInput_Click(object sender, EventArgs e)
        {
            tbInput.SelectAll();
        }

        private void tbInput_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                btnCalc_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CalcTime += timer1.Interval;

            if(CalcTime==1000 && tbInput.Text != "")
            {
                btnCalc_Click(sender, e);
                timer1.Enabled = false;
            }
        }
    }
}
