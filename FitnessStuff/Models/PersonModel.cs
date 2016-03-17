using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessStuff.Models
{
    public abstract class PersonModel
    {
        #region Public Properties

        #region User Input
        [Required]
        [Range(typeof(decimal), "0.001", "1000")]
        public decimal Weight { get; set; }

        [Required]
        [Range(typeof(decimal), "0.001", "1000")]
        public decimal Height { get; set; }

        [Required]
        [Range(typeof(int), "0", "1000")]
        [Display(Name = "Age (in years)")]
        public int AgeInYears { get; set; }

        [Range(typeof(decimal), "0.001", "1000")]
        [Display(Name = "Waist Length")]
        public decimal? WaistLength { get; set; }

        [Range(typeof(decimal), "0.001", "1000")]
        [Display(Name = "Neck Length")]
        public decimal? NeckLength { get; set; }

        [Required]
        [Display(Name = "Unit of Measure")]
        public UnitOfMeasureModel UOFM { get; set; }
        #endregion

        #region Calculated Properties
        [Display(Name = "Average BMR")]
        public decimal AverageBMR
        {
            get
            {
                return (OriginalHarrisBenedictBMR + RevisedHarrisBenedictBMR + MifflinStJeorBMR + BMR) / 4.0M;
            }
        }

        [Display(Name = "BMI")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal BMI
        {
            get
            {
                var heightInMeters = this.HeightInCM / 100.0M;

                return heightInMeters == 0
                    ? 0
                    : (decimal)((double)this.WeightInKilograms / (Math.Pow((double)heightInMeters, 2)));
            }
        }
        #endregion

        #region BFI Method1
        [Display(Name = "Lean Body Mass")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? LeanBodyMass
        {
            get
            {
                if (this.WaistLength.HasValue)
                {
                    return this.UOFM == UnitOfMeasureModel.Imperial ? this.LeanBodyMassInPounds : this.LeanBodyMassInKg;
                }
                else
                    return null;
            }
        }

        [Display(Name = "Body Fat Weight")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? BodyFatWeight
        {
            get
            {
                if (this.WaistLength.HasValue)
                {
                    return this.UOFM == UnitOfMeasureModel.Imperial ? this.BodyFatWeightInPounds : this.BodyFatWeightInKg;
                }
                else
                    return null;
            }
        }
        #endregion

        #region US Navy BFI
        [Display(Name = "Lean Body Mass (US Navy)")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? LeanBodyMassUSNavy
        {
            get
            {
                if (this.USNavyBFI.HasValue)
                {
                    return this.Weight - this.BodyFatWeightUSNavy;
                }
                else
                    return null;
            }
        }

        [Display(Name = "Body Fat Weight (US Navy)")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? BodyFatWeightUSNavy
        {
            get
            {
                if (this.USNavyBFI.HasValue)
                {
                    return (this.USNavyBFI / 100.0M) * this.Weight;
                }
                else
                    return null;
            }
        }
        #endregion

        #region Average BFI
        [Display(Name = "Lean Body Mass (Average)")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? LeanBodyMassAverage
        {
            get
            {
                if (this.LeanBodyMass.HasValue && this.LeanBodyMassUSNavy.HasValue)
                {
                    return (this.LeanBodyMass.GetValueOrDefault() + this.LeanBodyMassUSNavy.GetValueOrDefault()) / 2.0M;
                }
                else
                    return null;
            }
        }

        [Display(Name = "Body Fat Weight (Average)")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? BodyFatWeightAverage
        {
            get
            {
                if (this.BodyFatWeight.HasValue && this.BodyFatWeightUSNavy.HasValue)
                {
                    return (this.BodyFatWeight.GetValueOrDefault() + this.BodyFatWeightUSNavy.GetValueOrDefault()) / 2.0M;
                }
                else
                    return null;
            }
        }

        [Display(Name = "BFI (Average)")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? BFIAverage
        {
            get
            {
                return (this.BFI.GetValueOrDefault() + this.USNavyBFI.GetValueOrDefault()) / 2.0M;
            }
        }
        #endregion

        #region Abtract Properties
        [Display(Name = "Original Harris-Benedict BMR")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public abstract decimal OriginalHarrisBenedictBMR { get; }

        [Display(Name = "Revised Harris-Benedict BMR")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public abstract decimal RevisedHarrisBenedictBMR { get; }

        [Display(Name = "Mifflin St Jeor BMR")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public abstract decimal MifflinStJeorBMR { get; }

        [Display(Name = "BMR")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public abstract decimal BMR { get; }

        [Display(Name = "Lean Body Mass")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        protected abstract decimal? LeanBodyMassInPounds { get; }

        [Display(Name = "Body Fat Weight")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        protected abstract decimal? BodyFatWeightInPounds { get; }

        [Display(Name = "BFI")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public abstract decimal? BFI { get; }

        [Display(Name = "U.S. Navy BFI (developed by Drs. Hodgdon and Beckett)")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public abstract decimal? USNavyBFI { get; }
        #endregion

        #endregion

        #region Protected Properties
        protected decimal? LeanBodyMassInKg
        {
            get
            {
                if (this.WaistLength.HasValue)
                    return ConversionHelper.Pounds_to_kg(this.LeanBodyMassInPounds.GetValueOrDefault());
                else
                    return null;
            }
        }
        
        protected decimal? BodyFatWeightInKg
        {
            get
            {
                if (this.WaistLength.HasValue)
                    return ConversionHelper.Pounds_to_kg(this.BodyFatWeightInPounds.GetValueOrDefault());
                else
                    return null;
            }
        }

        protected decimal WeightInPounds
        {
            get
            {
                return this.UOFM == UnitOfMeasureModel.Imperial 
                    ? this.Weight 
                    : ConversionHelper.KG_to_pounds(this.Weight);
            }
        }

        protected decimal WeightInKilograms
        {
            get
            {
                return this.UOFM == UnitOfMeasureModel.Imperial 
                    ? ConversionHelper.Pounds_to_kg(this.Weight) 
                    : this.Weight;
            }
        }

        protected decimal HeightInIN
        {
            get
            {
                return this.UOFM == UnitOfMeasureModel.Imperial 
                    ? this.Height 
                    : ConversionHelper.CM_to_IN(this.Height);
            }
        }

        protected decimal HeightInCM
        {
            get
            {
                return this.UOFM == UnitOfMeasureModel.Imperial 
                    ? ConversionHelper.IN_to_CM(this.Height) 
                    : this.Height;
            }
        }

        protected decimal? WaistLengthInIN
        {
            get
            {
                if (!this.WaistLength.HasValue)
                    return null;

                return this.UOFM == UnitOfMeasureModel.Imperial
                    ? this.WaistLength
                    : ConversionHelper.CM_to_IN(this.WaistLength.GetValueOrDefault());
            }
        }

        protected decimal? WaistLengthInCM
        {
            get
            {
                if (!this.WaistLength.HasValue)
                    return null;

                return this.UOFM == UnitOfMeasureModel.Metric
                    ? this.WaistLength
                    : ConversionHelper.IN_to_CM(this.WaistLength.GetValueOrDefault());
            }
        }

        protected decimal? NeckLengthInIN
        {
            get
            {
                if (!this.NeckLength.HasValue)
                    return null;

                return this.UOFM == UnitOfMeasureModel.Imperial
                    ? this.NeckLength
                    : ConversionHelper.CM_to_IN(this.NeckLength.GetValueOrDefault());
            }
        }
        protected decimal? NeckLengthInCM
        {
            get
            {
                if (!this.NeckLength.HasValue)
                    return null;

                return this.UOFM == UnitOfMeasureModel.Metric
                    ? this.NeckLength
                    : ConversionHelper.IN_to_CM(this.NeckLength.GetValueOrDefault());
            }
        }
        #endregion
    }
}