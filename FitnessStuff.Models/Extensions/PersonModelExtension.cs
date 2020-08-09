using System;

namespace FitnessStuff.Models.Extensions
{
    public static class PersonModelExtension
    {
        #region Calculated Properties
        public static decimal GetAverageBMR(this PersonModel model)
        {
            return (model.OriginalHarrisBenedictBMR + model.RevisedHarrisBenedictBMR + model.MifflinStJeorBMR + model.BMR) / 4.0M;
        }

        public static decimal GetBMI(this PersonModel model)
        {
            var heightInMeters = model.Height / 100.0M;

            return heightInMeters == 0
                ? 0
                : (decimal)((double)model.GetWeightInKilograms() / (Math.Pow((double)heightInMeters, 2)));
        }
        #endregion

        #region BFI Method1
        public static decimal? GetLeanBodyMass(this PersonModel model)
        {
            if (model.WaistLength.HasValue)
            {
                return model.UOFM == UnitOfMeasureEnum.Imperial ? model.LeanBodyMassInPounds : model.GetLeanBodyMassInKg();
            }

            return null;
        }

        public static decimal? GetBodyFatWeight(this PersonModel model)
        {
            if (model.WaistLength.HasValue)
            {
                return model.UOFM == UnitOfMeasureEnum.Imperial ? model.BodyFatWeightInPounds : model.GetBodyFatWeightInKg();
            }
            
            return null;
        }
        #endregion

        #region US Navy BFI
        public static decimal? GetLeanBodyMassUSNavy(this PersonModel model)
        {
            if (model.USNavyBFI.HasValue)
            {
                return model.Weight - model.GetBodyFatWeightUSNavy();
            }

            return null;
        }

        public static decimal? GetBodyFatWeightUSNavy(this PersonModel model)
        {
            if (model.USNavyBFI.HasValue)
            {
                return (model.USNavyBFI / 100.0M) * model.Weight;
            }
            
            return null;
        }
        #endregion

        #region Average BFI
        public static decimal? LeanBodyMassAverage(this PersonModel model)
        {
            if (model.GetLeanBodyMass().HasValue && model.GetBodyFatWeightUSNavy().HasValue)
            {
                return (model.GetLeanBodyMass().GetValueOrDefault() + model.GetLeanBodyMassUSNavy().GetValueOrDefault()) / 2.0M;
            }
            else
                return null;
        }

        public static decimal? GetBodyFatWeightAverage(this PersonModel model)
        {
            if (model.GetBodyFatWeight().HasValue && model.GetBodyFatWeightUSNavy().HasValue)
            {
                return (model.GetBodyFatWeight().GetValueOrDefault() + model.GetBodyFatWeightUSNavy().GetValueOrDefault()) / 2.0M;
            }
            
            return null;
        }

        public static decimal? GetBFIAverage(this PersonModel model)
        {
            return (model.BFI.GetValueOrDefault() + model.USNavyBFI.GetValueOrDefault()) / 2.0M;
        }
        #endregion

        #region "Protected" Properties
        public static decimal? GetLeanBodyMassInKg(this PersonModel model)
        {
            if (model.WaistLength.HasValue)
                return ConversionHelper.Pounds_to_kg(model.LeanBodyMassInPounds.GetValueOrDefault());
            else
                return null;
        }

        public static decimal? GetBodyFatWeightInKg(this PersonModel model)
        {
            if (model.WaistLength.HasValue)
                return ConversionHelper.Pounds_to_kg(model.BodyFatWeightInPounds.GetValueOrDefault());
            else
                return null;
        }

        public static decimal GetWeightInPounds(this PersonModel model)
        {
            return model.UOFM == UnitOfMeasureEnum.Imperial
                ? model.Weight
                : ConversionHelper.KG_to_pounds(model.Weight);
        }

        public static decimal GetWeightInKilograms(this PersonModel model)
        {
            return model.UOFM == UnitOfMeasureEnum.Imperial
                ? ConversionHelper.Pounds_to_kg(model.Weight)
                : model.Weight;
        }

        public static decimal GetHeightInIN(this PersonModel model)
        {
            return model.UOFM == UnitOfMeasureEnum.Imperial
                ? model.Height
                : ConversionHelper.CM_to_IN(model.Height);
        }

        public static decimal GetHeightInCM(this PersonModel model)
        {
            return model.UOFM == UnitOfMeasureEnum.Imperial
                ? ConversionHelper.IN_to_CM(model.Height)
                : model.Height;
        }

        public static decimal? GetWaistLengthInIN(this PersonModel model)
        {
            if (!model.WaistLength.HasValue)
                return null;

            return model.UOFM == UnitOfMeasureEnum.Imperial
                ? model.WaistLength
                : ConversionHelper.CM_to_IN(model.WaistLength.GetValueOrDefault());
        }

        public static decimal? GetWaistLengthInCM(this PersonModel model)
        {
            if (!model.WaistLength.HasValue)
                return null;

            return model.UOFM == UnitOfMeasureEnum.Metric
                ? model.WaistLength
                : ConversionHelper.IN_to_CM(model.WaistLength.GetValueOrDefault());
        }

        public static decimal? GetNeckLengthInIN(this PersonModel model)
        {
            if (!model.NeckLength.HasValue)
                return null;

            return model.UOFM == UnitOfMeasureEnum.Imperial
                ? model.NeckLength
                : ConversionHelper.CM_to_IN(model.NeckLength.GetValueOrDefault());
        }

        public static decimal? GetNeckLengthInCM(this PersonModel model)
        {
            if (!model.NeckLength.HasValue)
                return null;

            return model.UOFM == UnitOfMeasureEnum.Metric
                ? model.NeckLength
                : ConversionHelper.IN_to_CM(model.NeckLength.GetValueOrDefault());
        }
        #endregion
    }
}
