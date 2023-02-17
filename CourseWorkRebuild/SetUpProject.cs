using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWorkRebuild
{
    public partial class SetUpProject : Form
    {
        public String projectRoot = "";
        private InitProject initProject;
        public SetFieldsForNewProject initInfoForCreateDB;
        public SetUpProject(SetFieldsForNewProject initInfoForCreateProject)
        {
            InitializeComponent();
            this.initInfoForCreateDB = initInfoForCreateProject;
        }
        private void openProject_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog selectProjectPath = new FolderBrowserDialog();
                if (selectProjectPath.ShowDialog() == DialogResult.OK)
                {
                    this.projectRoot = selectProjectPath.SelectedPath;
                }

                if (projectRoot.Equals(""))
                {
                    throw new NullProjectRootException("Не указана папка с проектом");
                }
                List<String> dbFiles = new List<String>();
                List<String> pngFiles = new List<String>();
                foreach (string f in Directory.GetFiles(projectRoot, "*.sqlite"))
                {
                    dbFiles.Add(System.IO.Path.GetFullPath(f));
                }
                foreach (string f in Directory.GetFiles(projectRoot, "*.png"))
                {
                    pngFiles.Add(System.IO.Path.GetFullPath(f));
                }

                if (dbFiles.Count == 0)
                {
                    throw new FilesNotFoundException("Не найден файл с базой данных");
                }

                if (pngFiles.Count == 0)
                {
                    throw new FilesNotFoundException("Не найдена схема объекта");
                }

                String elevatorAndValuesTablePath = dbFiles[0];
                String objectDiagramPath = pngFiles[0];

                this.initProject = new InitProject(elevatorAndValuesTablePath, objectDiagramPath);

                Close();


            }
            catch (NullProjectRootException err)
            {
                MessageBox.Show(err.Message);
            }
            catch (FilesNotFoundException err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public InitProject getInitProject()
        {
            return this.initProject;
        }

        private void createProject_Click(object sender, EventArgs e)
        {
            this.initInfoForCreateDB.ShowDialog();
            Close();
        }
    }
    public class NullProjectRootException : Exception
    {
        public NullProjectRootException(string message)
            : base(message) { }
    }

    public class FilesNotFoundException : Exception
    {
        public FilesNotFoundException(string message)
            : base(message) { }
    }
}

