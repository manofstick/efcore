// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Query.SqlExpressions;

/// <summary>
///     <para>
///         An expression that represents a table source in a SQL tree.
///     </para>
///     <para>
///         This type is typically used by database providers (and other extensions). It is generally
///         not used in application code.
///     </para>
/// </summary>
public abstract class TableExpressionBase : Expression, IPrintableExpression, ISqlExpressionAnnotatable
{
    /// <summary>
    ///     Creates a new instance of the <see cref="TableExpressionBase" /> class.
    /// </summary>
    /// <param name="alias">A string alias for the table source.</param>
    protected TableExpressionBase(string? alias)
    {
        Alias = alias;
    }

    /// <summary>
    ///     The alias assigned to this table source.
    /// </summary>
    public virtual string? Alias { get; internal set; }

    /// <inheritdoc />
    protected override Expression VisitChildren(ExpressionVisitor visitor)
        => this;

    /// <inheritdoc />
    public override Type Type
        => typeof(object);

    /// <inheritdoc />
    public sealed override ExpressionType NodeType
        => ExpressionType.Extension;



    /// <summary>
    ///     Gets the value annotation with the given name, returning <see langword="null" /> if it does not exist.
    /// </summary>
    /// <param name="name">The key of the annotation to find.</param>
    /// <returns>
    ///     The value of the existing annotation if an annotation with the specified name already exists.
    ///     Otherwise, <see langword="null" />.
    /// </returns>
    public virtual object? this[string name]
    {
        get => FindAnnotation(name)?.Value;

        set
        {
            Check.NotEmpty(name, nameof(name));

            if (value == null)
            {
                RemoveAnnotation(name);
            }
            else
            {
                SetAnnotation(name, value);
            }
        }
    }


    public object? this[string name] => throw new NotImplementedException();

    /// <summary>
    ///     Creates a printable string representation of the given expression using <see cref="ExpressionPrinter" />.
    /// </summary>
    /// <param name="expressionPrinter">The expression printer to use.</param>
    protected abstract void Print(ExpressionPrinter expressionPrinter);

    /// <inheritdoc />
    void IPrintableExpression.Print(ExpressionPrinter expressionPrinter)
        => Print(expressionPrinter);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj != null
            && (ReferenceEquals(this, obj)
                || obj is TableExpressionBase tableExpressionBase
                && Equals(tableExpressionBase));

    private bool Equals(TableExpressionBase tableExpressionBase)
        => Alias == tableExpressionBase.Alias;

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(Alias);

    public ISqlExpressionAnnotation? FindAnnotation(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ISqlExpressionAnnotation> GetAnnotations()
    {
        throw new NotImplementedException();
    }
}
