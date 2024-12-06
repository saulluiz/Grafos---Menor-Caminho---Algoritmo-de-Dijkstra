using TrabalhoMenorCaminho;

public class Program()
{
    public static void Main()
    {
 Dijkstra d = new Dijkstra(6, 0);

 d.CreateEdge(0, 2, 20);
 d.CreateEdge(0, 1, 60);
 d.CreateEdge(0, 3, 180);
 d.CreateEdge(1, 3, 35);
 d.CreateEdge(1, 4, 50);
 d.CreateEdge(2, 5, 350);
 d.CreateEdge(3, 2, 45);
 d.CreateEdge(3, 5, 70);
 d.CreateEdge(3, 4, 10);

 d.ListAllRoutes();

    }
}
