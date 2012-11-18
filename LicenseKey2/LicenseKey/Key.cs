// Copyright (C) 2005  Don Sweitzer
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

namespace LicenseKey
{

    /// <summary>
    /// A Summary description for Key.
    /// </summary>
    /// <remarks>
    /// This is the License Key class for generating License keys.
    /// It provides implementations of all License key operations. 
    /// </remarks>
    public class Key
    {
        /// <summary>
        /// Enumerated data types.
        /// </summary>
        public enum TokenTypes
        {
            /// <summary>
            /// The data type is Numeric.
            /// </summary>
            Number,

            /// <summary>
            ///  The data type is Date.
            /// </summary>
            Date,

            /// <summary>
            /// The data type is Character.
            /// </summary>
            Character
        };

        /// <summary>
        /// Internal structure for the tokens.
        /// </summary>

        // the list of tokens that can be used in the lciense string. 
        private readonly IList<Token> _tokens;

        //private readonly Randomm _rnd; // out random number class

        readonly Random _rnd;		// the random number 

        private string _strLicensekey; // the final license key that was made. 

        /// <summary>
        /// Get/Set the license template string.
        /// </summary>
        /// <example>
        /// <code>
        /// gkey.LicenseTemplate = "xxvv-xxxxxxxx-xxxxxxxx-ppxx"; 
        /// </code>
        /// </example>
        /// <returns>The license template.</returns>
        public string LicenseTemplate { get; protected set; }

        /// <summary>
        /// Key constructor.
        /// </summary>
        public Key(string licenseTemplate) : this(licenseTemplate, Checksum.ChecksumType.Type1)
        {

        }

        public Key(string licenseTemplate, Checksum.ChecksumType checksumAlgorithm)
        {
            ChecksumAlgorithm = checksumAlgorithm;
            LicenseTemplate = licenseTemplate;
            // create the random object
            _rnd = new Random(unchecked((int)DateTime.Now.Ticks));

            // initialzie the license key
            _strLicensekey = string.Empty;
            _tokens = new List<Token>();
        }

        /// <summary>
        /// Get/Set the property to the Checksum Algorithm.
        /// </summary>
        /// <example>
        /// <code>
        /// gkey.CheckSumAlgorithm = ChecksumType.Type1; 
        /// </code>
        /// </example>
        /// <returns>The Algorithm type.</returns>
        private Checksum.ChecksumType ChecksumAlgorithm { get; set; }





        /// <summary>
        /// Add a token into the array. 
        /// </summary>
        /// <param name="location">Location of the data within the array.</param>
        /// <param name="characterToken">The string representation of the characer token.</param>
        /// <param name="tokenTypeAdd">The type of value that will be used in the transformation.</param>
        /// <param name="initialValue">The initial value of the data.</param>
        /// <example>
        /// <code>
        /// gkey.AddToken(0, "v", LicenseKey.Key.TokenTypes.NUMBER, "1"); 
        /// </code>
        /// </example>
        /// <exception cref="System.ApplicationException">Thrown when the location is out of bounds</exception>
        /// <returns>None</returns>
        public void AddToken(int location, string characterToken, TokenTypes tokenTypeAdd, string initialValue)
        {
            _tokens.Add(new Token(Convert.ToChar(characterToken), tokenTypeAdd, initialValue));
        }

        public void AddToken(IToken token)
        {
            _tokens.Add(new Token(token.CharacterToken, token.TokenType, token.InitialValue));
        }

        public void AddToken(IEnumerable<IToken> tokens)
        {
            foreach (var token in tokens)
            {
                AddToken(token);
            }
        }

        /// <summary>
        /// Get a random number so we can add it into the license key string. 
        /// </summary>
        /// <param name="numberLength">The size of the field.</param>
        private void GetRandomNumber(int numberLength)
        {
            var remain = numberLength%4;
            if (remain != 0)
            {
                throw new ApplicationException("For Bits values each block should be a multiple of 4");
            }
            numberLength = numberLength/4;
                        
            var rndnum = _rnd.Next(numberLength);
            _strLicensekey = _strLicensekey + NumberDisplay.CreateNumberString((uint) rndnum, numberLength);
        }

        /// <summary>
        /// Get the checksum number so we can add it into the license key string. 
        /// </summary>
        /// <param name="licensekey">The license key string.</param>
        /// <param name="numberLength">The size of the field.</param>
        /// <param name="includeLicensekey">Include the original license key as part of the return value.</param>
        private string GetChecksumNumber(string licensekey, int numberLength, bool includeLicensekey)
        {
            //
            // create the checksum class
            //
            var chk = new Checksum();

            var remain = numberLength%4;
            if (remain != 0)
            {
                throw new ApplicationException("For Bits values each block should be a multiple of 4");
            }
            numberLength = numberLength/4;
            
            chk.ChecksumAlgorithm = ChecksumAlgorithm;
            chk.CalculateChecksum(licensekey);
            if (includeLicensekey)
            {
                var csum = chk.ChecksumNumber;
                licensekey = licensekey + NumberDisplay.CreateNumberString(csum, numberLength);
            }
            else
            {
                var csum = chk.ChecksumNumber;
                licensekey = NumberDisplay.CreateNumberString(csum, numberLength);
            }
            return licensekey;
        }


