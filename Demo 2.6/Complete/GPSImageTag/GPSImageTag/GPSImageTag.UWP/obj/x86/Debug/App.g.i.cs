﻿#pragma checksum "E:\Demo\Solutions\Demo 2.6\Complete\GPSImageTag\GPSImageTag\GPSImageTag.UWP\App.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "86D3713E9C6A05F203784B63CF86FF61"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace GPSImageTag.UWP
{
#if !DISABLE_XAML_GENERATED_MAIN
    /// <summary>
    /// Program class
    /// </summary>
    public static class Program
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        static void Main(string[] args)
        {
            global::Windows.UI.Xaml.Application.Start((p) => new App());
        }
    }
#endif

    partial class App : global::Windows.UI.Xaml.Application
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        private bool _contentLoaded;
        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
         {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
#if DEBUG && !DISABLE_XAML_GENERATED_BINDING_DEBUG_OUTPUT
            DebugSettings.BindingFailed += (sender, args) =>
            {
                global::System.Diagnostics.Debug.WriteLine(args.Message);
            };
#endif
#if DEBUG && !DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION
            UnhandledException += (sender, e) =>
            {
                if (global::System.Diagnostics.Debugger.IsAttached) global::System.Diagnostics.Debugger.Break();
            };
#endif
        }
    }
}

