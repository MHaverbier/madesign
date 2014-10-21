using System;
using System.Collections.Generic;
using System.Linq;

namespace wortfrequenzen
{

	class Domain {
		public IEnumerable<Tuple<string,int>> Count_frequencies(string text) {
			var words = Extract_words (text);
			return Count_frequency_of_each_word (words);
		}

		IEnumerable<string> Extract_words(string text) {
			return text.Split (new[]{ ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				       .Select (w => w.Trim ());
		}

		IEnumerable<Tuple<string,int>> Count_frequency_of_each_word(IEnumerable<string> words) {
			var freq = new Dictionary<string,int> ();

			var x = words.ToArray ();
			foreach (var w in x) {
				if (!freq.ContainsKey (w))
					freq [w] = 0;
				freq [w] += 1;
			}

			return freq.Select (kv => new Tuple<string,int> (kv.Key, kv.Value));
		}
	}

}
