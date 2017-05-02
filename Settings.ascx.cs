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
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Common;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

namespace DotNetNuke.Modules.DNNRadTagCloud {

	/// <summary>
	/// The Settings class manages Module Settings
	/// </summary>
	public partial class Settings : ModuleSettingsBase {

		#region Base Method Implementations
		
		/// <summary>
		/// LoadSettings loads the settings from the Database and displays them
		/// </summary>
		public override void LoadSettings() {
			try {


				if (!Page.IsPostBack)
				{
					PopulateSettingOptions();

					// General
					if (TabModuleSettings.ContainsKey(Constants.SettingsContentType)) {
						ddlContentType.SelectedValue = TabModuleSettings[Constants.SettingsContentType].ToString();
					}
					if (TabModuleSettings.ContainsKey(Constants.SettingsSort)) {
						ddlSort.SelectedValue = TabModuleSettings[Constants.SettingsSort].ToString();
					}
					// Appearance
					if (TabModuleSettings.ContainsKey(Constants.SettingsSkin)) {
						ddlSkin.SelectedValue = TabModuleSettings[Constants.SettingsSkin].ToString();
					}
					ntxtbxWidth.Value = TabModuleSettings.ContainsKey(Constants.SettingsWidth) ? Convert.ToInt32(TabModuleSettings[Constants.SettingsWidth]) : Constants.DefaultSettingWidth;
					rblstRenderWeight.SelectedValue = TabModuleSettings.ContainsKey(Constants.SettingsRenderCount) ? TabModuleSettings[Constants.SettingsRenderCount].ToString() : Constants.DefaultSettingRenderCount;
					ntxtbxMinWeight.Value = TabModuleSettings.ContainsKey(Constants.SettingsMinCount) ? Convert.ToInt32(TabModuleSettings[Constants.SettingsMinCount]) : 1;
					dsMaxTags.Value = TabModuleSettings.ContainsKey(Constants.SettingsMaxTags) ? Convert.ToInt32(TabModuleSettings[Constants.SettingsMaxTags]) : Constants.DefaultSettingMaxTags;
					// Advanced
					rblstDistro.SelectedValue = TabModuleSettings.ContainsKey(Constants.SettingsDistro) ? TabModuleSettings[Constants.SettingsDistro].ToString() : Convert.ToInt32(Telerik.Web.UI.TagCloudDistribution.Linear).ToString();
					dsFontRange.SelectionStart = TabModuleSettings.ContainsKey(Constants.SettingsMinFontSize) ? Convert.ToDecimal(TabModuleSettings[Constants.SettingsMinFontSize]) : Constants.DefaultSettingMinFontSize;
					dsFontRange.SelectionEnd = TabModuleSettings.ContainsKey(Constants.SettingsMaxFontSize) ? Convert.ToDecimal(TabModuleSettings[Constants.SettingsMaxFontSize]) : Constants.DefaultSettingMaxFontSize;
					if (TabModuleSettings.ContainsKey(Constants.SettingsMinFontColor)) {
						cpMinColor.SelectedColor = ColorTranslator.FromHtml(TabModuleSettings[Constants.SettingsMinFontColor].ToString());
					}
					if (TabModuleSettings.ContainsKey(Constants.SettingsMaxFontColor)) {
						cpMaxColor.SelectedColor = ColorTranslator.FromHtml(TabModuleSettings[Constants.SettingsMaxFontColor].ToString());
					}

					//SetDemoTagCloud();
				}
			} catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		/// <summary>
		/// UpdateSettings saves the modified settings to the Database
		/// </summary>
		public override void UpdateSettings() {
			try {
				var objModule = new ModuleController();

				//General
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsContentType, ddlContentType.SelectedValue);
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsSort, ddlSort.SelectedValue);
				// Appearance
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsSkin, ddlSkin.SelectedValue);
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsWidth, ntxtbxWidth.Value.ToString());
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsRenderCount, rblstRenderWeight.SelectedValue);
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsMinCount, ntxtbxMinWeight.Value.ToString());
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsMaxTags, dsMaxTags.Value.ToString());
				//Advanced
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsDistro, rblstDistro.SelectedValue);
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsMinFontSize, dsFontRange.SelectionStart.ToString());
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsMaxFontSize, dsFontRange.SelectionEnd.ToString());
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsMinFontColor, ColorTranslator.ToHtml(cpMinColor.SelectedColor));
				objModule.UpdateTabModuleSetting(TabModuleId, Constants.SettingsMaxFontColor, ColorTranslator.ToHtml(cpMaxColor.SelectedColor));
			} catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// This populates the drop downs and radio buttons with localized text. 
		/// </summary>
		private void PopulateSettingOptions()
		{
			//General
			var typeController = new ContentTypeController();
			var colContentTypes = typeController.GetContentTypes();
			ddlContentType.DataSource = colContentTypes.ToList();
			ddlContentType.DataBind();
			ddlContentType.Items.Insert(0, new ListItem(Localization.GetString("NoneSpecified", LocalResourceFile), "-1"));
			ddlContentType.Items.Insert(1, new ListItem(Localization.GetString("CurrentTab", LocalResourceFile), "0"));

			ddlSort.Items.Insert(0, new ListItem(Localization.GetString("NotSorted", LocalResourceFile), Convert.ToInt32(Telerik.Web.UI.TagCloudSorting.NotSorted).ToString()));
			ddlSort.Items.Insert(1, new ListItem(Localization.GetString("AlphabeticAsc", LocalResourceFile), Convert.ToInt32(Telerik.Web.UI.TagCloudSorting.AlphabeticAsc).ToString()));
			ddlSort.Items.Insert(2, new ListItem(Localization.GetString("AlphabeticDsc", LocalResourceFile), Convert.ToInt32(Telerik.Web.UI.TagCloudSorting.AlphabeticDsc).ToString()));
			ddlSort.Items.Insert(3, new ListItem(Localization.GetString("WeightedAsc", LocalResourceFile), Convert.ToInt32(Telerik.Web.UI.TagCloudSorting.WeightedAsc).ToString()));
			ddlSort.Items.Insert(4, new ListItem(Localization.GetString("WeightedDsc", LocalResourceFile), Convert.ToInt32(Telerik.Web.UI.TagCloudSorting.WeightedDsc).ToString()));

			// Appearance
			ddlSkin.Items.Insert(0, new ListItem("Default", "Default"));
			ddlSkin.Items.Insert(1, new ListItem("Black", "Black"));
			ddlSkin.Items.Insert(2, new ListItem("Forest", "Forest"));
			ddlSkin.Items.Insert(3, new ListItem("Hay", "Hay"));
			ddlSkin.Items.Insert(4, new ListItem("Office2007", "Office2007"));
			ddlSkin.Items.Insert(5, new ListItem("Outlook", "Outlook"));
			ddlSkin.Items.Insert(6, new ListItem("Simple", "Simple"));
			ddlSkin.Items.Insert(7, new ListItem("Sitefinity", "Sitefinity"));
			ddlSkin.Items.Insert(8, new ListItem("Sunset", "Sunset"));
			ddlSkin.Items.Insert(9, new ListItem("Telerik", "Telerik"));
			ddlSkin.Items.Insert(10, new ListItem("Vista", "Vista"));
			ddlSkin.Items.Insert(11, new ListItem("Web20", "Web20"));
			ddlSkin.Items.Insert(12, new ListItem("WebBlue", "WebBlue"));
			ddlSkin.Items.Insert(13, new ListItem("Windows7", "Windows7"));

			rblstRenderWeight.Items.Insert(0, new ListItem(Localization.GetString("Yes", LocalResourceFile), "True"));
			rblstRenderWeight.Items.Insert(1, new ListItem(Localization.GetString("No", LocalResourceFile), "False"));

			//Advanced
			rblstDistro.Items.Insert(0, new ListItem(Localization.GetString("Linear", LocalResourceFile), Convert.ToInt32(Telerik.Web.UI.TagCloudDistribution.Linear).ToString()));
			rblstDistro.Items.Insert(1, new ListItem(Localization.GetString("Logarithmic", LocalResourceFile), Convert.ToInt32(Telerik.Web.UI.TagCloudDistribution.Logarithmic).ToString()));
		}

		///// <summary>
		///// Takes the currently applied settings and applies them to the demo Tag Cloud. 
		///// </summary>
		//private void SetDemoTagCloud()
		//{
		//    dtgCloud.Distribution = rblstDistro.SelectedIndex == 0 ? Telerik.Web.UI.TagCloudDistribution.Linear : Telerik.Web.UI.TagCloudDistribution.Logarithmic;
		//    dtgCloud.Sorting = (Telerik.Web.UI.TagCloudSorting)Convert.ToInt32(ddlSort.SelectedValue);

		//    dtgCloud.MinimalWeightAllowed = Convert.ToInt32(ntxtbxMinWeight.Value);
		//    dtgCloud.MaxNumberOfItems = Convert.ToInt32(dsMaxTags.Value);
		//    dtgCloud.RenderItemWeight = Convert.ToBoolean(rblstRenderWeight.SelectedValue);
		//    dtgCloud.Skin = ddlSkin.SelectedItem.Text;

		//    dtgCloud.MinFontSize = new Unit(Convert.ToDouble(dsFontRange.SelectionStart), UnitType.Pixel);
		//    dtgCloud.MaxFontSize = new Unit(Convert.ToDouble(dsFontRange.SelectionEnd), UnitType.Pixel);

		//    dtgCloud.MinColor = cpMinColor.SelectedColor;
		//    dtgCloud.MaxColor = cpMaxColor.SelectedColor;
		//}

		#endregion

		#region Event Handlers

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			jQuery.RequestUIRegistration();
		}

		#endregion

	}
}