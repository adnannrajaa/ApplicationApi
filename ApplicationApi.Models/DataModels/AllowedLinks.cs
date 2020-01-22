using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class AllowedLinks
    {
        [Key]
        public int AllowedLinkId { get; set; }

        public int ActionLinkId { get; set; }

        [ForeignKey("ActionLinkId")]
        public ActionLinks ActionLinks { get; set; }
        public string UserId { get; set; }
        public bool IsAssinged { get; set; }


    }
}
