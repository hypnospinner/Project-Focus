using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProjectFocus.Model;

namespace ProjectFocus.Data
{
    /// <summary>
    /// Responsible for saving and reading data at runtime
    /// </summary>
    public class DataManager
    {
        public IEnumerable<ProblemModel> GetMockProblemList()
        {
            string jsonString;
            using (var reader = new StreamReader("mock.json"))
                jsonString = reader.ReadToEnd();

            var problemList = JsonConvert.DeserializeObject<ProblemModel[]>(jsonString);
            return problemList;
        }

        public void SaveMockProblemList(IEnumerable<ProblemModel> problemList)
        {
            var jsonString = JsonConvert.SerializeObject(problemList);
            File.WriteAllText("mock.json", jsonString);
        }
    }
}
