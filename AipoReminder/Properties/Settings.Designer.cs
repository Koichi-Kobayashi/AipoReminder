﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.261
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AipoReminder.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string userId {
            get {
                return ((string)(this["userId"]));
            }
            set {
                this["userId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string userPassword {
            get {
                return ((string)(this["userPassword"]));
            }
            set {
                this["userPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int checkTime {
            get {
                return ((int)(this["checkTime"]));
            }
            set {
                this["checkTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.24.52:81/aipo/")]
        public string url {
            get {
                return ((string)(this["url"]));
            }
            set {
                this["url"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string loginName {
            get {
                return ((string)(this["loginName"]));
            }
            set {
                this["loginName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkAutoRun {
            get {
                return ((bool)(this["checkAutoRun"]));
            }
            set {
                this["checkAutoRun"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkAutoLogin {
            get {
                return ((bool)(this["checkAutoLogin"]));
            }
            set {
                this["checkAutoLogin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string NpgsqlConnectionServer {
            get {
                return ((string)(this["NpgsqlConnectionServer"]));
            }
            set {
                this["NpgsqlConnectionServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5432")]
        public string NpgsqlConnectionPort {
            get {
                return ((string)(this["NpgsqlConnectionPort"]));
            }
            set {
                this["NpgsqlConnectionPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("aipo_reminder")]
        public string NpgsqlConnectionUserId {
            get {
                return ((string)(this["NpgsqlConnectionUserId"]));
            }
            set {
                this["NpgsqlConnectionUserId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string NpgsqlConnectionPassword {
            get {
                return ((string)(this["NpgsqlConnectionPassword"]));
            }
            set {
                this["NpgsqlConnectionPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("org001")]
        public string NpgsqlConnectionDatabase {
            get {
                return ((string)(this["NpgsqlConnectionDatabase"]));
            }
            set {
                this["NpgsqlConnectionDatabase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public string NpgsqlConnectionTimeout {
            get {
                return ((string)(this["NpgsqlConnectionTimeout"]));
            }
            set {
                this["NpgsqlConnectionTimeout"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkBlog {
            get {
                return ((bool)(this["checkBlog"]));
            }
            set {
                this["checkBlog"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkBlogComment {
            get {
                return ((bool)(this["checkBlogComment"]));
            }
            set {
                this["checkBlogComment"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkMsgboard {
            get {
                return ((bool)(this["checkMsgboard"]));
            }
            set {
                this["checkMsgboard"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkSchedule {
            get {
                return ((bool)(this["checkSchedule"]));
            }
            set {
                this["checkSchedule"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkWorkflow {
            get {
                return ((bool)(this["checkWorkflow"]));
            }
            set {
                this["checkWorkflow"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkMemo {
            get {
                return ((bool)(this["checkMemo"]));
            }
            set {
                this["checkMemo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6")]
        public int aipoVersion {
            get {
                return ((int)(this["aipoVersion"]));
            }
            set {
                this["aipoVersion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkOtherSchedule {
            get {
                return ((bool)(this["checkOtherSchedule"]));
            }
            set {
                this["checkOtherSchedule"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string checkGroupUserId {
            get {
                return ((string)(this["checkGroupUserId"]));
            }
            set {
                this["checkGroupUserId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool checkInformation {
            get {
                return ((bool)(this["checkInformation"]));
            }
            set {
                this["checkInformation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ckeckExtTimeCard {
            get {
                return ((bool)(this["ckeckExtTimeCard"]));
            }
            set {
                this["ckeckExtTimeCard"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("default")]
        public string browserName {
            get {
                return ((string)(this["browserName"]));
            }
            set {
                this["browserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool v6FirstTime {
            get {
                return ((bool)(this["v6FirstTime"]));
            }
            set {
                this["v6FirstTime"] = value;
            }
        }
    }
}
