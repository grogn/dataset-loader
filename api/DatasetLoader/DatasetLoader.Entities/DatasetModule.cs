using System;
using System.ComponentModel.DataAnnotations;

namespace DatasetLoader.Entities
{
    /// <summary>
    /// Модуль набора данных.
    /// </summary>
    public class DatasetModule: BaseDataObject
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime Date { get; set; }

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
        public AnswersLocation AnswersLocation { get; set; }
        
        /// <summary>
        /// Локальный путь до архива с набором данных.
        /// </summary>
        public string DatasetPath { get; set; }
    }
}