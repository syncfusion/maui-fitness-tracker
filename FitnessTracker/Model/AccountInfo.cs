namespace FitnessTracker
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonalInfo 
    {
        public string? Name { get; set; } = "David William";

        public string? FirstName { get ; set ; }= "David";

        public string? LastName { get ; set ; } = "William";

        public DateTime? DateOfBirth { get; set ; } = new DateTime(2000, 1, 1);

        public string? Email { get; set; } = "davidwilliam@gmail.com";

        public string? Password { get; set; } = "1234";
    }

    /// <summary>
    /// 
    /// </summary>
    public class PhysicalInfo 
    {
        public string? Height {  get; set; } = "162";
        public string? Weight { get; set; } = "63";

        public string? Gender { get; set; } = "Male";

        public string? BodyFat { get; set; } = "Medium";

        public string? ActiveStatus { get; set; } = "Moderately Active";

        public string? MeasurementUnit { get; set; } = "Cm";
    }

}
