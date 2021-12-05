using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEACW2_PathFinding;

namespace PathFindingUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public static Dijkstra Dijkstra { get; set; }
        public static NodeObjectPool NodePool;
        public static AStar AStar;
        public static FileData FileData;

        //TODO: Test if error thrown when incorrect arguments given
        //TODO: Test if error thrown when the file is in incorrect format
        //TODO: Test if error thrown if there is no route to the target node

        [ClassInitialize]
        public static void ClassInitialiser(TestContext testContext)
        {
            Node nodeA = new Node(1, "A", 0, 2);
            Node nodeB = new Node(2, "B", 3, 3);
            Node nodeC = new Node(3, "C", 2, 1);
            Node nodeD = new Node(4, "D", 5, 2);
            Node nodeE = new Node(5, "E", 4, 0);
            
            nodeA.AddChildNode(nodeB, 2);
            nodeA.AddChildNode(nodeC, 7);
            nodeB.AddChildNode(nodeA, 2);
            nodeB.AddChildNode(nodeD, 3);
            nodeC.AddChildNode(nodeA, 7);
            nodeC.AddChildNode(nodeD, 1);
            nodeC.AddChildNode(nodeE, 4);
            nodeD.AddChildNode(nodeB, 3);
            nodeD.AddChildNode(nodeC, 1);
            nodeD.AddChildNode(nodeE, 4);
            nodeE.AddChildNode(nodeC, 4);
            nodeE.AddChildNode(nodeD, 4);
            
            List<Node> ListOfNodes = new List<Node>();
            ListOfNodes.Add(nodeA);
            ListOfNodes.Add(nodeB);
            ListOfNodes.Add(nodeC);
            ListOfNodes.Add(nodeD);
            ListOfNodes.Add(nodeE);
            
            NodePool = new NodeObjectPool(ListOfNodes);
            Dijkstra = new Dijkstra(nodeA, nodeE);
            Dijkstra.NodePool = NodePool;
            AStar = new AStar(nodeA, nodeE);
            FileData = new FileData();
        }

        [TestMethod]
        public void TestBfs()
        {
            
        }
        
        [TestMethod]
        public void TestDijkstra()
        {
            string expected = "A-B-D-E 9";
            string actual = Dijkstra.Algorithm();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAStar()
        {
            string expected = "A-B-D-E 9";
            string actual = AStar.Algorithm();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestReadFile()
        {
            List<Node> expected = NodePool.GetNodePool();
            List<Node> actual = FileData.ReadFile("../../../../ACW2_test_data_03.txt").GetNodePool();

            foreach (Node expectedNode in expected)
            {
                bool contains = false;
                
                foreach (Node actualNode in actual)
                {
                    if (expectedNode.GetId() == actualNode.GetId() && expectedNode.GetName() == actualNode.GetName() 
                                                                   && expectedNode.GetX() == actualNode.GetX() &&
                                                                   expectedNode.GetY() == actualNode.GetY())
                    {
                        contains = true;
                        break;
                    }
                }

                if (!contains)
                {
                    Assert.Fail();
                }
            }
        }
    }
}