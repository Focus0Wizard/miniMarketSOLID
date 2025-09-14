namespace miniMarketSolid.Domain.Entities
{
    public interface Descuento
    {
        float AplicarDescuento(float montoTotal)
        {
            return montoTotal;
        }
    }
}
