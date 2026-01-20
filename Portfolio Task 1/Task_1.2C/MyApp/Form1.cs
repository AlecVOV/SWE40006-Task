using System;
using System.Windows.Forms;
using DataEngine; // This links to your DLL

namespace MyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // --- FIXED: This method is now INSIDE the class ---
        private void Form1_Load(object sender, EventArgs e)
        {
            // Empty method to satisfy the error
        }
        // --------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cleaner engine = new Cleaner();
                string result = engine.ProcessData(textBox1.Text);
                label1.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}