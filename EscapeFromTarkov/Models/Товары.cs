using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Товары
    {
        public string Udid { get; set; } = null!;
        public string Название { get; set; } = null!;
        public decimal? Цена { get; set; }
        public decimal? Цена24часа { get; set; }
        public decimal? Цена7дней { get; set; }
        public string Торговец { get; set; } = null!;
        public decimal ЦенаОбратногоВыкупа { get; set; }
        public string Единица { get; set; } = null!;
    }
}
