using System;
using LicenseKey.DataType;

namespace LicenseKey
{
    internal class Token : IToken
    {
        /// <summary>
        /// 
        /// </summary>
        public Datatype Datatype;

        /// <summary>
        /// Add a token into the collection.
        /// </summary>
        /// <param name="characterTokenAdd">The character token.</param>
        /// <param name="tokenTypeAdd">The data type.</param>
        /// <param name="dataValue">The data value.</param>
        public Token(char characterTokenAdd, Key.TokenTypes tokenTypeAdd, string dataValue)
        {
            CharacterToken = characterTokenAdd;
            TokenType = tokenTypeAdd;
            InitialValue = dataValue;

            switch (tokenTypeAdd)
            {
                case Key.TokenTypes.Number:
                    Datatype = new NumberDatatype();
                    break;
                case Key.TokenTypes.Character:
                    Datatype = new CharacterDatatype();
                    break;
                case Key.TokenTypes.Date:
                    // TODO  Fill in the date code later. 
                    //datatype = new CharacterDatatype(); // not done yet
                    break;
            }
        }


        /// <summary>
        /// Property to set/get the character token.
        /// </summary>		
        public char CharacterToken { get; set; }
        /// <summary>
        /// Property to set/get the character token.
        /// </summary>		
        public Key.TokenTypes TokenType { get; set; }


        /// <summary>
        /// Property to set/get the current value.
        /// </summary>		
        public string InitialValue { get; set; }

        public int Length { get; private set; }
    }
}