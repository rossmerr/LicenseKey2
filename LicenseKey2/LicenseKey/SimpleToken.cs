using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseKey.DataType;

namespace LicenseKey
{
    public class SimpleToken : IToken
    {
        public SimpleToken(char characterToken, int obj) : this(characterToken, obj, -1)
        {
        }

        public SimpleToken(char characterToken, int obj, int length)
        {
            Initialize(characterToken, length);

            TokenType = Key.TokenTypes.Number;

            InitialValue = obj.ToString(CultureInfo.InvariantCulture);
        }

        public SimpleToken(char characterToken, string obj) : this(characterToken, obj, -1)
        {
        }

        public SimpleToken(char characterToken, string obj, int length)
        {
            Initialize(characterToken, length);

            TokenType = Key.TokenTypes.Character;
            
            InitialValue = obj;
        }

        private void Initialize(char characterToken, int legth)
        {
            CharacterToken = characterToken;

            Length = legth;
        }
        public char CharacterToken { get; private set; }
        public Key.TokenTypes TokenType { get; private set; }
        public string InitialValue { get; private set; }
        public int Length { get; private set; }
    }
}
