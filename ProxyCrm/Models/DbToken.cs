using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    public class DbToken
    {
        private static List<Tokens> _tokens;
        public static List<Tokens> Token
        {
            get
            {
                if (_tokens == null)
                    _tokens = new List<Tokens>();
                return _tokens;
            }

            set
            {
                _tokens = value;
            }
        }
    }
}