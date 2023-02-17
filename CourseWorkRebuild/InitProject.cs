using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkRebuild
{
    public class InitProject
    {
        private String pathToElevatorTableRoot = "";
        private String pathToObjectDiagramRoot = "";
        private String valueOfT = "";
        private String valueOfa = "";
        private String pathToElevatorAndValuesTableRoot = "";

        public InitProject(String pathToElevatorTableRoot, String pathToObjectDiagramRoot, String valueOfT, String valueOfA)
        {
            this.pathToElevatorTableRoot = pathToElevatorTableRoot;
            this.pathToObjectDiagramRoot = pathToObjectDiagramRoot;
            this.valueOfT = valueOfT;
            this.valueOfa = valueOfA;
        }

        public InitProject(String pathToElevatorAndValuesTableRoot, String pathToObjectDiagramRoot)
        {
            this.pathToElevatorAndValuesTableRoot = pathToElevatorAndValuesTableRoot;
            this.pathToObjectDiagramRoot = pathToObjectDiagramRoot;
        }

        public String getPathToElevatorTableRoot()
        {
            return this.pathToElevatorTableRoot;
        }

        public String getPathToObjectDiagramRoot()
        {
            return this.pathToObjectDiagramRoot;
        }
        public String getValueOfT()
        {
            return this.valueOfT;
        }
        public String getValueOfa()
        {
            return this.valueOfa;
        }

        public String getPathToElevatorAndValuesTableRoot()
        {
            return this.pathToElevatorAndValuesTableRoot;
        }

        public void showObjectDiagram()
        {




        }
    }
}
