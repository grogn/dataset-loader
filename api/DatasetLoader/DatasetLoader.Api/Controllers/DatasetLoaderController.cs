using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using DatasetLoader.Api.Abstractions;
using DatasetLoader.Api.Dto;
using DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule;
using DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule.Dto;
using DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules;
using DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatasetLoader.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatasetLoaderController : ControllerBase
    {
        private readonly IDatasetSaver _datasetSaver;

        private readonly IDatasetValidator _datasetValidator;
        
        private readonly ISender _sender;
        
        private readonly IMapper _mapper;
        
        private readonly ILogger<DatasetLoaderController> _logger;

        public DatasetLoaderController(
            IDatasetSaver datasetSaver, 
            IDatasetValidator datasetValidator, 
            ISender sender, 
            IMapper mapper, 
            ILogger<DatasetLoaderController> logger)
        {
            _datasetSaver = datasetSaver;
            _datasetValidator = datasetValidator;
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Upload")]
        [RequestSizeLimit(1024 * 1024 * 1024)]
        public async Task<IActionResult> Upload([FromForm]UploadDatasetModuleDto datasetModuleDto)
        {
            try
            {
                if (!Regex.IsMatch(datasetModuleDto.Name, @"^[A-Za-z]+$"))
                {
                    ModelState.AddModelError(nameof(datasetModuleDto.Name), "Имя должно содержать только латинские буквы.");
                }

                if (datasetModuleDto.Name.IndexOf("captcha", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    ModelState.AddModelError(nameof(datasetModuleDto.Name), "Имя не может содержать слово 'captcha'.");
                }

                if (!datasetModuleDto.IsCyrillic && !datasetModuleDto.IsLatin && !datasetModuleDto.IsNumeric)
                {
                    ModelState.AddModelError("Content",
                        "Должно быть выбрано как минимум одно из 'Содержит кириллицу', 'Содержит латиницу','Содержит цифры'.");
                }

                foreach (var validationError in await _datasetValidator.Validate(datasetModuleDto))
                {
                    ModelState.AddModelError(validationError.Key, validationError.Error);
                }
            
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
            
                var createDatasetModuleDto = _mapper.Map<CreateDatasetModuleDto>(datasetModuleDto);
                createDatasetModuleDto.Date = DateTime.Now;
                createDatasetModuleDto.DatasetPath = _datasetSaver.Save(datasetModuleDto.File.OpenReadStream());
                await _sender.Send(new CreateDatasetModuleCommand
                {
                    Dto = createDatasetModuleDto
                });
            
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при загрузке набора данных.");
                return BadRequest("Ошибка при загрузке набора данных.");
            }
        }

        [HttpGet("Modules")]
        public async Task<ActionResult<List<DatasetModuleDto>>> GetModules()
        {
            try
            {
                var modules = await _sender.Send(new GetDatasetModulesQuery());
                return Ok(modules);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при получении модулей.");
                return BadRequest();
            }
        }
    }
}