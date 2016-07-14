using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebScrape.Core
{
    public static class Utility
    {   
        public static IEnumerable<string> EnumeratePath(string path)
        {
            var regex = new Regex(@"{(\d+)\.(\d*)\.(\d+)}", RegexOptions.Compiled);
            var match = regex.Match(path);
            if (!match.Success)
                yield return path;
            else
            {
                var getGroupVal = new Func<int, int?>(i =>
                {
                    var group = match.Groups[i].Value;
                    int result;
                    return int.TryParse(group, out result)
                        ? (int?) result
                        : null;
                });

                var startIndex = getGroupVal(1) ?? 1;
                var steps = getGroupVal(2) ?? 1;
                var stopIndex = getGroupVal(3) ?? 10;
                for (var index = startIndex; index <= stopIndex; index += steps)
                    yield return regex.Replace(path, index.ToString());
            }
        }
    }
}
