using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Босс
    {
        public int БоссId { get; set; }
        public string Наименование { get; set; } = null!;
        public string? Описание { get; set; }
        public byte[]? Изображение { get; set; }
    }
}
