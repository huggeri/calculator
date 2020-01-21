using System;
using System.Windows.Forms;

namespace calculator
{
    public partial class myCalc : Form
    {

        // статические переменные для записи чисел
        static private Value firstNumber = new Value(),
            secondNumber = new Value();
        
        // флаг выбранной операции
        static private int numberOfOperation = 0;
        // счётчик нажатий на равно (последняя операция в памяти)
        static private bool isEqualityPushed = false;
        // данные записаны
        static private bool dataHasBeenWritten = false;

        public myCalc()
        {
            InitializeComponent();

            this.KeyDown +=
                new KeyEventHandler(textBox_KeyDown);
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
            clickOnOperation(1);
        }

        private void division_Click(object sender, EventArgs e)
        {
            clickOnOperation(2);
        }

        private void addition_Click(object sender, EventArgs e)
        {
            clickOnOperation(3);
        }

        private void subtraction_Click(object sender, EventArgs e)
        {
            if (isTextBoxEmpty() || dataHasBeenWritten)
            {
                setDataIntoTheTextBox("-");
            }
            else
            {
                clickOnOperation(4);
            }
        }

        private void powN_Click(object sender, EventArgs e)
        {
            clickOnOperation(5);
        }

        private void radical_Click(object sender, EventArgs e)
        {
            clickOnOperation(6);
        }

        private void factorialN_Click(object sender, EventArgs e)
        {
            clickOnOperation(7);
        }
        
        private void oneDividedByN_Click(object sender, EventArgs e)
        {
            clickOnOperation(8);
        }
        
        private void open_Click(object sender, EventArgs e)
        {
            clickOnOperation(8);
        }

        private void getResult_Click(object sender, EventArgs e)
        {
            getRes();
            isEqualityPushed = true;
        }

        private void getRes()
        {
            if (numberOfOperation > 0 && numberOfOperation < 7)
            {
                if (!isEqualityPushed)
                    getDataFromTextBox(secondNumber);
                else
                    clearTextBox();
            }

            if (numberOfOperation != 0)
            {
                switch (numberOfOperation)
                {
                    case 1:
                        firstNumber *= secondNumber;
                        break;
                    case 2:
                        firstNumber /= secondNumber;
                        break;
                    case 3:
                        firstNumber += secondNumber;
                        break;
                    case 4:
                        firstNumber -= secondNumber;
                        break;
                    case 5:
                        firstNumber = firstNumber.ToPow(secondNumber);
                        break;
                    case 6:
                        firstNumber = firstNumber.GetRadical(secondNumber);
                        break;
                    case 7:
                        firstNumber = firstNumber.Factorial(firstNumber);
                        break;
                    case 8:
                        firstNumber = 1 / firstNumber;
                        break;
                }
                setDataIntoTheTextBox(firstNumber.NumberString);
            }
        }

        private void clickOnOperation(int operationNumber)
        {
            // выполнение предыдущей операции из памяти, если есть
            // для факториала нужно нажать на кнопку равенства
            if (numberOfOperation > 0 && !isEqualityPushed && operationNumber < 7)
            {
                getRes();
            }
         
            // данные для текущей операции
            getDataFromTextBox(firstNumber);
            numberOfOperation = operationNumber;
            isEqualityPushed = false;

            // у факториала и др. нет второго операнда
            if (numberOfOperation >= 7)
            {
                getRes();
                // сброс, чтобы работать с результатом вычислений
                numberOfOperation = 0;
            }
        }

        private void deleteData_Click(object sender, EventArgs e)
        {
            firstNumber.Val = 0;
            secondNumber.Val = 0;
            numberOfOperation = 0;
            isEqualityPushed = false;
            clearTextBox();
            dataHasBeenWrittenOff();
        }

        // запись в текстовое окно данных
        private void setDataIntoTheTextBox(string str)
        {
            dataHasBeenWrittenOff();
            string text = textBox.Text;
            textBox.Text = text + str;
        }

        // получение данных из текстового окна, запись их в объект Value
        private void getDataFromTextBox(Value number)
        {
            number.NumberString = textBox.Text;
            dataHasBeenWritten = true;
        }

        private void dataHasBeenWrittenOff()
        {
            if (dataHasBeenWritten)
            {
                dataHasBeenWritten = false;
                clearTextBox();
            }
        }

        // очистка textBox
        private void clearTextBox()
        {
            textBox.Text = "";
        }

        private void textBoxLostFocus(object sender, EventArgs e)
        {
            textBox.Focus();
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

        // события клавиатуры
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            // textBox.Text = e.KeyValue.ToString() + " ";
            // цифры
            if (e.KeyValue >= 48 && e.KeyValue <= 57 
                || e.KeyValue >= 96 && e.KeyValue <= 105)
            {
                e.SuppressKeyPress = true;
                switch (e.KeyValue)
                {
                    case 48:
                    case 96:
                        button10_Click(sender, e);
                        break;
                    case 49:
                    case 97:
                        button1_Click(sender, e);
                        break;
                    case 50:
                    case 98:
                        button2_Click(sender, e);
                        break;
                    case 51:
                    case 99:
                        button3_Click(sender, e);
                        break;
                    case 52:
                    case 100:
                        button4_Click(sender, e);
                        break;
                    case 53:
                    case 101:
                        button5_Click(sender, e);
                        break;
                    case 54:
                    case 102:
                        button6_Click(sender, e);
                        break;
                    case 55:
                    case 103:
                        button7_Click(sender, e);
                        break;
                    case 56:
                    case 104:
                        button8_Click(sender, e);
                        break;
                    case 57:
                    case 105:
                        button9_Click(sender, e);
                        break;
                }
            }

            // буквы
            else if (e.KeyValue >= 65 && e.KeyValue <= 90)
            {
                e.SuppressKeyPress = true;
                if (e.KeyValue == 67)
                    deleteData_Click(sender, e);
            }

            // операции
            else if (e.KeyValue == 13 || e.KeyValue >= 186 && e.KeyValue <= 222 
                    || e.KeyValue >= 106 && e.KeyValue <= 111)
            {
                e.SuppressKeyPress = true;
                switch (e.KeyValue)
                {
                    case 13:
                    case 108:
                        getResult_Click(sender, e);
                        break;
                    case 106:
                    case 192:
                        multiplication_Click(sender, e);
                        break;
                    case 107:
                    case 187:
                        addition_Click(sender, e);
                        break;
                    case 110:
                    case 188:
                    case 191:
                        dot_Click(sender, e);
                        break;
                    case 109:
                    case 189:
                        subtraction_Click(sender, e);
                        break;
                    case 111:
                    case 220:
                        division_Click(sender, e);
                        break;                                         
                }
            }
        }
    }
}
