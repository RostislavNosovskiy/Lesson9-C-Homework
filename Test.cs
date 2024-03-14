using System;
namespace Lesson9
{
	public class TestClass
	{
        [CustomName("CustomFieldName")]
        public int I ;

        public char[]? C { get; set; }
        [CustomName("CustomFieldStringName")]
        public string? S;
		public decimal D { get; set; }

        public TestClass(int i) 
        {
            I = i;
        }
        public TestClass()
        {
           
        }

        public TestClass( int i, char[] c, string s, decimal d) : this(i)
		{
			C = c;
			S = s;
			D = d;
		}

	}
}

