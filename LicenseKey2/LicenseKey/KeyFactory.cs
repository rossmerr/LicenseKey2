using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseKey
{
    public class KeyFactory
    {
        public string _template;
        private int _sets;
        private int _setLength;

        public KeyFactory(string template)
        {
            _template = template;
        }

        public KeyFactory(int sets)
            : this(sets, 10)
        {
            
        }

        public KeyFactory(int sets, int setLength)
        {
            _template = string.Empty;
            _sets = sets;
            _setLength = setLength;
        }

        public Key Generate<TEntity>(TEntity obj) where TEntity : ITokenizable
        {

            var coll = GetTokens(obj);

            var template = GetTemplate(coll);

            var key = new Key(template);
            key.AddToken(coll);
            
            return key;
        }

        private string GetTemplate(IEnumerable<SimpleToken> coll)
        {
            if (!string.IsNullOrEmpty(_template))
            {
                return _template;
            }

            var sb = new StringBuilder();

            foreach (var token in coll)
            {
                for (var i = 0; i < token.Length; i++)
                {
                    sb.Append(token.CharacterToken);
                }
            }
            
            return sb.ToString();
        }

        private static IList<SimpleToken> GetTokens<TEntity>(TEntity obj) where TEntity : ITokenizable
        {
            var coll = new List<SimpleToken>();

            var properties = typeof (TEntity).GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(TokenAttribute), true);

                foreach (TokenAttribute attr in attributes)
                {
                    var getMethod = property.GetGetMethod();
                    var data = getMethod.Invoke(obj, null);

                    if (data is string)
                    {
                        var s = data as string;

                        //if (s.Length < (attr.Length * 4))

                        coll.Add(new SimpleToken(attr.CharacterToken, s, s.Length));
                    }

                    if (data is int)
                    {
                        var number = (int) data;
                        coll.Add(new SimpleToken(attr.CharacterToken, number, number.ToString(CultureInfo.InvariantCulture).Length));
                    }
                }
            }

            return coll;
        }
    }
}
