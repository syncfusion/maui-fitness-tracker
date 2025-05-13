namespace FitnessTracker
{
    /// <summary>
    /// Represents personal information of a user, including name, date of birth, email, and password.
    /// </summary>
    public class PersonalInfo 
    {
        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string? Name { get; set; } = "David William";

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get ; set ; }= "David";

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get ; set ; } = "William";

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateTime? DateOfBirth { get; set ; } = new DateTime(2000, 1, 1);

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string? Email { get; set; } = "davidwilliam@gmail.com";

        /// <summary>
        /// Gets or sets the password for the user's account.
        /// </summary>
        public string? Password { get; set; } = "DavidWM@1102";
    }

    /// <summary>
    /// Represents the physical attributes of a user, such as height, weight, gender, and activity level.
    /// </summary>
    public class PhysicalInfo 
    {
        /// <summary>
        /// Gets or sets the height of the user.
        /// </summary>
        public string? Height {  get; set; } = "162";

        /// <summary>
        /// Gets or sets the weight of the user.
        /// </summary>
        public string? Weight { get; set; } = "63";

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public string? Gender { get; set; } = "Male";

        /// <summary>
        /// Gets or sets the body fat level of the user.
        /// </summary>
        public string? BodyFat { get; set; } = "Medium";

        /// <summary>
        /// Gets or sets the activity status of the user.
        /// </summary>
        public string? ActiveStatus { get; set; } = "Moderately Active";

        /// <summary>
        /// Gets or sets the measurement unit used for height and weight.
        /// </summary>
        public string? MeasurementUnit { get; set; } = "Cm";
    }

}
