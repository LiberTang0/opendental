﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenDentBusiness.Properties {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OpenDentBusiness.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap ApptBackTest {
            get {
                object obj = ResourceManager.GetObject("ApptBackTest", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The primary key could not be overriden, because either the object is not new and has been loaded from the database or the object does not contain an Identity key..
        /// </summary>
        public static string CannotOverridePrimaryKey {
            get {
                return ResourceManager.GetString("CannotOverridePrimaryKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object has been deleted, and can not be saved again to the database..
        /// </summary>
        public static string CannotSaveDeletedObject {
            get {
                return ResourceManager.GetString("CannotSaveDeletedObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current data type is not supported by the PIn class..
        /// </summary>
        public static string DataTypeNotSupportedByPIn {
            get {
                return ResourceManager.GetString("DataTypeNotSupportedByPIn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current data type ({0}) is not supported by the POut class..
        /// </summary>
        public static string DataTypeNotSupportedByPOut {
            get {
                return ResourceManager.GetString("DataTypeNotSupportedByPOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A DTO of type {0} is not supported..
        /// </summary>
        public static string DtoNotSupportedException {
            get {
                return ResourceManager.GetString("DtoNotSupportedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current object does not contain any data fields..
        /// </summary>
        public static string NoFields {
            get {
                return ResourceManager.GetString("NoFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This object has a primary key that consists of multiple columns..
        /// </summary>
        public static string NotASinglePrimaryKey {
            get {
                return ResourceManager.GetString("NotASinglePrimaryKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object has already been deleted, and cannot be deleted twice..
        /// </summary>
        public static string ObjectAlreadyDeleted {
            get {
                return ResourceManager.GetString("ObjectAlreadyDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object does not exist in the database, and can therefore not be deleted..
        /// </summary>
        public static string ObjectNotSaved {
            get {
                return ResourceManager.GetString("ObjectNotSaved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap Patient_Info {
            get {
                object obj = ResourceManager.GetObject("Patient_Info", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The primary key is not of the type Integer..
        /// </summary>
        public static string PrimaryKeyNotAnInteger {
            get {
                return ResourceManager.GetString("PrimaryKeyNotAnInteger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An attribute of type &quot;DataFieldAttribute&quot; can only be specified once..
        /// </summary>
        public static string TooManyDataFieldAttributes {
            get {
                return ResourceManager.GetString("TooManyDataFieldAttributes", resourceCulture);
            }
        }
    }
}
