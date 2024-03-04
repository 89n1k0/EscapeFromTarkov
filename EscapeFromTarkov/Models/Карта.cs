using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Карта
    {
        public Карта()
        {
            Выходыs = new HashSet<Выходы>();
            Пользовательs = new HashSet<Пользователь>();
        }

        public int КартаId { get; set; }
        public string Наименование { get; set; } = null!;
        public string? Описание { get; set; }
        public byte[]? Изображение { get; set; }

        public virtual ICollection<Выходы> Выходыs { get; set; }
        public virtual ICollection<Пользователь> Пользовательs { get; set; }
    }
}
