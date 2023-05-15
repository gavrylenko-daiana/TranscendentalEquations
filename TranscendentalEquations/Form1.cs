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
            //Bisection bisection = new Bisection();
            //double result = bisection.BisectionMethod(textBox1.Text);
            FindFunction findFunction = new FindFunction();
            double result = findFunction.f(0.25, textBox1.Text.ToLower());
            ResultLabel_Update(Convert.ToString(result));
        }
        private void ResultLabel_Update(string text)
        {
            resultLabel.Text = text;

            Controls.Add(resultLabel);
        }
    }
}

// sqrt((3)^(2)+sin(pi/2)-1)
// 8+2*(-1/(sin(pi/2))^(2))^(1)
// 8+2*(-1/(-2)^(2))^(5)
// (-sin(4*(x)^(3)))^(2+x)
// (sin(2*(x)^(1)))^(2)
// -1/(sin(2*(x)^(1)))^(2)-3

// 5*(-cos((x)^(2)))-3*(tg(pi/3))^(x*3)
// 5*(-(-sin(2*(x)^(1))))-3*(1/2*(cos(pi/3))^(1))^(x*3)

// 5*cos((x)^(2))-3*x
// cos(5*x)+|x-(x)^(2)|-pi+|2*(x-1)|-(x)^(sin(x+2))+15*pi-e/(tg(x))^(2)
// 5*(-cos((x)^(2)))-3*(tg(x+1))^(x*3)
// cos(5*(0.25)^(3))+|0.25-(0.25)^(2)|+|2*(0.25-1)|+15/(tg(-5*0.25))^(2)-(0.25+2)^(sin(0.25-1))/12-(2-(5-2))^(3)