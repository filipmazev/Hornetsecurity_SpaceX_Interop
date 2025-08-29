namespace spacexinterop.api._Common.Utility.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ResultStatusConnotationAttribute : Attribute
{
    public bool IsPositive { get; set; } = false;
}