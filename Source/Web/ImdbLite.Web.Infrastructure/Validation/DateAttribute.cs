﻿namespace ImdbLite.Web.Infrastructure.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Property)]
    public class DateAttribute : RangeAttribute
    {
        public DateAttribute()
            : base(typeof(DateTime),
                    DateTime.Now.AddYears(-6).ToShortDateString(),
                    DateTime.Now.ToShortDateString())
        { } 
    }

}
