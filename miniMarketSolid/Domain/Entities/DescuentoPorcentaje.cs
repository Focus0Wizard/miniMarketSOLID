namespace miniMarketSolid.Domain.Entities
{
    public sealed class DescuentoPorcentaje : Descuento
    {
        private readonly decimal porcentaje;

        public DescuentoPorcentaje(decimal porcentaje)
        {
            if (porcentaje < 0m || porcentaje > 100m)
                throw new ArgumentOutOfRangeException(nameof(porcentaje), "Debe ser de 0 a 100");
            this.porcentaje = porcentaje;
        }

        public decimal AplicarDescuento(decimal montoTotal)
            => montoTotal - (montoTotal * (porcentaje / 100m));
    }
}
