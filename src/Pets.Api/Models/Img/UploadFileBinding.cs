using System;

using FluentValidation;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pets.Api.Models.Img
{
    /// <summary>
    /// модель для заливки картинок
    /// </summary>
    public sealed class UploadFileBinding
    {
        /// <summary>
        /// Картинка для заливки
        /// jpeg / png
        /// и не более 10 мегабайт
        /// </summary>
        [FromForm]
        public IFormFile File { get; set; }
    }

    /// <summary>
    /// валидатор модели для заливки картинки
    /// </summary>
    public sealed class UploadFileBindingValidator : AbstractValidator<UploadFileBinding>
    {
        public UploadFileBindingValidator()
        {
            // файл должен быть обязательно
            RuleFor(_ => _.File)
                .NotNull();
            // только картинка
            RuleFor(_ => _.File.ContentType)
                .NotEmpty()
                .Must(x => x.Equals("image/jpeg") ||
                           x.Equals("image/jpg") ||
                           x.Equals("image/png"));
            // не более 10 мегабайт
            RuleFor(_ => _.File.Length)
                .LessThanOrEqualTo(10 * 1024 * 1024)
                .WithMessage("Размер файла не должен превышать 10 мб");
        }
    }
}