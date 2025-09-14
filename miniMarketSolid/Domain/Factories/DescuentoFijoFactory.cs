using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Domain.Factories
{
    public sealed class DescuentoFijoFactory : DescuentoFactory
    {
        private readonly decimal montoFijo;
        public DescuentoFijoFactory(decimal montoFijo) => this.montoFijo = montoFijo;
        public override Descuento CrearDescuento() => new DescuentoFijo(montoFijo);
    }
}
