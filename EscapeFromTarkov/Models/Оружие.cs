using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Оружие
    {
        public Оружие()
        {
            Сборкаs = new HashSet<Сборка>();
        }

        public int ОружиеId { get; set; }
        public string Наименование { get; set; } = null!;

        public virtual ICollection<Сборка> Сборкаs { get; set; }
    }
}
