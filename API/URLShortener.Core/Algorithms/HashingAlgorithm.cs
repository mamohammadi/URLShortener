using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts.Algorithms;

namespace URLShortener.Core.Algorithms
{
    public class HashingAlgorithm : IShortenAlgorithm
    {
        public string Apply(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            using (var method = SHA256.Create())
                return Convert.ToBase64String(method.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }
}
