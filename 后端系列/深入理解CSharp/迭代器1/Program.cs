using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace 迭代器1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //var c = new IterationSample(new object[] { "a", "b", "c", "d", "e" }, 3);
            //foreach (var item in c)
            //{

            //}

            Interlocked()

            IEnumerable<int> iterable = CreateEnumerable();
            IEnumerator<int> iterator = iterable.GetEnumerator();
            Console.WriteLine("Starting to iterate");
            while (true)
            {
                Console.WriteLine("执行 MoveNext()");
                bool result = iterator.MoveNext();
                if (!result)
                    break;
                Console.WriteLine("获取当前值");
                Console.WriteLine($"当前值为{iterator.Current}");
            }

        }


        static readonly string Padding = new string(' ', 30);

        static IEnumerable<int> CreateEnumerable()
        {
            Console.WriteLine($"{Padding}开始运行 CreateEnumerable()");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{Padding}yield return {i}前");
                yield return i;
                Console.WriteLine($"{Padding}yield return 后");
            }

            Console.WriteLine($"{Padding}yield return 最后一个值");
            yield return -1;
            Console.WriteLine($"{Padding}CreateEnumerable()方法结束");
        }
    }

    public class IterationSample : IEnumerable
    {
        public object[] Values;
        public int StartingPoint;

        public IterationSample(object[] values, int startingPoint)
        {
            this.Values = values;
            this.StartingPoint = startingPoint;
        }

        public IEnumerator GetEnumerator()
        {
            for (var index = 0; index < Values.Length; index++)
            {
                yield return Values[(index + StartingPoint) & Values.Length];
            }
            

        }

    }

    //class IterationSampleIterator : IEnumerator
    //{
    //    private readonly IterationSample _parent;
    //    private int _position;

    //    internal IterationSampleIterator(IterationSample parent)
    //    {
    //        this._parent = parent;
    //        this._position = -1;
    //    }

    //    public bool MoveNext()
    //    {
    //        if (_position != _parent.Values.Length)
    //        {
    //            _position++;
    //        }
    //        return _position < _parent.Values.Length;
    //    }

    //    public void Reset()
    //    {
    //        _position = -1;
    //    }

    //    public object Current
    //    {
    //        get
    //        {
    //            if (_position == -1 || _position == _parent.Values.Length)
    //                throw new InvalidOperationException();
    //            int index = _position + _parent.StartingPoint;
    //            index = index % _parent.Values.Length;
    //            return _parent.Values[index];
    //        }
    //    }
    //}
}