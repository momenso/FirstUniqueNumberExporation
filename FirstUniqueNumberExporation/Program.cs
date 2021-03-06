using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstUniqueNumberExporation
{
	internal class Program
	{
		private const int Seed = 0;
		private const int Iterations = 10;
		private const int Expected = -33;
		private const int NumberInArray = 5;

		public static int[] numberArray { get; private set; }

		private const string outputTemplate = "{0}  {1}   {2}  {3}   ";

		private static void Main(string[] args)
		{
			var na = NumberInArray / 2;
			var sb = new StringBuilder();
			for (int i = Seed; i < na + Seed; i++)
			{
				sb.AppendFormat("{0},{0},", i);
			}
			sb.Append(Expected);

			numberArray = sb.ToString().Split(',').Select(n => Convert.ToInt32(n)).ToArray();

			Console.WriteLine(string.Format("Numbers in Array {0}, seed {1}", numberArray.Length, Seed));
			Console.WriteLine(string.Format("Loops {0}", Iterations));
			Console.WriteLine();
			Console.WriteLine("Ticks");
			Console.WriteLine("   Min   Max     Avg  Actual");

			Console.WriteLine("------");
			Console.WriteLine("Test 1");
			Console.WriteLine("------");
			RunTests();

			Console.WriteLine("------");
			Console.WriteLine("Test 2");
			Console.WriteLine("------");
			numberArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, -33 };
			RunTests();

			Console.WriteLine("------");
			Console.WriteLine("Test 3");
			Console.WriteLine("------");
			numberArray = new[] { 1234, -33, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456, 44, 35435342, 35435342, 1234, 23456 };
			RunTests();

			Console.WriteLine("------");
			Console.WriteLine("Test 4");
			Console.WriteLine("------");
			numberArray = new[] { -33, 2, 3, 4, 5, 6, 7, 8, 9 };
			RunTests();

			Console.WriteLine("------");
			Console.WriteLine("Test 5");
			Console.WriteLine("------");
			numberArray = new[] { -33, 2, 3, 4, 5, 6, 0, 8, 9 };
			RunTests();

			Console.WriteLine("------");
			Console.WriteLine("Test 6");
			Console.WriteLine("------");
			numberArray = new[] { 1, 2, -33, 4, 5, 6, 0, 8, 9, 1, 2 };
			RunTests();

			Console.WriteLine();
			Console.WriteLine("Press any key to exit");

			Console.ReadKey();
		}

		public static void RunTests()
		{
			SoOriginal();
			SoAny();
			SoGroupBy2();
			SoGroupBy();
			SoToLookup();
			Traditional();
			Traditional2();
		}

		public static string Traditional()
		{
			var sw = new System.Diagnostics.StopWatch2() { ShowStatsForEachLoop = false };

			int actual = 0;
			//////////////////////////////////////
			sw.Restart();
			sw.Start();
			for (int i = 0; i < Iterations; i++)
			{
				actual = -1;
				for (int a = 0; a < numberArray.Length; a++)
				{
					bool unique = true;
					for (int b = 0; b < numberArray.Length; b++)
					{
						if (a == b)
							continue;
						if (numberArray[a] == numberArray[b])
						{
							unique = false;
							break;
						}
					}
					if (unique)
					{
						actual = numberArray[a];
						break;
					}
				}
				//// Assert.AreEqual(expected, actual);
				sw.RestartAndLog();
			}
			sw.Stop();
			var retval = string.Format(outputTemplate + "Traditional", sw.Minimum.Ticks.ToString().PadLeft(5), sw.Maximum.Ticks.ToString().PadLeft(5), sw.Average.Ticks.ToString().PadLeft(5), actual.ToString().PadRight(5));
			Console.WriteLine(retval);
			return retval;
		}

		public static string Traditional2()
		{
			var sw = new System.Diagnostics.StopWatch2() { ShowStatsForEachLoop = false };

			int actual = 0;
			//////////////////////////////////////
			sw.Restart();
			sw.Start();
			for (int i = 0; i < Iterations; i++)
			{
				int[] workArray = (int[])numberArray.Clone();
				actual = -1;
				for (int a = 0; a < workArray.Length; a += 1)
				{
					if (a != 0 && workArray[a] == numberArray[0])
						continue;
					bool unique = true;
					for (int b = a + 1; b < workArray.Length; b++)
					{
						if (workArray[a] == workArray[b])
						{
							unique = false;
							workArray[a] = numberArray[0];
							workArray[b] = numberArray[0];
							break;
						}
					}
					if (unique)
					{
						actual = workArray[a];
						break;
					}
				}
				//// Assert.AreEqual(expected, actual);
				sw.RestartAndLog();
			}
			sw.Stop();
			var retval = string.Format(outputTemplate + "Traditional 2", sw.Minimum.Ticks.ToString().PadLeft(5), sw.Maximum.Ticks.ToString().PadLeft(5), sw.Average.Ticks.ToString().PadLeft(5), actual.ToString().PadRight(5));
			Console.WriteLine(retval);
			return retval;
		}

		public static string SoOriginal()
		{

			var sw = new System.Diagnostics.StopWatch2() { ShowStatsForEachLoop = false };

			int actual = 0;
			//////////////////////////////////////
			sw.Restart();
			sw.Start();
			for (int i = 0; i < Iterations; i++)
			{
				actual = -1;
				//var x = 1;
				var y = numberArray.GroupBy(z => z).Where(z => z.Count() == 1).Select(z => z.Key).ToList();
				if (y.Count > 0)
					actual = y[0];
				//// Assert.AreEqual(expected, actual);
				sw.RestartAndLog();
			}
			sw.Stop();
			var retval = string.Format(outputTemplate + "Original", sw.Minimum.Ticks.ToString().PadLeft(5), sw.Maximum.Ticks.ToString().PadLeft(5), sw.Average.Ticks.ToString().PadLeft(5), actual.ToString().PadRight(5));
			Console.WriteLine(retval);
			return retval;
		}

		public static string SoAny()
		{

			var sw = new System.Diagnostics.StopWatch2() { ShowStatsForEachLoop = false };

			int actual = 0;
			//////////////////////////////////////
			sw.Restart();
			sw.Start();
			for (int i = 0; i < Iterations; i++)
			{
				actual = -1;
				//var x = 1;
				var y = numberArray.GroupBy(z => z).Where(z => z.Count() == 1).Select(z => z.Key).ToList();
				if (y.Any())
					actual = y[0];
				//// Assert.AreEqual(expected, actual);
				sw.RestartAndLog();
			}
			sw.Stop();
			string retval =
				string.Format(outputTemplate + "if (y.Any()) actual = y[0];", sw.Minimum.Ticks.ToString().PadLeft(5),
					sw.Maximum.Ticks.ToString().PadLeft(5), sw.Average.Ticks.ToString().PadLeft(5),
					actual.ToString().PadRight(5));
			Console.WriteLine(retval);
			return retval;
		}

		public static string SoGroupBy()
		{

			var sw = new System.Diagnostics.StopWatch2() { ShowStatsForEachLoop = false };

			int actual = -666;
			//////////////////////////////////////
			sw.Restart();


			/////////////////////////////////
			//sw = new System.Diagnostics.StopWatch2();
			sw.Start();
			// sw.ShowStatsForEachLoop = false;
			for (int i = 0; i < Iterations; i++)
			{
				actual = numberArray.GroupBy(g => g).Where(w => w.Count() == 1).Select(s => s.Key).FirstOrDefault();

				//Assert.AreEqual(Expected, actual);
				sw.RestartAndLog();
			}
			sw.Stop();
			var retval =
				string.Format(
					outputTemplate + "a.GroupBy(g => g).Where(w => w.Count() == 1)\r\n\t\t\t\t.Select(s => s.Key).FirstOrDefault();",
					sw.Minimum.Ticks.ToString().PadLeft(5), sw.Maximum.Ticks.ToString().PadLeft(5),
					sw.Average.Ticks.ToString().PadLeft(5)
                    , actual.ToString().PadRight(5));
			Console.WriteLine(retval);
			return retval;
			/////////////////////////////////
		}

		public static string SoToLookup()
		{

			var sw = new System.Diagnostics.StopWatch2() { ShowStatsForEachLoop = false };

			int actual = 0;
			//////////////////////////////////////
			sw.Restart();


			/////////////////////////////////
			//sw = new System.Diagnostics.StopWatch2();
			sw.Start();
			// sw.ShowStatsForEachLoop = false;
			for (int i = 0; i < Iterations; i++)
			{
				actual = numberArray.ToLookup(x => x).First(x => x.Count() == 1).Key;
				;

				// Assert.AreEqual(expected, actual);
				sw.RestartAndLog();
			}
			sw.Stop();
			var retval =
				string.Format(
					outputTemplate + "a.ToLookup(i => i)\r\n\t\t\t\t.First(i => i.Count() == 1).Key;",
					sw.Minimum.Ticks.ToString().PadLeft(5), sw.Maximum.Ticks.ToString().PadLeft(5),
					sw.Average.Ticks.ToString().PadLeft(5),
					actual.ToString().PadRight(5));
			Console.WriteLine(retval);
			return retval;
			/////////////////////////////////
		}

		public static string SoGroupBy2()
		{

			var sw = new System.Diagnostics.StopWatch2() { ShowStatsForEachLoop = false };

			int actual = 0;
			//////////////////////////////////////
			sw.Restart();


			/////////////////////////////////
			//sw = new System.Diagnostics.StopWatch2();
			sw.Start();
			// sw.ShowStatsForEachLoop = false;
			for (int i = 0; i < Iterations; i++)
			{
				actual = numberArray.GroupBy(x => x).First(x => x.Count() == 1).Key;
				;

				sw.RestartAndLog();
			}
			sw.Stop();
			var retval =
				string.Format(
					outputTemplate + "a.GroupBy(i => i)\r\n\t\t\t\t.First(i => i.Count() == 1).Key;",
					sw.Minimum.Ticks.ToString().PadLeft(5), sw.Maximum.Ticks.ToString().PadLeft(5),
					sw.Average.Ticks.ToString().PadLeft(5),
					actual.ToString().PadRight(5));
			Console.WriteLine(retval);
			return retval;
			/////////////////////////////////
		}
	}
}