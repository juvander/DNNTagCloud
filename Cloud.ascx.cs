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
using DotNetNuke.Modules.DNNRadTagCloud.Components.Common;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Entities;
using DotNetNuke.Web.Mvp;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Models;
using Telerik.Web.UI;
using WebFormsMvp;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Views;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Presenters;

namespace DotNetNuke.Modules.DNNRadTagCloud {

	/// <summary>
	/// 
	/// </summary>
	[PresenterBinding(typeof(CloudPresenter))]
	public partial class Cloud : ModuleView<CloudModel>, ICloudView
	{

		public event EventHandler<TagCloudEventArgs<TermInfo, RadTagCloudItem>> ItemDataBound;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RtcCloudItemDataBound(object sender, RadTagCloudEventArgs e)
		{
			var term = (TermInfo)e.Item.DataItem;
			var cloudLink = e.Item;

			ItemDataBound(this, new TagCloudEventArgs<TermInfo, RadTagCloudItem>(term, cloudLink));
		}

		/// <summary>
		/// 
		/// </summary>
		public void Refresh()
		{		
			rtgCloud.Sorting = Model.Sort;
			rtgCloud.Skin = Model.CloudSkin;
			rtgCloud.Width = Model.Width;
			rtgCloud.RenderItemWeight = Model.DisplayMatchCount;
			rtgCloud.MinimalWeightAllowed = Model.MinMatchCount;
			rtgCloud.MaxNumberOfItems = Model.MaxTags;
			rtgCloud.Distribution = Model.Distro;
			rtgCloud.MinFontSize = Model.MinFontSize;
			rtgCloud.MaxFontSize = Model.MaxFontSize;
			rtgCloud.MinColor = Model.MinFontColor;
			rtgCloud.MaxColor = Model.MaxFontColor;

			rtgCloud.DataSource = Model.ColTags;
			rtgCloud.DataBind();

			//var user = UserController.GetCurrentUserInfo();
			//var imgURL = ModuleContext.PortalSettings.HomeDirectory +  user.Profile.PhotoURL;

			//imgUser.ImageUrl = imgURL;
		}

	}
}