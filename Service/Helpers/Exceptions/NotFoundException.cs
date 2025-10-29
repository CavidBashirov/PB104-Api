namespace Service.Helpers.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msj = null) : base(msj) { }
        
    }
}
