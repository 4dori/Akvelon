﻿namespace Akvelon.Models
{
    public class UpdateTaskRequest
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}
