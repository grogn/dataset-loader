using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DatasetLoader.Api.Dto
{
    public class UploadDatasetModuleDto
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Required(ErrorMessage = "Укажите имя набора данных.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Максимальная длина имени - от 4 до 8 символов.")]
        public string Name { get; set; }

        /// <summary>
        /// Содержит кириллицу.
        /// </summary>
        public bool IsCyrillic { get; set; }
        
        /// <summary>
        /// Содержит латиницу.
        /// </summary>
        public bool IsLatin { get; set; }
        
        /// <summary>
        /// Содержит цифры.
        /// </summary>
        public bool IsNumeric { get; set; }
        
        /// <summary>
        /// Содержит специальные символы.
        /// </summary>
        public bool IsSpecialSymbols { get; set; }
        
        /// <summary>
        /// Чувствительность к регистру.
        /// </summary>
        public bool IsRegisterSpecific { get; set; }
        
        /// <summary>
        /// Расположение ответов на картинки.
        /// </summary>
        [Required]
        public AnswersLocation AnswersLocation { get; set; }
        
        /// <summary>
        /// Файл.
        /// </summary>
        [Required(ErrorMessage = "Укажите архив с набором данных.")]
        public IFormFile File { get; set; }
    }
}