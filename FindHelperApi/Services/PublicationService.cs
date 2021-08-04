using FindHelperApi.Data;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace FindHelperApi.Services
{
    public class PublicationService
    {
        private readonly FindHelperApiContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public PublicationService(FindHelperApiContext context, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<GETPublicationDTO> SaveAsync(CreatePublicationDTO publicationDTO/*, IFormFile file*/)
        {
            var newPublication = new Publication();

            newPublication.Description = publicationDTO.description;
            newPublication.Date = publicationDTO.date;
            //newPublication.Photo = SaveImage(file);
            newPublication.UserId = publicationDTO.userId;

            _context.Publications.Add(newPublication);
            await _context.SaveChangesAsync();

            var getPublicationDTO = new GETPublicationDTO();

            getPublicationDTO.Id = newPublication.Id;
            getPublicationDTO.Description = newPublication.Description;
            getPublicationDTO.Data = newPublication.Date;
            //getPublicationDTO.Photo = newPublication.Photo;
            getPublicationDTO.UserId = newPublication.UserId;
            
            return getPublicationDTO;
        }

        public async Task<List<GETPublicationDTO>> FindAllAsync()
        {
            var publications = await _context.Publications.ToListAsync();

            var listGetPublicationDto = new List<GETPublicationDTO>();

            for (int i = 0; i < publications.Count(); i++)
            {
                var newGetPublicationDto = new GETPublicationDTO();

                newGetPublicationDto.Id = publications[i].Id;
                newGetPublicationDto.Data = publications[i].Date;
                newGetPublicationDto.Description = publications[i].Description;
                //newGetPublicationDto.Photo = GetImageFromWwwroot(publications[i].Photo);
                newGetPublicationDto.UserId = publications[i].UserId;

                listGetPublicationDto.Add(newGetPublicationDto);
            }

            return listGetPublicationDto;
        }

        //public string SaveImage(IFormFile objectFile)
        //{
        //    try
        //    {
        //        if (objectFile.Length > 0)
        //        {
        //            string path = _webHostEnvironment.WebRootPath + "\\images\\";

        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }

        //            if (objectFile.Length == 0)
        //                return objectFile.ToString();

        //            using (FileStream fileStream = File.Create(path + objectFile.FileName))
        //            {
        //                objectFile.CopyTo(fileStream);
        //                fileStream.Flush();
        //                return objectFile.FileName;
        //            }
        //        }
        //        else
        //        {
        //            return "Not uploaded";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        ////https://stackoverflow.com/questions/42587428/get-image-from-wwwroot-images-in-asp-net-core
        //public string GetImageFromWwwroot(string image)
        //{
        //    var pathServer = _configuration.GetValue<string>("MySettings:PathsServer");
        //    var path = "https://" + pathServer + "/images/" + image;
        //    return path;
        //}
    }
}


