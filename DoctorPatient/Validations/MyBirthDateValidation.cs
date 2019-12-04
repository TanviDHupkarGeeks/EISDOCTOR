using System;
using System.ComponentModel.DataAnnotations;

namespace GreenHealth.Models
{
    public class MyBirthDateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = (DateTime)value;
            if (dt.Date.CompareTo(DateTime.Now) < 0)
                return true;
            else
                return false;
        }
    }

}