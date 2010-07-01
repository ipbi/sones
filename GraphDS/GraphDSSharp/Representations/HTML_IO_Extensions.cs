﻿/* 
 * HTML_IO_Extensions
 * Achim 'ahzf' Friedland, 2009-2010
 */

#region Usings

using System;
using System.Text;

using sones.GraphDB.QueryLanguage.Result;

#endregion

namespace sones.GraphDS.API.CSharp
{

    /// <summary>
    /// Extension methods to transform a QueryResult and a DBObjectReadout into a text/html representation
    /// </summary>

    public static class HTML_IO_Extensions
    {

        #region ToHTML(this myQueryResult)

        public static String ToHTML(this QueryResult myQueryResult)
        {
            return (new HTML_IO().Export(myQueryResult) as StringBuilder).ToString();
        }

        #endregion

        #region ToHTML(this myQueryResult, myStringBuilder)

        public static String ToHTML(this QueryResult myQueryResult, StringBuilder myStringBuilder)
        {
            return new HTML_IO().Export(myQueryResult, myStringBuilder).ToString();
        }

        #endregion


        #region ToHTML(this myDBObjectReadout)

        private static String ToHTML(this DBObjectReadout myDBObjectReadout)
        {
            return new HTML_IO().Export(myDBObjectReadout) as String;
        }

        #endregion

        #region ToHTML(this myDBObjectReadout, myStringBuilder)

        private static String ToHTML(this DBObjectReadout myDBObjectReadout, StringBuilder myStringBuilder)
        {
            return new HTML_IO().Export(myDBObjectReadout, myStringBuilder) as String;
        }

        #endregion


        #region HTMLBuilder(myGraphDBName, myFunc)

        public static String HTMLBuilder(String myGraphDBName, Action<StringBuilder> myFunc)
        {

            var _StringBuilder = new StringBuilder();

            _StringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            _StringBuilder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            _StringBuilder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            _StringBuilder.AppendLine("<head>");
            _StringBuilder.AppendLine("<title>sones GraphDS</title>");
            _StringBuilder.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"/WebShell/gql.css\" />");
            _StringBuilder.AppendLine("</head>");
            _StringBuilder.AppendLine("<body>");
            _StringBuilder.AppendLine("<h1>sones GraphDS</h1>");
            _StringBuilder.Append("<h2>").Append(myGraphDBName).AppendLine("</h2>");
            _StringBuilder.AppendLine("<table>");
            _StringBuilder.AppendLine("<tr>");
            _StringBuilder.AppendLine("<td style=\"width: 100px\">&nbsp;</td>");
            _StringBuilder.AppendLine("<td>");
            
            myFunc(_StringBuilder);

            _StringBuilder.AppendLine("</td>");
            _StringBuilder.AppendLine("</tr>");
            _StringBuilder.AppendLine("</table>");
            _StringBuilder.AppendLine("</body>").AppendLine("</html>").AppendLine();

            return _StringBuilder.ToString();

        }

        #endregion

    }

}
