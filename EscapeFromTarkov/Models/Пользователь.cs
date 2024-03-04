using System;
using System.Collections.Generic;

namespace EscapeFromTarkov
{
    public partial class Пользователь
    {
        public int ПользовательId { get; set; }
        public string Логин { get; set; } = null!;
        public string Пароль { get; set; } = null!;
        public int? Выживания { get; set; }
        public int? Смерти { get; set; }
        public int? ПотерянБезвести { get; set; }
        public int? КоличествоРейдов { get; set; }
        public int? Убийства { get; set; }
        public int? УбийстваЧвк { get; set; }
        public byte[]? Доказательство { get; set; }
        public bool? Онлайн { get; set; }
        public int? КартаId { get; set; }
        public int? РолиId { get; set; }

        public virtual Карта? Карта { get; set; }
        public virtual Роли? Роли { get; set; }
    }
}
