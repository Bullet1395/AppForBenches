namespace AppForBenches
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileMenu = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьВWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddBecnhamrksFile_toolTipMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddBenchmark_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.заменитьНазванияИгрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileMenu
            // 
            this.fileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.AddBecnhamrksFile_toolTipMenu,
            this.заменитьНазванияИгрToolStripMenuItem});
            this.fileMenu.Location = new System.Drawing.Point(0, 0);
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(1242, 24);
            this.fileMenu.TabIndex = 0;
            this.fileMenu.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьВWordToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // сохранитьВWordToolStripMenuItem
            // 
            this.сохранитьВWordToolStripMenuItem.Name = "сохранитьВWordToolStripMenuItem";
            this.сохранитьВWordToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.сохранитьВWordToolStripMenuItem.Text = "Сохранить в Word";
            this.сохранитьВWordToolStripMenuItem.Click += new System.EventHandler(this.СохранитьВWordToolStripMenuItem_Click);
            // 
            // AddBecnhamrksFile_toolTipMenu
            // 
            this.AddBecnhamrksFile_toolTipMenu.Name = "AddBecnhamrksFile_toolTipMenu";
            this.AddBecnhamrksFile_toolTipMenu.Size = new System.Drawing.Size(171, 20);
            this.AddBecnhamrksFile_toolTipMenu.Text = "Добавить файл Benchmarks";
            this.AddBecnhamrksFile_toolTipMenu.Click += new System.EventHandler(this.AddBecnhamrksFile_toolTipMenu_Click);
            // 
            // AddBenchmark_openFileDialog
            // 
            this.AddBenchmark_openFileDialog.FileName = "openFileDialog1";
            // 
            // заменитьНазванияИгрToolStripMenuItem
            // 
            this.заменитьНазванияИгрToolStripMenuItem.Name = "заменитьНазванияИгрToolStripMenuItem";
            this.заменитьНазванияИгрToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.заменитьНазванияИгрToolStripMenuItem.Text = "Заменить названия игр";
            this.заменитьНазванияИгрToolStripMenuItem.Click += new System.EventHandler(this.ЗаменитьНазванияИгрToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1242, 681);
            this.Controls.Add(this.fileMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.fileMenu;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.fileMenu.ResumeLayout(false);
            this.fileMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip fileMenu;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьВWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddBecnhamrksFile_toolTipMenu;
        private System.Windows.Forms.OpenFileDialog AddBenchmark_openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem заменитьНазванияИгрToolStripMenuItem;
    }
}