        /// <summary>
        /// Create the License key.
        /// </summary>
        /// <example>
        /// <code>
        /// gkey = new Key();
        /// gkey.LicenseTemplate = "vvvvppppxxxxxxxxxxxx-wwwwwwwwxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx";
        /// gkey.MaxTokens = 3;
        /// gkey.AddToken(0, "v", LicenseKey.Key.TokenTypes.NUMBER, "1");
        /// gkey.AddToken(1, "p", LicenseKey.Key.TokenTypes.NUMBER, "2");
        /// gkey.AddToken(2, "w", LicenseKey.Key.TokenTypes.CHARACTER, "QR");
        /// gkey.UseBase10 = false;
        /// gkey.UseBytes = false;
        /// gkey.<b>CreateKey</b>();
        /// finalkey = gkey.GetLicenseKey();
        /// </code>
        /// </example>
        /// <exception cref="System.ApplicationException">Thrown when no key template has been entered.</exception>
        /// <exception cref="System.ApplicationException">Thrown when no the Tokens is not found in input list.</exception>
        /// <returns>None</returns>
        public string CreateKey()
        {
            //
            // Initialize the finalkey so we do not reuse it. 
            //
            _strLicensekey = string.Empty;
            //
            // Check the length of the template string 
            //
            if (LicenseTemplate.Length == 0)
            {
                // throw a exception that the user can understand. 
                throw new ApplicationException("Enter a key template");
            }
            //
            // if a token is in the license string then make sure it fits into it's field for size.
            // make sure it is as large as what is entered in the tempate string
            //
            foreach (var token in _tokens)
            {
                token.Datatype.CheckToken(LicenseTemplate, token.InitialValue, token.CharacterToken);
            }

            // initialize the variables. 
            var slast = '\0';
            var scnt = 0;
            // now go through the license template to see what tokens are found. 
            // fill in the license key string now that we know everything will fit. 

            foreach (var stok in LicenseTemplate.ToCharArray())
            {
                if (stok != slast)
                {
                    if (scnt != 0)
                    {
                        GetToken(slast, scnt);
                        scnt = 1;
                    }
                    else
                    {
                        scnt++;
                    }
                }
                else
                {
                    scnt++;
                }
                slast = stok;                
            }

            //
            // handle anything that was left over. 
            //
            if (scnt != 0)
            {
                GetToken(slast, scnt);
            }

            return _strLicensekey;
        }

        private void GetToken(char slast, int scnt)
        {
            // find the token in the token class
            var coll = _tokens.Where(p => p.CharacterToken == slast).ToList();
            if (coll.Any())
            {
                _strLicensekey = _strLicensekey + coll.First().Datatype.CreateStringRepresentation(scnt);
            }
            else
            {
                // we will not see a x in the list so if it is an
                // x then handle it here. 
                XHandler(slast, scnt);
            }
        }

        private void XHandler(char slast, int scnt)
        {
            switch (slast)
            {
                case 'x':
                    GetRandomNumber(scnt);
                    break;
                case 'c':
                    _strLicensekey = GetChecksumNumber(_strLicensekey, scnt, true);
                    break;
                case '-':
                    _strLicensekey = _strLicensekey + "-";
                    break;
                default:
                    // we could not find the token so this is illegal
                    throw new ApplicationException("Tokens not found in input list");
            }
        }

        /// <summary>
        /// See if the input string has a checksum character.
        /// </summary>
        /// <param name="strIn">The input string to check.</param>
        /// <returns>bool flag.</returns>
        private static bool MatchInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            // such as the dash that we have in the string
            var m = Regex.Match(strIn, @"c", RegexOptions.IgnoreCase);
            return m.Success;
        }

