using movies.Attributes;

namespace movies
{
    public class DependencyFactory
    {
        private IServiceProvider ServiceProvider { get; set; }

        public DependencyFactory(IServiceProvider serviceProvider) 
        {
            ServiceProvider = serviceProvider;
        }

        public void ResolveDependency(object instance)
        {
            if (ServiceProvider == null)
                return;

            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                var dependencyAttributes = (DependencyAttribute[])Attribute.GetCustomAttributes(property, typeof(DependencyAttribute));
                foreach (var attribute in dependencyAttributes)
                {
                    var value = ServiceProvider.CreateScope().ServiceProvider.GetRequiredService(property.PropertyType);
                    property.SetValue(instance, value);
                }
            }
        }
    }
}
