namespace FinolProj.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            labelMainMenu.Visible = false;
            buttonStart.Visible = false;

            Label_Question1.Visible = true;
            Q1_YesButton.Visible = true;
            Q1_NoButton.Visible = true;
        }

        private void Q1_NoButton_Click(object sender, EventArgs e)
        {
            Label_Question1.Visible = false;
            Q1_YesButton.Visible = false;
            Q1_NoButton.Visible = false;

            DelphiResult_Label.Visible = true;
            ExitButton.Visible = true;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}