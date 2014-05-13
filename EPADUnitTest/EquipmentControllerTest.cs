using System;
using NUnit.Framework;
using StJohnEPAD.Controllers;
using System.Web;
using System.Web.Mvc;

namespace EPADUnitTest
{
    [TestFixture]
    public class EquipmentControllerTest
    {
        [Test]
        public void IndexActionReturnsIndexView()
        {
            string expected = "Index";
            var controller = new EquipmentController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [Test]
        public void Blah()
        {
        }
    }
    
   
}
