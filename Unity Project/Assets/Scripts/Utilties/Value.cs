using System;


namespace OnLooker
{

    //Value Class
    [Flags()]
    public enum ParseError
    {
        NO_ERROR = 0,
        AS_BOOL = 1,
        AS_BYTE = 2,
        AS_INT = 4,
        AS_FLOAT = 8,
        AS_DOUBLE = 16
    }
    /*
    *   Class: Value
    *   Base Class: object
    *   Interfaces: none
    *   Description: Holds data in a string format. This data can be parsed to different data types suchs as bool, byte, int , float , double
    *   Provides error checking using bit flags, if there was an error in parsing the bit flag is raised.
    *   Date Reviewed: 11/4/2014 by Nathan Hanlan
    */
    public class Value
    {
        //Constructor
        public Value(string aData)
        {
            m_Data = aData;
        }
        //The data of the value
        private string m_Data = string.Empty;
        //Value's independantly do their own error checking
        private ParseError m_Error = ParseError.NO_ERROR;


        //Get data as string
        public string asString()
        {
            return m_Data;
        }
        //Get data as a boolean
        public bool asBool()
        {
            //If the data was written with words
            //if true then return true
            if (m_Data.Contains("true") || m_Data.Contains("TRUE") || m_Data.Contains("True"))
            {
                return true;
            }
            //if false return false
            else if (m_Data.Contains("false") || m_Data.Contains("FALSE") || m_Data.Contains("False"))
            {
                return false;
            }
            //If the data was written as a number
            //If 0 return false

            byte byteValue = asByte();
            //If there was an error parsing the byte then there was an error parsing the bool
            if ((m_Error & ParseError.AS_BYTE) == ParseError.AS_BYTE)
            {
                m_Error = m_Error | ParseError.AS_BOOL;
            }


            if (byteValue == 0)
            {
                return false;
            }
            //Not 0 return false;
            else if (byteValue != 0)
            {
                return true;
            }

            //If we get down here, failed to parse as bool
            m_Error = m_Error | ParseError.AS_BOOL;
            return false;
        }
        //get string as a byte
        public byte asByte()
        {
            byte byteValue = 0;
            bool error = byte.TryParse(m_Data, out byteValue);
            if (error == true)
            {
                //Success
                return byteValue;
            }
            //Failure
            m_Error = m_Error | ParseError.AS_BYTE;
            return 0;
        }
        //Get string as an int
        public int asInt()
        {

            int intValue = 0;
            bool error = int.TryParse(m_Data, out intValue);
            if (error == true)
            {
                //Success
                return intValue;
            }
            //Failure
            m_Error = m_Error | ParseError.AS_INT;
            return 0;
        }
        //Get string as a float
        public float asFloat()
        {
            float floatValue = 0;
            bool error = float.TryParse(m_Data, out floatValue);
            if (error == true)
            {
                //Success
                return floatValue;
            }
            //Failure
            m_Error = m_Error | ParseError.AS_FLOAT;
            return 0.0f;
        }
        //Get string as a double
        public double asDouble()
        {
            double doubleValue = 0;
            bool error = double.TryParse(m_Data, out doubleValue);
            if (error == true)
            {
                //Success
                return doubleValue;
            }
            //Failure
            m_Error = m_Error | ParseError.AS_DOUBLE;
            return 0.0;
        }

        public ParseError getError()
        {
            //Check the bit fields for an error and then return with the error or return no error if there were none
            //remove the error from the bit field as the user will have been notified already.
            if ((m_Error & ParseError.AS_BOOL) == ParseError.AS_BOOL)
            {
                m_Error -= ParseError.AS_BOOL;
                return ParseError.AS_BOOL;
            }
            if ((m_Error & ParseError.AS_BYTE) == ParseError.AS_BYTE)
            {
                m_Error -= ParseError.AS_BYTE;
                return ParseError.AS_BYTE;
            }
            if ((m_Error & ParseError.AS_INT) == ParseError.AS_INT)
            {
                m_Error -= ParseError.AS_INT;
                return ParseError.AS_INT;
            }
            if ((m_Error & ParseError.AS_FLOAT) == ParseError.AS_FLOAT)
            {
                m_Error -= ParseError.AS_FLOAT;
                return ParseError.AS_FLOAT;
            }
            if ((m_Error & ParseError.AS_DOUBLE) == ParseError.AS_DOUBLE)
            {
                m_Error -= ParseError.AS_DOUBLE;
                return ParseError.AS_DOUBLE;
            }

            //No Errors clear fields
            m_Error = ParseError.NO_ERROR;
            return ParseError.NO_ERROR;
        }

    }

}