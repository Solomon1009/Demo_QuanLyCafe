using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C4Demo
{
    public partial class FrmMayTinh : Form
    {
        float number1, number2;
        public FrmMayTinh()
        {
            InitializeComponent();
        }

        private bool KiemTraSo(TextBox txtNumber, out float number)
        {
            bool isNumber = float.TryParse(txtNumber.Text, out number);
            if (!isNumber)
                errSo.SetError(txtNumber, "Gia tri nhap khong phai la so!!!");
            else
                errSo.SetError(txtNumber, "");
            return isNumber;
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
           KiemTraSo((TextBox)sender, out _);
        }

        private void btnPhepTinh_Click(object sender, EventArgs e)
        {
            if (KiemTraSo(txtNumber1, out number1) && KiemTraSo(txtNumber2, out number2))
            {
                float answer = 0;
                Button btnPhepTinh = (Button)sender;
                switch (btnPhepTinh.Name)
                {
                    case "btnSum":
                        answer = number1 + number2;
                        break;
                    case "btnSub":
                        answer = number1 - number2;
                        break;
                    case "btnMul":
                        answer = number1 * number2;
                        break;
                    case "btnDiv":
                        if (number2 == 0)
                        {
                            errSo.SetError(txtNumber2, "Number2 phai khac 0!!!");
                            return;
                        }
                        answer = number1 / number2;
                        break;
                    default:
                        break;
                }
                txtAnswer.Text = answer.ToString();
            }
        }
    }
}
