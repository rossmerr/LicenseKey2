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


namespace LicenseKey.DataType
{
    /// <summary>
    /// Base class for the data types that will be used.
    /// </summary>
    public class Datatype
    {
        private readonly string mystring; // a generic data type for the base class. 

        /// <summary>
        /// do we use bytes or bits
        /// </summary>
        protected bool flagBytes; // do we use bytes or bits

        /// <summary>
        /// The datatype base contructor
        /// </summary>
        public Datatype()
        {
            mystring = "";
        }

        /// <summary>
        /// Check the token size.
        /// </summary>
        /// <param name="tokStream">The input stream of the token.</param>
        /// <param name="rcnt">The size of the input token.</param>
        /// <returns>true/false if the token is the right size.</returns>
        public virtual bool CheckTokenSize(string tokStream, int rcnt)
        {
            return (true);
        }

        /// <summary>
        /// Check the token.
        /// </summary>
        /// <param name="licTempInp">The license template.</param>
        /// <param name="tokinp">The token value.</param>
        /// <param name="tok">The token.</param>
        public virtual void CheckToken(string licTempInp, string tokinp, char tok)
        {
        }


        /// <summary>
        /// Create the string representation of the token.
        /// </summary>
        /// <param name="targetLength">The length of the string.</param>
        /// <returns>The string value.</returns>
        public virtual string CreateStringRepresentation(int targetLength)
        {
            return (mystring);
        }
    }
}