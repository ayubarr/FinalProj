namespace FinolProj.WinForm
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
            labelMainMenu = new Label();
            buttonStart = new Button();
            label1 = new Label();
            Label_Question1 = new Label();
            Q1_YesButton = new Button();
            Q1_NoButton = new Button();
            DelphiResult_Label = new Label();
            ExitButton = new Button();
            SuspendLayout();
            // 
            // labelMainMenu
            // 
            labelMainMenu.AutoSize = true;
            labelMainMenu.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point);
            labelMainMenu.Location = new Point(150, 9);
            labelMainMenu.Name = "labelMainMenu";
            labelMainMenu.Size = new Size(490, 36);
            labelMainMenu.TabIndex = 0;
            labelMainMenu.Text = "Тест на язык программирования";
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(325, 392);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(151, 46);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "Начать Тест";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // Label_Question1
            // 
            Label_Question1.AutoSize = true;
            Label_Question1.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Label_Question1.Location = new Point(176, 9);
            Label_Question1.Name = "Label_Question1";
            Label_Question1.Size = new Size(438, 36);
            Label_Question1.TabIndex = 2;
            Label_Question1.Text = "Хотите много зарабатывать?\r\n";
            Label_Question1.Visible = false;
            // 
            // Q1_YesButton
            // 
            Q1_YesButton.Location = new Point(127, 392);
            Q1_YesButton.Name = "Q1_YesButton";
            Q1_YesButton.Size = new Size(139, 46);
            Q1_YesButton.TabIndex = 3;
            Q1_YesButton.Text = "Да";
            Q1_YesButton.UseVisualStyleBackColor = true;
            Q1_YesButton.Visible = false;
            // 
            // Q1_NoButton
            // 
            Q1_NoButton.Location = new Point(530, 392);
            Q1_NoButton.Name = "Q1_NoButton";
            Q1_NoButton.Size = new Size(139, 46);
            Q1_NoButton.TabIndex = 4;
            Q1_NoButton.Text = "Нет";
            Q1_NoButton.UseVisualStyleBackColor = true;
            Q1_NoButton.Visible = false;
            Q1_NoButton.Click += Q1_NoButton_Click;
            // 
            // DelphiResult_Label
            // 
            DelphiResult_Label.AutoSize = true;
            DelphiResult_Label.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point);
            DelphiResult_Label.Location = new Point(225, 159);
            DelphiResult_Label.Name = "DelphiResult_Label";
            DelphiResult_Label.Size = new Size(319, 36);
            DelphiResult_Label.TabIndex = 5;
            DelphiResult_Label.Text = "Вам подойдет Delphi";
            DelphiResult_Label.Visible = false;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(325, 338);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(151, 48);
            ExitButton.TabIndex = 6;
            ExitButton.Text = "Выход";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Visible = false;
            ExitButton.Click += ExitButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ExitButton);
            Controls.Add(DelphiResult_Label);
            Controls.Add(label1);
            Controls.Add(buttonStart);
            Controls.Add(labelMainMenu);
            Controls.Add(Label_Question1);
            Controls.Add(Q1_NoButton);
            Controls.Add(Q1_YesButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelMainMenu;
        private Button buttonStart;
        private Label label1;
        private Label Label_Question1;
        private Button Q1_YesButton;
        private Button Q1_NoButton;
        private Label DelphiResult_Label;
        private Button ExitButton;
    }
}