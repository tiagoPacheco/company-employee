namespace challenge_samsung.Services
{
    interface IAllocationService
    {
        /// <summary>
        /// Will allocate all employees in Team. The team won't be balanced.
        /// </summary>
        void Allocate();
    }
}
