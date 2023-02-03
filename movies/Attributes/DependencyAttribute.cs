namespace movies.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute() { }
    }
}
