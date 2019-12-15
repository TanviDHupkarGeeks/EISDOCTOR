using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using GreenHealth.Controllers;
using GreenHealth.Helpers;
using GreenHealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GreenHealth.ViewModels
{
    public class PatientFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Gender Sex { get; set; }
        [Required]
        [ValidDate]
        public string BirthDate { get; set; }


        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        public byte City { get; set; }

        public DateTime Date { get; set; }

        public string Heading { get; set; }


        public DateTime GetBirthDate()
        {
            //TODO: Validate BirthDate 

            return DateTime.Parse(string.Format("{0}", BirthDate));
            //return DateTime.ParseExact(BirthDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);
        }

        public IEnumerable<City> Cities { get; set; }



        public string Action
        {
            get
            {
                Expression<Func<PatientsController, IActionResult>> update = (c => c.Update(this));
                Expression<Func<PatientsController, IActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }
        }

        #region for dropdownlist

        public IEnumerable<SelectListItem> GendersList
        {
            get { return ClinicMgtHelpers.GenderToSelectList(); }
            set { }
        }

        #endregion
    }
}