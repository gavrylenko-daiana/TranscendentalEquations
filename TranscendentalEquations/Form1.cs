using MathNet.Numerics.RootFinding;
using TranscendentalEquations.Services;
using TranscendentalEquations.TranscendentalMethods;

namespace TranscendentalEquations;

public partial class Form1 : Form
{
    private Label resultLabel = new Label();

    public Form1()
    {
        InitializeComponent();
        Starter_Init();
        ResultLabel_Init();
        this.Size = new Size(520, 400);
        Events_Init();
    }

    private void Events_Init()
    {
        button2.Click += button2_Click;
        button3.Click += button3_Click;
        button4.Click += Mouse_Enter;
    }

    private void Mouse_Enter(object? sender, EventArgs e)
    {
        toolTip1.Show("Only for Bisection or Secant methods.", button4);
    }

    private void ResultLabel_Init()
    {
        resultLabel.Font = new Font("Constantia", 16);
        resultLabel.Location = new Point(this.Left, this.Bottom - 100);
        resultLabel.AutoSize = true;
    }

    private void Starter_Init()
    {
        button1.Enabled = false;
        button2.Enabled = false;
        button3.Enabled = false;

        textBox1.TextChanged += CheckIfAllInputed;
        textBox2.TextChanged += CheckIfAllInputed;
        textBox3.TextChanged += CheckIfAllInputed;
        textBox4.TextChanged += CheckIfAllInputed;
        textBox5.TextChanged += CheckIfAllInputed;
    }

    private void textBox1_TextChanged(object? sender, EventArgs e)
    {
        button1.Enabled = string.IsNullOrEmpty(textBox1.Text) ? false : true;
        button2.Enabled = string.IsNullOrEmpty(textBox1.Text) ? false : true;
        button3.Enabled = string.IsNullOrEmpty(textBox1.Text) ? false : true;
    }

    private void button1_Click(object? sender, EventArgs e)
    {
        OutputForButton1Click.Visible = true;

        MyBisection bisection = new MyBisection();
        (double, double) result = bisection.BisectionMethod(textBox1.Text, int.Parse(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));

        //FindFunction findFunction = new FindFunction();
        //double result = findFunction.df(0.25, textBox1.Text.ToLower());
        OutputForButtonClick_Update(OutputForButton1Click, Convert.ToString(result));
    }

    private void button2_Click(object? sender, EventArgs e)
    {
        OutputForButton2Click.Visible = true;

        MyNewton newtons = new MyNewton();
        (double, double) result = newtons.NewtonsMethod(textBox1.Text, int.Parse(textBox2.Text), Convert.ToDouble(textBox3.Text));

        OutputForButtonClick_Update(OutputForButton2Click, Convert.ToString(result));
    }

    private void button3_Click(object? sender, EventArgs e)
    {
        OutputForButton3Click.Visible = true;

        MySecant secant = new MySecant();
        double result = secant.SecantMethod(textBox1.Text, int.Parse(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));

        OutputForButtonClick_Update(OutputForButton3Click, Convert.ToString(result));
    }

    private void OutputForButtonClick_Update(TextBox infoTextBox, string text)
    {
        infoTextBox.Text = text;
    }

    private void CheckIfAllInputed(object? sender, EventArgs e)
    {
        List<TextBox> textBoxes = new()
        {
            textBox1,
            textBox2,
            textBox3
        };

        button2.Enabled = textBoxes.OfType<TextBox>().All(c => !string.IsNullOrEmpty(c.Text));

        textBoxes.AddRange(new List<TextBox>() { textBox4, textBox5 });

        button1.Enabled = button3.Enabled = textBoxes.OfType<TextBox>().All(c => !string.IsNullOrEmpty(c.Text));
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



// (x)^(3)+3*(x)^(2)+12*x+8   -5;5  (1, 2, 3)
// 5*cos(x)   0.5;1.5  (2, 3)
// 5*cos((x)^(2))-3*x    0.8;1.1   (1)
// 1-2*x   0;1  (1, 2, 3)
// sin(2*x)-2*(x)^(2)   2;3  (2, 3)
// (x)^(4)-16*x-64   3;4   (1, 2)
// sin(x*2)+cos(x*2)-10*x  0.2;0.4  (2, 3)
