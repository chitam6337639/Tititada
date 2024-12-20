using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class Student : Person
	{
		protected string UniversityName { get; set; }

		public Student(string name, int age, float height, string car, string home, string universityName)
			: base(name, age, height, car, home)
		{
			UniversityName = universityName;
		}

		public Student(string name, int age, float height, string universityName)
			: base(name, age, height, null, null)
		{
			UniversityName = universityName;
		}

		public override void DisplayAllInfo()
		{
			base.DisplayAllInfo();
			Console.WriteLine($"University Name: {UniversityName}");
		}

		public void DisplayInfo()
		{
			Console.WriteLine($"Name: {Name}");
			Console.WriteLine($"Age: {GetAge()}");
			Console.WriteLine($"Height: {Height}");
			Console.WriteLine($"Car: {Car}");
			Console.WriteLine($"Home: {Home}");
			Console.WriteLine($"University Name: {UniversityName}");
		}
	}
}
