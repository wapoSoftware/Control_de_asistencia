﻿#pragma checksum "C:\Assistence_Control\Assistence_Control\Views\Empleados\abcEmpleado.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DB9A992B989A04A6FFCDB1F9304639C7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assistence_Control.Views.Empleados
{
    partial class abcEmpleado : 
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
                }
                break;
            case 2:
                {
                    this.gridAgregar = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.tbNumeroEmpleado = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.tbNombre = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.tbApellidoPaterno = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.tbApellidoMaterno = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7:
                {
                    this.btnGuardar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 86 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnGuardar).Click += this.btnGuardar_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.btnCancelar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 94 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnCancelar).Click += this.btnCancelar_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.btnEditar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 46 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnEditar).Click += this.btnEditar_Click;
                    #line default
                }
                break;
            case 10:
                {
                    this.btnEliminar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 51 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnEliminar).Click += this.btnEliminar_Click;
                    #line default
                }
                break;
            case 11:
                {
                    this.btnAgregar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 57 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAgregar).Click += this.btnAgregar_Click;
                    #line default
                }
                break;
            case 12:
                {
                    global::Windows.UI.Xaml.Controls.AutoSuggestBox element12 = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    #line 30 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element12).TextChanged += this.AutoSuggestBox_TextChanged;
                    #line 30 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element12).QuerySubmitted += this.AutoSuggestBox_QuerySubmitted;
                    #line 30 "..\..\..\..\Views\Empleados\abcEmpleado.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element12).SuggestionChosen += this.AutoSuggestBox_SuggestionChosen;
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

