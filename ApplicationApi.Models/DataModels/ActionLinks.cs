using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class ActionLinks
    {
        [Key]
        public int ActionLinkId { get; set; }
        public string ApiControllerName { get; set; }
        public string ApiControllerUrl { get; set; }
        public string PosLinkTitle { get; set; }
        public string PosLinkIcon { get; set; }
        public string PosLinkUrl { get; set; }
        public bool IsActiveLink { get; set; }


    }
}
