using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saneparse{
	public class Rule
	{
		static Regex FindTokens = new Regex ("(?<!\\\\)/[a-zA-Z0-9]/");//regex to find /token/s
		public char ToType;
		public string MatchRegex;
		public Tokeniser tokeniser;
		public Rule(){
		}
		public Rule(string TypeStr, string RegexStr, Tokeniser tokeniser)
		{
			if (tokeniser.Types.ContainsValue (TypeStr)) 
			{
				//if type already has a char:
				ToType = tokeniser.Types.FirstOrDefault (x => x.Value == TypeStr).Key;
			} 
			else 
			{
				//if type needs a new char:
				tokeniser.Types.Add ((char)(((int)'\uEC80') + tokeniser.Types.Count + 1), TypeStr);
				ToType = (char)(((int)'\uEC80') + tokeniser.Types.Count);
			}
		}
		public Regex GenMatchRegex()
		{
			string CompiledMatchRegex = MatchRegex;
			while (FindTokens.IsMatch(CompiledMatchRegex)) 
			{

			}
			return new Regex ("");
		}
	}
}
