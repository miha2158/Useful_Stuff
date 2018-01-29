using System;

namespace Generator
{
    public static class ArrayStuff
    {
        private static int[,] GenerateBasic(int x, int y, int initialValuesLessThan)
        {
            var graph = new int[x, y];

            for (var i0 = 0; i0 < x; i0++)
                for (var i1 = 0; i1 < y; i1++)
                    if (i0 != i1)
                        graph[i0, i1] = NewValue.Int(1, initialValuesLessThan);

            return graph;
        }

        public static int[,] ShuffleArray(this int[,] array)
        {
            int x = array.GetLength(0);
            int y = array.GetLength(1);

            var result = (int[,]) array.Clone();

            /*int[,] result = new int[x,y];

            for (var i0 = 0; i0 < graph.GetLength(0); i0++)
                for (var i1 = 0; i1 < graph.GetLength(1); i1++)
                    result[i0, i1] = graph[i0, i1];*/

            var switch0 = new Tuple<int, int>(NewValue.Int(x), NewValue.Int(x));
            var switch1 = new Tuple<int, int>(NewValue.Int(y), NewValue.Int(y));

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

        public static int IncreaseLinesLengthwise(this int[,] array,  int increaseByUpTo)
        {
            int x = array.GetLength(0);
            int y = array.GetLength(1);
            int result = 0;

            for (var i0 = 0; i0 < x; i0++)
            {
                var num1 = NewValue.Int();
                result += num1;
                for (var i1 = 0; i1 < y; i1++)
                    array[i0, i1] += num1;
            }

            return result;
        }
        public static int IncreaseLinesWidthwise(this int[,] array, int increaseByUpTo)
        {
            int x = array.GetLength(0);
            int y = array.GetLength(1);
            int result = 0;

            for (var i1 = 0; i1 < y; i1++)
            {
                var num1 = NewValue.Int();
                result += num1;
                for (var i0 = 0; i0 < x; i0++)
                    array[i0, i1] += num1;
            }

            return result;
        }
        private static int IncreaseAll(this int[,] array, int eachIncreaseByUpTo)
        {
            int result = array.IncreaseLinesLengthwise(eachIncreaseByUpTo);
            result += array.IncreaseLinesWidthwise(eachIncreaseByUpTo);

            return result;
        }

        public static Tuple<int, int[,]> Generate(int x, int y, int initialValuesLessThan = 5, ushort totalIncreaseByUpTo = 12)
        {
            int[,] graph = GenerateBasic(x, y, initialValuesLessThan);

            graph = graph.ShuffleArray();

            int result = 0;
            result += IncreaseAll(graph, totalIncreaseByUpTo / 2);

            return new Tuple<int, int[,]>(result, graph);
        }

        public static Tuple<int, int[,]> GenerateAdvanced(int x, int y, int initialValuesLessThan = 5, ushort eachIncreaseByUpTo = 4)
        {
            int[,] graph = GenerateBasic(x, y, initialValuesLessThan);

            int result = 0;

            {
                var a = NewValue.Int(eachIncreaseByUpTo);
                result += a;
                var index0 = NewValue.Int(Math.Min(x, y));

                var index1 = 0;
                while (index0 == index1)
                    index1 = NewValue.Int(y);

                graph[index0, index1] = 0;

                for (var i0 = 0; i0 < graph.GetLength(0); i0++)
                {
                    if (graph[i0, index1] != 0)
                        graph[i0, index1] -= a;

                    if (graph[i0, index1] <= 0)
                        graph[i0, index1] = NewValue.Int(initialValuesLessThan);
                }

                for (var i1 = 0; i1 < graph.GetLength(1); i1++)
                    if (i1 != index1)
                        graph[index0, i1] += a;
            }

            result += IncreaseAll(graph, eachIncreaseByUpTo);

            return new Tuple<int, int[,]>(result, graph);
        }
    }
}