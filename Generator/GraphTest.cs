using System;

namespace Generator
{
    public static class GraphTest
    {
        private static int[,] GenerateBasic(int x, int y, int initialValuesLessThan)
        {
            var graph = new int[x, y];

            for (var i0 = 0; i0 < x; i0++)
                for (var i1 = 0; i1 < y; i1++)
                    if (i0 != i1)
                        graph[i0, i1] = Value.Int(1, initialValuesLessThan);

            return graph;
        }

        public static int[,] ShuffleGraph(this int[,] graph)
        {
            int x = graph.GetLength(0);
            int y = graph.GetLength(1);

            var result = (int[,]) graph.Clone();

            /*int[,] result = new int[x,y];

            for (var i0 = 0; i0 < graph.GetLength(0); i0++)
                for (var i1 = 0; i1 < graph.GetLength(1); i1++)
                    result[i0, i1] = graph[i0, i1];*/

            var switch0 = new Tuple<int, int>(Value.Int(x), Value.Int(x));
            var switch1 = new Tuple<int, int>(Value.Int(y), Value.Int(y));

            for (int i = 0; i < x; i++)
            {
                var temp = result[i, switch0.Item1];
                result[i, switch0.Item1] = result[i, switch0.Item2];
                result[i, switch0.Item2] = temp;
            }

            for (int i = 0; i < y; i++)
            {
                var temp = result[switch1.Item1, i];
                result[switch1.Item1, i] = result[switch1.Item2, i];
                result[switch1.Item2, i] = temp;
            }

            return result;
        }

        public static Tuple<int, int[,]> Generate(int x, int y, int initialValuesLessThan = 5, ushort increaseByUpTo = 12)
        {
            int[,] graph = GenerateBasic(x, y, initialValuesLessThan);

            graph = graph.ShuffleGraph();

            int result = 0;

            for (var i0 = 0; i0 < x; i0++)
            {
                var num1 = Value.Int((int) Math.Round(increaseByUpTo / 2d));
                result += num1;
                for (var i1 = 0; i1 < y; i1++)
                    graph[i0, i1] += num1;
            }

            for (var i1 = 0; i1 < y; i1++)
            {
                var num1 = Value.Int(increaseByUpTo / 2);
                result += num1;
                for (var i0 = 0; i0 < x; i0++)
                    graph[i0, i1] += num1;
            }

            return new Tuple<int, int[,]>(result, graph);
        }

        public static Tuple<int, int[,]> GenerateAdvanced(int x, int y, int initialValuesLessThan = 5, ushort increaseByUpTo = 12)
        {
            int[,] graph = GenerateBasic(x, y, initialValuesLessThan);

            int result = 0;

            {
                var i = Value.Int(increaseByUpTo / 4);
            }

            return new Tuple<int, int[,]>(result, graph);
        }
    }
}