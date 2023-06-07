namespace FinalProj.WinForm
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            MainMenu_Label.Visible = false;
            StartButton.Visible = false;

            Lable_Q1.Visible = true;
            NoButton_Q1.Visible = true;
            YesButton_Q1.Visible = true;
        }

        private void NoButton_Q1_Click(object sender, EventArgs e)
        {
            Lable_Q1.Visible = false;
            NoButton_Q1.Visible = false;
            YesButton_Q1.Visible = false;

            DelphiResultLabel.Visible = true;
            ExitButton.Visible = true;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void YesButton_Q1_Click(object sender, EventArgs e)
        {
            Lable_Q1.Visible = false;
            NoButton_Q1.Visible = false;
            YesButton_Q1.Visible = false;

            LabelQ2.Visible = true;
            NoButtonQ2.Visible = true;
            YesButtonQ2.Visible = true;

        }

        private void NoButtonQ2_Click(object sender, EventArgs e)
        {
            LabelQ2.Visible = false;
            NoButtonQ2.Visible = false;
            YesButtonQ2.Visible = false;

            LabelQ3.Visible = true;
            NoButtonQ3.Visible = true;
            YesButtonQ3.Visible = true;
        }

        private void YesButtonQ2_Click(object sender, EventArgs e)
        {
            LabelQ2.Visible = false;
            NoButtonQ2.Visible = false;
            YesButtonQ2.Visible = false;

            LabelQ4.Visible = true;
            NoButtonQ4.Visible = true;
            YesButtonQ4.Visible = true;
        }

        private void NoButtonQ3_Click(object sender, EventArgs e)
        {
            LabelQ3.Visible = false;
            NoButtonQ3.Visible = false;
            YesButtonQ3.Visible = false;

            LabelQ5.Visible = true;
            NoButtonQ5.Visible = true;
            YesButtonQ5.Visible = true;

        }

        private void YesButtonQ3_Click(object sender, EventArgs e)
        {
            LabelQ3.Visible = false;
            NoButtonQ3.Visible = false;
            YesButtonQ3.Visible = false;

            LabelQ7.Visible = true;
            NoButtonQ7.Visible = true;
            YesButtonQ7.Visible = true;
        }

        private void YesButtonQ4_Click(object sender, EventArgs e)
        {
            LabelQ4.Visible = false;
            NoButtonQ4.Visible = false;
            YesButtonQ4.Visible = false;

            labelQ11.Visible = true;
            NoQ11.Visible = true;
            YesQ11.Visible = true;
        }

        private void NoButtonQ4_Click(object sender, EventArgs e)
        {
            LabelQ4.Visible = false;
            NoButtonQ4.Visible = false;
            YesButtonQ4.Visible = false;

            LabelQ8.Visible = true;
            YesQ8.Visible = true;
            NoQ8.Visible = true;
        }

        private void NoButtonQ5_Click(object sender, EventArgs e)
        {
            LabelQ5.Visible = false;
            NoButtonQ5.Visible = false;
            YesButtonQ5.Visible = false;

            LabelQ6.Visible = true;
            YesButtonQ6.Visible = true;
            NoButtonQ6.Visible = true;

        }

        private void YesButtonQ5_Click(object sender, EventArgs e)
        {
            LabelQ5.Visible = false;
            NoButtonQ5.Visible = false;
            YesButtonQ5.Visible = false;

            CResultLabel.Visible = true;
            ExitButton.Visible = true;
        }

        private void YesButtonQ6_Click(object sender, EventArgs e)
        {
            LabelQ6.Visible = false;
            YesButtonQ6.Visible = false;
            NoButtonQ6.Visible = false;

            CPlusPlusResLable.Visible = true;
            ExitButton.Visible = true;
        }

        private void NoButtonQ6_Click(object sender, EventArgs e)
        {
            LabelQ6.Visible = false;
            YesButtonQ6.Visible = false;
            NoButtonQ6.Visible = false;

            JavaResultLabel.Visible = true;
            ExitButton.Visible = true;
        }

        private void YesButtonQ7_Click(object sender, EventArgs e)
        {
            LabelQ7.Visible = false;
            NoButtonQ7.Visible = false;
            YesButtonQ7.Visible = false;

            FortranResultLabel.Visible = true;
            ExitButton.Visible = true;

        }

        private void NoButtonQ7_Click(object sender, EventArgs e)
        {
            LabelQ7.Visible = false;
            NoButtonQ7.Visible = false;
            YesButtonQ7.Visible = false;

            MatLabResultLabel.Visible = true;
            ExitButton.Visible = true;
        }

        private void NoQ8_Click(object sender, EventArgs e)
        {
            LabelQ8.Visible = false;
            YesQ8.Visible = false;
            NoQ8.Visible = false;

            labelQ9.Visible = true;
            NoQ9.Visible = true;
            YesQ9.Visible = true;
        }

        private void YesQ8_Click(object sender, EventArgs e)
        {
            LabelQ8.Visible = false;
            YesQ8.Visible = false;
            NoQ8.Visible = false;

            PythonResultLabel.Visible = true;
            ExitButton.Visible = true;
        }

        private void YesQ9_Click(object sender, EventArgs e)
        {
            labelQ9.Visible = false;
            NoQ9.Visible = false;
            YesQ9.Visible = false;

            CSharpResultLable.Visible = true;
            ExitButton.Visible = true;
        }

        private void NoQ9_Click(object sender, EventArgs e)
        {
            labelQ9.Visible = false;
            NoQ9.Visible = false;
            YesQ9.Visible = false;

            labelQ10.Visible = true;
            NoQ10.Visible = true;
            YesQ10.Visible = true;
        }

        private void YesQ10_Click(object sender, EventArgs e)
        {
            labelQ10.Visible = false;
            NoQ10.Visible = false;
            YesQ10.Visible = false;

            SwiftResLable.Visible = true;
            ExitButton.Visible = true;
        }

        private void NoQ10_Click(object sender, EventArgs e)
        {
            labelQ10.Visible = false;
            NoQ10.Visible = false;
            YesQ10.Visible = false;

            PerlResLabel.Visible = true;
            ExitButton.Visible = true;
        }

        private void YesQ11_Click(object sender, EventArgs e)
        {
            labelQ11.Visible = false;
            NoQ11.Visible = false;
            YesQ11.Visible = false;

            labelQ12.Visible = true;
            NoQ12.Visible = true;
            YesQ12.Visible = true;
        }

        private void NoQ11_Click(object sender, EventArgs e)
        {
            labelQ11.Visible = false;
            NoQ11.Visible = false;
            YesQ11.Visible = false;

            PHPResLabel.Visible = true;
            ExitButton.Visible = true;
        }

        private void YesQ12_Click(object sender, EventArgs e)
        {
            labelQ12.Visible = false;
            NoQ12.Visible = false;
            YesQ12.Visible = false;

            JavaScriptResLabel.Visible = true;
            ExitButton.Visible = true;

        }

        private void NoQ12_Click(object sender, EventArgs e)
        {
            labelQ12.Visible = false;
            NoQ12.Visible = false;
            YesQ12.Visible = false;

            RubyResLabel.Visible = true;
            ExitButton.Visible = true;
        }
    }
}