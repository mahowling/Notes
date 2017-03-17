using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Entities.Models
{
    public class Note
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
