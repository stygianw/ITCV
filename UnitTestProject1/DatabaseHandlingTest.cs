using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITCV.Models.Repositories;
using ITCV.Models.Views;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;


namespace UnitTestProject1
{
    [TestClass]
    public class DatabaseHandlingTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            CVRepository rep = new CVRepository();
            FileStream stream = new FileStream(@"C:\Users\StygianW\Documents\visual studio 2013\Projects\ITCV\ITCV\cv.dat", FileMode.Open, FileAccess.Read);
            //FileStream stream = new FileStream(@"C:\Users\StygianW\Documents\visual studio 2013\Projects\ITCV\ITCV", FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            CV cv = (CV)formatter.Deserialize(stream);
        //    rep.Insert(cv);
            CV cvFromBase = rep.All().FirstOrDefault(m => m.CVId == cv.CVId);
            Assert.AreEqual(cv.CVId, cvFromBase.CVId);
        }

        [TestMethod]
        public void Test2()
        {
            HashSet<CV> i = new HashSet<CV>();
            
            Type type = typeof(object);
            Type type2 = typeof(object);
            Assert.IsTrue(i is ICollection<object>);
        }
    }
}
