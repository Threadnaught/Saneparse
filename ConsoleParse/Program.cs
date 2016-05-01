using System;
using System.IO;
using Saneparse;

namespace ConsoleParse
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string In = "";
			int i = 0;
			while((i  = Console.Read()) != -1)
			{
				In += (char)i;
			}
			string Rules = File.ReadAllText (args [0]);
			Tokeniser t = new Tokeniser ();
			t.LoadString (Rules);
			Console.WriteLine (Rules);
			//Console.Write (t.Run(In));
		}
	}
}