using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lollapalooza.Services.Enumerations
{
    public enum ShowsDay
    {
        [Description("Sexta")]
        Friday,
        [Description("Sábado")]
        Saturday,
        [Description("Domingo")]
        Sunday
    }

    public enum ShowsStage
    {
        [Description("BudWeiser!")]
        BudWeiser,
        [Description("Onix!")]
        Onix,
        [Description("Axe!")]
        Axe,
        [Description("Perry's!")]
        Perrys,
    }
}
