using System;
namespace Lesson9
{
	[AttributeUsage(AttributeTargets.Property)]
	public class DontSaveAttribute : Attribute
	{
		public DontSaveAttribute()
		{
		}
	}
}

