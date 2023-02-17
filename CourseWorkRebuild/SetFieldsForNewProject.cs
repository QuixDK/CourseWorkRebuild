using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWorkRebuild
{
    public partial class SetFieldsForNewProject : Form
    {
        InitProject initProject;
        public SetFieldsForNewProject()
        {
            InitializeComponent();
        }

        private void selectElevationTablePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectElevatorTablePath = new OpenFileDialog();
            selectElevatorTablePath.Filter = "Текстовые файлы (*.sqlite)|*.sqlite|Все файлы (*.*)|*.*";
            if (selectElevatorTablePath.ShowDialog(this) == DialogResult.OK)
            {
                elevatorTablePath.Text = selectElevatorTablePath.FileName;
            }
        }

        private void selectObjectDiagramPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectObjectDiagramPath = new OpenFileDialog();
            selectObjectDiagramPath.Filter = "Изображения PNG(*.png)|*.png|Изображения JPG(*.jpg)|*.jpg|Изображения JPEG(*.jpeg)|*.jpeg|Изображения BMP(*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (selectObjectDiagramPath.ShowDialog() == DialogResult.OK)
            {
                objectDiagramPath.Text = selectObjectDiagramPath.FileName;
            }
        }

        private void createProjectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (elevatorTablePath.Text.Equals(""))
                {
                    throw new NullElevatorTablePathException("Укажите путь к таблице высот");
                }

                if (objectDiagramPath.Text.Equals(""))
                {
                    throw new NullObjectDiagramPathException("Укажите путь к схеме объекта");
                }

                this.initProject = new InitProject(elevatorTablePath.Text, objectDiagramPath.Text, selectTValue.Text, selectAValue.Text);
                Close();
            }
            catch (NullElevatorTablePathException err)
            {
                MessageBox.Show(err.Message);
            }
            catch (NullObjectDiagramPathException err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public InitProject getInitProject()
        {
            return this.initProject;
        }
    }

    public class NullElevatorTablePathException : Exception
    {
        public NullElevatorTablePathException(string message)
            : base(message) { }

    }

    public class NullObjectDiagramPathException : Exception
    {
        public NullObjectDiagramPathException(string message)
            : base(message) { }

    }
}
