namespace SealedClassAPI.Models
{
    /// <summary>
    /// class which can manage the schema of child 
    /// </summary>
    public class Child
    {
        #region Public Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Parents Parent { get; set; }
        #endregion
    }
}