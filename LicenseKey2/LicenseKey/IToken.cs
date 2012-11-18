using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseKey
{
    public interface IToken
    {
        char CharacterToken { get; }
        Key.TokenTypes TokenType { get; }
        string InitialValue { get; }
        int Length { get; }
    }
}
