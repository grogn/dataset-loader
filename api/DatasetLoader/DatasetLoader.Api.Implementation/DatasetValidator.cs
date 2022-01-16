using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DatasetLoader.Api.Abstractions;
using DatasetLoader.Api.Dto;

namespace DatasetLoader.Api.Implementation
{
    public class DatasetValidator: IDatasetValidator
    {
        public async Task<List<ValidationError>> Validate(UploadDatasetModuleDto datasetModuleDto)
        {
            var errors = new List<ValidationError>();
            var range = 0;
            range += datasetModuleDto.IsCyrillic ? 1 : 0;
            range += datasetModuleDto.IsLatin ? 1 : 0;
            range += datasetModuleDto.IsNumeric ? 1 : 0;
            range += datasetModuleDto.IsSpecialSymbols ? 1 : 0;
            range += datasetModuleDto.IsRegisterSpecific ? 1 : 0;
            var minCount = 2000 + range * 3000;
            var maxCount = minCount + 1000;

            using var archive = new ZipArchive(datasetModuleDto.File.OpenReadStream(), ZipArchiveMode.Read);
            var imagesCount = archive.Entries.Count - (datasetModuleDto.AnswersLocation == AnswersLocation.File ? 1 : 0);

            if (imagesCount < minCount || imagesCount > maxCount)
            {
                errors.Add(new ValidationError("File",
                    $"Количество картинок должно находиться в диапазоне от {minCount} до {maxCount}."));
            }

            if (datasetModuleDto.AnswersLocation != AnswersLocation.File) 
                return errors;
            
            var answers = archive.Entries.FirstOrDefault(x => x.Name == "answers.txt");
            if (answers == null)
            {
                errors.Add(new ValidationError("File", "В архиве нет файла с ответами."));
            }
            else
            {
                using var streamReader = new StreamReader(answers.Open());
                var answersCount = 0;
                while (await streamReader.ReadLineAsync() != null)
                {
                    answersCount++;
                }

                if (imagesCount != answersCount)
                {
                    errors.Add(new ValidationError("File",
                        "Количество картинок не совпадает с количеством ответов в файле."));
                }
            }

            return errors;
        }
    }
}