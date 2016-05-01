﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelToObject
{
	public class Table
	{
		public string Name { get; private set; }

		SheetData mSheet;
		int mRowStart;
		int mColStart;

		int mColumnCount;
		public int Columns { get { return mColumnCount; } }

		int mRowCount;
		public int Rows { get { return mRowCount; } }

		public Table(string name, SheetData sheet, int row, int col)
		{
			Name = name;
			mSheet = sheet;
			mRowStart = row;
			mColStart = col;

			CalcTableSize();
		}

		public string this[int row, int col]
		{
			get
			{
				return mSheet[mRowStart + row, mColStart + col];
			}
		}

		void CalcTableSize()
		{
			int maxColumns = mSheet.Columns - mColStart;
			int maxRows = mSheet.Rows - mRowStart;

			for( mColumnCount = 0; mColumnCount < maxColumns; mColumnCount++ )
				if( GetColumn(mColumnCount) == null )
					break;

			if( mColumnCount == 0 )
				return;

			for( mRowCount = 0; mRowCount < maxRows; mRowCount++ )
			{
				bool emptyRow = true;
				for( int col = 0; col < mColumnCount; col++ )
				{
					if( GetValue(mRowCount, col) != null )
					{
						emptyRow = false;
						break;
					}
				}

				if( emptyRow )
					break;
			}
		}

		public string GetColumn(int index)
		{
			return this[1, index];
		}

		public string GetValue(int row, int col)
		{
			return this[row + 2, col];
		}

		public int FindColumn(string name)
		{
			for( int i = 0; i < mSheet.Columns; i++ )
			{
				string n = GetColumn(i);
				if( n == name )
					return i;

				if( n == null )
					break;
			}

			return -1;
		}
	}
}