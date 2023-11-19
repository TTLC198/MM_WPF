using System;

namespace MM_WPF;

public class TransportProblemSolver
{
    private int[] supplies;
    private int[] demands;
    private int[,] costs;

    public TransportProblemSolver(int[] supplies, int[] demands, int[,] costs)
    {
        this.supplies = supplies;
        this.demands = demands;
        this.costs = costs;
    }

    public int[,] Solve()
    {
        int[,] solution = new int[supplies.Length, demands.Length];

        int i = 0, j = 0;
        while (i < supplies.Length && j < demands.Length)
        {
            int quantity = Math.Min(supplies[i], demands[j]);
            solution[i, j] = quantity;
            supplies[i] -= quantity;
            demands[j] -= quantity;

            if (supplies[i] == 0)
                i++;
            if (demands[j] == 0)
                j++;
        }

        return solution;
    }
}
