namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Impuestos
    {
         
        public int id { get; set; }

        [StringLength(2)]
        public string codigo { get; set; }

        [StringLength(2)]
        public string codigoTarifa { get; set; }

        public decimal? tarifa { get; set; }
    }
}
