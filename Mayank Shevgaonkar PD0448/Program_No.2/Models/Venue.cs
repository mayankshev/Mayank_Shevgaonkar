﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Program_No._2.Models
{
    public class Venue
    {
        #region Properties
        public int Id { get; set; }
        // [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        // [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
        // [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        public string CountryName { get; set; }
        // [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        // [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public List<Country> Countries { get; set; }
        public string CountryCode { get; set; }
        #endregion
    }
    public class Country
    {
        #region Properties
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string MapReference { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeLong { get; set; }
        #endregion
    }
    public class Prediction
    {
        public string description { get; set; }
        public string id { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public List<string> types { get; set; }
    }
    public class RootObject
    {
        public List<Prediction> predictions { get; set; }
        public string status { get; set; }
    }
}