using System;
using FitnessStuff.Models.Extensions;

namespace FitnessStuff.Models
{
    public class MaleModel : PersonModel
    {
        public override decimal OriginalHarrisBenedictBMR
        {
            get
            {
                return (13.7516M * this.GetWeightInKilograms()) + (5.0033M * this.GetHeightInCM()) + (6.7550M * this.AgeInYears) + 66.4730M;
            }
        }

        public override decimal RevisedHarrisBenedictBMR
        {
            get
            {
                return (13.397M * this.GetWeightInKilograms()) + (4.799M * this.GetHeightInCM()) + (5.677M * this.AgeInYears) + 88.392M;
            }
        }

        public override decimal MifflinStJeorBMR
        {
            get
            {
                return (10.0M * this.GetWeightInKilograms()) + (6.25M * this.GetHeightInCM()) + (5.0M * this.AgeInYears) + 5.0M;
            }
        }

        public override decimal BMR
        {
            get
            {
                return (13.7M * this.GetWeightInKilograms()) + (5.0M * this.GetHeightInCM()) + (6.8M * this.AgeInYears) + 66.0M;
            }
        }

        public override decimal? LeanBodyMassInPounds
        {
            get
            {
                if (this.WaistLength.HasValue)
                {
                    // factor1 - factor2
                    return ((this.GetWeightInPounds() * 1.082M) + 94.42M) - (this.GetWaistLengthInIN().GetValueOrDefault() * 4.15M);
                }
                else
                    return null;
            }
        }

        public override decimal? BodyFatWeightInPounds
        {
            get
            {
                if (this.WaistLength.HasValue)
                    return this.GetWeightInPounds() - this.LeanBodyMassInPounds;
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
                return (this.BodyFatWeightInPounds * 100.0M) / this.GetWeightInPounds();
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
                        - 0.19077M * (decimal)Math.Log((double)(this.GetWaistLengthInCM() - this.GetNeckLengthInCM()), 10D)
                        + 0.15456M * (decimal)Math.Log((double)this.GetHeightInCM(), 10D)
                        ) - 450.0M;
                else
                    return null;
            }
        }
    }
}