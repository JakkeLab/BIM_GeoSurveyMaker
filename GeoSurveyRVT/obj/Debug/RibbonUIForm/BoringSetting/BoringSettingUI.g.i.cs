﻿#pragma checksum "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4056381F5BFBBB7664CC4F3C7D80DE3A8381AF808C2F1B74E7761B41766CFE47"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GeoSurveyRVT.RibbonUIForm.BoringSetting {
    
    
    /// <summary>
    /// BoringSettingUI
    /// </summary>
    public partial class BoringSettingUI : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 1 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GeoSurveyRVT.RibbonUIForm.BoringSetting.BoringSettingUI wdBoringSetting;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gdSetting;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTitle;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgBoringSettings;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btCancel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btOk;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GeoSurveyRVT;component/ribbonuiform/boringsetting/boringsettingui.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.wdBoringSetting = ((GeoSurveyRVT.RibbonUIForm.BoringSetting.BoringSettingUI)(target));
            
            #line 7 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
            this.wdBoringSetting.Closing += new System.ComponentModel.CancelEventHandler(this.IsClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.gdSetting = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.lbTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.dgBoringSettings = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.btCancel = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
            this.btCancel.Click += new System.Windows.RoutedEventHandler(this.btCancel_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btOk = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
            this.btOk.Click += new System.Windows.RoutedEventHandler(this.btOk_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 16 "..\..\..\..\RibbonUIForm\BoringSetting\BoringSettingUI.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSetColor_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

