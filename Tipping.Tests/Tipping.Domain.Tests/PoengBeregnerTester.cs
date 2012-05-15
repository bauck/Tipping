using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tipping.Domain;

namespace Tipping.Tests.Tipping.Domain.Tests
{
    /// <summary>
    /// Summary description for PoengBeregnerTester
    /// </summary>
    [TestClass]
    public class PoengBeregnerTester
    {
        public PoengBeregnerTester()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ForskjelligKampIDOgTipsKampIDSkalAlltidGiNullPoeng()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 0, 0, true);
            var tips = new Tips(2, "Rasmus", 0, 0, true);
            PoengBeregner.BerengPoeng(kamp, tips);

            Assert.AreEqual(0, tips.Poeng);
        }

        [TestMethod]
        public void KampIkkeFerdigspiltSkalAlltidGiNullPoeng()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 0, 0, false);
            var tips = new Tips(1, "Rasmus", 0, 0, true);
            PoengBeregner.BerengPoeng(kamp, tips);

            Assert.AreEqual(0, tips.Poeng);
        }

        [TestMethod]
        public void TipsIkkeLevertSkalAlltidGiNullPoeng()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 0, 0, true);
            var tips = new Tips(1, "Rasmus", 0, 0, false);
            PoengBeregner.BerengPoeng(kamp, tips);

            Assert.AreEqual(0, tips.Poeng);
        }
        [TestMethod]
        public void EksaktRiktigTipsSkalGiFirePoeng()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 0, 0, true);
            var tips = new Tips(1, "Rasmus", 0, 0, true);
            PoengBeregner.BerengPoeng(kamp, tips);

            Assert.AreEqual(4, tips.Poeng);
        }
        [TestMethod]
        public void RiktigUavgjortTippetegnMenFeilResultatSkalGiEttPoeng()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 0, 0, true);
            var tips = new Tips(1, "Rasmus", 1, 1, true);
            PoengBeregner.BerengPoeng(kamp, tips);

            Assert.AreEqual(1, tips.Poeng);
        }
        [TestMethod]
        public void RiktigDifferanseMenFeilResultatSkalGiToPoeng()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 1, 0, true);
            var tips = new Tips(1, "Rasmus", 2, 1, true);
            PoengBeregner.BerengPoeng(kamp, tips);

            Assert.AreEqual(2, tips.Poeng);
        }
        [TestMethod]
        public void RiktigTippeTegnMenFeilDifferanseSkalGiEttPoeng1()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 1, 0, true);
            var tips = new Tips(1, "Rasmus", 3, 1, true);
            PoengBeregner.BerengPoeng(kamp, tips);


            Assert.AreEqual(1, tips.Poeng);
        }
        [TestMethod]
        public void RiktigTippeTegnMenFeilDifferanseSkalGiEttPoeng2()
        {
            var kamp = new Kamp(1, "A", "B", new DateTime(2012, 5, 1, 20, 0, 0), 0, 1, true);
            var tips = new Tips(1, "Rasmus", 1, 3, true);
            PoengBeregner.BerengPoeng(kamp, tips);

            Assert.AreEqual(1, tips.Poeng);
        }
    }
}
