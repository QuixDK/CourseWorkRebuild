namespace CourseWorkRebuild
{
    partial class SetUpProject
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
            this.createProject = new System.Windows.Forms.Button();
            this.openProject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createProject
            // 
            this.createProject.Location = new System.Drawing.Point(12, 12);
            this.createProject.Name = "createProject";
            this.createProject.Size = new System.Drawing.Size(205, 24);
            this.createProject.TabIndex = 0;
            this.createProject.Text = "Создать проект";
            this.createProject.UseVisualStyleBackColor = true;
            this.createProject.Click += new System.EventHandler(this.createProject_Click);
            // 
            // openProject
            // 
            this.openProject.Location = new System.Drawing.Point(12, 42);
            this.openProject.Name = "openProject";
            this.openProject.Size = new System.Drawing.Size(205, 24);
            this.openProject.TabIndex = 1;
            this.openProject.Text = "Открыть готовый проект";
            this.openProject.UseVisualStyleBackColor = true;
            this.openProject.Click += new System.EventHandler(this.openProject_Click);
            // 
            // SetUpProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 77);
            this.Controls.Add(this.openProject);
            this.Controls.Add(this.createProject);
            this.Name = "SetUpProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проекты";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createProject;
        private System.Windows.Forms.Button openProject;
    }
}