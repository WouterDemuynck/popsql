﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlUpdate
	{
		internal class UpdateClause : SqlClause<SqlUpdate>, ISqlUpdateClause
		{
			public UpdateClause(SqlTable table) 
				: base(new SqlUpdate(table))
			{
			}

			[ExcludeFromCodeCoverage]
			public override SqlExpressionType ExpressionType
				=> Parent.ExpressionType;

			ISqlSetClause ISqlUpdateClause.Set(SqlColumn column, SqlValue value)
			{
				ISqlSetClause clause = new SetClause(this);
				clause.Set(column, value);
				return clause;
			}
		}
	}
}