using System.IO;
using System.Text;
using System.Windows.Forms;
using TranscendentalEquations.Helper;
using TranscendentalEquations.Model;
using TranscendentalEquations.Services;
using TranscendentalEquations.TranscendentalMethods;
using TranscendentalEquations.Validation;

namespace TranscendentalEquations;

public partial class Form1 : Form
{
    private StringBuilder intermediateData = new StringBuilder();
    private Label resultLabel = new Label();
    private Instruction instruction = new Instruction();
    private bool helpIsOpened = false;

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

    private void CorrectSpellingInput()
    {
        CorrectionEquation correctionEquation = new CorrectionEquation();

        textBox1.Text = correctionEquation.RemoveSpaces(textBox1.Text);
        textBox2.Text = correctionEquation.RemoveSpaces(textBox2.Text);
        textBox3.Text = correctionEquation.RemoveSpaces(textBox3.Text);
        textBox4.Text = correctionEquation.RemoveSpaces(textBox4.Text);
        textBox5.Text = correctionEquation.RemoveSpaces(textBox5.Text);

        textBox3.Text = correctionEquation.ReplaceDotsWithCommas(textBox3.Text);
        textBox4.Text = correctionEquation.ReplaceDotsWithCommas(textBox4.Text);
        textBox5.Text = correctionEquation.ReplaceDotsWithCommas(textBox5.Text);

        textBox1.Text = correctionEquation.ToLower(textBox1.Text);
    }

