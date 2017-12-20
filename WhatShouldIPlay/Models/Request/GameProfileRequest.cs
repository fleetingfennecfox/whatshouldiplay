using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WhatShouldIPlay.Models.Domain;

namespace WhatShouldIPlay.Models.Request
{
    public class GameProfileRequest
    {
        public int Id { get; set; }
        [Required, MaxLength(128)]
        public string Title { get; set; }
        [Required]
        public int[] Platforms { get; set; }
        [Required]
        public int[] Genres { get; set; }
        [Required]
        public int Studio { get; set; }
        public int[] Directors { get; set; }
    }
}