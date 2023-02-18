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
        private Rectangle originalFormSize;
        private Rectangle label1Rectangle;
        private Rectangle label2Rectangle;
        private Rectangle label3Rectangle;
        private Rectangle label4Rectangle;
        private Rectangle label5Rectangle;
        private Rectangle elevatorTablePathRectangle;
        private Rectangle objectDiagramPathRectangle;
        private Rectangle selectTValueRectangle;
        private Rectangle selectAValueRectangle;
        private Rectangle createProjectButtonRectangle;
        private Rectangle selectElevationTablePathRectangle;
        private Rectangle selectObjectDiagramPathRectangle;


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

        private void SetFieldsForNewProject_Load(object sender, EventArgs e)
        {
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            label1Rectangle = new Rectangle(label1.Location.X, label1.Location.Y, label1.Width, label1.Height) ;
            label2Rectangle = new Rectangle(label2.Location.X, label2.Location.Y, label2.Width, label2.Height);
            label3Rectangle = new Rectangle(label3.Location.X, label3.Location.Y, label3.Width, label3.Height);
            label4Rectangle = new Rectangle(label4.Location.X, label4.Location.Y, label4.Width, label4.Height);
            label5Rectangle = new Rectangle(label5.Location.X, label5.Location.Y, label5.Width, label5.Height);
            elevatorTablePathRectangle = new Rectangle(elevatorTablePath.Location.X, elevatorTablePath.Location.Y, elevatorTablePath.Width, elevatorTablePath.Height) ;
            objectDiagramPathRectangle = new Rectangle(objectDiagramPath.Location.X, objectDiagramPath.Location.Y, objectDiagramPath.Width, objectDiagramPath.Height);
            selectTValueRectangle = new Rectangle(selectTValue.Location.X, selectTValue.Location.Y, selectTValue.Width, selectTValue.Height);
            selectAValueRectangle = new Rectangle(selectAValue.Location.X, selectAValue.Location.Y, selectAValue.Width, selectAValue.Height);
            createProjectButtonRectangle = new Rectangle(createProjectButton.Location.X, createProjectButton.Location.Y, createProjectButton.Width, createProjectButton.Height);
            selectElevationTablePathRectangle = new Rectangle(selectElevationTablePath.Location.X, selectElevationTablePath.Location.Y, selectElevationTablePath.Width, selectElevationTablePath.Height);
            selectObjectDiagramPathRectangle = new Rectangle(selectObjectDiagramPath.Location.X, selectObjectDiagramPath.Location.Y, selectObjectDiagramPath.Width, selectObjectDiagramPath.Height);
            
        }

        private void SetFieldsForNewProject_Resize(object sender, EventArgs e)
        {

            resizeControl(label1Rectangle, label1);
            resizeControl(label2Rectangle, label2);
            resizeControl(label3Rectangle, label3);
            resizeControl(label4Rectangle, label4);
            resizeControl(label5Rectangle, label5);
            resizeControl(elevatorTablePathRectangle, elevatorTablePath);
            resizeControl(objectDiagramPathRectangle, objectDiagramPath);
            resizeControl(selectTValueRectangle, selectTValue);
            resizeControl(selectAValueRectangle, selectAValue);
            resizeControl(createProjectButtonRectangle, createProjectButton);
            resizeControl(selectElevationTablePathRectangle, selectElevationTablePath);
            resizeControl(selectObjectDiagramPathRectangle, selectObjectDiagramPath);

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
