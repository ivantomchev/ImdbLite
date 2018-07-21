namespace ImdbLite.Services.Data.DTOs.Base
{
    using System;

    public abstract class BaseDTO
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
