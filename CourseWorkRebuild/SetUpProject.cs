using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CourseWorkRebuild
{
    public partial class SetUpProject : Form
    {
        private Rectangle originalCreateProjectButton;
        private Rectangle originalOpenProjectButton;
        private Rectangle originalFormSize;
        public String projectRoot = "";
        private InitProject initProject;
        public SetFieldsForNewProject initInfoForCreateDB;
        private String valueOfT;
        private String buildingCount;
        private String markCount;
        public SetUpProject(SetFieldsForNewProject initInfoForCreateProject)
        {
            InitializeComponent();
            this.initInfoForCreateDB = initInfoForCreateProject;
        }
        private void openProject_Click(object sender, EventArgs e)
        {
            try
            {
                String archivePath = "";
                OpenFileDialog chooseRarFile = new OpenFileDialog();
                chooseRarFile.Title = "Выберите архив с проектом";
                chooseRarFile.Filter = "RAR files(*.rar) | *.rar";
                chooseRarFile.Multiselect = false;

                if (chooseRarFile.ShowDialog() == DialogResult.OK)
                {
                    archivePath = Path.GetFullPath(chooseRarFile.FileName);
                }

                String filePath = "D:\\Projects";

                if (archivePath != "")
                {
                    using (var archive = ArchiveFactory.Open(archivePath))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            string outputPath = Path.Combine(filePath, entry.Key);

                            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                            entry.WriteToDirectory(filePath, new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                }

                FolderBrowserDialog selectProjectPath = new FolderBrowserDialog();
                selectProjectPath.SelectedPath = "D:\\Projects";
                selectProjectPath.Description = "Выберите проект";

                if (selectProjectPath.ShowDialog() == DialogResult.OK)
                {
                    this.projectRoot = selectProjectPath.SelectedPath;
                }

                if (projectRoot.Equals(""))
                {
                    throw new NullProjectRootException("Не указана папка с проектом");
                }

                String dbFilePath = "";
                String pngFilePath = "";
                String txtFilePath = "";

                OpenFileDialog chooseFile = new OpenFileDialog();
                chooseFile.InitialDirectory = projectRoot;
                chooseFile.Multiselect = false;

                int dbFilesCount = Directory.GetFiles(projectRoot, "*.sqlite").Length;
                int pngFilesCount = Directory.GetFiles(projectRoot, "*.png").Length;
                int txtFilesCount = Directory.GetFiles(projectRoot, "*.txt").Length;

                if (dbFilesCount > 1 | dbFilesCount == 0)
                {
                    chooseFile.Title = "Выберите таблицу высот";
                    chooseFile.Filter = "SQLite files (*.sqlite)|*.sqlite";
                    dbFilePath = ChoosePathToFile(projectRoot, chooseFile);
                }
                else dbFilePath = Path.GetFullPath(Directory.GetFiles(projectRoot, "*.sqlite")[0]);

                if (pngFilesCount > 1 | pngFilesCount == 0)
                {
                    chooseFile.Title = "Выберите схему объекта";
                    chooseFile.Filter = "PNG files (*.png)|*.png";
                    pngFilePath = ChoosePathToFile(projectRoot, chooseFile);
                }
                else pngFilePath = Path.GetFullPath(Directory.GetFiles(projectRoot, "*.png")[0]);

                if (txtFilesCount > 1 | txtFilesCount == 0)
                {
                    chooseFile.Title = "Выберите текстовый документ с данными";
                    chooseFile.Filter = "Text files (*.txt)|*.txt";
                    txtFilePath = ChoosePathToFile(projectRoot, chooseFile);
                }
                else txtFilePath = Path.GetFullPath(Directory.GetFiles(projectRoot, "*.txt")[0]);

                if (dbFilePath == "")
                {
                    throw new FilesNotFoundException("Не найден файл с базой данных");
                }
                if (pngFilePath == "")
                {
                    throw new FilesNotFoundException("Не найдена схема объекта");
                }
                if (txtFilePath == "")
                {
                    throw new FilesNotFoundException("Не найден файл с данными об объекте");
                }

                List<String> valueLines = File.ReadAllLines(txtFilePath, Encoding.Unicode).ToList();

                foreach(String valueLine in valueLines)
                {
                    if (valueLine.StartsWith("Точность измерений"))
                    {
                        List<String> line = valueLine.Split(' ').ToList();
                        valueOfT = line[2].Split('м')[0];
                        
                    }
                }
                foreach (String valueLine in valueLines)
                {
                    if (valueLine.StartsWith("Количество структурных блоков"))
                    {
                        List<String> line = valueLine.Split(' ').ToList();
                        buildingCount = line[3];

                    }
                }
                foreach (String valueLine in valueLines)
                {
                    if (valueLine.StartsWith("Количество геодезических марок, закрепленных в теле объекта"))
                    {
                        List<String> line = valueLine.Split(' ').ToList();
                        markCount = line[7];

                    }
                }

                this.initProject = new InitProject(dbFilePath, pngFilePath, valueOfT, "0,9", markCount, buildingCount);
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

        private String ChoosePathToFile(String projectRoot, OpenFileDialog chooseFile)
        {
            String filePath = "";
            if (chooseFile.ShowDialog() == DialogResult.OK)
            {
                filePath = Path.GetFullPath(chooseFile.FileName);
            }
            return filePath;
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

        private void SetUpProject_Load(object sender, EventArgs e)
        {
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width,this.Height);
            originalCreateProjectButton = new Rectangle(createProjectButton.Location.X, createProjectButton.Location.Y, createProjectButton.Width, createProjectButton.Height);
            originalOpenProjectButton = new Rectangle(openProjectButton.Location.X, openProjectButton.Location.Y, openProjectButton.Width, openProjectButton.Height);
        }

        private void resizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Width) / (float)(originalFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalFormSize.Height);

            int newX = (int)(r.Location.X * xRatio);
            int newY = (int)(r.Location.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private void SetUpProject_Resize(object sender, EventArgs e)
        {
            resizeControl(originalCreateProjectButton, createProjectButton);
            resizeControl(originalOpenProjectButton, openProjectButton);
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

