﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Represents a named parameter in SQL.
    /// </summary>
    public class SqlParameter : SqlValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameter"/> class using
        /// the specified parameter name and <paramref name="value"/>.
        /// </summary>
        /// <param name="parameterName">
        /// The name of the parameter.
        /// </param>
        /// <param name="value">
        /// The value of the parameter.
        /// </param>
        public SqlParameter(string parameterName, object value)
        {
            if (string.IsNullOrWhiteSpace(parameterName)) throw new ArgumentNullException("parameterName");
            ParameterName = parameterName;
            Value = value;
        }

        /// <summary>
        /// Gets the parameter name name of this <see cref="SqlParameter"/>.
        /// </summary>
        public string ParameterName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of this <see cref="SqlConstant"/>.
        /// </summary>
        public object Value
        {
            get;
            private set;
        }
    }
}