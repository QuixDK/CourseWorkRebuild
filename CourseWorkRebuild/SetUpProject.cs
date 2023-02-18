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
using System.Windows.Forms.VisualStyles;

namespace CourseWorkRebuild
{
    public partial class SetUpProject : Form
    {
        public String projectRoot = "";
        private InitProject initProject;
        public SetFieldsForNewProject initInfoForCreateDB;
        private String valueOfT;
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
                List<String> values = new List<String>();
                foreach (string f in Directory.GetFiles(projectRoot, "*.sqlite"))
                {
                    dbFiles.Add(System.IO.Path.GetFullPath(f));
                }
                foreach (string f in Directory.GetFiles(projectRoot, "*.png"))
                {
                    pngFiles.Add(System.IO.Path.GetFullPath(f));
                }
                foreach (string f in Directory.GetFiles(projectRoot, "*.txt"))
                {
                    values.Add(System.IO.Path.GetFullPath(f));
                }

                if (dbFiles.Count == 0)
                {
                    throw new FilesNotFoundException("Не найден файл с базой данных");
                }

                if (pngFiles.Count == 0)
                {
                    throw new FilesNotFoundException("Не найдена схема объекта");
                }

                if (values.Count == 0)
                {
                    throw new FilesNotFoundException("Не найдена схема объекта");
                }

                String valuesPath = values[0];
                String elevatorAndValuesTablePath = dbFiles[0];
                String objectDiagramPath = pngFiles[0];

                List<String> valueLines = File.ReadAllLines(valuesPath, Encoding.Unicode).ToList();

                foreach(String valueLine in valueLines)
                {
                    if (valueLine.StartsWith("Точность измерений"))
                    {
                        List<String> line = valueLine.Split(' ').ToList();
                        valueOfT = line[2].Split('м')[0];
                        
                    }
                }

                this.initProject = new InitProject(elevatorAndValuesTablePath, objectDiagramPath, valueOfT);

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

