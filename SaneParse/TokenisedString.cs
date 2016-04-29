using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saneparse 
{
	public class TokenisedString
	{
		public TokenisedString(string From, Tokeniser tokeniser)
		{
			InternalStr = From;
			tok = tokeniser;
		}
		string InternalStr;
		Tokeniser tok;
		public bool MatchRule(Rule r)
		{
			bool ret = false;
			Regex re = r.GenMatchRegex ();
			while (re.IsMatch(InternalStr)) 
			{
				ret = true;
				Match m = re.Match (InternalStr);
				char type = r.ToType;
				char token = tok.GenString (m.Value);
				InternalStr = m.Replace(InternalStr, new string(new char[]{ type, token }));
			}
			return ret;
		}
		public override string ToString ()
		{
			string Out = "";
			foreach (char c in InternalStr) 
			{
				if (Utils.IsTypeChar (c)) continue;
				if (Utils.IsTokenChar (c)) 
				{
					Out += "(" + tok.Strings [c].ToString () + ")";
					continue;
				}
				Out += c;
			}
			return Out;
		}
		public string GenJSON()
		{
			List<string> Builder = new List<string> ();
			Builder.Add ("");
			for (int i = 0; i < InternalStr.Length; i++) 
			{
				if (InternalStr [i].IsTypeChar ())
				{
					Builder.Add (new string (new char[] { InternalStr[i], InternalStr[i + 1] }));
					i++;
					Builder.Add ("");
				}
				else
				{
					Builder [Builder.Count - 1] += InternalStr [i];
				}
			}
			if (Builder [0] == "") Builder.RemoveAt (0);
			if (Builder [Builder.Count - 1] == "") Builder.RemoveAt (Builder.Count - 1);
			string Out = "";
			int Counter = 0;
			foreach (string s in Builder)
			{
				if (s [0].IsTypeChar ())
				{
					Out += "{\"" + tok.Types [s [0]] + Counter.ToString() + "\":" + tok.Strings [s [1]].GenJSON () + "},";
					Counter++;
				}
				else
				{
					Out += "\"" + s.EscapeJSON() + "\",";
				}
			}
			return "[" + Out.Trim(new char[]{','}) + "]";
		}
	}
}