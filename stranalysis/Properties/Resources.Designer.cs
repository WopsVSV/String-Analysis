﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace stranalysis.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("stranalysis.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to vbox
        ///virtualbox
        ///sandboxie
        ///sbiesvc.
        /// </summary>
        internal static string Blacklist {
            get {
                return ResourceManager.GetString("Blacklist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: stranalysis.exe &lt;flags&gt; &lt;file_path&gt;
        ///Example: stranalysis.exe -dump C:/malware.exe
        ///
        ///NOTE: The &lt;file_path&gt; argument should always be last.
        ///
        ///Flags:
        ///-dump &gt; Dumps the strings to a &lt;file_path&gt;_dump.txt file
        ///-log &gt; Logs the console output into a &lt;file_path&gt;_log.txt file.
        /// </summary>
        internal static string HelpDocument {
            get {
                return ResourceManager.GetString("HelpDocument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // Methods
        ///
        ///Assembly.Load|Loads an external assembly
        ///CodeDom|References the CodeDOM compiler
        ///ResourceBuilder|Builds resources at runtime
        ///
        ///// P/Invoke methods
        ///
        ///.
        /// </summary>
        internal static string ManagedMethods {
            get {
                return ResourceManager.GetString("ManagedMethods", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to advapi32.dll
        ///avifil32.dll
        ///cards.dll
        ///cfgmgr32.dll
        ///comctl32.dll
        ///comdlg32.dll
        ///credui.dll
        ///crypt32.dll
        ///dbghelp.dll
        ///dbghlp.dll
        ///dbghlp32.dll
        ///dhcpsapi.dll
        ///difxapi.dll
        ///dmcl40.dll
        ///dnsapi.dll
        ///dtl.dll
        ///dwmapi.dll
        ///faultrep.dll
        ///fbwflib.dll
        ///fltlib.dll
        ///fwpuclnt.dll
        ///gdi32.dll
        ///gdiplus.dll
        ///getuname.dll
        ///glu32.dll
        ///glut32.dll
        ///gsapi.dll
        ///hhctrl.dll
        ///hid.dll
        ///hlink.dll
        ///httpapi.dll
        ///icmp.dll
        ///imm32.dll
        ///iphlpapi.dll
        ///iprop.dll
        ///irprops.dll
        ///kernel32.dll
        ///mapi32.dll
        ///MinCore.dll
        ///mpr.dll
        ///mqrt.dll
        ///mscorsn. [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string PInvokeLibraries {
            get {
                return ResourceManager.GetString("PInvokeLibraries", resourceCulture);
            }
        }
    }
}