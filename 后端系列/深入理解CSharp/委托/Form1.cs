using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 委托
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        static void HandlerEvent(object sender, EventArgs s)
        {
            Console.WriteLine("统统都到我这来");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SomeMethod som = ChildMethod;
        }

        delegate Parent SomeMethod();

        static Child ChildMethod()
        {
            return null;
        }
    }
    class Parent
    {

    }
    class Child : Parent
    {

    }
}
