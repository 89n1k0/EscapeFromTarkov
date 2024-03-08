using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace EscapeFromTarkov.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardAPI : ControllerBase
    {
        EscapeFromTarkovContext db = new EscapeFromTarkovContext();

        [HttpPost]
        [Route("Card/addCard")]
        public async Task<IActionResult> CardAdd(string name, string description, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Файл не выбран");
            }
            else
            {
                if (name == "" || description == "")
                {
                    return BadRequest("Не все данные заполнены");
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        byte[] imageBytes = memoryStream.ToArray();
                        Карта босс = new Карта()
                        {
                            Наименование = name,
                            Описание = description,
                            Изображение = imageBytes
                        };
                        db.Картаs.Add(босс);
                        db.SaveChanges();
                        return Ok("Данные добавлены");
                    }
                }
            }
        }

        [HttpPut]
        [Route("Card/updateCard")]
        [SwaggerOperation("Загрузить изображение")]
        [SwaggerResponse(200, "Изображение успешно загружено")]
        public async Task<IActionResult> CardUpdate(int id, string name, string description, IFormFile file)
        {
            var card = db.Картаs.Where(x => x.КартаId == id).FirstOrDefault();
            if (card == null)
            {
                return NotFound("Даныый босс не найден");
            }
            else
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Файл не выбран");
                }
                else
                {
                    if (name == "" || description == "")
                    {
                        return BadRequest("Не все данные заполнены");
                    }
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            byte[] imageBytes = memoryStream.ToArray();
                            card = db.Картаs.Where(x => x.КартаId == id).FirstOrDefault();
                            card.Наименование = name;
                            card.Описание = description;
                            card.Изображение = imageBytes;
                            db.SaveChanges();
                            return Ok("Данные добавлены");
                        }
                    }
                }
            }
        }
        [HttpDelete]
        [Route("Card/deleteCard")]
        [SwaggerOperation("Загрузить изображение")]
        [SwaggerResponse(200, "Изображение успешно загружено")]
        public async Task<IActionResult> CardDelete(int id)
        {
            var card = db.Картаs.Where(x => x.КартаId == id).FirstOrDefault();
            if (card == null)
            {
                return NotFound("Даныый босс не найден");
            }
            else
            {
                card = db.Картаs.Where(x => x.КартаId == id).FirstOrDefault();
                db.Картаs.Remove(card);
                db.SaveChanges(true);
                return Ok("Удаление совершено успешно");
            }
        }
    }
}
