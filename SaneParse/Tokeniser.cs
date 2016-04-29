using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saneparse {

	public class Tokeniser
	{
		public Dictionary<char, TokenisedString> Strings = new Dictionary<char, TokenisedString>();
		public Dictionary<char, string> Types = new Dictionary<char, string>();
		public List<Rule> Rules = new List<Rule>();
		public char root;
		public void ParseRules(string RuleString)
		{
			string[] lines = RuleString.Split (new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string s in lines) 
			{
				if (s.Contains (':')) //if not its just a comment
				{
					string[] subSections = s.Split (':');
					Rules.Add (new Rule (subSections [0], s.Substring (subSections [0].Length + 1), this));
					Rules.Last ().tokeniser = this;
				}
			}
		}
		public void LoadString(string s)
		{
			root = GenString (s);
		}
		public bool RunPass()
		{
			bool ret = false;
			foreach (Rule r in Rules) 
			{
				while (Strings[root].MatchRule(r)) 
				{
					ret = true;
				}
			}
			return ret;
		}
		public string Run(string s)
		{
			LoadString (s);
			while (RunPass()) {	}
			return GenJSON ();
		}
		public char GenString(string In)
		{
			char ret = (char)(((int)Utils.TokenStart) + Strings.Count + 1);
			Strings.Add(ret, new TokenisedString(In, this));
			return ret;
		}
		public string GenJSON()
		{
			return "{\"ROOT\":" + Strings [root].GenJSON () + "}";
		}
		public char TypeToChar(string s)
		{
			return Types.FirstOrDefault (x => x.Value == s).Key;
		}
	}

}

