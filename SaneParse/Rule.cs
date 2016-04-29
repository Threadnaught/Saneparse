using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saneparse{
	public class Rule
	{
		static Regex FindTokens = new Regex ("(?<!\\\\)/[a-zA-Z0-9]+/");//regex to find /token/s
		public char ToType;
		public string MatchRegex;
		public Tokeniser tokeniser;
		public Rule(){
		}
		public Rule(string TypeStr, string LookForRegex, Tokeniser tok)
		{
			MatchRegex = LookForRegex;
			tokeniser = tok;
			if (tokeniser.Types.ContainsValue (TypeStr)) 
			{
				//if type already has a char:
				ToType = tokeniser.Types.FirstOrDefault (x => x.Value == TypeStr).Key;
			} 
			else 
			{
				//if type needs a new char:
				tokeniser.Types.Add ((char)(((int)Utils.TypeStart) + tokeniser.Types.Count + 1), TypeStr);
				ToType = (char)(((int)Utils.TypeStart) + tokeniser.Types.Count);
			}
		}
		public Regex GenMatchRegex()
		{
			string CompiledMatchRegex = MatchRegex;
			while (FindTokens.IsMatch(CompiledMatchRegex)) 
			{
				Match m = FindTokens.Match (CompiledMatchRegex);
				char c = tokeniser.TypeToChar (m.Value.Trim (new char[] { '/' }));
				CompiledMatchRegex = m.Replace (CompiledMatchRegex, "(" + c + "[" + Utils.TokenStart + "-" + Utils.TokenEnd + "])");
			}
			return new Regex (CompiledMatchRegex);
		}
	}
}
