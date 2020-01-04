using System.Collections.Generic;
using System.Linq;
using GreenHealth.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GreenHealth.Helpers
{
    public static class ClinicMgtHelpers
    {
        public static IEnumerable<SelectListItem> GenderToSelectList()
        {
            var genderItems = EnumHelpers.ToSelectList(typeof(Gender)).ToList();
            genderItems.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            return genderItems;
        }

    }
}