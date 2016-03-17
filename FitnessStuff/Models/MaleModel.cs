using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessStuff.Models
{
    public class MaleModel : PersonModel
    {
        public override decimal OriginalHarrisBenedictBMR
        {
            get
            {
                return (13.7516M * this.WeightInKilograms) + (5.0033M * this.HeightInCM) + (6.7550M * this.AgeInYears) + 66.4730M;
            }
        }

        public override decimal RevisedHarrisBenedictBMR
        {
            get
            {
                return (13.397M * this.WeightInKilograms) + (4.799M * this.HeightInCM) + (5.677M * this.AgeInYears) + 88.392M;
            }
        }

        public override decimal MifflinStJeorBMR
        {
            get
            {
                return (10.0M * this.WeightInKilograms) + (6.25M * this.HeightInCM) + (5.0M * this.AgeInYears) + 5.0M;
            }
        }

        public override decimal BMR
        {
            get
            {
                return (13.7M * this.WeightInKilograms) + (5.0M * this.HeightInCM) + (6.8M * this.AgeInYears) + 66.0M;
            }
        }

        protected override decimal? LeanBodyMassInPounds
        {
            get
            {
                if (this.WaistLength.HasValue)
                {
                    // factor1 - factor2
                    return ((this.WeightInPounds * 1.082M) + 94.42M) - (this.WaistLengthInIN.GetValueOrDefault() * 4.15M);
                }
                else
                    return null;
            }
        }

        protected override decimal? BodyFatWeightInPounds
        {
            get
            {
                if (this.WaistLength.HasValue)
                    return this.WeightInPounds - this.LeanBodyMassInPounds;
                else
                    return null;
            }
        }

        public override decimal? BFI
        {
            get
            {
                if (!this.WaistLength.HasValue)
                    return null;
                else
                return (this.BodyFatWeightInPounds * 100.0M) / this.WeightInPounds;
            }
        }

        public override decimal? USNavyBFI
        {
            get
            {
                if (this.WaistLength.HasValue && this.NeckLength.HasValue)
                    return 495.0M /
                        (
                        1.0324M
                        - 0.19077M * (decimal)Math.Log((double)(this.WaistLengthInCM - this.NeckLengthInCM), 10D)
                        + 0.15456M * (decimal)Math.Log((double)this.HeightInCM, 10D)
                        ) - 450.0M;
                else
                    return null;
            }
        }
    }
}