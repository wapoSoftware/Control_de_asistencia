﻿#pragma checksum "C:\Assistence_Control\Assistence_Control\Views\Areas\abcAreas.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C9442669EE66324B173A8EA7370B2D6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assistence_Control.Views.Areas
{
    partial class abcAreas : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.DataGrid = (global::MyToolkit.Controls.DataGrid)(target);
                    #line 28 "..\..\..\..\Views\Areas\abcAreas.xaml"
                    ((global::MyToolkit.Controls.DataGrid)this.DataGrid).SelectionChanged += this.DataGrid_SelectionChanged;
                    #line default
                }
                break;
            case 2:
                {
                    this.gridAgregar = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.prLoading = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 4:
                {
                    this.tbClave = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.tbNombreArea = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 70 "..\..\..\..\Views\Areas\abcAreas.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.tbNombreArea).KeyUp += this.tbNombreArea_KeyUp;
                    #line default
                }
                break;
            case 6:
                {
                    this.btnGuardar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 71 "..\..\..\..\Views\Areas\abcAreas.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnGuardar).Click += this.btnGuardar_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.btnCancelar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 79 "..\..\..\..\Views\Areas\abcAreas.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnCancelar).Click += this.btnCancelar_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.btnEditar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 38 "..\..\..\..\Views\Areas\abcAreas.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnEditar).Click += this.btnEditar_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.btnEliminar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 43 "..\..\..\..\Views\Areas\abcAreas.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnEliminar).Click += this.btnEliminar_Click;
                    #line default
                }
                break;
            case 10:
                {
                    this.btnAgregar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 49 "..\..\..\..\Views\Areas\abcAreas.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAgregar).Click += this.btnAgregar_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

