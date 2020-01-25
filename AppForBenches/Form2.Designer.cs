namespace AppForBenches
{
    partial class Form2
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
            this.oldName_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newName_textBox = new System.Windows.Forms.TextBox();
            this.names_listBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // oldName_textBox
            // 
            this.oldName_textBox.Location = new System.Drawing.Point(12, 12);
            this.oldName_textBox.Name = "oldName_textBox";
            this.oldName_textBox.ReadOnly = true;
            this.oldName_textBox.Size = new System.Drawing.Size(275, 20);
            this.oldName_textBox.TabIndex = 0;
            this.oldName_textBox.Text = "Заменяемое имя...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = " - - - > ";
            // 
            // newName_textBox
            // 
            this.newName_textBox.Location = new System.Drawing.Point(336, 12);
            this.newName_textBox.Name = "newName_textBox";
            this.newName_textBox.Size = new System.Drawing.Size(267, 20);
            this.newName_textBox.TabIndex = 2;
            // 
            // names_listBox
            // 
            this.names_listBox.ColumnWidth = 150;
            this.names_listBox.FormattingEnabled = true;
            this.names_listBox.Location = new System.Drawing.Point(12, 92);
            this.names_listBox.MultiColumn = true;
            this.names_listBox.Name = "names_listBox";
            this.names_listBox.Size = new System.Drawing.Size(592, 173);
            this.names_listBox.TabIndex = 3;
            this.names_listBox.SelectedIndexChanged += new System.EventHandler(this.Names_listBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Изменить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Список уже имеющихся сопоставлений";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 280);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.names_listBox);
            this.Controls.Add(this.newName_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.oldName_textBox);
            this.Name = "Form2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сопоставление названий с отображаемыми";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox oldName_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newName_textBox;
        private System.Windows.Forms.ListBox names_listBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}