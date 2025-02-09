namespace Serilog.Exceptions.Filters;

/// <summary>
/// Filters the exception properties based only on their name. If exception property matches any of provided
/// property names, exception property is ignored altogether. Comparison method is exact case-sensitive.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IgnorePropertyByNameExceptionFilter"/> class.
/// </remarks>
/// <param name="propertiesToIgnore">The properties to ignore.</param>
public class IgnorePropertyByNameExceptionFilter(params string[] propertiesToIgnore) :
    IExceptionPropertyFilter
{
    /// <summary>
    /// The usage of array instead of HashSet is dictated by the assumption
    /// that there will be only small number of properties to ignore and for such
    /// case array is much faster than HashSet.
    /// </summary>
    private readonly string[] propertiesToIgnore = propertiesToIgnore;

    /// <inheritdoc />
    public bool ShouldPropertyBeFiltered(Exception exception, string propertyName, object? value)
    {
        if (this.propertiesToIgnore is null)
        {
            return false;
        }

        for (var i = 0; i < this.propertiesToIgnore.Length; i++)
        {
            if (this.propertiesToIgnore[i].Equals(propertyName, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }
}
