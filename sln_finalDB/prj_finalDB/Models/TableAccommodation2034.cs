//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prj_finalDB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TableAccommodation2034
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TableAccommodation2034()
        {
            this.TableBrowser2034 = new HashSet<TableBrowser2034>();
        }
    
        public string aName { get; set; }
        public string aPrice { get; set; }
        public string aPhone { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public string aCounty { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TableBrowser2034> TableBrowser2034 { get; set; }
    }
}