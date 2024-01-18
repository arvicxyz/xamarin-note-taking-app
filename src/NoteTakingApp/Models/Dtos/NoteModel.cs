using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NoteTakingApp.Models.Dtos
{
    public class NoteModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("date_last_updated")]
        public DateTime DateLastUpdated { get; set; }

        [JsonPropertyName("is_favourite")]
        public bool IsFavourite { get; set; }
    }

    public class NoteListModel
    {
        [JsonPropertyName("data")]
        public List<NoteModel> Notes { get; set; }
    }
}
