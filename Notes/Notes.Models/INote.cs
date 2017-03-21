using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public interface INote
    {
        Guid Id { get; set; }

        string Title { get; set; }

        string Content { get; set; }

        DateTime CreatedUtc { get; set; }

    }
}
