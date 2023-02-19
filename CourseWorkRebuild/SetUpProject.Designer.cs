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
            this.createProjectButton = new System.Windows.Forms.Button();
            this.openProjectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createProjectButton
            // 
            this.createProjectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createProjectButton.AutoSize = true;
            this.createProjectButton.Location = new System.Drawing.Point(12, 12);
            this.createProjectButton.Name = "createProjectButton";
            this.createProjectButton.Size = new System.Drawing.Size(206, 35);
            this.createProjectButton.TabIndex = 0;
            this.createProjectButton.Text = "Создать проект";
            this.createProjectButton.UseVisualStyleBackColor = true;
            this.createProjectButton.Click += new System.EventHandler(this.createProject_Click);
            // 
            // openProjectButton
            // 
            this.openProjectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openProjectButton.AutoSize = true;
            this.openProjectButton.Location = new System.Drawing.Point(12, 53);
            this.openProjectButton.Name = "openProjectButton";
            this.openProjectButton.Size = new System.Drawing.Size(206, 27);
            this.openProjectButton.TabIndex = 1;
            this.openProjectButton.Text = "Открыть готовый проект";
            this.openProjectButton.UseVisualStyleBackColor = true;
            this.openProjectButton.Click += new System.EventHandler(this.openProject_Click);
            // 
            // SetUpProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 243);
            this.Controls.Add(this.openProjectButton);
            this.Controls.Add(this.createProjectButton);
            this.Name = "SetUpProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проекты";
            this.Load += new System.EventHandler(this.SetUpProject_Load);
            this.Resize += new System.EventHandler(this.SetUpProject_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createProjectButton;
        private System.Windows.Forms.Button openProjectButton;
    }
}