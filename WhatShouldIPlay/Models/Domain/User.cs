using System.ComponentModel.DataAnnotations;

namespace WhatShouldIPlay.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(128)]
        public string Email { get; set; }
        [Required, MaxLength(64)]
        public string BasicPass { get; set; }
        public string EncryptedPass { get; set; }
        public string Salt { get; set; }
    }
}