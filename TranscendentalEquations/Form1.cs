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
            double result = Bisection.BisectionMethod(textBox1.Text);
            ResultLabel_Update(Convert.ToString(result));
        }
        private void ResultLabel_Update(string text)
        {
            resultLabel.Text = text;

            Controls.Add(resultLabel);
        }

    }
}


// cos(5*x)+|x-(x)^2|-pi+|2*(x-1)|+15*pi-e/(tg(-5*x))^2