using System;

namespace ProjectFocus.DataModel
{
    public class CreateProblemDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
