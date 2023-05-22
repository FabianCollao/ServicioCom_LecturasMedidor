namespace ServicioComunicacionModel.DTO
{
    public class Lectura
    {
        private int nroMedidor;
        private string fecha;
        private decimal valorConsumo;

        public int NroMedidor { get => nroMedidor; set => nroMedidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public decimal ValorConsumo { get => valorConsumo; set => valorConsumo = value; }

        public override string ToString()
        {
            return nroMedidor + "|"  + fecha + "|"+ valorConsumo ;
        }
    }
}
