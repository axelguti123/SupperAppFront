namespace SupperApp.Models
{
    public class Response
    {
        public string Status {  get; set; }
        public string Message { get; set; }

    }
    public class Response<TEntity> : Response
    {
        public TEntity Data { get; set; }
    }
}
