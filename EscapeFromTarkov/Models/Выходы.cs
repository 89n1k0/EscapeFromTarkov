using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Выходы
    {
        public int ВыходыId { get; set; }
        public string Наименование { get; set; } = null!;
        public int КартаId { get; set; }
        public int ПерсонажиId { get; set; }

        public virtual Карта Карта { get; set; } = null!;
        public virtual Персонажи Персонажи { get; set; } = null!;
    }
}
