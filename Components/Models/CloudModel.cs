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
using System.Web.UI.WebControls;
using DotNetNuke.Modules.DNNRadTagCloud.Components.Entities;

namespace DotNetNuke.Modules.DNNRadTagCloud.Components.Models {

	/// <summary>
	/// 
	/// </summary>
	public class CloudModel {

		public List<TermInfo> ColTags { get; set; }
		public Telerik.Web.UI.TagCloudSorting Sort { get; set; }
		public string CloudSkin { get; set; }
		public Unit Width { get; set; }
		public bool DisplayMatchCount { get; set; }
		public int MinMatchCount { get; set; }
		public int MaxTags { get; set; }
		public Telerik.Web.UI.TagCloudDistribution Distro { get; set; }
		public Unit MinFontSize { get; set; }
		public Unit MaxFontSize { get; set; }
		public System.Drawing.Color MinFontColor { get; set; }
		public System.Drawing.Color MaxFontColor { get; set; }

	}
}