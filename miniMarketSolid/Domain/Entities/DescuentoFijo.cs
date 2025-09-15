using System;

namespace miniMarketSolid.Domain.Entities
{
    public sealed class DescuentoFijo : IDescuento
    {
        private readonly decimal montoFijo;

        public DescuentoFijo(decimal montoFijo)
        {
            this.montoFijo = montoFijo;
        }

        public decimal AplicarDescuento(decimal montoTotal)
        {
            decimal resultado = montoTotal - montoFijo;
            if (resultado < 0m)
            {
                return 0m;
            }
            return resultado;
        }
    }
}
