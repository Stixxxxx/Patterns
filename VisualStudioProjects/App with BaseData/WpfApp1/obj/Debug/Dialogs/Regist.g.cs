﻿#pragma checksum "..\..\..\Dialogs\Regist.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B83413344E9EDB9E24A1DAAA9DA4982F"
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
using WpfApp1.Dialogs;


namespace WpfApp1.Dialogs {
    
    
    /// <summary>
    /// Regist
    /// </summary>
    public partial class Regist : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Dialogs\Regist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblLogin;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Dialogs\Regist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxLogin;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Dialogs\Regist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblPass;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Dialogs\Regist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pbPass;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Dialogs\Regist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Dialogs\Regist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/dialogs/regist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dialogs\Regist.xaml"
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
            this.tblLogin = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.tbxLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tblPass = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.pbPass = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Dialogs\Regist.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.btnOk_Click_Ok);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Dialogs\Regist.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

