using System;
namespace Lesson9
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CustomName : Attribute
	{
		public string Name { get; }
		public CustomName(string name)
		{
			Name = name;
		}
	}
}

