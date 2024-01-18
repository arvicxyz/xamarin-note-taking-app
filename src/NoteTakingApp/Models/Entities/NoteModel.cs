using System;
using System.Collections.Generic;

namespace NoteTakingApp.Models.Entities
{
    public class NoteModel : EntityRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public bool IsFavourite { get; set; }
    }

    public class NoteListModel : EntityRecord
    {
        public List<NoteModel> Notes { get; set; }
    }
}
