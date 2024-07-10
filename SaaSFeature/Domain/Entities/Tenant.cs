namespace SaaSFeature.Domain.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
    }
}
