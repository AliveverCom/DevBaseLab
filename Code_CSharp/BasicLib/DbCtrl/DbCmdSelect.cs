/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2007-02-26  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;
using System.Collections;
using Alivever.Com.DevBasic.BasicLib.ToolsCtrl;


namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// CDbCmdSelect 的摘要说明。
	/// </summary>
	public class CDbCmdSelect// : CDbCmdNode
	{
		/// <summary>
		/// Vector(CDbCmdCondition)
		/// </summary>
		private ArrayList m_Conditions = null;

		/// <summary>
		/// vector(string)
		/// </summary>
		private ArrayList m_Tables = null ; 

		/// <summary>
		/// vector(CDbColumn)
		/// </summary>
		private ArrayList m_OrderBys = null;

		private ArrayList m_DspColumns = null;

		public CDbCmdSelect()
		{
		}

		public ArrayList Tables
		{
			set
			{
				m_Tables = value;
			}

			get
			{
				ArrayList rstList = GetIndirectTableNames();
				
				/*
				if ( rstList == null )
				{
					if ( m_Tables == null )
						return null;
					else 
						return (ArrayList)m_Tables.Clone();
				}
				else
				{
					if ( m_Tables == null )
						return rstList;
					else 
					{
						rstList.AddRange( rstList);
						return rstList;
					}

				}//if : return branchs
				*/
				if ( m_Tables == null )
					return rstList;
				else 
				{
					rstList.AddRange( rstList);
					return rstList;
				}
			}//get
		}//ArrayList Tables

		/// <summary>
		/// search indirect tables' name from "cloumn" "where" "order by",etc
		/// </summary>
		/// <returns> If not found, return null </returns>
		private ArrayList GetIndirectTableNames()
		{
			ArrayList rstList = new ArrayList();
			int nRst = 0;

			nRst += GetIndirectTableNamesFromDspColumns( ref rstList );
			nRst += GetIndirectTableNamesFromConditions( ref rstList );

			//if ( nRst == 0 )
			//	return null;
			//else 
			return rstList;
		}

		private int GetIndirectTableNamesFromDspColumns( ref ArrayList _rstList )
		{
			return _rstList.Count;
		}

		private int GetIndirectTableNamesFromConditions( ref ArrayList _rstList )
		{
			return _rstList.Count;
		}

		public /*override*/ bool InitFromString(string _initStr) 
		{
			_initStr.ToLower();
			_initStr.Substring( _initStr.IndexOf("select") );
			_initStr.Trim();

			if ( _initStr.Length < 15 )
				return false;

			//protect  attributes
			bool bInitOK =false;
			ArrayList _Conditions = m_Conditions;
			ArrayList _Tables = m_Tables ; 
			ArrayList _OrderBys = m_OrderBys;
			ArrayList _DspColumns = m_DspColumns;

			///////////// demo for get table name only ///////////
			///
			_initStr.Substring( _initStr.IndexOf("from") );
			CStringHelper strHelper = new CStringHelper();
			char[] _Seperaters = {',' , '\t'};
			strHelper.m_Seperaters = _Seperaters;
			m_Tables = strHelper.ParseNameList( _initStr );

			if ( m_Tables != null && m_Tables.Count != 0 )
				bInitOK = true;

			///////////// demo for get table name only ///////////
			
			if ( !bInitOK )
			{
				 m_Conditions = _Conditions;
				 m_Tables     = _Tables ; 
				 m_OrderBys   = _OrderBys;
				 m_DspColumns = _DspColumns;
			}
			return false;
		}

		public /*override*/ string ToInitString()
		{
			if ( m_DspColumns == null ||  m_DspColumns.Count == 0 )
				return "";

			//string DspColStr = "";

			return "select ";

		}

	}//class CDbCmdSelect
}//namespace
