using miniMarketSolid.Domain.Entities;

namespace miniMarketSolid.Domain.Factories
{
    public sealed class DescuentoPorcentajeFactory : DescuentoFactory
    {
        private readonly decimal porcentaje;
        public DescuentoPorcentajeFactory(decimal porcentaje) => this.porcentaje = porcentaje;
        public override IDescuento CrearDescuento() => new DescuentoPorcentaje(porcentaje);
    }
}
