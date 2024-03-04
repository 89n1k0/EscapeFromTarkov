using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Персонажи
    {
        public Персонажи()
        {
            Выходыs = new HashSet<Выходы>();
        }

        public int ПерсонажиId { get; set; }
        public string Наименование { get; set; } = null!;
        public string? Описание { get; set; }
        public byte[]? Изображение { get; set; }

        public virtual ICollection<Выходы> Выходыs { get; set; }
    }
}
