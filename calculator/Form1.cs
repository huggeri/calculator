using System;
using System.Windows.Forms;

namespace calculator
{

    public partial class myCalc : Form
    {

        static Value firstNumber = new Value(),
            secondNumber = new Value();
        // статические переменные для записи чисел
        
        // флаг выбранной операции
        static int flag = 0;
        // счётчик нажатий на равно
        static int cnt = 0;

        public myCalc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("5");
        }

        private void dot_Click(object sender, EventArgs e)
        {
            if (textBox.Text.IndexOf(",") == -1)
            {
            setDataIntoTheTextBox(",");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("9");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            setDataIntoTheTextBox("0");
        }

        private void multiplication_Click(object sender, EventArgs e)
        {
            try
            {
                getDataFromTextBox(firstNumber);
            }
            catch(Exception)
            {
                clearTextBox();
                setDataIntoTheTextBox(firstNumber.StringNumber);
            }
            flag = 1;
            cnt = 0;
        }

        private void division_Click(object sender, EventArgs e)
        {
            try
            {
                getDataFromTextBox(firstNumber);
            }
            catch(Exception)
            {
                clearTextBox();
                setDataIntoTheTextBox(firstNumber.StringNumber);
            }
            flag = 2;
            cnt = 0;
        }

        private void addition_Click(object sender, EventArgs e)
        {
            try
            {
                getDataFromTextBox(firstNumber);
            }
            catch(Exception)
            {
                clearTextBox();
                setDataIntoTheTextBox(firstNumber.StringNumber);
            }
            flag = 3;
            cnt = 0;
        }

        private void subtraction_Click(object sender, EventArgs e)
        {
            if(isTextBoxEmpty())
            {
                setDataIntoTheTextBox("-");
            }
            else
            {
                try
                {
                    getDataFromTextBox(firstNumber);
                }
                catch(Exception)
                {
                    clearTextBox();
                    setDataIntoTheTextBox(firstNumber.StringNumber);
                }
                flag = 4;
                cnt = 0;
            }
        }

        private void getResult_Click(object sender, EventArgs e)
        {
            if (cnt == 0)
            {
                try
                {
                    getDataFromTextBox(secondNumber);
                    cnt++;
                }
                catch(Exception)
                {
                    clearTextBox();
                    setDataIntoTheTextBox(secondNumber.StringNumber);
                }
            }
            else
            {
                clearTextBox();
            }

            switch (flag)
            {
                case 1:
                    firstNumber *= secondNumber;
                    setDataIntoTheTextBox(firstNumber.StringNumber);
                    break;
                case 2:
                    firstNumber /= secondNumber;
                    setDataIntoTheTextBox(firstNumber.StringNumber);
                    break;
                case 3:
                    firstNumber += secondNumber;
                    setDataIntoTheTextBox(firstNumber.StringNumber);
                    break;
                case 4:
                    firstNumber -= secondNumber;
                    setDataIntoTheTextBox(firstNumber.StringNumber);
                    break;
                case 5:
                    firstNumber = firstNumber.ToPow(firstNumber, secondNumber);
                    setDataIntoTheTextBox(firstNumber.StringNumber);
                    break;
                default:
                    setDataIntoTheTextBox(secondNumber.StringNumber);
                    break;
            }
        }

        private void radical_Click(object sender, EventArgs e)
        {
            getDataFromTextBox(firstNumber);
            firstNumber = firstNumber.GetRadical(firstNumber, 2);
            setDataIntoTheTextBox(firstNumber.StringNumber);
            flag = 0;
        }

        private void CubicRadical_Click(object sender, EventArgs e)
        {
            getDataFromTextBox(firstNumber);
            firstNumber = firstNumber.GetRadical(firstNumber, 3);
            setDataIntoTheTextBox(firstNumber.StringNumber);
            flag = 0;
        }

        private void powN_Click(object sender, EventArgs e)
        {
            getDataFromTextBox(firstNumber);
            flag = 5;
            cnt = 0;
        }

        private void factorialN_Click(object sender, EventArgs e)
        {
            getDataFromTextBox(firstNumber);
            firstNumber = firstNumber.Factorial(firstNumber);
            setDataIntoTheTextBox(firstNumber.StringNumber);
            flag = 0;
        }

        private void deleteData_Click(object sender, EventArgs e)
        {
            firstNumber.Val = 0;
            secondNumber.Val = 0;
            flag = 0;
            cnt = 0;
            clearTextBox();
        }

        // запись в текстовое окно данных
        private void setDataIntoTheTextBox(string str)
        {
            string text = textBox.Text;
            textBox.Text = text + str;
        }

        // получение данных из текстового окна, запись их в объект Value
        private void getDataFromTextBox(Value number)
        {
            number.StringNumber = textBox.Text;
            clearTextBox();
        }

        // очистка textBox
        private void clearTextBox()
        {
            textBox.Text = "";
        }

        // сравнение содержимого textBox с 0 или ""
        private bool isTextBoxEmpty()
        {
            bool result = false;
            if(textBox.Text == "0" || textBox.Text == "")
            {
                result = true;
            }

            return result;
        }
    }
}