using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace HackerRank_problem_solving_intermadiate
{

public class AuthenticationSystem
    {
        public List<int> CheckAuthorizations(List<List<string>> events)
        {
            string password = null;
            List<int> results = new List<int>();

            long p = 131;
            long M = (long)Math.Pow(10, 9) + 7;

            foreach (List<string> ev in events)
            {
                string action = ev[0];
                string input = ev[1];

                if (action == "setPassword")
                {
                    password = input;
                }
                else if (action == "authorize")
                {
                    if (password == null)
                    {
                        results.Add(0); // No password set
                    }
                    else
                    {
                        bool authorized = false;
                        int n = password.Length;

                        // Check authorization with original password
                        long originalHashValue = CalculateHash(password, p, M);
                        if (originalHashValue.ToString() == input)
                        {
                            authorized = true;
                            results.Add(authorized ? 1 : 0);
                            continue;
                        }

                        // Check authorization with appended character
                        foreach (char c in GetPossibleCharacters())
                        {
                            string appendedPassword = password + c;
                            long appendedHashValue = CalculateHash(appendedPassword, p, M);
                            if (appendedHashValue.ToString() == input)
                            {
                                authorized = true;
                                break;
                            }
                        }

                        results.Add(authorized ? 1 : 0);
                    }
                }
            }

            return results;
        }

        private long CalculateHash(string password, long p, long M)
        {
            long hashValue = 0;
            int n = password.Length;

            for (int i = 0; i < n; i++)
            {
                hashValue += (password[i] * ModPow(p, n - i - 1, M)) % M;
                hashValue %= M;
            }

            return hashValue;
        }

        private long ModPow(long x, int y, long m)
        {
            if (y == 0)
            {
                return 1;
            }
            else if (y % 2 == 0)
            {
                long temp = ModPow(x, y / 2, m);
                return (temp * temp) % m;
            }
            else
            {
                return (x * ModPow(x, y - 1, m)) % m;
            }
        }

        private IEnumerable<char> GetPossibleCharacters()
        {
            // Define the possible characters that can be appended to the password
            for (char c = ' '; c <= '~'; c++) // Range from space (' ') to tilde ('~')
            {
                yield return c;
            }
        }
    }

  
 }
