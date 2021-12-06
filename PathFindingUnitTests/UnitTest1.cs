using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEACW2_PathFinding;

namespace PathFindingUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public static Bfs BFS;
        public static Dijkstra Dijkstra;
        public static NodeObjectPool NodePool;
        public static AStar AStar;
        public static FileData FileData;
        
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
            
            BFS = new Bfs(nodeA, nodeE);
            Dijkstra = new Dijkstra(nodeA, nodeE);
            NodePool = new NodeObjectPool(ListOfNodes);
            Dijkstra.NodePool = NodePool;
            BFS.NodePool = NodePool;
            AStar = new AStar(nodeA, nodeE);
            FileData = new FileData();
        }

        [TestMethod]
        public void TestBfs()
        {
            string expected = "A-B-D-E 9";
            string actual = BFS.Algorithm();
            Assert.AreEqual(expected, actual);
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

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestFileFormat()
        {
            FileData.ReadFile("../../../../ACW2_test_data_04.txt");
            FileData.ReadFile("../../../../ACW2_test_data_05.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestNodeFormat()
        {
            string[] test1 = {"A", "\"A\"", "1", "2"};
            string[] test2 = {"1", "A", "1", "2"};
            string[] test3 = {"1", "\"A\"", "A", "2"};
            string[] test4 = {"1", "\"A\"", "1", "A"};
            
            FileData.FormatNodes(test1);
            FileData.FormatNodes(test2);
            FileData.FormatNodes(test3);
            FileData.FormatNodes(test4);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestEdgeFormat()
        {
            string[] test1 = {"A", "1", "2"};
            string[] test2 = {"1", "A", "1"};
            string[] test3 = {"1", "2", "A"};
            
            FileData.FormatEdges(test1, out Node nodeA, out Node nodeB, out int lenth);
            FileData.FormatEdges(test2, out nodeA, out nodeB, out lenth);
            FileData.FormatEdges(test3, out nodeA, out nodeB, out lenth);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestNoPathBfs()
        {
            Node nodeA = new Node(1, "A", 1, 2);
            Node nodeB = new Node(2, "B", 2, 3);
            Node nodeC = new Node(3, "C", 4, 5);
            
            List<Node> ListOfNodes = new List<Node>();
            ListOfNodes.Add(nodeA);
            ListOfNodes.Add(nodeB);
            ListOfNodes.Add(nodeC);
            
            nodeA.AddChildNode(nodeB, 1);
            nodeB.AddChildNode(nodeA, 1);
            
            BFS = new Bfs(nodeA, nodeC);
            NodePool = new NodeObjectPool(ListOfNodes);
            BFS.NodePool = NodePool;

            BFS.Algorithm();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestNoPathDijkstra()
        {
            Node nodeA = new Node(1, "A", 1, 2);
            Node nodeB = new Node(2, "B", 2, 3);
            Node nodeC = new Node(3, "C", 4, 5);
            
            List<Node> ListOfNodes = new List<Node>();
            ListOfNodes.Add(nodeA);
            ListOfNodes.Add(nodeB);
            ListOfNodes.Add(nodeC);
            
            nodeA.AddChildNode(nodeB, 1);
            nodeB.AddChildNode(nodeA, 1);
            
            Dijkstra = new Dijkstra(nodeA, nodeC);
            NodePool = new NodeObjectPool(ListOfNodes);
            Dijkstra.NodePool = NodePool;

            Dijkstra.Algorithm();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestNoPathAStar()
        {
            Node nodeA = new Node(1, "A", 1, 2);
            Node nodeB = new Node(2, "B", 2, 3);
            Node nodeC = new Node(3, "C", 4, 5);
            
            List<Node> ListOfNodes = new List<Node>();
            ListOfNodes.Add(nodeA);
            ListOfNodes.Add(nodeB);
            ListOfNodes.Add(nodeC);
            
            nodeA.AddChildNode(nodeB, 1);
            nodeB.AddChildNode(nodeA, 1);
            
            AStar = new AStar(nodeA, nodeC);

            AStar.Algorithm();
        }
    }
}