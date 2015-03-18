namespace ImdbLite.Web.Areas.Administration.ViewModels.Base
{
    using ImdbLite.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class NotDeletedIndexViewModel
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeFullFormatString)]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeFullFormatString)]
        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }
    }
}