using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Сборка
    {
        public int СборкаId { get; set; }
        public string Наименование { get; set; } = null!;
        public byte[]? Изображение { get; set; }
        public int ОружиеId { get; set; }

        public virtual Оружие Оружие { get; set; } = null!;
    }
}
