namespace miniMarketSolid.Domain.Entities
{
    public class Cliente
    {
        #region Atributos
        private int idCliente;
        private string nombre;
        private string email;
        private int telefono;
        #endregion

        #region Propiedades
        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public int Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        #endregion

        #region Constructores
        public Cliente()
        {
               
        }
        public Cliente(int idCliente, string nombre, string email, int telefono)
        {
            this.idCliente = idCliente;
            this.nombre = nombre;
            this.email = email;
            this.telefono = telefono;
        }
        #endregion
    }
}
