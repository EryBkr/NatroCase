namespace Core.Persistence.Dynamic;

public class Filter
{
    //Column
    public string Field { get; set; }
    public string? Value { get; set; }
    //GreaterThen,Equal etc...
    public string Operator { get; set; }
    //And,Or etc...
    public string? Logic { get; set; }

    //for Multiple Filter -> Nested
    public IEnumerable<Filter>? Filters { get; set; }

    public Filter()
    {
        Field = string.Empty;
        Operator = string.Empty;
    }

    public Filter(string field, string @operator)
    {
        Field = field;
        Operator = @operator;
    }
}