        /// <summary>
        /// Clean the input string of any unwanted characters.
        /// </summary>
        /// <param name="strIn">The input string to clean unwanted characters.</param>
        /// <returns>The cleaned string.</returns>
        private static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            // such as the dash that we have in the string
            return Regex.Replace(strIn, @"-", "");
        }

        /// <summary>
        /// Check the legal input for bit templates.
        /// </summary>
        /// <param name="lictemp">The license Template.</param>
        /// <param name="lickey">The license Key.</param>
        /// <returns></returns>
        private void CheckInputLegalBit(string lictemp, string lickey)
        {
            var tokLic = ' ';
            
            // initialize the string
            var chkLicString = string.Empty;

            // check the length of the strings. 
            if (lictemp.Length/4 != lickey.Length)
            {
                throw new ApplicationException("Template and key string lengths are not equal");
            }

            // since we are looking at the tokens being on a byte boundary  

            // determine the length of the template
            var slen = lictemp.Length;

            // now go through the license template to see what tokens are found. 
            // fill in the license key string now that we know everything will fit. 


            for (var i = 0; i < slen; i++)
            {
                var tokTem = lictemp[i];
                // every four move the license as well. 
                var remain = i % 4;
                if ((remain == 0))
                {
                    tokLic = lickey[i / 4];
                }
                if (tokTem == 'c')
                {
                    // now that we have found the first token. 
                    var tokenCnt = i / 4;
                    if (tokenCnt <= 0)
                    {
                        throw new ApplicationException("No space for the checksum value");
                    }
                    var toklen = lictemp.ToCharArray().Count(c => c == 'c');
                    // remember we are in bits mode
                    toklen = toklen / 4;
                    // put together the checksum from the license key
                    var chktemp1 = string.Empty;
                    for (var j = 0; j < toklen; j++)
                    {
                        var tokJunk = lickey[tokenCnt + j];
                        chktemp1 = chktemp1 + tokJunk;
                    }
                    // get the calculated checksum
                    GetChecksumNumber(chkLicString, (toklen * 4), false);

                    break;
                }
                // add to the checksum string. 
                if ((remain == 0))
                {
                    chkLicString = chkLicString + tokLic;
                }
            }
        }
    

        /// <summary>
        /// Disassemble the Key for the Bits mode.
        /// </summary>
        /// <param name="token">What token to search and dissemble for.</param>
        /// <returns>The string with the result.</returns>
        private string DisassembleKeyBits(string token)
        {
            int i;
            int j;

            // use local variable so we can strip the dashes
            var localLicenseTemplate = LicenseTemplate;
            var localLicensekey = _strLicensekey;

            // clean the input of the dashes
            localLicenseTemplate = CleanInput(localLicenseTemplate);
            localLicensekey = CleanInput(localLicensekey);

            // check to see if any checksum
            if (MatchInput(localLicenseTemplate))
            {
                CheckInputLegalBit(localLicenseTemplate, localLicensekey);
            }

            // Now go through the license template to see what tokens are found. 
            // fill in the license key string now that we know everything will fit. 
            // remember that for this version the tokens must start and end on a
            // byte boundary anyways. 
            var tokLicOut = '\x0';
            var finalString = string.Empty;
            for (i = 0, j = 0; i < localLicenseTemplate.Length; i++)
            {
                var tokTem = localLicenseTemplate[i];
                var tokLic = localLicensekey[j];
                // if we are on a byte boundary then move to the next byte
                var modvalue = i%4;
                int itokLicOut;
                if (tokTem == token[0])
                {
                    var result = tokLic.ToString(CultureInfo.InvariantCulture);

                    var itokLic = int.Parse(result, NumberStyles.AllowHexSpecifier);

                    switch (modvalue)
                    {
                        case 0:
                            itokLicOut = tokLicOut;
                            itokLicOut = (itokLicOut << 1); // move it over one
                            if ((itokLic & 0x8) != 0)
                            {
                                itokLicOut = itokLicOut | 0x1; // Set it if it is set
                            }
                            tokLicOut = Convert.ToChar(itokLicOut);
                            break;
                        case 1:
                            itokLicOut = tokLicOut;
                            itokLicOut = itokLicOut << 1; // move it over one
                            if ((itokLic & 0x4) != 0)
                            {
                                itokLicOut = itokLicOut | 0x1;
                            }
                            tokLicOut = Convert.ToChar(itokLicOut);
                            break;
                        case 2:
                            itokLicOut = tokLicOut;
                            itokLicOut = itokLicOut << 1; // move it over one
                            if ((itokLic & 0x2) != 0)
                            {
                                itokLicOut = itokLicOut | 0x1;
                            }
                            tokLicOut = Convert.ToChar(itokLicOut);
                            break;
                        case 3:
                            itokLicOut = tokLicOut;
                            itokLicOut = itokLicOut << 1; // move it over one
                            if ((itokLic & 0x1) != 0)
                            {
                                itokLicOut = itokLicOut | 0x1;
                            }
                            tokLicOut = Convert.ToChar(itokLicOut);
                            break;
                    }
                }
                if (modvalue == 3)
                {
                    j++; // go to the next charater
                    if (tokTem == token[0])
                    {
                        itokLicOut = tokLicOut;
                        finalString = finalString + itokLicOut.ToString("X");
                    }
                    tokLicOut = '\x0'; // Hexadecimal
                }
            }
            return finalString;
        }

        /// <summary>
        /// Dissassemble the license key based on the template.
        /// </summary>
        /// <param name="token">The token that you want disassembled.</param>
        /// <example>
        /// <code>
        /// result = gkey.DisassembleKey("p"); 
        /// </code>
        /// </example>
        /// <exception cref="System.ApplicationException">Thrown when the Licensekey Is Empty</exception>
        /// <exception cref="System.ApplicationException">Thrown when the Licensekey Template Is Empty</exception>
        /// <returns>The token value represented as a string.</returns>
        public string DisassembleKey(string token)
        {
            //
            // If the final License key is empty then error.  
            //
            if (_strLicensekey.Length == 0)
            {
                // we could not find the token so this is illegal
                throw new ApplicationException("License key Is Empty");
            }
            //
            // If the final License key is empty then error.  
            //
            var slen = LicenseTemplate.Length;
            if (slen == 0)
            {
                // we could not find the token so this is illegal
                throw new ApplicationException("License key Template Is Empty");
            }

            return DisassembleKeyBits(token);
        }
    }
}