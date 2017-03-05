using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnit.Learn
{
    public class Class1
    {
        [Fact]
        [Trait("Group", "Add测试")]
        public void PassingTest()
        {
            Assert.Equal(3, Add(1, 2));
        }

        [Fact]
        [Trait("Group", "Add测试")]
        public void FailingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact(DisplayName = "测试异常")]
        public void TestException()
        {
            Assert.Throws<InvalidOperationException>(() => Opreation());
        }

        void Opreation()
        {
            throw new InvalidOperationException();
        }

        int Add(int a, int b)
        {
            return a + b;
        }
    }
}
