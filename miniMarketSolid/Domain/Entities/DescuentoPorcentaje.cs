using System;

namespace miniMarketSolid.Domain.Entities
{
    public sealed class DescuentoPorcentaje : IDescuento
    {
        private readonly decimal porcentaje;

        public DescuentoPorcentaje(decimal porcentaje)
        {
            this.porcentaje = porcentaje;
        }

        public decimal AplicarDescuento(decimal montoTotal)
        {
            decimal factor = porcentaje / 100m;
            decimal descuentoCalculado = montoTotal * factor;
            decimal total = montoTotal - descuentoCalculado;
            return total;
        }
    }
}
