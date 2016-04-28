using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SaneParse
{
	public struct TokenisedString
	{
		public TokenisedString(string From, Tokeniser tokeniser)
		{
			InternalStr = From;
			tok = tokeniser;
		}
		string InternalStr;
		Tokeniser tok;
		public bool MatchRule(Rule r){
			//MATCH RULE - TURN /X/ INTO ([\uE000-\uEC7F]\uXXXX) (BUT IGNORE \/)
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
					Rules.Add (new Rule (subSections [0], s.Substring (subSections [0].Length + 1), this));
				}
			}
		}
		public char TypeToChar(string s)
		{
			return Types.FirstOrDefault (x => x.Value == s).Key;
		}
	}

}

