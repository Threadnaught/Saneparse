using System;
using System.Collections.Generic;
using System.Linq;

namespace SaneParse
{
	public struct TokenisedString
	{
		public TokenisedString(string From)
		{
			InternalStr = From;
		}
		string InternalStr;
		Tokeniser tok;
		public bool MatchRule(Rule r){
			//MATCH RULE - TURN /X/ INTO [\uE000-\uEC7F]\uXXXX (BUT IGNORE \/)
			//RETURN TRUE IF RULE APPLIED
			return true;
		}
	}
	public class Tokeniser
	{
		public Dictionary<char, TokenisedString> Strings = new Dictionary<char, TokenisedString>();
		public Dictionary<char, string> Types = new Dictionary<char, string>();
		public List<Rule> Rules = new List<Rule>();
		public void ParseRules(string RuleString)
		{
			string[] lines = RuleString.Split (new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string s in lines) 
			{
				if (s.Contains (':')) //if not its just a comment
				{
					string[] subSections = s.Split (':');
					Rules.Add (new Rule (subSections [0], s.Substring (subSections [0].Length + 1)));
				}
			}
		}
	}
	public class Rule
	{
		char ToType;
		string MatchRegex;
		public Tokeniser tokeniser;
		public Rule(string TypeStr, string RegexStr)
		{
			if (tokeniser.Types.ContainsValue (TypeStr)) 
			{
				ToType = tokeniser.Types.FirstOrDefault (x => x.Value == TypeStr).Key;
			}

		}
	}

}