    private void button1_Click(object? sender, EventArgs e)
    {
        CorrectSpellingInput();
        OutputForButton1Click.Visible = true;

        try
        {
            StringBuilder intermediateData = new StringBuilder();
            TranscendentalEquation bisection = new MyBisection(intermediateData);
            EquationManager equationManager = new EquationManager();
            string intermediateDataFileName = $"data_from_bisection.txt";
            string nameClass = $"{nameof(MyBisection)}";

            bisection.Equation = textBox1.Text;
            bisection.MaxIterations = int.Parse(textBox2.Text);
            bisection.Tolerance = Convert.ToDouble(textBox3.Text);
            bisection.A = Convert.ToDouble(textBox4.Text);
            bisection.B = Convert.ToDouble(textBox5.Text);

            equationManager.EquationSolver(intermediateData, bisection, intermediateDataFileName, nameClass);

            OutputForButtonClick_Update(OutputForButton1Click, Convert.ToString(bisection.Result));

            MessageBox.Show($"Bisection method completed. Intermediate data saved to {intermediateDataFileName}.");
        }
        catch
        {
            MessageBox.Show("You didn`t follow the instruction!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void button2_Click(object? sender, EventArgs e)
    {
        CorrectSpellingInput();
        OutputForButton2Click.Visible = true;

        try
        {
            StringBuilder intermediateData = new StringBuilder();
            TranscendentalEquation newton = new MyNewton(intermediateData);
            EquationManager equationManager = new EquationManager();
            string intermediateDataFileName = $"data_from_newton.txt";
            string nameClass = $"{nameof(MyNewton)}";

            newton.Equation = textBox1.Text;
            newton.MaxIterations = int.Parse(textBox2.Text);
            newton.Tolerance = Convert.ToDouble(textBox3.Text);

            equationManager.EquationSolver(intermediateData, newton, intermediateDataFileName, nameClass);

            OutputForButtonClick_Update(OutputForButton2Click, Convert.ToString(newton.Result));

            MessageBox.Show($"Newton method completed. Intermediate data saved to {intermediateDataFileName}.");
        }
        catch
        {
            MessageBox.Show("You didn`t follow the instruction!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void button3_Click(object? sender, EventArgs e)
    {
        CorrectSpellingInput();
        OutputForButton3Click.Visible = true;

        try
        {
            StringBuilder intermediateData = new StringBuilder();
            TranscendentalEquation secant = new MySecant(intermediateData);
            EquationManager equationManager = new EquationManager();
            string intermediateDataFileName = $"data_from_secant.txt";
            string nameClass = $"{nameof(MySecant)}";

            secant.Equation = textBox1.Text;
            secant.MaxIterations = int.Parse(textBox2.Text);
            secant.Tolerance = Convert.ToDouble(textBox3.Text);
            secant.A = Convert.ToDouble(textBox4.Text);
            secant.B = Convert.ToDouble(textBox5.Text);

            equationManager.EquationSolver(intermediateData, secant, intermediateDataFileName, nameClass);
            
            OutputForButtonClick_Update(OutputForButton3Click, Convert.ToString(secant.Result));

            MessageBox.Show($"Secant method completed. Intermediate data saved to {intermediateDataFileName}.");
        }
        catch
        {
            MessageBox.Show("You didn't follow the instruction!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OutputForButtonClick_Update(TextBox infoTextBox, string? text)
    {
        infoTextBox.Text = text;
    }

    private void CheckIfAllInputed(object? sender, EventArgs e)
    {
        ButtonEnabler();
    }
    private void ButtonEnabler()
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

    private void helpButton_Click(object sender, EventArgs e)
    {
        if (!helpIsOpened)
        {
            Controls.Add(instruction);
            instruction.Location = new Point((Width - instruction.Width) / 2, (Height - instruction.Height) / 2);
            instruction.BringToFront();
        }
        else
        {
            Controls.Remove(instruction);
        }

        helpIsOpened = !helpIsOpened;
    }

    private void openInfrmationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string intermediateDataFileName = "data_from_bisection.txt";

        FileManager fileHelper = new FileManager();
        fileHelper.ReadFromFile(intermediateDataFileName);
    }

    private void infoAboutNewtonMethodToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string intermediateDataFileName = "data_from_newton.txt";

        FileManager fileHelper = new FileManager();
        fileHelper.ReadFromFile(intermediateDataFileName);
    }

    private void infoAboutSecantMethodToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string intermediateDataFileName = "data_from_secant.txt";

        FileManager fileHelper = new FileManager();
        fileHelper.ReadFromFile(intermediateDataFileName);
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
// cos(5*(0.25)^(3))+|0.25-(0.25)^(2)|+|2*(0.25-1)|+15/(tg(-5*0.25))^(2)-(0.25+2)^(sin(0.25-1))/12-(2-(5-2))^(3)


// 5*cos(x)   0.5;1.5  (2, 3)
// (e)^(x)
// sin(2*x)-2*(x)^(2)   2;3  (2, 3)
// (x)^(4)-16*x-64   3;4   (1, 2)
// sin(x*2)+cos(x*2)-10*x  0.2;0.4  (2, 3)
// tg(x)+log(2; x+10)-(x)^(2)-x   0.8;1.2  (2, 3)

// log(2;(x))+3*(x)^(2)   0.5;1.2  (1, 2, 3)
// cos(7*x)+(x)^(2)-3   0;10 (1, 2, 3)
// cos(7*x)+(x)^(2)-3   1;2 (1, 2, 3) -------
// cos(7*x)+(x)^(2)-3   1,62;1,63 (1, 2, 3)
// -(3/sqrt(2))*(sin(pi*(x+1/4))+cos(pi*(x+1/4)))   1;2 (1, 2, 3)  
// (x)^(3)+3*(x)^(2)+12*x+8   -5;5  (1, 2, 3)
// 5*cos((x)^(2))-3*x    0;1   (1, 2, 3)
// (x)^(2)*(1/4*x-4)+log(e;4*x)+2*x   0,8;1,1 (1, 2, 3)
// x*(e)^(x)-1   0;1 (1, 2, 3)
// 1-2*x   0;1  (1, 2, 3)
// x-cos(x)   -1;1 (1, 2, 3)
// tg(x)-x   -2;3 (1, 2, 3)
// (x)^(3)-18   0;3 (1, 2, 3)
// (x)^(3)-x-15  0;3 (1, 2, 3)
// (x)^(3)-6*x-4  -1;1 (1, 2, 3)
// sin(x)-x   -2;1 (1, 2, 3)
