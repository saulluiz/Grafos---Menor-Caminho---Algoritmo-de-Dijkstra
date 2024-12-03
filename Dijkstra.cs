using System;
using System.Collections.Generic;

namespace TrabalhoMenorCaminho
{
    public class Dijkstra
    {
        private int[,] edges; // Matriz de adjacência que armazena os pesos das arestas entre os vértices.
        private int storageNode;
        public Dijkstra(int numVertices, int storageNodeIndex)
        {
            if (storageNodeIndex >= numVertices)
                throw new Exception("O deposito não pertence ao grafo");

            this.edges = new int[numVertices, numVertices];
            this.storageNode = storageNodeIndex;
        }

        public void CreateEdge(int originNode, int destinationNode, int edgeWeight)
        {

            edges[originNode, destinationNode] = edgeWeight;
            edges[destinationNode, originNode] = edgeWeight; // Para grafos não direcionados.
        }

        private static int GetClosestNode(int[] weightList, HashSet<int> unvisitedNodes)
        {

            int minDistance = int.MaxValue;
            int closestNode = 0;

            foreach (int node in unvisitedNodes)
            {

                if (weightList[node] < minDistance)
                {
                    minDistance = weightList[node];
                    closestNode = node;
                }
            }
            return closestNode;
        }

        private List<int> GetAdj(int node)
        {

            List<int> adj = new List<int>();

            for (int i = 0; i < edges.GetLength(0); i++)
            {
                if (edges[node, i] > 0)
                {
                    adj.Add(i);
                }
            }
            return adj;
        }

        private int GetWeight(int originNode, int destinationNode)
        {
            // Retorna o peso da aresta entre dois nós.

            return edges[originNode, destinationNode];
        }

        public List<int>? MinimumPath(int originNode, int destinationNode)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"Calculando rota de {originNode} para {destinationNode}");
            Console.ResetColor();
            Console.WriteLine();
           
            int n = edges.GetLength(0);
            int[] edgeWeights = new int[n];
            int[] pred = new int[n];
            HashSet<int> unvisitedNodes = new HashSet<int>();

            edgeWeights[originNode] = 0;

            // Inicializa os pesos e predecessores para todos os nós.
            for (int i = 0; i < n; i++)
            {
                if (i != originNode)
                    edgeWeights[i] = int.MaxValue; // Define o peso como infinito para nós não alcançados.
                pred[i] = -1;
                unvisitedNodes.Add(i);
            }

            while (unvisitedNodes.Count > 0)
            {
                // Seleciona o nó mais próximo entre os não visitados.
                int closestNode = GetClosestNode(edgeWeights, unvisitedNodes);

                if (edgeWeights[closestNode] == int.MaxValue)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Não há caminho para alcançar o destino.");
                    Console.ResetColor();
                    return null;
                }

                unvisitedNodes.Remove(closestNode);
               
                Console.WriteLine($"Nó atual: {closestNode}, Peso acumulado: {edgeWeights[closestNode]}");


                foreach (var v in GetAdj(closestNode))
                {
                    int totalWeight = edgeWeights[closestNode] + GetWeight(closestNode, v);

                    Console.WriteLine($"  Verificando nó adjacente {v} (peso da aresta: {GetWeight(closestNode, v)})");


                    if (totalWeight < edgeWeights[v])
                    {
                        if (edgeWeights[v] == int.MaxValue)
                            Console.WriteLine($"    Atualizando nó {v}: Peso antigo = infinito, Novo peso = {totalWeight}");
                        else
                            Console.WriteLine($"    Atualizando nó {v}: Peso antigo = {edgeWeights[v]}, Novo peso = {totalWeight}");
                        edgeWeights[v] = totalWeight;
                        pred[v] = closestNode; // Define o predecessor.
                    }
                }

                // Interrompe o loop se o nó de destino for alcançado.
                if (closestNode == destinationNode)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write($"Destino {destinationNode} alcançado. Menor caminho calculado:");
                    Console.ResetColor();
                    Console.WriteLine();
                    return ClosestRoute(pred, destinationNode); // Retorna o caminho mínimo.

                }
            }

            return null;
        }

        public List<int> ClosestRoute(int[] pred, int closestNode)
        {
            // Reconstrói o caminho mínimo a partir dos predecessores.
            List<int> path = new List<int>();

            while (closestNode != -1)
            {
                path.Add(closestNode); // Adiciona o nó atual ao caminho.
                closestNode = pred[closestNode]; // Move para o predecessor.
            }

            path.Reverse(); // Reverte a ordem para obter o caminho correto.
            return path;
        }

        public void ListAllRoutes()
        {
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                var minimumPath = this.MinimumPath(storageNode, i);
                if(minimumPath != null) { 
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;

                foreach (int n in minimumPath)
                {  
                    string node = n == int.MaxValue ? "infinito" : n.ToString();
                    Console.Write(" --> " + node);
                }

                Console.Write("  ");
                Console.ResetColor();
                Console.WriteLine();
                }       
            }
        }
    }
}
