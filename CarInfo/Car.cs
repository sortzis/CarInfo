using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo
{
    class Car
    {
        public enum Country
        {
            none,
            germany,
            USA,
            UK,
            italy,
            sweden,
            japan
        }

        #region FIELDS

        private string _make;
        private string _model;
        private Country _countries;
        private int _price;

        #endregion

        #region PROPERTIES

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public Country Countries
        {
            get { return _countries; }
            set { _countries = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Car()
        {

        }

        public Car(string make, string model, Country countries, int price)
        {
            _make = make;
            _model = model;
            _countries = countries;
            _price = price;
        }

        #endregion

        #region METHODS

        #endregion
    }
}
