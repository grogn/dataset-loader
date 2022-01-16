using System;
using System.ComponentModel.DataAnnotations;

namespace DatasetLoader.Entities
{
    /// <summary>
    /// Базовый объект данных.
    /// </summary>
    public abstract class BaseDataObject
    {
        /// <summary>
        /// Первичный ключ.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}