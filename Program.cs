using TrabalhoMenorCaminho;

public class Program()
{
    public static void Main()
    {
        Dijkstra d = new Dijkstra(6,1);
      
        d.CreateEdge(1, 3, 12);
        d.CreateEdge(1, 5, 2);
        d.CreateEdge(1, 4, 1);
        d.CreateEdge(5,2,9);
        d.CreateEdge(5, 4, 3);
        d.CreateEdge(4,3,5);
        d.CreateEdge(3,2,8);

       
        d.ListAllRoutes();



    }
}