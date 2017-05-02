<%@ Control Language="C#" AutoEventWireup="true" Inherits="DotNetNuke.Modules.DNNRadTagCloud.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<%@ Register TagPrefix="dnnweb" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<div class="dnnForm dnnTagCloudSettings dnnClear" id="dnnTagCloudSettings">
	<div class="dnnFormExpandContent"><a href=""><%=Localization.GetString("ExpandAll", Localization.SharedResourceFile)%></a></div>
	<h2 id="dnnSitePanel-GeneralTC" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("TabGeneral")%></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblContentType" runat="server" controlname="ddlContentType" suffix=":" />
			<asp:DropDownList ID="ddlContentType" runat="server" DataTextField="ContentType" DataValueField="ContentTypeID"  AutoPostBack="false" />
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblSort" runat="server" controlname="ddlSort" suffix=":" />
			<asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="false" />
		</div>
	</fieldset>
	<h2 id="dnnSitePanel-AppearanceTC" class="dnnFormSectionHead"><a href="" class=""><%=LocalizeString("TabAppearance")%></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblSkin" runat="server" controlname="ddlSkin" suffix=":" />
			<asp:DropDownList ID="ddlSkin" runat="server" AutoPostBack="false" />
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblWidth" runat="server" controlname="ntxtbxWidth" suffix=":" />
			<dnnweb:DnnNumericTextBox NumberFormat-DecimalDigits="0" ID="ntxtbxWidth" runat="server" ShowSpinButtons="true" MinValue="100" MaxValue="1200" CssClass="dnnFormRequired" />
			<asp:RequiredFieldValidator ID="valWidthRequired" runat="server" CssClass="dnnFormMessage dnnFormError" resourcekey="valWidthRequired" ControlToValidate="ntxtbxWidth" Display="Dynamic" />
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblRenderWeight" runat="server" controlname="rblstRenderWeight" suffix=":" />
			<asp:RadioButtonList ID="rblstRenderWeight" runat="server" AutoPostBack="false" RepeatDirection="Horizontal" CssClass="dnnFormRadioButtons" />
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblMinWeight" runat="server" controlname="ntxtbxMinWeight" suffix=":" />
			<dnnweb:DnnNumericTextBox ID="ntxtbxMinWeight" runat="server" MinValue="1" MaxValue="1000" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" AutoPostBack="false" />
				<asp:RequiredFieldValidator ID="valMinWeightRequired" runat="server" CssClass="dnnFormMessage dnnFormError" resourcekey="valMinWeightRequired" ControlToValidate="ntxtbxMinWeight" />
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblMaxTags" runat="server" controlname="dsMaxTags" suffix=":" />
			<div class="dnnLeft">
				<dnnweb:DnnSlider ID="dsMaxTags" runat="server" MinimumValue="0" MaximumValue="100" AutoPostBack="false" SmallChange="5" LargeChange="10" ItemType="tick" Height="70px" Width="350px" AnimationDuration="500" />
			</div>
		</div>
	</fieldset>
	<h2 id="dnnSitePanel-AdvancedTC" class="dnnFormSectionHead"><a href="" class=""><%=LocalizeString("tabAdvanced")%></a></h2>
	<fieldset>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblDistribution" runat="server" controlname="rblstDistro" suffix=":" />
			<asp:RadioButtonList ID="rblstDistro" runat="server" AutoPostBack="false" RepeatDirection="Horizontal" CssClass="dnnFormRadioButtons" />
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblFontRange" runat="server" controlname="dsFontRange" suffix=":" />
			<div class="dnnLeft">
				<dnnweb:DnnSlider ID="dsFontRange" runat="server" AutoPostBack="false" Height="70px" Width="350px" SelectionStart="10" SelectionEnd="50" MinimumValue="10" ItemType="Tick" MaximumValue="50" LargeChange="10" SmallChange="1" IsSelectionRangeEnabled="true" AnimationDuration="500"  />
			</div>
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblMinColor" runat="server" controlname="cpMinColor" suffix=":" />
			<div class="dnnLeft">
				<dnnweb:DnnColorPicker runat="server" ID="cpMinColor" AutoPostBack="false" Preset="Standard" />
			</div>
		</div>
		<div class="dnnFormItem">
			<dnn:label id="dnnlblMaxColor" runat="server" controlname="cpMaxColor" suffix=":" />
			<div class="dnnLeft">
				<dnnweb:DnnColorPicker runat="server" ID="cpMaxColor" AutoPostBack="false" Preset="Standard" />
			</div>
		</div>  
	</fieldset>
</div>
<script language="javascript" type="text/javascript">
	/*globals jQuery, window, Sys */
	(function ($, Sys) {
		function setUpRadTagCloudSettings() {
			$('#dnnTagCloudSettings .dnnFormExpandContent a').dnnExpandAll({
				expandText: '<%=Localization.GetString("ExpandAll", Localization.SharedResourceFile)%>',
				collapseText: '<%=Localization.GetString("CollapseAll", Localization.SharedResourceFile)%>',
				targetArea: '#dnnTagCloudSettings'
			});
		}

		$(document).ready(function () {
			setUpRadTagCloudSettings();
			Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
				setUpRadTagCloudSettings();
			});
		});
	} (jQuery, window.Sys));
</script>