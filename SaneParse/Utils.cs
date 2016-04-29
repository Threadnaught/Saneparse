using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saneparse
{	
	public static class Utils
	{
		public static char TokenStart = '\uE000';
		public static char TokenEnd = '\uE47F';
		public static char TypeStart = '\uE480';
		public static char TypeEnd = '\uF8FF';
		public static string Replace(this Match m, string OldFull, string NewReplacment)
		{
			return OldFull.Substring (0, m.Index) + NewReplacment + OldFull.Substring (m.Index + m.Length);
		}
		public static bool IsTokenChar(this char c)
		{
			return (int)c >= (int)TokenStart && (int)c <= (int)TokenEnd;
		}
		public static bool IsTypeChar(this char c)
		{
			return (int)c >= (int)TypeStart && (int)c <= (int)TypeEnd;
		}
	}
}
