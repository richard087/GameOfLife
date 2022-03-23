using GameOfLifeLib;
using NUnit.Framework;
namespace GameOfLifeTests
{
    public class Tests
    {
        GameOfLife g;
        [SetUp]
        public void Setup()
        {
            g = new GameOfLife(3, 3);
        }

        [Test]
        public void trivial()
        {
            bool[,] empty = new bool[,] { { false, false, false }, { false, false, false }, { false, false, false } };
            Assert.AreEqual(empty, g.render());
        }
        [Test]
        public void setStateWillMakeAlive()
        {
            bool[,] horizontal = new bool[,] {
                { false, false, false },
                { true,  true,  true },
                { false, false, false } };
            g.setState(1, 0).setState(1, 1).setState(1, 2);
            Assert.AreEqual(horizontal, g.render());
        }
        [Test]
        public void setStateWillMakeDead()
        {
            bool[,] gap = new bool[,] {
                { false, false, false },
                { true,  false, true },
                { false, false, false } };
            g.setState(1, 0).setState(1, 1).setState(1, 2);
            g.setState(1, 1, false);
            Assert.AreEqual(gap, g.render());
        }
        [Test]
        public void runBlinker()
        {
            bool[,] blinkerVertical = new bool[,] {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, false, true, false, false },
                { false, false, true, false, false },
                { false, false, false, false, false }
            };
            g = new GameOfLife(5, 5);
            g.setState(2, 1).setState(2, 2).setState(2, 3);
            Assert.AreEqual(blinkerVertical, g.run().render());
            Assert.AreNotEqual(blinkerVertical, g.run().render());
            Assert.AreEqual(blinkerVertical, g.run(10001).render());
        }
    }
}