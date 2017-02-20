using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Models
{
    [MetadataType(typeof(BookMetadata))]
    public partial class Book
    {
        [Bind(Exclude = "BookID")]
        public class BookMetadata
        {
            [ScaffoldColumn(false)]
            public int BookID { get; set; }

            [DisplayName("Title")]
            [Required(ErrorMessage = "Title is required.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50)]
            public string Title { get; set; }

            [DisplayName("Authors")]
            [Required(ErrorMessage = "Authors is required.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50)]
            public string Authors { get; set; }

            [DisplayName("Description")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(500)]
            public string Description { get; set; }

            [DisplayName("Availible")]
            [Required(ErrorMessage = "Availible is required.")]
            [Range(0, 1, ErrorMessage = "Please, set 'true(1)' or 'false(0)'")]
            public short Availible { get; set; }
        }
    }
}