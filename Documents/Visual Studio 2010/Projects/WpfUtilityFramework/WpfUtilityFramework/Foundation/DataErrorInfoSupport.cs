using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WpfUtilityFramework.Foundation
{
    public sealed class DataErrorInfoSupport:IDataErrorInfo
    {
        private readonly object instance;

        public DataErrorInfoSupport(object instance)
        {
            if (instance == null) { throw new ArgumentNullException("instance"); }
            this.instance = instance;
        }

        public string Error { get { return this[""]; } }

        public string this[string memberName]
        {
            get
            {
                List<ValidationResult> validationResult = new List<ValidationResult>();

                if (string.IsNullOrEmpty(memberName))
                {
                    Validator.TryValidateObject(instance, new ValidationContext(instance, null, null), validationResult, true);
                }
                else
                {
                    PropertyDescriptor property = TypeDescriptor.GetProperties(this)[memberName];
                    if (property == null)
                    {
                        throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "The specified member {0} was not found on the instance {1}", memberName, instance.GetType()));
                    }
                    Validator.TryValidateProperty(property.GetValue(instance),
                        new ValidationContext(instance, null, null) { MemberName = memberName }, validationResult);
                }
                return string.Join(Environment.NewLine, validationResult.Select(x => x.ErrorMessage));
            }
        }
    }
}
