namespace CourseWorkRebuild
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.elevatorTable = new System.Windows.Forms.DataGridView();
            this.objectDiagramPicture = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.topLineSelectBox = new System.Windows.Forms.CheckBox();
            this.bottomLineSelectBox = new System.Windows.Forms.CheckBox();
            this.forecastResponseFunctionSelectBox = new System.Windows.Forms.CheckBox();
            this.showResponseFunctionSelectBox = new System.Windows.Forms.CheckBox();
            this.responseFunctionDiagram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.elevatorTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDiagramPicture)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.responseFunctionDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // elevatorTable
            // 
            this.elevatorTable.AllowUserToAddRows = false;
            this.elevatorTable.AllowUserToDeleteRows = false;
            this.elevatorTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.elevatorTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.elevatorTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.elevatorTable.Location = new System.Drawing.Point(3, 312);
            this.elevatorTable.Name = "elevatorTable";
            this.elevatorTable.RowHeadersWidth = 51;
            this.elevatorTable.Size = new System.Drawing.Size(1221, 241);
            this.elevatorTable.TabIndex = 1;
            // 
            // objectDiagramPicture
            // 
            this.objectDiagramPicture.Location = new System.Drawing.Point(924, 6);
            this.objectDiagramPicture.Name = "objectDiagramPicture";
            this.objectDiagramPicture.Size = new System.Drawing.Size(300, 300);
            this.objectDiagramPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.objectDiagramPicture.TabIndex = 2;
            this.objectDiagramPicture.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripTextBox2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1237, 27);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(8, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(82, 277);
            this.listBox1.TabIndex = 4;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(96, 6);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(82, 277);
            this.listBox2.TabIndex = 5;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.AutoSize = true;
            this.saveChangesButton.Location = new System.Drawing.Point(212, 0);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(135, 27);
            this.saveChangesButton.TabIndex = 6;
            this.saveChangesButton.Text = "Применить изменения";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += new System.EventHandler(this.saveChangesButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1237, 593);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.topLineSelectBox);
            this.tabPage1.Controls.Add(this.bottomLineSelectBox);
            this.tabPage1.Controls.Add(this.forecastResponseFunctionSelectBox);
            this.tabPage1.Controls.Add(this.showResponseFunctionSelectBox);
            this.tabPage1.Controls.Add(this.objectDiagramPicture);
            this.tabPage1.Controls.Add(this.elevatorTable);
            this.tabPage1.Controls.Add(this.responseFunctionDiagram);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1229, 567);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBox4);
            this.tabPage2.Controls.Add(this.listBox3);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.listBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1229, 567);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(275, 6);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(85, 277);
            this.listBox4.TabIndex = 7;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(184, 6);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(85, 277);
            this.listBox3.TabIndex = 6;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1229, 567);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // topLineSelectBox
            // 
            this.topLineSelectBox.AutoSize = true;
            this.topLineSelectBox.Location = new System.Drawing.Point(781, 282);
            this.topLineSelectBox.Name = "topLineSelectBox";
            this.topLineSelectBox.Size = new System.Drawing.Size(112, 17);
            this.topLineSelectBox.TabIndex = 14;
            this.topLineSelectBox.Text = "Верхняя граница";
            this.topLineSelectBox.UseVisualStyleBackColor = true;
            // 
            // bottomLineSelectBox
            // 
            this.bottomLineSelectBox.AutoSize = true;
            this.bottomLineSelectBox.Location = new System.Drawing.Point(781, 259);
            this.bottomLineSelectBox.Name = "bottomLineSelectBox";
            this.bottomLineSelectBox.Size = new System.Drawing.Size(110, 17);
            this.bottomLineSelectBox.TabIndex = 13;
            this.bottomLineSelectBox.Text = "Нижняя граница";
            this.bottomLineSelectBox.UseVisualStyleBackColor = true;
            // 
            // forecastResponseFunctionSelectBox
            // 
            this.forecastResponseFunctionSelectBox.AutoSize = true;
            this.forecastResponseFunctionSelectBox.Location = new System.Drawing.Point(781, 236);
            this.forecastResponseFunctionSelectBox.Name = "forecastResponseFunctionSelectBox";
            this.forecastResponseFunctionSelectBox.Size = new System.Drawing.Size(137, 17);
            this.forecastResponseFunctionSelectBox.TabIndex = 12;
            this.forecastResponseFunctionSelectBox.Text = "Прогнозное значение";
            this.forecastResponseFunctionSelectBox.UseVisualStyleBackColor = true;
            this.forecastResponseFunctionSelectBox.CheckedChanged += new System.EventHandler(this.forecastResponseFunctionSelectBox_CheckedChanged);
            // 
            // showResponseFunctionSelectBox
            // 
            this.showResponseFunctionSelectBox.AutoSize = true;
            this.showResponseFunctionSelectBox.Location = new System.Drawing.Point(781, 213);
            this.showResponseFunctionSelectBox.Name = "showResponseFunctionSelectBox";
            this.showResponseFunctionSelectBox.Size = new System.Drawing.Size(116, 17);
            this.showResponseFunctionSelectBox.TabIndex = 11;
            this.showResponseFunctionSelectBox.Text = "Функция отклика";
            this.showResponseFunctionSelectBox.UseVisualStyleBackColor = true;
            this.showResponseFunctionSelectBox.CheckedChanged += new System.EventHandler(this.showResponseFunctionSelectBox_CheckedChanged);
            // 
            // responseFunctionDiagram
            // 
            chartArea3.Name = "ChartArea1";
            this.responseFunctionDiagram.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.responseFunctionDiagram.Legends.Add(legend3);
            this.responseFunctionDiagram.Location = new System.Drawing.Point(3, 3);
            this.responseFunctionDiagram.Name = "responseFunctionDiagram";
            this.responseFunctionDiagram.Size = new System.Drawing.Size(915, 303);
            this.responseFunctionDiagram.TabIndex = 15;
            this.responseFunctionDiagram.Text = "chart1";
            title3.Name = "Функция Отклика";
            title3.Text = "График";
            this.responseFunctionDiagram.Titles.Add(title3);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 621);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.saveChangesButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.elevatorTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDiagramPicture)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.responseFunctionDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView elevatorTable;
        private System.Windows.Forms.PictureBox objectDiagramPicture;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox topLineSelectBox;
        private System.Windows.Forms.CheckBox bottomLineSelectBox;
        private System.Windows.Forms.CheckBox forecastResponseFunctionSelectBox;
        private System.Windows.Forms.CheckBox showResponseFunctionSelectBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart responseFunctionDiagram;
    }
}

