using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace 深入理解CSharp
{
    class Program
    {
        private static readonly object[] objects = { 1, 2, 3, 4, 5, 6, 7, 78, null, 10, 50, 30, 70, 80, 60, null, null, null, null };

        static void Main(string[] args)
        {
            
            Stopwatch wa = new Stopwatch();
            wa.Start();
            for (int i = 0; i < 10000000; i++)
            {
                foreach (var ob in objects)
                {
                    if (ob is int)
                    {
                        var x = Convert.ToInt32(ob);
                    }

                    //int? x = ob as int?;
                }
            }
            
            wa.Stop();
            Console.WriteLine(wa.ElapsedMilliseconds);

           

            //MyNullable<int> x = new MyNullable<int>();
            //var ss = x.Value;
            //MyNullable<int> y = new MyNullable<int>(3);
            //var dd = y.Value;


            //var type = typeof(Person);

            //var method = type.GetMethod("PrintTypeParameter");

            //var m = method.MakeGenericMethod(typeof(string));

            //m.Invoke(null, null);

            //CompareToDefault("df");
            //Console.WriteLine("Hello World!");
        }

        static int CompareToDefault<T>(T value) where T : IComparable<T>
        {
            return value.CompareTo(default(T));
        }
        Pair<string, string> xxx = Pair.Of("1", "2");
    }




    public sealed class Pair<T1, T2> : IEquatable<Pair<T1, T2>>
    {
        public bool Equals(Pair<T1, T2> other)
        {
            throw new NotImplementedException();
        }
    }

    public static class Pair
    {
        public static Pair<T1, T2> Of<T1, T2>(T1 t1, T2 t2)
        {
            return new Pair<T1, T2>();
        }
    }

    public class Person
    {
        public void PrintTypeParameter<T>()
        {
            Console.WriteLine(typeof(T));
        }
    }



    public class Shape
    {

    }

    class Circle : Shape
    {

    }

    class Rectangle : Shape
    {

    }

    interface IDrawing
    {
        IEnumerable<Shape> Shapes { get; set; }
    }

    class MondrianDrawing : IDrawing, IComparer
    {
        public IEnumerable<Shape> Shapes { get; set; }
        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
        }
    }
    class SeuratDrawing : IDrawing
    {
        public IEnumerable<Shape> Shapes { get; set; }

        public static void Main1()
        {

            int? s = 1;

            //List<Shape> s = new List<Circle>();

            //s.Add(new Circle());
        }
    }

    public struct MyNullable<T> where T : struct
    {
        public Boolean HasValue { get; }

        private readonly T _value;

        public T Value
        {
            get
            {
                if (HasValue)
                {
                    return _value;
                }
                throw new InvalidCastException("nullable Object mush have a value");
            }
        }


        //构造函数
        public MyNullable(T value)
        {
            _value = value;
            HasValue = true;
        }


    }


}