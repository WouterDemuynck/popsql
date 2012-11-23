using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL expression tree node that can be used as a value.
    /// </summary>
    public abstract class SqlValue : SqlToken
    {
        /// <summary>
        /// Implicitly converts an <see cref="Int16"/> to a <see cref="SqlConstant"/> instance.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="SqlConstant"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SqlConstant"/> instance representing the specified value.
        /// </returns>
        public static implicit operator SqlValue(short value)
        {
            return new SqlConstant(value);
        }

        /// <summary>
        /// Implicitly converts an <see cref="Int32"/> to a <see cref="SqlConstant"/> instance.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="SqlConstant"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SqlConstant"/> instance representing the specified value.
        /// </returns>
        public static implicit operator SqlValue(int value)
        {
            return new SqlConstant(value);
        }

        /// <summary>
        /// Implicitly converts an <see cref="Int64"/> to a <see cref="SqlConstant"/> instance.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="SqlConstant"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SqlConstant"/> instance representing the specified value.
        /// </returns>
        public static implicit operator SqlValue(long value)
        {
            return new SqlConstant(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="String"/> to a <see cref="SqlConstant"/> instance.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="SqlConstant"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SqlConstant"/> instance representing the specified value.
        /// </returns>
        public static implicit operator SqlValue(string value)
        {
            return new SqlConstant(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="Guid"/> to a <see cref="SqlConstant"/> instance.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="SqlConstant"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SqlConstant"/> instance representing the specified value.
        /// </returns>
        public static implicit operator SqlValue(Guid value)
        {
            return new SqlConstant(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="Single"/> to a <see cref="SqlConstant"/> instance.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="SqlConstant"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SqlConstant"/> instance representing the specified value.
        /// </returns>
        public static implicit operator SqlValue(float value)
        {
            return new SqlConstant(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="Double"/> to a <see cref="SqlConstant"/> instance.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="SqlConstant"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SqlConstant"/> instance representing the specified value.
        /// </returns>
        public static implicit operator SqlValue(double value)
        {
            return new SqlConstant(value);
        }
    }
}
