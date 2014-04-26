using System;
using System.ComponentModel.DataAnnotations;

namespace RestByDesign.Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinAttribute : DataTypeAttribute
    {
        private const string DefaultErrorMsg = "The {0} must be greater than or equal to {1}";
        public object Min { get { return _min; } }

        private readonly double _min;

        public MinAttribute(int min) : this((double)min)
        { }

        public MinAttribute(double min) : base("min")
        {
            _min = min;
            ErrorMessage = DefaultErrorMsg;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(ErrorMessageString, name, _min);
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            double valueAsDouble;

            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);

            return isDouble && valueAsDouble >= _min;
        }
    }
}