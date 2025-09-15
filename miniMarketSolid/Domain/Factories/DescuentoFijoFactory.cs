using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Domain.Factories
{
    public sealed class DescuentoFijoFactory : DescuentoFactory
    {
        private readonly decimal montoFijo;
        public DescuentoFijoFactory(decimal montoFijo) => this.montoFijo = montoFijo;
        public override IDescuento CrearDescuento() => new DescuentoFijo(montoFijo);
    }
}
