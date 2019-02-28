using Common;
using PathSystem.NodesScript;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
    public class PathService : IPathService
    {
        List<int> shortestPath;
        GameObject line;
        List<GameObject> physicalPath = new List<GameObject>();
        NodeControllerView nodeprefab, targetNode;
        int shortestPathLength;
        [SerializeField] List<Node> graph = new List<Node>();
        public void DrawGraph(ScriptableGraph Graph)
        {
            nodeprefab = Graph.nodeprefab;
            targetNode = Graph.targetNode;
            line = Graph.line;
            for (int i = 0; i < Graph.Graph.Count; i++)
            {
                Node node = new Node();
                node.node = Graph.Graph[i].node;
                node.connections = Graph.Graph[i].GetConnections();
                graph.Add(node);
                nodeprefab.SetNodeID(i);
                if (graph[i].node.property == NodeProperty.TARGETNODE)
                {
                    physicalPath.Add(GameObject.Instantiate(targetNode.gameObject, new Vector3(node.node.nodePosition.x, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z), Quaternion.identity));
                }
                else
                {
                    physicalPath.Add(GameObject.Instantiate(nodeprefab.gameObject, new Vector3(node.node.nodePosition.x, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z), Quaternion.identity));
                }
                if (node.connections[0] != -1)
                {
                    physicalPath.Add(GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z - 2.5f), Quaternion.Euler(new Vector3(0, 90, 0))));
                }
                if (node.connections[2] != -1)
                {
                    physicalPath.Add(GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x + 2.5f, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z), new Quaternion(0, 0, 0, 0)));
                }

            }
            shortestPathLength = graph.Count;
            GetShortestPath(0, 3);
        }
        public void DestroyPath()
        {
            graph = new List<Node>();
            for (int i = 0; i < physicalPath.Count; i++)
            {
                GameObject.Destroy(physicalPath[i]);
            }
        }
        private void printAllPaths(int s, int d)
        {
            bool[] isVisited = new bool[graph.Count];
            List<int> pathList = new List<int>();
            pathList.Add(s);
            printAllPathsUtil(s, d, isVisited, pathList);
        }
        private void printAllPathsUtil(int u, int d, bool[] isVisited, List<int> localPathList)
        {
            isVisited[u] = true;
            if (u.Equals(d))
            {
                isVisited[u] = false;
                return;
            }
            foreach (int i in graph[u].connections)
            {
                if (i != -1)
                {
                    if (!isVisited[i])
                    {
                        localPathList.Add(i);
                        printAllPathsUtil(i, d, isVisited, localPathList);
                        localPathList.Remove(i);
                    }
                }
            }
            isVisited[u] = false;
        }
        public List<int> GetShortestPath(int _currentNode, int _destinationNode)
        {
            shortestPath = new List<int>();
            printAllPaths(_currentNode, _destinationNode);
//            Debug.Log("Shortest Path Length is" + shortestPath.Count);
            return shortestPath;
        }
        public int GetNextNodeID(int _nodeId, Directions _dir)
        {
            return graph[_nodeId].connections[(int)_dir];
        }

        public Vector3 GetNodeLocation(int _nodeID)
        {
            return graph[_nodeID].node.nodePosition;
        }

        public List<int> GetPickupSpawnLocation(InteractablePickup type)
        {
            List<int> pickableNodeList = new List<int>();
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].node.spawnPickups == type)
                {
                    pickableNodeList.Add(graph[i].node.uniqueID);
                }
            }
            return pickableNodeList;
        }

        public int GetPlayerNodeID()
        {
            int playerNode = -1;
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].node.property == NodeProperty.SPAWNPLAYER)
                {
                    playerNode = graph[i].node.uniqueID;
                    break;
                }
            }
            return playerNode;
        }
        public List<int> GetEnemySpawnLocation(EnemyType type)
        {
            List<int> enemySpawnNode = new List<int>();
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].ContainsEnemyType(type))
                {
                    enemySpawnNode.Add(graph[i].node.uniqueID);
                }
            }
            return enemySpawnNode;
        }
        public List<int> GetAlertedNodes(int _targetNodeID)
        {
            throw new NotImplementedException();
        }

        public Directions GetEnemySpawnDirection(int _nodeID)
        {
            return graph[_nodeID].node.spawnEnemies[0].dir;
        }

        public bool CheckForTargetNode(int _NodeID)
        {
            return graph[_NodeID].node.property == NodeProperty.TARGETNODE;
        }
    }
}