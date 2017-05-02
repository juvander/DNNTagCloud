// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2011
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace DotNetNuke.Modules.DNNRadTagCloud.Providers.Data.SqlDataProvider {

	/// <summary>
	/// SQL Server implementation (ie. concrete provider) of the abstract DataProvider class
	/// </summary>
	public class SqlDataProvider : IDataProvider {

		#region Private Members

		private const string ModuleQualifier = "RTC_";
		private string _connectionString = String.Empty;
		private string _databaseOwner = String.Empty;
		private string _objectQualifier = String.Empty;

		#endregion

		#region Properties

		public string ConnectionString {
			get {
				return string.IsNullOrEmpty(_connectionString) ? DotNetNuke.Data.DataProvider.Instance().ConnectionString : _connectionString;
			}
			set { _connectionString = value; }
		}

		public string DatabaseOwner {
			get {
				return string.IsNullOrEmpty(_databaseOwner) ? DotNetNuke.Data.DataProvider.Instance().DatabaseOwner : _databaseOwner;
			}
			set { _databaseOwner = value; }
		}

		public string ObjectQualifier {
			get {
				return string.IsNullOrEmpty(_objectQualifier) ? DotNetNuke.Data.DataProvider.Instance().ObjectQualifier : _objectQualifier;
			}
			set { _objectQualifier = value; }
		}

		#endregion

		#region Private Methods

		private string GetFullyQualifiedName(string name) {
			return DatabaseOwner + ObjectQualifier + ModuleQualifier + name;
		}

		#endregion

		#region Public Methods

		public IDataReader GetTags(int portalID) {
			return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Tags_Get"), portalID);
		}

		public IDataReader GetTagsByContentType(int portalID, int contentTypeID) {
			return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Tags_GetByContentType"), portalID, contentTypeID);
		}

		public IDataReader GetTagsByTab(int portalID, int tabID) {
			return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Tags_GetByTab"), portalID, tabID);
		}

		#endregion

	}
}