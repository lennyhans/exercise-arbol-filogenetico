using NUnit.Framework;
using arbolFilogenetico;

namespace arbolFilogenetico.Tests
{
    public class Tests
    {
        RepositoryTreeFile RepoService { get; set; }

        [SetUp]
        public void Setup()
        {
            RepoService = new RepositoryTreeFile();
        }

        [Test]
        public void GetTest()
        {
            var resp = RepoService.Get("Input.txt");
            var node = resp.getNode("1.2.1");
            Assert.That(resp != null, "The expected node should not be null");
            Assert.That(node.id == "1.2.1", "The expected id is the same as geted");
        }

        [Test()]
        [TestCase("1.1.1.1.1.1", 5)]
        [TestCase("1", 0)]
        [TestCase("1.2.3.4", 3)]
        public void countLevelTest(string value, int expected){
            var resp = RepoService.countLevel(value, '.');
            Assert.That(resp == expected, "The expected amount of elements are {0}", expected);
        }

        [Test()]
        [TestCase("1.1.1.2", "1.1.1", true)]
        [TestCase("1.1.2.3", "1.1.2", true)]
        [TestCase("1.3", "1", true)]
        [TestCase("1.1.1.2", "1.2.1", false)]
        [TestCase("1.1.1.2", "2", false)]
        [TestCase("1.1.1.2", "1", false)]
        [TestCase("2.1.1.2", "1", false)]
        public void isParentTest(string idChild, string idParent, bool expected){
            var resp = RepoService.isParent(idChild, idParent);
            
            Assert.That(resp == expected, "The value is expected to be equal");
        }

        [Test()]
        [TestCase("1.1.1", true)]
        [TestCase("1.1.2", true)]
        [TestCase("1.3", true)]
        [TestCase("3", false)]
        [TestCase("2", true)]
        [TestCase("1.3.1",  true)]
        public void getNodeTest( string idToFind, bool expectedFound){
            var nodes = new Nodo(null, null);
            var n1 = new Nodo("1", "TEST 1");
            var n2 = new Nodo("2", "TEST 2");
            var n11 = new Nodo("1.1", "Test 1.1");
            var n111 = new Nodo("1.1.1", "Test 1.1.1");
            var n112 = new Nodo("1.1.2", "Test 1.1.2");
            var n113 = new Nodo("1.1.3", "Test 1.1.3");
            var n12 = new Nodo("1.2", "Test 1.2");
            var n121 = new Nodo("1.2.1", "Test 1.2.1");
            var n13 = new Nodo("1.3", "Test 1.3");
            var n131 = new Nodo("1.3.1", "Test 1.3.1");

            n11.childs.Add(n111);
            n11.childs.Add(n112);
            n11.childs.Add(n113);

            n12.childs.Add(n121);
            n13.childs.Add(n131);
            n1.childs.Add(n11);
            n1.childs.Add(n12);
            n1.childs.Add(n13);
            nodes.childs.Add(n1);
            nodes.childs.Add(n2);

            var targetNode = RepoService.getNode(idToFind, nodes);
            if(expectedFound)
            Assert.That( targetNode.id == idToFind &&
                expectedFound, "The expected value is {0}, but obtained", n111.id, targetNode.id);
            else
                Assert.That(targetNode == null, "The node should not be in the set");
        }
    }
}