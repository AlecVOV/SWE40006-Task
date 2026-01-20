using System;
using System.Windows.Forms;

namespace TeencodeConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Convert formal Vietnamese into Teencode from 9x generation";

            // --- THIS CODE MUST BE HERE (Inside the Constructor) ---

            // Attach the Input Changed event to the TextBox
            this.txtInput.TextChanged += new System.EventHandler(this.OnInputChanged);

            // Attach the same event to ALL RadioButtons
            this.rbStyle1.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle2.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle3.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle4.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle5.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle6.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle7.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle8.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle9.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle10.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle11.CheckedChanged += new System.EventHandler(this.OnInputChanged);
            this.rbStyle12.CheckedChanged += new System.EventHandler(this.OnInputChanged);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOutput.Text))
            {
                Clipboard.SetText(txtOutput.Text);
                MessageBox.Show("Đã sao chép!", "Thông báo");
            }
        }

        private void OnInputChanged(object sender, EventArgs e)
        {
            PerformConversion();
        }

        private void PerformConversion()
        {
            string input = txtInput.Text;
            int styleId = 1;

            if (rbStyle1.Checked) styleId = 1;
            else if (rbStyle2.Checked) styleId = 2;
            else if (rbStyle3.Checked) styleId = 3;
            else if (rbStyle4.Checked) styleId = 4;
            else if (rbStyle5.Checked) styleId = 5;
            else if (rbStyle6.Checked) styleId = 6;
            else if (rbStyle7.Checked) styleId = 7;
            else if (rbStyle8.Checked) styleId = 8;
            else if (rbStyle9.Checked) styleId = 9;
            else if (rbStyle10.Checked) styleId = 10;
            else if (rbStyle11.Checked) styleId = 11;
            else if (rbStyle12.Checked) styleId = 12;

            // Call the Helper file
            txtOutput.Text = TeencodeHelper.Convert(input, styleId);
        }
    }
}