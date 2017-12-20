using System.ComponentModel.DataAnnotations;

namespace WhatShouldIPlay.Models.Domain
{
    public class GameProfile
    {
        public int Id { get; set; }
        [Required, MaxLength(128)]
        public string Title { get; set; }
    }
}