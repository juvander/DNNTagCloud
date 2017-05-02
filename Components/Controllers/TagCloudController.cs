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

using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Entities;
using DotNetNuke.Modules.DNNRadTagCloud.Providers.Data;
using DotNetNuke.Modules.DNNRadTagCloud.Providers.Data.SqlDataProvider;

namespace DotNetNuke.Modules.DNNRadTagCloud.Components.Controllers {

	/// <summary>
	/// 
	/// </summary>
	public class TagCloudController : ITagCloudController {

		private readonly IDataProvider _dataProvider;

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public TagCloudController() {
			_dataProvider = ComponentModel.ComponentFactory.GetComponent<IDataProvider>();
			if (_dataProvider != null) return;

			// get the provider configuration based on the type
			var defaultprovider = Data.DataProvider.Instance().DefaultProviderName;
			const string dataProviderNamespace = "DotNetNuke.Modules.DNNRadTagCloud.Providers.Data";

			if (defaultprovider == "SqlDataProvider") {
				_dataProvider = new SqlDataProvider();
			} else {
				var providerType = dataProviderNamespace + "." + defaultprovider;
				_dataProvider = (IDataProvider)Framework.Reflection.CreateObject(providerType, providerType, true);
			}

			ComponentModel.ComponentFactory.RegisterComponentInstance<IDataProvider>(_dataProvider);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataProvider"></param>
		public TagCloudController(IDataProvider dataProvider) {
			DotNetNuke.Common.Requires.NotNull("dataProvider", dataProvider);
			_dataProvider = dataProvider;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="portalID"></param>
		/// <returns></returns>
		List<TermInfo> ITagCloudController.GetTags(int portalID) {
			//DotNetNuke.Common.Requires.PropertyNotNegative("portalID", "", portalID);
			return CBO.FillCollection<TermInfo>(_dataProvider.GetTags(portalID));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="portalID"></param>
		/// <param name="contentTypeID"></param>
		/// <returns></returns>
		List<TermInfo> ITagCloudController.GetTagsByContentType(int portalID, int contentTypeID) {
			//DotNetNuke.Common.Requires.PropertyNotNegative("portalID", "", portalID);
			//DotNetNuke.Common.Requires.PropertyNotNegative("contentTypeID", "", contentTypeID);
			return CBO.FillCollection<TermInfo>(_dataProvider.GetTagsByContentType(portalID, contentTypeID));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="portalID"></param>
		/// <param name="tabID"></param>
		/// <returns></returns>
		List<TermInfo> ITagCloudController.GetTagsByTab(int portalID, int tabID) {
			//DotNetNuke.Common.Requires.PropertyNotNegative("portalID", "", portalID);
			//DotNetNuke.Common.Requires.PropertyNotNegative("tabID", "", tabID);
			return CBO.FillCollection<TermInfo>(_dataProvider.GetTagsByTab(portalID, tabID));
		}

		#endregion

	}
}