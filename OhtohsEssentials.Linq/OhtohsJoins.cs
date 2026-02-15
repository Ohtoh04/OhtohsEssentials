namespace OhtohsEssentials.Linq;


public static class OhtohsJoins
{
    /// <summary>
    /// Performs an inner join between two sequences by matching keys, returning only pairs where both sides have matches.
    /// Similar to SQL's INNER JOIN.
    /// </summary>
    /// <returns>Matching pairs from both sequences.</returns>
    public static IEnumerable<TResult> InnerJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector
    )
    {
        if (outer == null) throw new ArgumentNullException(nameof(outer));
        if (inner == null) throw new ArgumentNullException(nameof(inner));
        if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
        if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector);
    }

    /// <summary>
    /// Performs a right outer join, returning all elements from the right sequence (`inner`) and matching elements from the left sequence (`outer`).
    /// Unmatched left elements are replaced with `default(TOuter)`.
    /// Similar to SQL's RIGHT JOIN.
    /// </summary>
    /// <returns>All right elements + matched (or default) left elements.</returns>
    public static IEnumerable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector,
        IEqualityComparer<TKey>? comparer = null)
    {
        if (outer == null) throw new ArgumentNullException(nameof(outer));
        if (inner == null) throw new ArgumentNullException(nameof(inner));
        if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
        if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        comparer ??= EqualityComparer<TKey>.Default;

        var outerLookup = outer.ToLookup(outerKeySelector, comparer);

        foreach (var innerItem in inner)
        {
            var innerKey = innerKeySelector(innerItem);
            var outerMatches = outerLookup[innerKey];

            if (outerMatches.Any())
            {
                foreach (var outerItem in outerMatches)
                {
                    yield return resultSelector(outerItem, innerItem);
                }
            }
            else
            {
                yield return resultSelector(default!, innerItem);
            }
        }
    }

    /// <summary>
    /// Performs a left outer join, returning all elements from the left sequence (`outer`) and matching elements from the right sequence (`inner`).
    /// Unmatched right elements are replaced with `default(TInner)`.
    /// Similar to SQL's LEFT JOIN.
    /// </summary>
    /// <returns>All left elements + matched (or default) right elements.</returns>
    public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector,
        IEqualityComparer<TKey>? comparer = null)
    {
        if (outer == null) throw new ArgumentNullException(nameof(outer));
        if (inner == null) throw new ArgumentNullException(nameof(inner));
        if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
        if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        comparer ??= EqualityComparer<TKey>.Default;

        var innerLookup = inner.ToLookup(innerKeySelector, comparer);

        foreach (var outerItem in outer)
        {
            var outerKey = outerKeySelector(outerItem);
            var innerMatches = innerLookup[outerKey];

            if (innerMatches.Any())
            {
                foreach (var innerItem in innerMatches)
                {
                    yield return resultSelector(outerItem, innerItem);
                }
            }
            else
            {
                yield return resultSelector(outerItem, default!);
            }
        }
    }

    /// <summary>
    /// Performs a cross join (Cartesian product), combining every element from the left sequence with every element from the right sequence.
    /// Similar to SQL's CROSS JOIN.
    /// </summary>
    /// <returns>All possible pairs between the two sequences.</returns>
    public static IEnumerable<TResult> CrossJoin<TLeft, TRight, TResult>(
        this IEnumerable<TLeft> left,
        IEnumerable<TRight> right,
        Func<TLeft, TRight, TResult> resultSelector)
    {
        if (left == null) throw new ArgumentNullException(nameof(left));
        if (right == null) throw new ArgumentNullException(nameof(right));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        return left.SelectMany(
            leftItem => right,
            (leftItem, rightItem) => resultSelector(leftItem, rightItem)
        );
    }
}