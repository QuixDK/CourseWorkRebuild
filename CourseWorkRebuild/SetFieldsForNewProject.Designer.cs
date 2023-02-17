namespace CourseWorkRebuild
{
    partial class SetFieldsForNewProject
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.elevatorTablePath = new System.Windows.Forms.TextBox();
            this.objectDiagramPath = new System.Windows.Forms.TextBox();
            this.selectTValue = new System.Windows.Forms.TextBox();
            this.selectElevationTablePath = new System.Windows.Forms.Button();
            this.selectObjectDiagramPath = new System.Windows.Forms.Button();
            this.selectAValue = new System.Windows.Forms.TextBox();
            this.createProjectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите путь к таблице высот:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Укажите путь к схеме объекта: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Укажите значение T и a: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "T: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(319, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "a: ";
            // 
            // elevatorTablePath
            // 
            this.elevatorTablePath.Location = new System.Drawing.Point(185, 6);
            this.elevatorTablePath.Name = "elevatorTablePath";
            this.elevatorTablePath.Size = new System.Drawing.Size(274, 20);
            this.elevatorTablePath.TabIndex = 5;
            // 
            // objectDiagramPath
            // 
            this.objectDiagramPath.Location = new System.Drawing.Point(185, 38);
            this.objectDiagramPath.Name = "objectDiagramPath";
            this.objectDiagramPath.Size = new System.Drawing.Size(274, 20);
            this.objectDiagramPath.TabIndex = 6;
            // 
            // selectTValue
            // 
            this.selectTValue.Location = new System.Drawing.Point(185, 68);
            this.selectTValue.Name = "selectTValue";
            this.selectTValue.Size = new System.Drawing.Size(117, 20);
            this.selectTValue.TabIndex = 7;
            // 
            // selectElevationTablePath
            // 
            this.selectElevationTablePath.Location = new System.Drawing.Point(465, 6);
            this.selectElevationTablePath.Name = "selectElevationTablePath";
            this.selectElevationTablePath.Size = new System.Drawing.Size(31, 20);
            this.selectElevationTablePath.TabIndex = 8;
            this.selectElevationTablePath.Text = "...";
            this.selectElevationTablePath.UseVisualStyleBackColor = true;
            this.selectElevationTablePath.Click += new System.EventHandler(this.selectElevationTablePath_Click);
            // 
            // selectObjectDiagramPath
            // 
            this.selectObjectDiagramPath.Location = new System.Drawing.Point(465, 36);
            this.selectObjectDiagramPath.Name = "selectObjectDiagramPath";
            this.selectObjectDiagramPath.Size = new System.Drawing.Size(31, 22);
            this.selectObjectDiagramPath.TabIndex = 9;
            this.selectObjectDiagramPath.Text = "...";
            this.selectObjectDiagramPath.UseVisualStyleBackColor = true;
            this.selectObjectDiagramPath.Click += new System.EventHandler(this.selectObjectDiagramPath_Click);
            // 
            // selectAValue
            // 
            this.selectAValue.Location = new System.Drawing.Point(333, 68);
            this.selectAValue.Name = "selectAValue";
            this.selectAValue.Size = new System.Drawing.Size(126, 20);
            this.selectAValue.TabIndex = 10;
            // 
            // createProjectButton
            // 
            this.createProjectButton.Location = new System.Drawing.Point(11, 94);
            this.createProjectButton.Name = "createProjectButton";
            this.createProjectButton.Size = new System.Drawing.Size(485, 25);
            this.createProjectButton.TabIndex = 11;
            this.createProjectButton.Text = "Создать проект";
            this.createProjectButton.UseVisualStyleBackColor = true;
            this.createProjectButton.Click += new System.EventHandler(this.createProjectButton_Click);
            // 
            // SetFieldsForNewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 129);
            this.Controls.Add(this.createProjectButton);
            this.Controls.Add(this.selectAValue);
            this.Controls.Add(this.selectObjectDiagramPath);
            this.Controls.Add(this.selectElevationTablePath);
            this.Controls.Add(this.selectTValue);
            this.Controls.Add(this.objectDiagramPath);
            this.Controls.Add(this.elevatorTablePath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SetFieldsForNewProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создать проект";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox elevatorTablePath;
        private System.Windows.Forms.TextBox objectDiagramPath;
        private System.Windows.Forms.TextBox selectTValue;
        private System.Windows.Forms.Button selectElevationTablePath;
        private System.Windows.Forms.Button selectObjectDiagramPath;
        private System.Windows.Forms.TextBox selectAValue;
        private System.Windows.Forms.Button createProjectButton;
    }
}