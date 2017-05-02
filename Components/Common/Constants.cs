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

namespace DotNetNuke.Modules.DNNRadTagCloud.Components.Common {

	/// <summary>
	/// A centralized point for storing constants used throughout the module. An example of constants located here are the names for module settings.
	/// </summary>
	public class Constants {

		internal const string SettingsDistro = "RTC_Settings_Distro";
		internal const string SettingsSort = "RTC_Settings_Sort";
		internal const string SettingsMaxTags = "RTC_Settings_MaxTags";
		internal const string SettingsMinCount = "RTC_Settings_MinCount";
		internal const string SettingsRenderCount = "RTC_Settings_RenderCount";
		internal const string SettingsSkin = "RTC_Settings_Skin";
		internal const string SettingsMinFontSize = "RTC_Settings_MinFontSize";
		internal const string SettingsMaxFontSize = "RTC_Settings_MaxFontSize";
		internal const string SettingsMinFontColor = "RTC_Settings_MinFontColor";
		internal const string SettingsMaxFontColor = "RTC_Settings_MaxFontColor";
		internal const string SettingsWidth = "RTC_Settings_Width";
		internal const string SettingsContentType = "RTC_Settings_ContentType";
		internal const string CacheKey = "DotNetNuke_RadTagCloud-";

		internal const string DefaultSettingSkin = @"Default";
		internal const int DefaultSettingWidth = 250;
		internal const string DefaultSettingRenderCount = "True";
		internal const int DefaultSettingMaxTags = 25;
		internal const int DefaultSettingMinFontSize = 14;
		internal const int DefaultSettingMaxFontSize = 24;
		
	}
}