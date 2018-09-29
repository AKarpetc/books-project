﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;

namespace book_editor.service.Utility
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class InputMaskAttribute : ValidationAttribute
    {
        // Internal field to hold the mask value.
        readonly string _mask;

        public string Mask
        {
            get { return _mask; }
        }

        public InputMaskAttribute(string mask)
        {
            _mask = mask;
        }


        public override bool IsValid(object value)
        {
            var phoneNumber = (String)value;
            bool result = true;
            if (this.Mask != null)
            {
                result = MatchesMask(this.Mask, phoneNumber);
            }
            return result;
        }

        // Checks if the entered phone number matches the mask.
        internal bool MatchesMask(string mask, string phoneNumber)
        {
            if (mask.Length != phoneNumber.Trim().Length)
            {
                // Length mismatch.
                return false;
            }
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'd' && char.IsDigit(phoneNumber[i]) == false)
                {
                    // Digit expected at this position.
                    return false;
                }
                if (mask[i] == '-' && phoneNumber[i] != '-')
                {
                    // Spacing character expected at this position.
                    return false;
                }
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name, this.Mask);
        }

    }
}