using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.IO;

namespace EscapeFromTarkov.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BossAPI : ControllerBase
    {
        EscapeFromTarkovContext db = new EscapeFromTarkovContext();

        [HttpPost]
        [Route("Boss/addBoss")]
        public async Task<IActionResult> BossAdd(string name, string description, IFormFile file)
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
                        Босс босс = new Босс()
                        {
                            Наименование = name,
                            Описание = description,
                            Изображение = imageBytes
                        };
                        db.Боссs.Add(босс);
                        db.SaveChanges();
                        return Ok("Данные добавлены");
                    }
                }
            }
        }

        [HttpPut]
        [Route("Boss/updateBoss")]
        [SwaggerOperation("Загрузить изображение")]
        [SwaggerResponse(200, "Изображение успешно загружено")]
        public async Task<IActionResult> BossUpdate(int id, string name, string description, IFormFile file)
        {
            
            var boss = db.Боссs.Where(x => x.БоссId == id).FirstOrDefault();
            if (boss == null)
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
                            boss = db.Боссs.Where(x => x.БоссId == id).FirstOrDefault();
                            boss.Наименование = name;
                            boss.Описание = description;
                            boss.Изображение = imageBytes;
                            db.SaveChanges();
                            return Ok("Данные добавлены");
                        }
                    }
                }
            }
        }
        [HttpDelete]
        [Route("Boss/deleteBoss")]
        public async Task<IActionResult> BossDelete(int id)
        {
            var boss = db.Боссs.Where(x => x.БоссId == id).FirstOrDefault();
            if (boss == null)
            {
                return NotFound("Даныый босс не найден");
            }
            else
            {
                boss = db.Боссs.Where(x => x.БоссId == id).FirstOrDefault();
                db.Боссs.Remove(boss);
                db.SaveChanges(true);
                return Ok("Удаление совершено успешно");
            }
        }
    }
}
