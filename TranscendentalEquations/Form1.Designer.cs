namespace TranscendentalEquations
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            textBox1 = new TextBox();
            button1 = new Button();
            label1 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            label5 = new Label();
            toolTip1 = new ToolTip(components);
            button4 = new Button();
            OutputForButton1Click = new TextBox();
            OutputForButton2Click = new TextBox();
            OutputForButton3Click = new TextBox();
            helpButton = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.HighlightText;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(95, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(347, 29);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(146, 195);
            button1.Name = "button1";
            button1.Size = new Size(137, 43);
            button1.TabIndex = 1;
            button1.Text = "Bisection";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Constantia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(77, 19);
            label1.TabIndex = 2;
            label1.Text = "Equation:";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Window;
            textBox2.Location = new Point(172, 71);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(72, 23);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(365, 71);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(77, 23);
            textBox3.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Constantia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 75);
            label2.Name = "label2";
            label2.Size = new Size(154, 19);
            label2.TabIndex = 6;
            label2.Text = "Number of iteration:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Constantia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(283, 75);
            label3.Name = "label3";
            label3.Size = new Size(76, 19);
            label3.TabIndex = 7;
            label3.Text = "Accuracy:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Constantia", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(130, 166);
            label4.Name = "label4";
            label4.Size = new Size(168, 26);
            label4.TabIndex = 8;
            label4.Text = "Choose method";
            // 
            // button2
            // 
            button2.Location = new Point(146, 244);
            button2.Name = "button2";
            button2.Size = new Size(137, 43);
            button2.TabIndex = 11;
            button2.Text = "Newton";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(146, 293);
            button3.Name = "button3";
            button3.Size = new Size(137, 43);
            button3.TabIndex = 12;
            button3.Text = "Secant";
            button3.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(104, 119);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(37, 23);
            textBox4.TabIndex = 15;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(163, 119);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(38, 23);
            textBox5.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Constantia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(17, 123);
            label5.Name = "label5";
            label5.Size = new Size(149, 19);
            label5.TabIndex = 13;
            label5.Text = "Interval:  a:            b:";
            // 
            // button4
            // 
            button4.BackColor = Color.PaleTurquoise;
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.Red;
            button4.Location = new Point(207, 118);
            button4.Name = "button4";
            button4.Size = new Size(20, 23);
            button4.TabIndex = 16;
            button4.Text = "!";
            button4.UseVisualStyleBackColor = false;
            // 
            // OutputForButton1Click
            // 
            OutputForButton1Click.BackColor = SystemColors.HighlightText;
            OutputForButton1Click.Enabled = false;
            OutputForButton1Click.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OutputForButton1Click.Location = new Point(289, 200);
            OutputForButton1Click.Name = "OutputForButton1Click";
            OutputForButton1Click.ReadOnly = true;
            OutputForButton1Click.Size = new Size(115, 29);
            OutputForButton1Click.TabIndex = 17;
            OutputForButton1Click.TextAlign = HorizontalAlignment.Center;
            OutputForButton1Click.Visible = false;
            // 
            // OutputForButton2Click
            // 
            OutputForButton2Click.BackColor = SystemColors.HighlightText;
            OutputForButton2Click.Enabled = false;
            OutputForButton2Click.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OutputForButton2Click.Location = new Point(289, 250);
            OutputForButton2Click.Name = "OutputForButton2Click";
            OutputForButton2Click.ReadOnly = true;
            OutputForButton2Click.Size = new Size(115, 29);
            OutputForButton2Click.TabIndex = 17;
            OutputForButton2Click.TextAlign = HorizontalAlignment.Center;
            OutputForButton2Click.Visible = false;
            // 
            // OutputForButton3Click
            // 
            OutputForButton3Click.BackColor = SystemColors.HighlightText;
            OutputForButton3Click.Enabled = false;
            OutputForButton3Click.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OutputForButton3Click.Location = new Point(289, 299);
            OutputForButton3Click.Name = "OutputForButton3Click";
            OutputForButton3Click.ReadOnly = true;
            OutputForButton3Click.Size = new Size(115, 29);
            OutputForButton3Click.TabIndex = 17;
            OutputForButton3Click.TextAlign = HorizontalAlignment.Center;
            OutputForButton3Click.Visible = false;
            // 
            // helpButton
            // 
            helpButton.BackColor = Color.PaleTurquoise;
            helpButton.FlatStyle = FlatStyle.Flat;
            helpButton.ForeColor = Color.Red;
            helpButton.Location = new Point(448, 22);
            helpButton.Name = "helpButton";
            helpButton.Size = new Size(20, 26);
            helpButton.TabIndex = 18;
            helpButton.Text = "?";
            helpButton.UseVisualStyleBackColor = false;
            helpButton.Click += helpButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(515, 379);
            Controls.Add(helpButton);
            Controls.Add(button4);
            Controls.Add(textBox4);
            Controls.Add(textBox5);
            Controls.Add(label5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(OutputForButton1Click);
            Controls.Add(OutputForButton2Click);
            Controls.Add(OutputForButton3Click);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = SystemColors.HotTrack;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "Transcendental Equation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button2;
        private Button button3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label5;
        private ToolTip toolTip1;
        private Button button4;
        private TextBox OutputForButton1Click;
        private TextBox OutputForButton2Click;
        private TextBox OutputForButton3Click;
        private Button helpButton;
    }
}