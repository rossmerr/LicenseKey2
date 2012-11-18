using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseKey
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TokenAttribute : Attribute
    {
        public TokenAttribute(char characterToken)
        {
            CharacterToken = characterToken;
        }

        public TokenAttribute(char characterToken, int length)
        {
            CharacterToken = characterToken;
            Length = length;
        }

        public char CharacterToken;
        public int Length = -1;
    }   
}
