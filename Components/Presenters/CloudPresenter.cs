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

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Entities;
using DotNetNuke.Modules.DNNRadTagCloud.Providers.Data.SqlDataProvider;
using DotNetNuke.Web.Mvp;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Controllers;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Models;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Views;
using System;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Common;
using Telerik.Web.UI;

namespace DotNetNuke.Modules.DNNRadTagCloud.Components.Presenters {

	/// <summary>
	/// 
	/// </summary>
	public class CloudPresenter : ModulePresenter<ICloudView, CloudModel> {

		#region Private Members

		protected ITagCloudController Controller { get; private set; }

		public new Dictionary<string, string> Settings { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		public CloudPresenter(ICloudView view)
			: this(view, new TagCloudController(new SqlDataProvider())) {
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		/// <param name="controller"></param>
		public CloudPresenter(ICloudView view, ITagCloudController controller)
			: base(view) {
			if (view == null) {
				throw new ArgumentException(@"View is nothing.", "view");
			}

			if (controller == null) {
				throw new ArgumentException(@"Controller is nothing.", "controller");
			}

			Controller = controller;
		}

		#endregion

		#region Events

		/// <summary>
		/// 
		/// </summary>
		protected override void OnInit() {
			base.OnInit();

			// we need to use the content filter here to determine how we populate the tag collection.
			var contentTypeID = -1;
			if (Settings.ContainsKey(Constants.SettingsContentType)) {
				contentTypeID = Convert.ToInt32(Settings[Constants.SettingsContentType]);
			}

			if (contentTypeID > 0)
			{
				//filter by content type
				var colTags = Controller.GetTagsByContentType(ModuleContext.PortalId, contentTypeID);
				View.Model.ColTags = colTags;
			}
			else
			{
				if (contentTypeID == 0)
				{
					// Show tags used on the current tab
					var colTags = Controller.GetTagsByTab(ModuleContext.PortalId, ModuleContext.TabId);
					View.Model.ColTags = colTags;
				}
				else
				{
					// Show all tags in the portal
					var colTags = Controller.GetTags(ModuleContext.PortalId);
					View.Model.ColTags = colTags;
				}
			}

			View.Model.Sort = Settings.ContainsKey(Constants.SettingsSort)
			                  	? (TagCloudSorting) Convert.ToInt32(Settings[Constants.SettingsSort])
			                  	: TagCloudSorting.NotSorted;
			View.Model.CloudSkin = Settings.ContainsKey(Constants.SettingsSkin) ? Settings[Constants.SettingsSkin] : Constants.DefaultSettingSkin;
			View.Model.Width = Settings.ContainsKey(Constants.SettingsWidth) ? Convert.ToInt32(Settings[Constants.SettingsWidth]) : Constants.DefaultSettingWidth; 
			View.Model.DisplayMatchCount = Settings.ContainsKey(Constants.SettingsRenderCount) ? Convert.ToBoolean(Settings[Constants.SettingsRenderCount]) : Convert.ToBoolean(Constants.DefaultSettingRenderCount);
			if (Settings.ContainsKey(Constants.SettingsMinCount)) {
				View.Model.MinMatchCount = Convert.ToInt32(Settings[Constants.SettingsMinCount]);
			}
			View.Model.MaxTags = Settings.ContainsKey(Constants.SettingsMaxTags) ? Convert.ToInt32(Settings[Constants.SettingsMaxTags]) : Constants.DefaultSettingMaxTags;
			View.Model.Distro = Settings.ContainsKey(Constants.SettingsDistro)
			                    	? (TagCloudDistribution) Convert.ToInt32(Settings[Constants.SettingsDistro])
			                    	: TagCloudDistribution.Linear;
			View.Model.MinFontSize = Settings.ContainsKey(Constants.SettingsMinFontSize) ? Convert.ToInt32(Settings[Constants.SettingsMinFontSize]) : Constants.DefaultSettingMinFontSize;
			View.Model.MaxFontSize = Settings.ContainsKey(Constants.SettingsMaxFontSize) ? Convert.ToInt32(Settings[Constants.SettingsMaxFontSize]) : Constants.DefaultSettingMaxFontSize;
			if (Settings.ContainsKey(Constants.SettingsMinFontColor)) {
				View.Model.MinFontColor = ColorTranslator.FromHtml(Settings[Constants.SettingsMinFontColor]);
			}
			if (Settings.ContainsKey(Constants.SettingsMaxFontColor)) {
				View.Model.MaxFontColor = ColorTranslator.FromHtml(Settings[Constants.SettingsMaxFontColor]);
			}

			View.ItemDataBound += ItemDataBound;
			View.Refresh();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ItemDataBound(object sender, TagCloudEventArgs<TermInfo, RadTagCloudItem> e) {
			var objModules = new ModuleController();
			var searchModule = objModules.GetModuleByDefinition(ModuleContext.PortalSettings.PortalId, "Search Results");

			if (searchModule == null) return;
			var searchTabId = searchModule.TabID;
			e.CloudLink.NavigateUrl = Globals.NavigateURL(searchTabId, "", "Tag=" + Server.UrlEncode(e.ObjTerm.TermName));
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void LoadFromContext() {
			base.LoadFromContext();
			if (ModuleContext == null) return;
			Settings = new Dictionary<string, string>();
			foreach (DictionaryEntry item in ModuleContext.Settings) {
				Settings.Add(item.Key.ToString(), item.Value.ToString());
			}
		}

		#endregion

	}
}