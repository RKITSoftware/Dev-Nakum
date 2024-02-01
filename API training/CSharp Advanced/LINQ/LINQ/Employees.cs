using System.Data;

namespace LINQ
{
    /// <summary>
    /// class which is derived from DataTable and create column 
    /// </summary>
    public class Employees : DataTable
    {
        #region Constructor
        public Employees()
        {
            this.Columns.Add("Id", typeof(int));
            this.Columns.Add("Name", typeof(string));
            this.Columns.Add("Age", typeof(int));
            this.Columns.Add("Department", typeof(string));
            this.Columns.Add("Salary", typeof(int));
        }
        #endregion
    }
}
