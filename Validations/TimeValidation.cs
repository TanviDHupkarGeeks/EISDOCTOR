using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHealth.Validations
{
    public class TimeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = (DateTime)value;
            if (dt.Minute == 30 || dt.Minute == 00)
                return true;
            else
                return false;
        }
    }
}
