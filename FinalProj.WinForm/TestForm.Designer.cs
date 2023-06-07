namespace FinalProj.WinForm
{
    partial class TestForm
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
            MainMenu_Label = new Label();
            StartButton = new Button();
            SuspendLayout();
            // 
            // MainMenu_Label
            // 
            MainMenu_Label.AutoSize = true;
            MainMenu_Label.Font = new Font("Arial Narrow", 24F, FontStyle.Regular, GraphicsUnit.Point);
            MainMenu_Label.Location = new Point(132, 31);
            MainMenu_Label.Name = "MainMenu_Label";
            MainMenu_Label.Size = new Size(514, 46);
            MainMenu_Label.TabIndex = 0;
            MainMenu_Label.Text = "Тест на язык программирования";
            // 
            // StartButton
            // 
            StartButton.Location = new Point(323, 329);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(134, 52);
            StartButton.TabIndex = 1;
            StartButton.Text = "НАЧА";
            StartButton.UseVisualStyleBackColor = true;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(StartButton);
            Controls.Add(MainMenu_Label);
            Name = "TestForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label MainMenu_Label;
        private Button StartButton;
    }
}