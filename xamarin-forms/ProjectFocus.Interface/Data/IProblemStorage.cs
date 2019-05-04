using ProjectFocus.DataModel;
using System;

namespace ProjectFocus.Interface.Data
{
    public interface IProblemStorage
    {
        /// <summary>
        /// Create a new problem record
        /// </summary>
        /// <param name="createProblem"></param>
        /// <returns>A reference ID by which the problem
        /// can later be identified in the system</returns>
        Guid CreateProblem(CreateProblemDto createProblem);
    }
}
