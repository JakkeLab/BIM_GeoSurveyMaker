﻿#pragma checksum "..\..\..\DockablePaneUI\ShowBoringTable.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E1B1642174B45C378F3402BACF98E5BB9A74F37FEEDDB80ACC7319E7C9009298"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GeoSurveyRVT.DockablePaneUI;
using GeoSurveyRVT.DockablePaneUI.SingleBoring;
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


namespace GeoSurveyRVT.DockablePaneUI {
    
    
    /// <summary>
    /// ShowBoringTable
    /// </summary>
    public partial class ShowBoringTable : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 15 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTitle;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgBorings;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btBoringDetail;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GeoSurveyRVT.DockablePaneUI.SingleBoring.SingleBoringDetail dgBoringDetail;
        
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
            System.Uri resourceLocater = new System.Uri("/GeoSurveyRVT;component/dockablepaneui/showboringtable.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.lbTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.dgBorings = ((System.Windows.Controls.DataGrid)(target));
            
            #line 16 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            this.dgBorings.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgBorings_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            this.dgBorings.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.dgBorings_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            this.dgBorings.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.dgBorings_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btBoringDetail = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            this.btBoringDetail.Click += new System.Windows.RoutedEventHandler(this.btnSetPosition_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dgBoringDetail = ((GeoSurveyRVT.DockablePaneUI.SingleBoring.SingleBoringDetail)(target));
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
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 3:
            
            #line 21 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.HeaderCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.HeaderCheckBox_Unchecked);
            
            #line default
            #line hidden
            break;
            case 4:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseLeftButtonDownEvent;
            
            #line 26 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.DataGridCell_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 5:
            
            #line 36 "..\..\..\DockablePaneUI\ShowBoringTable.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSetPosition_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

