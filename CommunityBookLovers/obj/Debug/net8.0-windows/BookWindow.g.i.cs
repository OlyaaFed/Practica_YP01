﻿#pragma checksum "..\..\..\BookWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78B2C1ACF74CF3519616E509DA6B7CDF2CD4D118"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CommunityBookLovers;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace CommunityBookLovers {
    
    
    /// <summary>
    /// BookWindow
    /// </summary>
    public partial class BookWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBookButton;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstBooks;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBookTitle;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBookAuthor;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBookGenre;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBookYear;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBookPages;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBookDescription;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\BookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image txtBookImage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CommunityBookLovers;V1.0.0.0;component/bookwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\BookWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\BookWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_AboutMe_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 12 "..\..\..\BookWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_MyShelf_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 13 "..\..\..\BookWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Books_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 14 "..\..\..\BookWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Friends_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 15 "..\..\..\BookWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Review_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AddBookButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\BookWindow.xaml"
            this.AddBookButton.Click += new System.Windows.RoutedEventHandler(this.AddBookButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 19 "..\..\..\BookWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LogoutButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lstBooks = ((System.Windows.Controls.ListBox)(target));
            
            #line 26 "..\..\..\BookWindow.xaml"
            this.lstBooks.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LstBooks_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.txtBookTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.txtBookAuthor = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.txtBookGenre = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.txtBookYear = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.txtBookPages = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.txtBookDescription = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            this.txtBookImage = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

