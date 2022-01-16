namespace DatasetLoader.Entities
{
    /// <summary>
    /// Расположение ответов на картинки.
    /// </summary>
    public enum AnswersLocation
    {
        /// <summary>
        /// Отсутствуют.
        /// </summary>
        None,
        
        /// <summary>
        /// В именах файлов.
        /// </summary>
        FileName,
        
        /// <summary>
        /// В отдельном файле.
        /// </summary>
        File
    }
}