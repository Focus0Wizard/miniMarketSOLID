namespace miniMarketSolid.Domain.Entities
{
    public sealed class DescuentoFijo : Descuento
    {
        private readonly decimal montoFijo;

        public DescuentoFijo(decimal montoFijo)
        {
            if (montoFijo < 0m)
                throw new ArgumentOutOfRangeException(nameof(montoFijo), "Debe ser mayor o igual a 0");
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
