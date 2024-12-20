using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class Person
	{
		protected string Name { get; set; }
		private int age;
		protected float Height { get; set; }
		protected string Car { get; set; }
		protected string Home { get; set; }

		public Person(string name, int age, float height, string car, string home)
		{
			Name = name;
			this.age = age;
			Height = height;
			Car = car;
			Home = home;
		}

		public int GetAge()
		{
			return age;
		}

		public void SetAge(int age)
		{
			if (age >= 0 && age <= 100)
			{
				this.age = age;
			}
		}

		public virtual void DisplayAllInfo()
		{
			Console.WriteLine($"Name: {Name}");
			Console.WriteLine($"Age: {age}");
			Console.WriteLine($"Height: {Height}");
			Console.WriteLine($"Car: {Car}");
			Console.WriteLine($"Home: {Home}");
		}
	}
}
