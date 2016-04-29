using System;
using System.Text.RegularExpressions;
using Saneparse;
using System.IO;

namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Tokeniser t = new Tokeniser ();
			t.ParseRules (File.ReadAllText ("CalcDemo.saneparse"));
			Console.Write(t.Run ("(1+1)+1"));
		}
	}
}