using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Contracts.Algorithms
{
    public interface IShortenAlgorithm
    {
        public string Apply(string input, string nounce);
    }
}
