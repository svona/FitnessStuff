namespace FitnessStuff.Models
{
    public abstract class PersonModel
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public int AgeInYears { get; set; }
        public decimal? WaistLength { get; set; }
        public decimal? NeckLength { get; set; }
        public UnitOfMeasureEnum UOFM { get; set; }

        #region Heart Rate
        public int MaxHeartRatesBPM => 220 - this.AgeInYears;
        public decimal LowHeartRateBPM => this.MaxHeartRatesBPM * 0.5M;
        public decimal MidHeartRateBPM => this.MaxHeartRatesBPM * 0.7M;
        public decimal HighHeartRateBPM => this.MaxHeartRatesBPM * 0.85M;
        #endregion

        public abstract decimal OriginalHarrisBenedictBMR { get; }
        public abstract decimal RevisedHarrisBenedictBMR { get; }
        public abstract decimal MifflinStJeorBMR { get; }
        public abstract decimal BMR { get; }
        public abstract decimal? LeanBodyMassInPounds { get; }
        public abstract decimal? BodyFatWeightInPounds { get; }
        public abstract decimal? BFI { get; }
        public abstract decimal? USNavyBFI { get; }
    }
}