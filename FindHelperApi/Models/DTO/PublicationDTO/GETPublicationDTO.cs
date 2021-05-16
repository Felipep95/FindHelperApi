using System;

namespace FindHelperApi.Models.DTO
{
    public class GETPublicationDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Data { get; set; }
        public string Photo { get; set; }
        public int UserId { get; set; }

    }
}
