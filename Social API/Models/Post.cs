//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Social_API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public partial class Post
    {
        List<Link> links = new List<Link>();
        public int PostId { get; set; }
        public string Post1 { get; set; }
        public int ProfileId { get; set; }
        //[JsonIgnore, XmlIgnore]
        public List<Link> Links
        {
            get { return links; }
        }
        
    }
}
