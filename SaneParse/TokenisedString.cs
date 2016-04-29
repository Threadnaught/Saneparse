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
				if (Utils.IsTypeChar (c))
					continue;
				if (Utils.IsTokenChar (c)) 
				{
					Out += "(" + tok.Strings [c].ToString () + ")";
					continue;
				}
				Out += c;
			}
			return Out;
		}
	}
}