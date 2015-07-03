using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalRDashboard.Common
{
    /// <summary>
    /// Represents a "counter" for a single machine instance.
    /// </summary>
    [CustomValidation(typeof(StatusCounter), "ValidateAllProperties")]
    public class StatusCounter: INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the id of the client web page
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine
        /// </summary>
        [Required]
        public string CounterName { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for this counter
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Maximum { get; set; }

        /// <summary>
        /// Gets or sets the minimum value for this counter
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Minimum { get; set; }

        private int _value;

        /// <summary>
        /// Gets or sets the current value for this counter
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                // has the value changed?
                if (_value != value)
                {
                    // the value has changed, so apply it.
                    _value = value;

                    // raise propertychanged events so that databinding can work
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));

                        //RedisSupport.SendStatusUpdate(this.ClientId, this.CounterName, this.Value.ToString(), this.Status.ToString());
                        SignalRSupport.Instance.SendStatusUpdate(this.ClientId, this.CounterName, this.Value.ToString(), this.Status.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value where the machine is considered to be in a warning state.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int WarningThreshold { get; set; }

        /// <summary>
        /// Gets or sets the value where the machine is considered to be in a critical state.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int CriticalThreshold { get; set; }

        /// <summary>
        /// Gets the current status of the machine
        /// </summary>
        public Status Status 
        { 
            get
            {
                var s = Status.Unknown;

                if (this.Value >= this.CriticalThreshold)
                {
                    s = Status.Critical;
                }
                else if (this.Value >= this.WarningThreshold)
                {
                    s = Status.Warning;
                }
                else
                {
                    s = Status.Normal;
                }

                return s;
            }
        }

        #region Validation logic

        /// <summary>
        /// Perform validation of the counter values.
        /// This handles both data annotation rules and custom validation.
        /// </summary>
        public void Validate()
        {
            List<ValidationResult> vErrors = new List<ValidationResult>();

            // validate class-level attributes
            // When the last parameter is true, it also evaluates all ValidationAttributes on this entity's properties
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), vErrors, true);

            // handle errors
            if (vErrors.Count > 0)
                throw new ValidationException(vErrors[0], null, this);
        }

        /// <summary>
        /// Perform custom class-level validation.
        /// </summary>
        public static ValidationResult ValidateAllProperties(StatusCounter counter, ValidationContext context)
        {
            string errorMessage = string.Empty;
            List<string> propertiesWithErrors = new List<string>();

            // minimum & maximum
            if (counter.Maximum < counter.Minimum)
            {
                errorMessage += "Maximum must be greater than or equal to Minimum.\n";
                propertiesWithErrors.Add("Maximum");
            }

            // value
            if (counter.Value > counter.Maximum || counter.Value < counter.Minimum)
            {
                errorMessage += "Value must be between Minimum and Maximum.\n";
                propertiesWithErrors.Add("Value");
            }

            // warning threshold
            if (counter.WarningThreshold > counter.Maximum || counter.WarningThreshold < counter.Minimum)
            {
                errorMessage += "WarningThreshold must be between Minimum and Maximum.\n";
                propertiesWithErrors.Add("WarningThreshold");
            }

            if (counter.WarningThreshold > counter.CriticalThreshold)
            {
                errorMessage += "WarningThreshold must be less than or equal to CriticalThreshold.\n";
                propertiesWithErrors.Add("WarningThreshold");
            }

            // critical threshold
            if (counter.CriticalThreshold > counter.Maximum || counter.CriticalThreshold < counter.Minimum)
            {
                errorMessage += "CriticalThreshold must be between Minimum and Maximum.\n";
                propertiesWithErrors.Add("CriticalThreshold");
            }

            if (counter.CriticalThreshold < counter.WarningThreshold)
            {
                errorMessage += "CriticalThreshold must be greater than or equal to WarningThreshold.\n";
                propertiesWithErrors.Add("CriticalThreshold");
            }


            // return value
            if (propertiesWithErrors.Count > 0 )
            {
                return new ValidationResult(errorMessage, propertiesWithErrors);
            }
            else
            {
                return ValidationResult.Success;
            }

        }

        #endregion

        #region INotifyPropertyChanged support

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}