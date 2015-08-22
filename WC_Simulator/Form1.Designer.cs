namespace WC_Simulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numTeamsLabel = new System.Windows.Forms.Label();
            this.continueButton = new System.Windows.Forms.Button();
            this.numTeamsDropdown = new System.Windows.Forms.ComboBox();
            this.team1Label = new System.Windows.Forms.Label();
            this.team1Textbox = new System.Windows.Forms.TextBox();
            this.team2Textbox = new System.Windows.Forms.TextBox();
            this.team2Label = new System.Windows.Forms.Label();
            this.team3Textbox = new System.Windows.Forms.TextBox();
            this.team3Label = new System.Windows.Forms.Label();
            this.team4Textbox = new System.Windows.Forms.TextBox();
            this.team4Label = new System.Windows.Forms.Label();
            this.team5Textbox = new System.Windows.Forms.TextBox();
            this.team5Label = new System.Windows.Forms.Label();
            this.team6Textbox = new System.Windows.Forms.TextBox();
            this.team6Label = new System.Windows.Forms.Label();
            this.textboxPanel = new System.Windows.Forms.Panel();
            this.labelPanel = new System.Windows.Forms.Panel();
            this.textboxPanel.SuspendLayout();
            this.labelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // numTeamsLabel
            // 
            this.numTeamsLabel.AutoSize = true;
            this.numTeamsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTeamsLabel.Location = new System.Drawing.Point(25, 18);
            this.numTeamsLabel.Name = "numTeamsLabel";
            this.numTeamsLabel.Size = new System.Drawing.Size(303, 24);
            this.numTeamsLabel.TabIndex = 0;
            this.numTeamsLabel.Text = "How many teams are in the group?";
            // 
            // continueButton
            // 
            this.continueButton.AutoSize = true;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.Location = new System.Drawing.Point(190, 300);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 34);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // numTeamsDropdown
            // 
            this.numTeamsDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numTeamsDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTeamsDropdown.FormattingEnabled = true;
            this.numTeamsDropdown.Location = new System.Drawing.Point(334, 15);
            this.numTeamsDropdown.Name = "numTeamsDropdown";
            this.numTeamsDropdown.Size = new System.Drawing.Size(121, 32);
            this.numTeamsDropdown.TabIndex = 3;
            this.numTeamsDropdown.TextChanged += new System.EventHandler(this.numTeamsChanged);
            // 
            // team1Label
            // 
            this.team1Label.AutoSize = true;
            this.team1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team1Label.Location = new System.Drawing.Point(3, 0);
            this.team1Label.Name = "team1Label";
            this.team1Label.Size = new System.Drawing.Size(74, 24);
            this.team1Label.TabIndex = 4;
            this.team1Label.Text = "Team 1";
            this.team1Label.Visible = false;
            // 
            // team1Textbox
            // 
            this.team1Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team1Textbox.Location = new System.Drawing.Point(3, 0);
            this.team1Textbox.MaxLength = 18;
            this.team1Textbox.Name = "team1Textbox";
            this.team1Textbox.Size = new System.Drawing.Size(194, 29);
            this.team1Textbox.TabIndex = 5;
            this.team1Textbox.Visible = false;
            this.team1Textbox.Enter += new System.EventHandler(this.selectAll);
            // 
            // team2Textbox
            // 
            this.team2Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team2Textbox.Location = new System.Drawing.Point(3, 35);
            this.team2Textbox.MaxLength = 18;
            this.team2Textbox.Name = "team2Textbox";
            this.team2Textbox.Size = new System.Drawing.Size(194, 29);
            this.team2Textbox.TabIndex = 7;
            this.team2Textbox.Visible = false;
            this.team2Textbox.Enter += new System.EventHandler(this.selectAll);
            // 
            // team2Label
            // 
            this.team2Label.AutoSize = true;
            this.team2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team2Label.Location = new System.Drawing.Point(3, 35);
            this.team2Label.Name = "team2Label";
            this.team2Label.Size = new System.Drawing.Size(74, 24);
            this.team2Label.TabIndex = 6;
            this.team2Label.Text = "Team 2";
            this.team2Label.Visible = false;
            // 
            // team3Textbox
            // 
            this.team3Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team3Textbox.Location = new System.Drawing.Point(3, 70);
            this.team3Textbox.MaxLength = 18;
            this.team3Textbox.Name = "team3Textbox";
            this.team3Textbox.Size = new System.Drawing.Size(194, 29);
            this.team3Textbox.TabIndex = 9;
            this.team3Textbox.Visible = false;
            this.team3Textbox.Enter += new System.EventHandler(this.selectAll);
            // 
            // team3Label
            // 
            this.team3Label.AutoSize = true;
            this.team3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team3Label.Location = new System.Drawing.Point(3, 70);
            this.team3Label.Name = "team3Label";
            this.team3Label.Size = new System.Drawing.Size(74, 24);
            this.team3Label.TabIndex = 8;
            this.team3Label.Text = "Team 3";
            this.team3Label.Visible = false;
            // 
            // team4Textbox
            // 
            this.team4Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team4Textbox.Location = new System.Drawing.Point(3, 105);
            this.team4Textbox.MaxLength = 18;
            this.team4Textbox.Name = "team4Textbox";
            this.team4Textbox.Size = new System.Drawing.Size(194, 29);
            this.team4Textbox.TabIndex = 11;
            this.team4Textbox.Visible = false;
            this.team4Textbox.Enter += new System.EventHandler(this.selectAll);
            // 
            // team4Label
            // 
            this.team4Label.AutoSize = true;
            this.team4Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team4Label.Location = new System.Drawing.Point(3, 105);
            this.team4Label.Name = "team4Label";
            this.team4Label.Size = new System.Drawing.Size(74, 24);
            this.team4Label.TabIndex = 10;
            this.team4Label.Text = "Team 4";
            this.team4Label.Visible = false;
            // 
            // team5Textbox
            // 
            this.team5Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team5Textbox.Location = new System.Drawing.Point(3, 140);
            this.team5Textbox.MaxLength = 18;
            this.team5Textbox.Name = "team5Textbox";
            this.team5Textbox.Size = new System.Drawing.Size(194, 29);
            this.team5Textbox.TabIndex = 13;
            this.team5Textbox.Visible = false;
            this.team5Textbox.Enter += new System.EventHandler(this.selectAll);
            // 
            // team5Label
            // 
            this.team5Label.AutoSize = true;
            this.team5Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team5Label.Location = new System.Drawing.Point(3, 140);
            this.team5Label.Name = "team5Label";
            this.team5Label.Size = new System.Drawing.Size(74, 24);
            this.team5Label.TabIndex = 12;
            this.team5Label.Text = "Team 5";
            this.team5Label.Visible = false;
            // 
            // team6Textbox
            // 
            this.team6Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team6Textbox.Location = new System.Drawing.Point(3, 175);
            this.team6Textbox.MaxLength = 18;
            this.team6Textbox.Name = "team6Textbox";
            this.team6Textbox.Size = new System.Drawing.Size(194, 29);
            this.team6Textbox.TabIndex = 15;
            this.team6Textbox.Visible = false;
            this.team6Textbox.Enter += new System.EventHandler(this.selectAll);
            // 
            // team6Label
            // 
            this.team6Label.AutoSize = true;
            this.team6Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team6Label.Location = new System.Drawing.Point(3, 175);
            this.team6Label.Name = "team6Label";
            this.team6Label.Size = new System.Drawing.Size(74, 24);
            this.team6Label.TabIndex = 14;
            this.team6Label.Text = "Team 6";
            this.team6Label.Visible = false;
            // 
            // textboxPanel
            // 
            this.textboxPanel.Controls.Add(this.team1Textbox);
            this.textboxPanel.Controls.Add(this.team2Textbox);
            this.textboxPanel.Controls.Add(this.team3Textbox);
            this.textboxPanel.Controls.Add(this.team4Textbox);
            this.textboxPanel.Controls.Add(this.team5Textbox);
            this.textboxPanel.Controls.Add(this.team6Textbox);
            this.textboxPanel.Location = new System.Drawing.Point(190, 71);
            this.textboxPanel.Name = "textboxPanel";
            this.textboxPanel.Size = new System.Drawing.Size(200, 204);
            this.textboxPanel.TabIndex = 17;
            // 
            // labelPanel
            // 
            this.labelPanel.Controls.Add(this.team1Label);
            this.labelPanel.Controls.Add(this.team2Label);
            this.labelPanel.Controls.Add(this.team3Label);
            this.labelPanel.Controls.Add(this.team4Label);
            this.labelPanel.Controls.Add(this.team5Label);
            this.labelPanel.Controls.Add(this.team6Label);
            this.labelPanel.Location = new System.Drawing.Point(103, 71);
            this.labelPanel.Name = "labelPanel";
            this.labelPanel.Size = new System.Drawing.Size(81, 204);
            this.labelPanel.TabIndex = 18;
            // 
            // Form1
            // 
            this.AcceptButton = this.continueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.labelPanel);
            this.Controls.Add(this.textboxPanel);
            this.Controls.Add(this.numTeamsDropdown);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.numTeamsLabel);
            this.Name = "Form1";
            this.Text = "WC Simulator";
            this.textboxPanel.ResumeLayout(false);
            this.textboxPanel.PerformLayout();
            this.labelPanel.ResumeLayout(false);
            this.labelPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label numTeamsLabel;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.ComboBox numTeamsDropdown;
        private System.Windows.Forms.Label team1Label;
        private System.Windows.Forms.TextBox team1Textbox;
        private System.Windows.Forms.TextBox team2Textbox;
        private System.Windows.Forms.Label team2Label;
        private System.Windows.Forms.TextBox team3Textbox;
        private System.Windows.Forms.Label team3Label;
        private System.Windows.Forms.TextBox team4Textbox;
        private System.Windows.Forms.Label team4Label;
        private System.Windows.Forms.TextBox team5Textbox;
        private System.Windows.Forms.Label team5Label;
        private System.Windows.Forms.TextBox team6Textbox;
        private System.Windows.Forms.Label team6Label;
        private System.Windows.Forms.Panel textboxPanel;
        private System.Windows.Forms.Panel labelPanel;
    }
}

