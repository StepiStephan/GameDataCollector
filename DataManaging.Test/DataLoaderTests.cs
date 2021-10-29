using NUnit.Framework;
using System.Collections.Generic;

namespace DataManaging.Test
{
    
    public class DataLoaderTests
    {
        DataLoader<List<string>> dataLoader;
        DataSaver<List<string>> dataSaver;
        List<string> testList;
        [SetUp]
        public void Setup()
        {
            dataLoader = new DataLoader<List<string>>();
            dataLoader.SetName("TestLoader");
            dataSaver = new DataSaver<List<string>>();
            dataSaver.SetName("TestLoader");
            testList = new List<string>()
            {
                "das",
                "ist",
                "ein",
                "Test",
                "das",
                "ist",
                "ein",
                "Test",
                "das",
                "ist",
                "ein",
                "Test",
                "das",
                "ist",
                "ein",
                "Test"
            };
        }

        [Test]
        public void LengthTest()
        {
            dataSaver.SaveObject(testList);
            var resultList = dataLoader.LoadObject();
            Assert.AreEqual(testList.Count, resultList.Count);
        }
        [Test]
        public void ContentSameTest()
        {
            dataSaver.SaveObject(testList);
            var resultList = dataLoader.LoadObject();
            for(int i = 0; i < resultList.Count; i++)
            {
                if(resultList[i] != testList[i])
                {
                    Assert.Fail();
                }
            }
            Assert.Pass();
        }
    }
}