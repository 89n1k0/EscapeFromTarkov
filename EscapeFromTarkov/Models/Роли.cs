using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Роли
    {
        public Роли()
        {
            Пользовательs = new HashSet<Пользователь>();
        }

        public int РолиId { get; set; }
        public string Наименование { get; set; } = null!;

        public virtual ICollection<Пользователь> Пользовательs { get; set; }
    }
}
