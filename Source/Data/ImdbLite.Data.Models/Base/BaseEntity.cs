namespace ImdbLite.Data.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using ImdbLite.Data.Common.Models;

    public abstract class BaseEntity : IAuditInfo, IDeletableEntity
    {
        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
