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
	/// Summary description for NumberDatatype.
	/// </summary>
	class NumberDatatype : Datatype
	{
		int		_dataValue;	// the data value of this data type
        
	    /// <summary>
		/// Check the token size
		/// </summary>
		/// <param name="tokStream">The input stream of the token.</param>
		/// <param name="rcnt">The size of the input token.</param>|
		/// <returns>true/false if the token is the right size.</returns>
		public override bool CheckTokenSize(string tokStream, int rcnt)
		{
		    int		fin;
			int		tokvalue;
			//
			// Check to see if it is a decimal or hex number based on the
			// proper selection. Convert it to a numeric representation.
			//
			try 
			{
				tokvalue = Convert.ToInt32(tokStream, 16);				
			}
			catch(Exception)
			{
				// throw a exception that the user can understand. 
				throw new ApplicationException("Enter a number based on the base selected");
			}

		    var t2 = Math.Log(tokvalue, 16);
		    if ( !flagBytes ) 
		    {
			    rcnt = rcnt / 4;	// if this is bits then divide by 4
		    }
			
			var i1 = (int)t2; 
			var t4 = Math.Round(t2);
			var i2 = (int) t4;
			if ( i2 == i1 ) 
			{
				fin = i2+1;
			}
			else 
			{
				fin = i2;
			}
			//
			// Check to see if the calculated len is greater thatn the temp length
			//
			if ( fin > rcnt ) 
			{
				return false;
			}

	        _dataValue = tokvalue; 
	        return true;
		}


		/// <summary>
		/// Check the token.
		/// </summary>
		/// <param name="licTempInp">The license template.</param>
		/// <param name="tokinp">The token value.</param>
		/// <param name="tok">The token.</param>
		public override void CheckToken(string licTempInp, string tokinp, char tok)
		{
		    bool	flgchk;

		    var ipos = 0; 
			var tempstr = licTempInp;
			//
			// if no tokinput (string representation) is defined in the template
			//
            if (tokinp.Length <= 0) 
			{
				// this is ok, there just is not tokens at this point. 
				return;
			}
			//
			// if no token is defined in the template
			//
			ipos = tempstr.IndexOf(tok, ipos);
			if ( ipos < 0 ) 
			{
				// throw a exception that the user can understand. 
				throw new ApplicationException("If you enter a token you must also have an entry in the template string");
			}
			var rcnt = 0;
			var flgfound = false;
            foreach (var stok in licTempInp.ToCharArray())
		    {
                if (stok == tok)
                {
                    rcnt++;
                }
                else
                {
                    if (rcnt != 0)
                    {
                        if (flgfound)
                        {
                            // throw a exception that the user can understand. 
                            throw new ApplicationException("You can not specify more than one of the same token");
                        }
                        flgchk = CheckTokenSize(tokinp, rcnt);
                        if (flgchk == false)
                        {
                            // throw a exception that the user can understand. 
                            throw new ApplicationException("Please enter a token that will fit into the size to the specified token");
                        }
                        flgfound = true;
                    }
                    rcnt = 0;
                }		        
		    }

		    if (rcnt == 0) return;

		    flgchk = CheckTokenSize(tokinp, rcnt);
		    if ( flgchk == false) 
		    {
		        // throw a exception that the user can understand. 
		        throw new ApplicationException("Please enter a token that will fit into the size to the specified token");
		    }
		}


		/// <summary>
		/// Create the string representation of the token.
		/// </summary>
		/// <param name="targetLength">The length of the string.</param>
		/// <returns>The string value.</returns>
		public override string CreateStringRepresentation(int targetLength)
		{
			if ( !flagBytes ) 
			{
				// get the number of visible fields so divide the number by 4
				// to find it.
				targetLength = targetLength / 4;
			}
			
			// find out the number of fields we now have to fill in.
            var diflen = targetLength - _dataValue.ToString("X").Length;
		    var ostr = string.Empty;
			// for each we we missed add a leading 0
			while ( diflen > 0 ) 
			{
				ostr = ostr + "0";
				diflen--;
			}

			ostr = ostr + _dataValue.ToString("X");
			
			return ostr;
		}
	}
}
