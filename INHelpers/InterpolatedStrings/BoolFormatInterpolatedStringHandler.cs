﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace INHelpers.InterpolatedStrings
{
    /// <summary>
    /// An inpolated string handler that provides format options for boolean values in interpolated strings.
    /// <code>
    /// BoolFormatInterpolatedStringHandler str = $"{true:true string:false string}";
    /// </code>
    /// Types other than bool are passed through to the default handler.
    /// This type name is a bit long so it's recommended to alias it in your own code:
    /// <code>
    /// using BoolFormat = INHelpers.InterpolatedStrings.BoolFormatInterpolatedStringHandler;
    /// BoolFormat str = $"{true:true string:false string}";
    /// </code>
    /// A known limitation of this handler is that the true and false strings cannot contain a colon (:).
    /// </summary>
    [InterpolatedStringHandler]
    public ref struct BoolFormatInterpolatedStringHandler
    {
        private System.Runtime.CompilerServices.DefaultInterpolatedStringHandler _defaultHandler;
        public BoolFormatInterpolatedStringHandler(int literalLength, int formattedLength)
        {
            _defaultHandler = new System.Runtime.CompilerServices.DefaultInterpolatedStringHandler(literalLength, formattedLength);
        }


        public void AppendLiteral(string s)
        {
            _defaultHandler.AppendLiteral(s);
        }

        public void AppendFormatted<T>(T t, string? format = null)
        {
            if (format != null &&
                t is bool || Nullable.GetUnderlyingType(typeof(T)) == typeof(bool))
            {
                var value = t as bool?;
                var formatOptions = GetBoolFormat(format!);
                if (value == true)
                    _defaultHandler.AppendLiteral(formatOptions.trueString);
                else
                    _defaultHandler.AppendLiteral(formatOptions.falseString);
            }
            else
            {
                _defaultHandler.AppendFormatted(t, format: format);
            }
        }

        private (string trueString, string falseString) GetBoolFormat(string format)
        {
            var builder = new StringBuilder();
            var separatorFound = false;
            string? trueString = null;
            foreach (char ch in format)
            {
                if (ch == ':')
                {
                    if (separatorFound)
                        throw new ArgumentException($"A bool format string can coontain only one ':', but two were found in the string '{format}'", nameof(format));
                    trueString = builder.ToString();
                    builder.Clear();
                    separatorFound = true;
                }
                else
                {
                    builder.Append(ch);
                }
            }

            if (!separatorFound)
                throw new ArgumentException($"A format string must contain one ':' character which separates the strings to use for true and false, however '{format}' was supplied", nameof(format));

            return (trueString!, builder.ToString());
        }

        public override string ToString()
        {
            return _defaultHandler.ToStringAndClear();
        }

        #region operators

        public static implicit operator string(BoolFormatInterpolatedStringHandler handler) => handler.ToString();

        #endregion

    }
}
