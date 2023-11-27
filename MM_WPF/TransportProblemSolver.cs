using System;
using System.Text;

public class TransportProblemSolver
{
    public static int[] Min(int[][] c, int ACount, int BCount)
    {
        int min = 0;
        int minI = 0, minJ = 0;

        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                if (c[i][j] != -1)
                {
                    min = c[i][j];
                    minI = i;
                    minJ = j;
                    break;
                }
            }
        }

        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                if (c[i][j] == -1) continue;
                if (min > c[i][j])
                {
                    min = c[i][j];
                    minI = i;
                    minJ = j;
                }
            }
        }

        int[] result = new int[2];
        result[0] = minI;
        result[1] = minJ;
        c[minI][minJ] = -1;

        //Console.WriteLine($"\nМинимальный с - {min} на координатах [{minI}, {minJ}]\n");
        return result;
    }

    public static int[] XSearch(int ACount, int BCount, int[][] x, int dI, int dJ, int isHorizontal, int[] resultX, int index,
        ref int isComplete)
    {
        if (index > 5)
        {
            isComplete = 1;
            return null;
        }

        if (isHorizontal == -1)
        {
            int leftX, rightX;
            int downX, upX;

            for (int j = 0; j < BCount; j++)
            {
                if (isComplete == 1)
                    return null;

                if (x[dI][j] != 0 && j < dJ)
                {
                    leftX = x[dI][j];
                    resultX[index] = dI;
                    resultX[index + 1] = j;
                    XSearch(ACount, BCount, x, dI, j, 1, resultX, index + 2, ref isComplete);
                }
                else if (x[dI][j] != 0 && j > dJ)
                {
                    rightX = x[dI][j];
                    resultX[index] = dI;
                    resultX[index + 1] = j;
                    XSearch(ACount, BCount, x, dI, j, 1, resultX, index + 2, ref isComplete);
                }
            }

            for (int i = 0; i < ACount; i++)
            {
                if (isComplete == 1)
                    return null;

                if (x[i][dJ] != 0 && i < dI)
                {
                    upX = x[i][dJ];
                    resultX[index] = i;
                    resultX[index + 1] = dJ;
                    XSearch(ACount, BCount, x, i, dJ, 0, resultX, index + 2, ref isComplete);
                }
                else if (x[i][dJ] != 0 && i > dI)
                {
                    downX = x[i][dJ];
                    resultX[index] = i;
                    resultX[index + 1] = dJ;
                    XSearch(ACount, BCount, x, i, dJ, 0, resultX, index + 2, ref isComplete);
                }
            }
        }
        else if (isHorizontal == 0)
        {
            int leftX, rightX;
            for (int j = 0; j < BCount; j++)
            {
                if (isComplete == 1)
                    return null;

                if (x[dI][j] != 0 && j < dJ)
                {
                    leftX = x[dI][j];
                    resultX[index] = dI;
                    resultX[index + 1] = j;
                    XSearch(ACount, BCount, x, dI, j, 1, resultX, index + 2, ref isComplete);
                }
                else if (x[dI][j] != 0 && j > dJ)
                {
                    rightX = x[dI][j];
                    resultX[index] = dI;
                    resultX[index + 1] = j;
                    XSearch(ACount, BCount, x, dI, j, 1, resultX, index + 2, ref isComplete);
                }
            }
        }
        else if (isHorizontal == 1)
        {
            int downX, upX;
            for (int i = 0; i < ACount; i++)
            {
                if (isComplete == 1)
                    return null;

                if (x[i][dJ] != 0 && i < dI)
                {
                    upX = x[i][dJ];
                    resultX[index] = i;
                    resultX[index + 1] = dJ;
                    XSearch(ACount, BCount, x, i, dJ, 0, resultX, index + 2, ref isComplete);
                }
                else if (x[i][dJ] != 0 && i > dI)
                {
                    downX = x[i][dJ];
                    resultX[index] = i;
                    resultX[index + 1] = dJ;
                    XSearch(ACount, BCount, x, i, dJ, 0, resultX, index + 2, ref isComplete);
                }
            }
        }

        // Возвращает массив координат
        return resultX;
    }

    public static int[] PotentialSearch(int ACount, int BCount, int[][] c, int[][] x)
    {
        int[] V = new int[BCount];
        int[] U = new int[ACount];

        for (int i = 0; i < ACount; i++)
        {
            U[i] = -999;
        }

        for (int i = 0; i < BCount; i++)
        {
            V[i] = -999;
        }

        U[0] = 0;
        while (true)
        {
            bool updated = false;

            for (int i = 0; i < ACount; i++)
            {
                for (int j = 0; j < BCount; j++)
                {
                    if (x[i][j] != 0)
                    {
                        if (U[i] != -999 && V[j] == -999)
                        {
                            V[j] = c[i][j] - U[i];
                            updated = true;
                        }
                        else if (U[i] == -999 && V[j] != -999)
                        {
                            U[i] = c[i][j] - V[j];
                            updated = true;
                        }
                    }
                }
            }

            if (!updated)
            {
                break;
            }
        }

        int[] result = new int[ACount + BCount];
        Console.Write("\nU - ");
        for (int i = 0; i < ACount; i++)
        {
            Console.Write($"{U[i]}\t");
            result[i] = U[i];
        }

        Console.Write("\nV - ");
        for (int i = ACount, j = 0; i < BCount + ACount && j < BCount; i++, j++)
        {
            Console.Write($"{V[j]}\t");
            result[i] = V[j];
        }

        Console.WriteLine("\n");

        return result;
    }
    
    public static int[][] DeltaSearch(int ACount, int BCount, int[] U, int[] V, int[][] c, ref int isOptimal, StringBuilder sb)
    {
        isOptimal = 1;

        int[][] d = new int[ACount][];
        for (int i = 0; i < ACount; i++)
        {
            d[i] = new int[BCount];
        }

        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                d[i][j] = V[j] - U[i] - c[i][j];
                sb.Append($"d[{i}][{j}] = {V[j]} - {U[i]} - {c[i][j]} = {d[i][j]}\n");
                if (d[i][j] > 0)
                {
                    isOptimal = 0;
                }
            }
        }

        sb.Append("\nДельты\n");
        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                sb.Append($"{d[i][j]}\t");
            }
            sb.Append("\n");
        }
        sb.Append("\n");

        return d;
    }
    
    public static int[][] OptimalSearch(int ACount, int BCount, int[] U, int[] V, int[][] c, int[][] x, int[][] d, ref int isOptimal, StringBuilder sb)
    {
        int dI = 0, dJ = 0;
        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                if (d[i][j] > 0)
                {
                    dI = i;
                    dJ = j;
                    break;
                }
            }
        }

        int[] resultX = new int[6];
        int isComplete = 0;
        XSearch(ACount, BCount, x, dI, dJ, -1, resultX, 0, ref isComplete);
        int x1 = x[resultX[0]][resultX[1]];
        int x2 = x[resultX[2]][resultX[3]];
        int x3 = x[resultX[4]][resultX[5]];

        int minX = 0;
        int mI = 0, mJ = 0;
        if (x1 <= x3)
        {
            minX = x1;
            mI = resultX[0];
            mJ = resultX[1];
            x[resultX[2]][resultX[3]] = x2 + minX;
            x[resultX[4]][resultX[5]] = x3 - minX;
        }
        else if (x3 <= x1)
        {
            minX = x3;
            mI = resultX[4];
            mJ = resultX[5];
            x[resultX[0]][resultX[1]] = x1 - minX;
            x[resultX[2]][resultX[3]] = x3 + minX;
        }

        x[dI][dJ] = minX;
        x[mI][mJ] = 0;

        sb.Append($"\nОбновленный X\n");
        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                sb.Append($"{x[i][j]}\t");
            }
            sb.Append("\n");
        }

        int[] UV = PotentialSearch(ACount, BCount, c, x);

        for (int i = 0; i < ACount; i++)
        {
            U[i] = UV[i];
        }
        for (int i = ACount, j = 0; i < BCount + ACount && j < BCount; i++, j++)
        {
            V[j] = UV[i];
        }

        return DeltaSearch(ACount, BCount, U, V, c, ref isOptimal, sb);
    }

    public static string SolveKTZ(int[] A, int[] B, int[][] c)
    {
        int ACount = A.Length, BCount = B.Length;
        
        int aCount = 0, bCount = 0;
        foreach (int a in A)
            aCount += a;

        foreach (int b in B)
            bCount += b;

        if (aCount != bCount)
        {
            return "Данные для решения задачи некорректны. Выход из программы...";
        }

        int[][] cCopy = new int[ACount][];
        for (int i = 0; i < ACount; i++)
        {
            cCopy[i] = new int[BCount];
            Array.Copy(c[i], cCopy[i], BCount);
        }

        int[][] x = new int[ACount][];
        for (int i = 0; i < ACount; i++)
        {
            x[i] = new int[BCount];
        }

        var sb = new StringBuilder();
        
        while (true)
        {
            int[] minCoordinates = Min(c, ACount, BCount);
            int a = A[minCoordinates[0]];
            int b = B[minCoordinates[1]];

            if (a > b)
            {
                x[minCoordinates[0]][minCoordinates[1]] = b;
                B[minCoordinates[1]] = 0;
                A[minCoordinates[0]] = a - b;
                for (int i = 0; i < ACount; i++)
                {
                    c[i][minCoordinates[1]] = -1;
                }
            }
            else if (a < b)
            {
                x[minCoordinates[0]][minCoordinates[1]] = a;
                A[minCoordinates[0]] = 0;
                B[minCoordinates[1]] = b - a;
                for (int j = 0; j < BCount; j++)
                {
                    c[minCoordinates[0]][j] = -1;
                }
            }
            else
            {
                x[minCoordinates[0]][minCoordinates[1]] = b;
                B[minCoordinates[1]] = 0;
                A[minCoordinates[0]] = a - b;
                for (int i = 0; i < ACount; i++)
                {
                    c[i][minCoordinates[1]] = -1;
                }
            }

            int endACount = 0;
            int endBCount = 0;

            foreach (int aVal in A)
            {
                if (aVal == 0) endACount++;
            }

            foreach (int bVal in B)
            {
                if (bVal == 0) endBCount++;
            }

            sb.Append("\nОбновленный с\n");
            for (int i = 0; i < ACount; i++)
            {
                for (int j = 0; j < BCount; j++)
                {
                    sb.Append($"{c[i][j]}\t");
                }
                sb.Append("\n");
            }

            sb.Append("\nОбновленный X\n");
            for (int i = 0; i < ACount; i++)
            {
                for (int j = 0; j < BCount; j++)
                {
                    sb.Append($"{x[i][j]}\t");
                }
                sb.Append("\n");
            }

            if (endACount == ACount || endBCount == BCount)
            {
                break;
            }
        }

        sb.Append("\nИтоговый с\n");
        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                sb.Append($"{c[i][j]}\t");
            }
            sb.Append("\n");
        }

        sb.Append("\nX\n");
        for (int i = 0; i < ACount; i++)
        {
            for (int j = 0; j < BCount; j++)
            {
                sb.Append($"{x[i][j]}\t");
            }
            sb.Append("\n");
        }

        int[] UV = PotentialSearch(ACount, BCount, cCopy, x);
        int[] V = new int[BCount];
        int[] U = new int[ACount];

        Array.Copy(UV, U, ACount);
        Array.Copy(UV, ACount, V, 0, BCount);

        int isOptimal = 0;
        int[][] d = DeltaSearch(ACount, BCount, U, V, cCopy, ref isOptimal, sb);

        while (isOptimal == 0)
        {
            sb.Append("\nПлан неоптимален. Перестройка...\n");
            d = OptimalSearch(ACount, BCount, U, V, cCopy, x, d, ref isOptimal, sb);
        }

        return sb.ToString();
    }
}