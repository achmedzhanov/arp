﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arp.Databases.Meta.Providers.MSSQL2005
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Tables> Tables
		{
			get
			{
				return this.GetTable<Tables>();
			}
		}
	}
	
	[Table(Name="INFORMATION_SCHEMA.Tables")]
	public partial class Tables
	{
		
		private string _TableCatalog;
		
		private string _TableSchema;
		
		private string _TableName;
		
		private string _TableType;
		
		public Tables()
		{
		}
		
		[Column(Name="TABLE_CATALOG", Storage="_TableCatalog", CanBeNull=false)]
		public string TableCatalog
		{
			get
			{
				return this._TableCatalog;
			}
			set
			{
				if ((this._TableCatalog != value))
				{
					this._TableCatalog = value;
				}
			}
		}
		
		[Column(Name="Table_Schema", Storage="_TableSchema", CanBeNull=false)]
		public string TableSchema
		{
			get
			{
				return this._TableSchema;
			}
			set
			{
				if ((this._TableSchema != value))
				{
					this._TableSchema = value;
				}
			}
		}
		
		[Column(Name="Table_Name", Storage="_TableName", CanBeNull=false)]
		public string TableName
		{
			get
			{
				return this._TableName;
			}
			set
			{
				if ((this._TableName != value))
				{
					this._TableName = value;
				}
			}
		}
		
		[Column(Name="Table_Type", Storage="_TableType", CanBeNull=false)]
		public string TableType
		{
			get
			{
				return this._TableType;
			}
			set
			{
				if ((this._TableType != value))
				{
					this._TableType = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
