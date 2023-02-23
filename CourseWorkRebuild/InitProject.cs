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
        private String markCount = "";
        private String buildingCount = "";

        public InitProject(String pathToElevatorTableRoot, String pathToObjectDiagramRoot, String valueOfT, String valueOfA)
        {
            this.pathToElevatorTableRoot = pathToElevatorTableRoot;
            this.pathToObjectDiagramRoot = pathToObjectDiagramRoot;
            this.valueOfT = valueOfT;
            this.valueOfa = valueOfA;
        }

        public InitProject(String pathToElevatorAndValuesTableRoot, String pathToObjectDiagramRoot, String valueOfT, String valueOfA, String markCount, String buildingCount)
        {
            this.pathToElevatorAndValuesTableRoot = pathToElevatorAndValuesTableRoot;
            this.pathToObjectDiagramRoot = pathToObjectDiagramRoot;
            this.valueOfT = valueOfT;
            this.valueOfa= valueOfA;
            this.markCount = markCount;
            this.buildingCount = buildingCount;
        }

        public String getPathToElevatorTableRoot()
        {
            return this.pathToElevatorTableRoot;
        }
        public String getMarkCount()
        {
            return this.markCount;
        }
        public String getBuildingCount()
        {
            return this.buildingCount;
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

        public void setTValue(String newTValue)
        {
            this.valueOfT = newTValue;
        }

        public void setAValue(String newAValue)
        {
            this.valueOfa = newAValue; 
        }
    }
}
