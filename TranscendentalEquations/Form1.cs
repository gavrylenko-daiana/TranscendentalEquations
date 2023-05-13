using TranscendentalEquations.Services;
using TranscendentalEquations.TranscendentalMethods;
namespace TranscendentalEquations
{
    public partial class Form1 : Form
    {
        private Label resultLabel = new Label();
        public Form1()
        {
            InitializeComponent();
            Starter_Init();
            ResultLabel_Init();
        }
        private void ResultLabel_Init()
        {
            resultLabel.Font = new Font("Segue UI", 16);
            resultLabel.Location = new Point(textBox1.Left, textBox1.Bottom + 10);
            resultLabel.AutoSize = true;
        }
        private void Starter_Init()
        {
            button1.Enabled = false;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = string.IsNullOrEmpty(textBox1.Text) ? false : true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //double result = Secant.SecantMethod(textBox1.Text);
            // double result = Bisection.BisectionMethod(textBox1.Text);
            double result = FindFunction.f(0.25, textBox1.Text.ToLower());
            ResultLabel_Update(Convert.ToString(result));
        }
        private void ResultLabel_Update(string text)
        {
            resultLabel.Text = text;

            Controls.Add(resultLabel);
        }
    }
}

// -1/(sin2*(x)^(1))^(2)-3
// 5*(-cos((x)^(2)))-3*(tgpi/3)^(x*3)
// 5*cos((x)^(2))-3*x
// cos(5*x)+|x-(x)^(2)|-pi+|2*(x-1)|-(x)^(sin(x+2))+15*pi-e/(tgx)^(2)
// 5*(-cos((x)^(2)))-3*(tgx+1)^(x*3)