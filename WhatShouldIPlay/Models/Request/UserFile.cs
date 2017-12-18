namespace WhatShouldIPlay.Models.Request
{
    public class UserFile
    {
        public int UserId { get; set; }
        public byte[] ByteArray { get; set; }
        public string UserFileName { get; set; }
        public string Extension { get; set; }
        public string SaveLocation { get; set; }
    }
}