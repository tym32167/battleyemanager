using System;
using System.Collections.Generic;

namespace BattlEyeManager.Spa.Infrastructure.Utils
{
    public class StringTemplater
    {
        private readonly IDictionary<string, Func<string>> _params;

        public StringTemplater()
        {
            _params = new Dictionary<string, Func<string>>();
        }

        public string Template(string someText)
        {
            var result = someText;

            foreach (var p in _params)
            {
                result = result.Replace($"{{{p.Key}}}", p.Value());
            }

            return result;
        }

        public void AddParameter(string param, string value)
        {
            _params.Add(param, () => value);
        }

        public void AddParameter(string param, Func<string> value)
        {
            _params.Add(param, value);
        }
    }
}