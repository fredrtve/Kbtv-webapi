using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
    public class MissionImageUploader : IMissionImageUploader
    {
        private readonly IAsyncRepository<MissionImage> _imageRepository;
        private readonly IBlobStorageService _storageService;

        public MissionImageUploader(
            IAsyncRepository<MissionImage> imageRepository,
            IBlobStorageService storageService)
        {
            _imageRepository = imageRepository;
            _storageService = storageService;
        }
        public async Task<IEnumerable<MissionImage>> UploadCollection(IFormFileCollection files, int missionId)
        {
            try
            {
                var imageURIs = await _storageService.UploadAsync(files);
                var images = new List<MissionImage>();
                foreach (var uri in imageURIs)
                {
                    images.Add(new MissionImage() { MissionId = missionId, FileURL = uri });
                }
                await _imageRepository.AddRangeAsync(images);

                return images;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> DeleteImage(int id)
        {
            var missionImage = await _imageRepository.GetByIdAsync(id);
            if (missionImage == null) return false;

            await _imageRepository.DeleteAsync(missionImage);

            await _storageService.DeleteAsync(missionImage.FileURL.ToString());

            return true;
        }
    }
}
