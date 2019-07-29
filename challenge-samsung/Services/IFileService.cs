namespace challenge_samsung.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Reads the Team file and store on global class
        /// </summary>
        /// <param name="file">Team file</param>
        void LoadFileTeam(string file);

        /// <summary>
        /// Reads the Employee file and store on global class
        /// </summary>
        /// <param name="file">Employees file</param>
        void LoadFileEmployee(string file);
    }
}
