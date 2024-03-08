using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace EscapeFromTarkov.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NpsAPI : ControllerBase
    {
        EscapeFromTarkovContext db = new EscapeFromTarkovContext();

        [HttpPost]
        [Route("NPS/addNPS")]
        public async Task<IActionResult> NpsAdd(string name, string description, IFormFile file)
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
                        Персонажи nps = new Персонажи()
                        {
                            Наименование = name,
                            Описание = description,
                            Изображение = imageBytes
                        };
                        db.Персонажиs.Add(nps);
                        db.SaveChanges();
                        return Ok("Данные добавлены");
                    }
                }
            }
        }

        [HttpPut]
        [Route("NPS/updateNPS")]
        public async Task<IActionResult> NpsUpdate(int id, string name, string description, IFormFile file1)
        {
            try
            {
                var nps = db.Персонажиs.Where(x => x.ПерсонажиId == id).FirstOrDefault();
                if (nps == null)
                {
                    return NotFound("Даныый nps не найден");
                }
                else
                {
                    if (file1 == null || file1.Length == 0)
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
                                await file1.CopyToAsync(memoryStream);
                                byte[] imageBytes = memoryStream.ToArray();
                                nps = db.Персонажиs.Where(x => x.ПерсонажиId == id).FirstOrDefault();
                                nps.Наименование = name;
                                nps.Описание = description;
                                nps.Изображение = imageBytes;
                                db.SaveChanges();
                                return Ok("Данные добавлены");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("NPS/deleteNPS")]
        public async Task<IActionResult> NpsDelete(int id)
        {
            var nps = db.Персонажиs.Where(x => x.ПерсонажиId == id).FirstOrDefault();
            if (nps == null)
            {
                return NotFound("Даныый nps не найден");
            }
            else
            {
                nps = db.Персонажиs.Where(x => x.ПерсонажиId == id).FirstOrDefault();
                db.Персонажиs.Remove(nps);
                db.SaveChanges(true);
                return Ok("Удаление совершено успешно");
            }
        }
    }
}
