using System;
using System.Text.RegularExpressions;
using SaneParse;
using System.IO;

namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Tokeniser t = new Tokeniser ();
			t.ParseRules (File.ReadAllText ("CalcDemo.saneparse"));
		}
	}
}
