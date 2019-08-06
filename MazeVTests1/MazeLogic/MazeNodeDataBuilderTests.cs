
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using MazeV.MazeLogic;

namespace MazeVTests1.MazeLogic
{
    public class MazeNodeDataBuilderTests
    {
        
        public void GenerateNodeDataTest()
        {
            var builder = new MazeNodeDataBuilder(5, 3);
            IMazeNodeData data = builder.GenerateNodeData(12345);


            var y = MLinq(2, 0, 2);

            foreach (var item in y)
            {
                var number = new StringBuilder();
                item.ToList().ForEach(x => number.Append(x));

                System.Diagnostics.Debug.WriteLine(number);

            }
            //AllSequences(0, 5, 3);

            Assert.NotNull(data);
        }

        static IEnumerable<IEnumerable<int>> M(int size, int start, int end)
        {
            if (size == 0)
            {
                yield return Enumerable.Empty<int>();
            }
            else
            {
                for (int first = start; first <= end; ++first)
                {
                    foreach (var rest in M(size - 1, first, end))
                    {
                        yield return Prepend(first, rest);
                    }
                }
            }
        }

        static IEnumerable<T> Singleton<T>(T first)
        {
            yield return first;
        }

        static IEnumerable<T> Prepend<T>(T first, IEnumerable<T> rest)
        {
            yield return first;
            foreach (var item in rest)
                yield return item;
        }

        static IEnumerable<IEnumerable<int>> MLinq(int size, int start, int end)
        {
            return size == 0 ?

                Singleton(Enumerable.Empty<int>()) :

                from first in Enumerable.Range(start, end - start + 1)
                from rest in MLinq(size - 1, first, end)
                select Prepend(first, rest);
        }

    }
}