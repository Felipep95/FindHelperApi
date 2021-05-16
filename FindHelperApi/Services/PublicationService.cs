using Azure.Storage.Blobs;
using FindHelperApi.Data;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FindHelperApi.Services
{
    public class PublicationService
    {
        private readonly FindHelperApiContext _context;
        public static IWebHostEnvironment _webHostEnvironment;

        public PublicationService(FindHelperApiContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<GETPublicationDTO> SaveAsync(CreatePublicationDTO publicationDTO, IFormFile file)
        {
            var newPublication = new Publication();

            newPublication.Description = publicationDTO.description;
            newPublication.Date = publicationDTO.date;
            newPublication.Photo = SaveImage(file);
            newPublication.UserId = publicationDTO.userId;

            _context.Publications.Add(newPublication);
            await _context.SaveChangesAsync();
            
           // _context.Publications.Where(p => p.Id == newPublication.Id);

            var getPublicationDTO = new GETPublicationDTO();

            getPublicationDTO.Data = newPublication.Date;
            getPublicationDTO.Description = newPublication.Description;
            getPublicationDTO.Photo = newPublication.Photo;
            getPublicationDTO.Id = newPublication.Id;

            return getPublicationDTO;
        }

        public async Task<List<Publication>> FindAllAsync() => await _context.Publications.ToListAsync();

        public string SaveImage(IFormFile objectFile)
        {
            try
            {
                if (objectFile.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\images\\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + objectFile.FileName))
                    {
                        objectFile.CopyTo(fileStream);
                        fileStream.Flush();
                        return objectFile.FileName;
                    }
                }
                else
                {
                    return "Not uploaded";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        //public string UploadBase64Image(string base64Image, string container)
        //{
        //    // Gera um nome randomico para imagem
        //    var fileName = Guid.NewGuid().ToString() + ".jpg";

        //    // Limpa o hash enviado
        //    var data = new Regex(@"^data:image/[a-z]+;base64,").Replace(base64Image, "");

        //    // Gera um array de Bytes
        //    byte[] imageBytes = Convert.FromBase64String(data);

        //    // Define o BLOB no qual a imagem será armazenada
        //    var blobClient = new BlobClient("SUA CONN STRING", container, fileName);

        //    // Envia a imagem
        //    using (var stream = new MemoryStream(imageBytes)) { blobClient.Upload(stream); }
        //}
    }
}


