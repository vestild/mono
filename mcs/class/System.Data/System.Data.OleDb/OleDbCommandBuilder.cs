//
// System.Data.OleDb.OleDbCommandBuilder
//
// Author:
//   Rodrigo Moya (rodrigo@ximian.com)
//   Tim Coleman (tim@timcoleman.com)
//
// Copyright (C) Rodrigo Moya, 2002
// Copyright (C) Tim Coleman, 2002
//

//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>
	/// Provides a means of automatically generating single-table commands used to reconcile changes made to a DataSet with the associated database. This class cannot be inherited.
	/// </summary>
	public sealed class OleDbCommandBuilder :
		DbCommandBuilder
	{
		#region Fields

		OleDbDataAdapter adapter;
		#endregion // Fields

		#region Constructors
		
		public OleDbCommandBuilder ()
		{
		}

		public OleDbCommandBuilder (OleDbDataAdapter adapter) 
			: this ()
		{
			this.adapter = adapter;
		}

		#endregion // Constructors

		#region Properties

		[DefaultValue (null)]
		public new OleDbDataAdapter DataAdapter {
			get {
				return adapter;
			}
			set {
				adapter = value;
			}
		}


		#endregion // Properties

		#region Methods

		protected override void ApplyParameterInfo (DbParameter parameter,
				                                    DataRow datarow,
				                                    StatementType statementType,
				                                    bool whereClause)
		{
			OleDbParameter p = (OleDbParameter) parameter;
			p.Size = int.Parse (datarow ["ColumnSize"].ToString ());
			if (datarow ["NumericPrecision"] != DBNull.Value) {
				p.Precision = byte.Parse (datarow ["NumericPrecision"].ToString ());
			}
			if (datarow ["NumericScale"] != DBNull.Value) {
				p.Scale = byte.Parse (datarow ["NumericScale"].ToString ());
			}
			p.DbType = (DbType) datarow ["ProviderType"];
		}

		[MonoTODO]
		public static void DeriveParameters (OleDbCommand command)
		{
			if (command.CommandType != CommandType.StoredProcedure) {
				throw new InvalidOperationException ("You can perform this " +
						                             "operation only on CommandTye" + 
						                             " StoredProcedure");
			}
			// FIXME: Retrive info from server
			throw new NotImplementedException ();
		}


		[MonoTODO]
		public new OleDbCommand GetDeleteCommand ()
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public new OleDbCommand GetDeleteCommand (bool useColumnsForParameterNames)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public new OleDbCommand GetInsertCommand ()
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public new OleDbCommand GetInsertCommand (bool useColumnsForParameterNames)
		{
			throw new NotImplementedException ();
		}

		protected override string GetParameterName (int parameterOrdinal)
		{
			return String.Format("@p{0}", parameterOrdinal);
		}

		protected override string GetParameterName (string parameterName)
		{
			return String.Format("@{0}", parameterName);                       
		}
                
		protected override string GetParameterPlaceholder (int parameterOrdinal)
		{
			return GetParameterName (parameterOrdinal);
		}
                

		[MonoTODO]
		public new OleDbCommand GetUpdateCommand ()
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public new OleDbCommand GetUpdateCommand (bool useColumnsForParameterNames)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return base.QuoteIdentifier (unquotedIdentifier);
		}

		[MonoTODO]
		public string QuoteIdentifier(string unquotedIdentifier, OleDbConnection connection)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return base.UnquoteIdentifier (quotedIdentifier);
		}

		[MonoTODO]
		public string UnquoteIdentifier(string quotedIdentifier, OleDbConnection connection)
		{
			throw new NotImplementedException ();
		}

		#endregion // Methods
	}
}
