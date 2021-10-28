using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using LantanaGroup.TestDataGenerator.Shared.Logic;
using LantanaGroup.TestDataGenerator.Shared.Data;


namespace LantanaGroup.TestDataGenerator
{
    [TestClass]
    public class GoodFileGeneratorTests
    {
        private FileInfo GetTestFile(string fileName)
        {
            string path = Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            return new FileInfo(path.Substring(6) + "\\data\\" + fileName);
        }

        [TestMethod]
        public void GetOutputFilesCountTest()
        {
            FileInfo inputFile = GetTestFile("TestDataSource.xls");

            int max = -1;
            string largestSection = "";

            SampleDataSet dataSet = SampleDataSetRetriever.GetFromXLS(inputFile);

            FileGenerator.GetOutputFilesCount(null, dataSet, out max, out largestSection);

            Assert.AreEqual(51, max, "Incorrect number of output files");
            Assert.AreEqual("Header", largestSection, "Incorrect largest section");
            
        }

        [TestMethod]
        public void GetOutputFileNameTest()
        {
            DirectoryInfo outputLocation = new DirectoryInfo("C:\\temp");
            FileInfo masterFile = new FileInfo("C:\\nottemp\\awesome.xml");

            string outputName = FileGenerator.GetOutputFileName(outputLocation, 2, masterFile);

            Assert.AreEqual("C:\\temp\\awesome_3.xml", outputName, "File names do not match!");
        }

        [TestMethod]
        public void GetActiveSectionRowsTest()
        {
            int max = -1;
            string largestSection = "";

            FileInfo inputFile = GetTestFile("TestDataSource.xls");
            SampleDataSet dataSet = SampleDataSetRetriever.GetFromXLS(inputFile);
            FileGenerator.GetOutputFilesCount(null, dataSet, out max, out largestSection);

            Dictionary<string, int> activeRows = FileGenerator.GetActiveSectionRows(dataSet, 0, largestSection);
            Dictionary<string, string[]> activeRowsData = FileGenerator.GetActiveSectionRowsData(dataSet, activeRows);
            Assert.AreEqual(dataSet.Sections[largestSection].Data[0][0], activeRowsData[largestSection][0], "First row first value first section does not match");
            Assert.AreEqual(dataSet.Sections[largestSection].Data[0][dataSet.Sections[largestSection].Data.Length - 1], activeRowsData[largestSection][activeRowsData[largestSection].Length - 1], "First row last value first section does not match");
            Assert.AreEqual(dataSet.Sections["Results"].Data[0][0], activeRowsData["Results"][0], "First row first value Results section does not match");
            Assert.AreEqual(dataSet.Sections["Results"].Data[0][dataSet.Sections["Results"].Data[0].Length - 1], activeRowsData["Results"][activeRowsData["Results"].Length - 1], "First row last value Results section does not match");


            activeRows = FileGenerator.GetActiveSectionRows(dataSet, 10, largestSection);
            activeRowsData = FileGenerator.GetActiveSectionRowsData(dataSet, activeRows);
            Assert.AreEqual(dataSet.Sections[largestSection].Data[10][0], activeRowsData[largestSection][0], "Second test First row first value first section does not match");
            Assert.AreEqual(dataSet.Sections[largestSection].Data[10][dataSet.Sections[largestSection].Data.Length - 1], activeRowsData[largestSection][activeRowsData[largestSection].Length - 1], "Second test first row last value first section does not match");
            Assert.AreEqual(dataSet.Sections["Results"].Data[2][0], activeRowsData["Results"][0], "First row first value Results section does not match");
            Assert.AreEqual(dataSet.Sections["Results"].Data[2][dataSet.Sections["Results"].Data[0].Length - 1], activeRowsData["Results"][activeRowsData["Results"].Length - 1], "First row last value Results section does not match");


        }

       /* [TestMethod]
        public void GenerateOutputFilesTest()
        {
            FileInfo inputFile = GetTestFile("TestDataSourceForOutput.xls");
            FileInfo masterFile = GetTestFile("sample-tokenized.xml");
            DirectoryInfo outputDir = new DirectoryInfo("C:\\temp\\output");
            if(!outputDir.Exists)
                outputDir.Create();


            int count = FileGenerator.GenerateOutputFiles(masterFile, outputDir, inputFile, true);
            Assert.AreEqual(51, count, "Incorrect number of files written");

        }
        */


        [TestMethod]
        public void GetApplicableInstancesTest()
        {
            FileInfo actionConfigurationFile = GetTestFile("TestActionConfiguration.xml");
            string masterFile = string.Empty;
            string dataSourceFile = string.Empty;
            ActionConfiguration configuration = ConfigurationFileParser.ParseConfigurationFile(actionConfigurationFile, out dataSourceFile, out masterFile);
            bool found = false;

            Dictionary<string, int> activeRows = new Dictionary<string, int>();
            activeRows.Add("Results", 0);

            Instance[] instances = FileGenerator.GetApplicableInstances(configuration, activeRows, true, null, 0);

            foreach (Instance instance in instances)
            {
                if (instance.name.Equals("test3"))
                    found = true;
            }

            Assert.IsTrue(found, "Didn't find test3");
        }
    }
}
