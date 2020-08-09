using System;
using FitnessStuff.Models.Extensions;

namespace FitnessStuff.Models
{
    public class FemaleModel : PersonModel
    {
        public override decimal OriginalHarrisBenedictBMR
        {
            get
            {
                return (9.5634M * this.GetWeightInKilograms()) + (1.8496M * this.GetHeightInCM()) + (4.6756M * this.AgeInYears) + 655.0955M;
            }
        }

        public override decimal RevisedHarrisBenedictBMR
        {
            get
            {
                return (9.247M * this.GetWeightInKilograms()) + (3.098M * this.GetHeightInCM()) + (4.330M * this.AgeInYears) + 447.593M;
            }
        }

        public override decimal MifflinStJeorBMR
        {
            get
            {
                return (10.0M * this.GetWeightInKilograms()) + (6.25M * this.GetHeightInCM()) + (5.0M * this.AgeInYears) - 161.0M;
            }
        }

        public override decimal BMR
        {
            get
            {
                return (9.6M * this.GetWeightInKilograms()) + (1.8M * this.GetHeightInCM()) - (4.7M * this.AgeInYears) + 655.0M;
            }
        }

        public override decimal? LeanBodyMassInPounds
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override decimal? BodyFatWeightInPounds
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override decimal? BFI
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override decimal? USNavyBFI
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}