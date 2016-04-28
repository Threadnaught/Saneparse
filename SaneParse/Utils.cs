using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saneparse
{	
	public static class Utils
	{
		public static string Replace(this Match m, string OldFull, string NewReplacment)
		{
			return OldFull.Substring (0, m.Index) + NewReplacment + OldFull.Substring (m.Index + m.Length);
		}
	}
}
