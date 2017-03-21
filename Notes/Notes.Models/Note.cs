using System;

namespace Notes.Models
{
    public class Note : INote
    {

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedUtc { get; set; }

        public Note()
        {
            CreatedUtc = DateTime.UtcNow;
        }

    }
}
