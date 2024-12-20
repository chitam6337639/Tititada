using OOP;

class Program
{
	static void Main(string[] args)
	{
		// Inheritance
		Student student = new Student("Tam", 20, 1.8f, "Wave", "Villa", "HSU");
		Console.WriteLine("inheritance Ex:");
		student.DisplayInfo();

		Console.WriteLine();

		// Override 
		Student student1 = new Student("Tam", 20, 1.8f, "HSU");
		Console.WriteLine("override Ex:");
		student1.DisplayAllInfo();
	}
}