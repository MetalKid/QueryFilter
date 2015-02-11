QueryFilter provides a way of providing complex filtering on IQueryable<> with dynamic expression trees.  With one property, you can do multiple filters, contains, less than, not equals, etc for anything that implements IQueryable.  This also applies to Entity Framework.  You also get the ability to add grouping to your queries with a very simple syntax to support ands/ors.  Best of all, you only have to work with the filter properties.  You simply send your filter object and the query you want to apply the .Where clause to and QueryFilter takes care of the rest!  You can even deserialize JSON into your filter if you want the UI to be able to apply these complex filters!

## Basic Usage

```csharp
public class MyFilter
{
   [MapToProperty]
   public FilterString Name { get; set; }

   public MyFilter()
   {
      Name = new FilterString();
   }
}
```

Now if you want a query where Name contains "x" and does not startswith "y":

```csharp
var filter = new MyFilter();
filter.Name.Contains("x");
filter.Name.NotStartsWith("y");
```

Done!  Now all you have to do is pass it into the query builder:

```csharp
IQueryable<MyEntity> query = ctx.MyEntities;
query = QueryFilterBuilder.Build(query, filter);
```

Done! 

## Initializing FilterString/Range/Equatable

Unfortunately, I haven't come up with a good way around being forced to new up each filter property in the constructor before you start using it.  If someone has a clever idea, I'm all ears! In the meantime, you will just have to new up each property you declare in the constructor.  (Note: Making these a struct won't work because they all store a list of FilterItems internally...)

## MapToPropertyAttribute

In order to determine which property a filter is for on the entity side, the MapToProperty attribute is used.  If not value is provided, QueryFilter assumes the name of the property on the filter is the same name on the entity.  Otherwise, you can pass in a string name of the entity to map to.  If you have a complex property (i.e. Parent.Child.A), then you could pass in "Child.A" and the path will be parsed for you.  If you have some sort of complex scenario, then you can use the AddCustomMapping call discussed later on.

```csharp
public class SomeFilter
{
   [MapToProperty("SomeOtherName")]
   public FilterString Name { get; set; }

   [MapToProperty]
   public FilterRange<int> Speed { get; set; }

   [MapToProperty("Child.AnotherProperty")]
   public FilterString AnotherProperty { get; set; }
}
```

## FilterString

FilterString allows you to filter in the following ways:

* EqualTo
* NotEqualTo
* Contains
* NotContains
* StartsWith
* NotStartsWith
* EndsWith
* NotEndsWith

Also, each of these can be made to include or ignore casing (includes by default).  In order to ensure Entity Framework works with the ignore casing, ToLower() is done to both sides.

Here are a few examples:

```csharp
var filter = new MyFilter();
filter.Name.EqualTo("a");
filter.Name.NotEqualTo("9");
filter.Name.Contains("x");
filter.Name.NotContains("z");
filter.Name.StartsWith("U");
filter.Name.NotStartsWith("Y");
filter.Name.EndsWith("k");
filter.Name.NotEndsWith("e");

query = QueryFilterBuilder.Build(query, filter);
```

Just for laughs, here is the SQL EF produced from applying these lines of code:

```csharp
WHERE (N'a' = [Extent1].[Name]) AND (N'9' <> [Extent1].[Name]) AND ([Extent1].[Name] LIKE N'%x%') AND ( NOT ([Extent1].[Name] LIKE N'%z%')) AND ([Extent1].[Name] LIKE N'U%') AND ( NOT ([Extent1].[Name] LIKE N'Y%')) AND ([Extent1].[Name] LIKE N'%k') AND ( NOT ([Extent1].[Name] LIKE N'%e'))
```

## FilterRange

FilterRange allows you to filter in the following ways:

* EqualTo
* NotEqualTo
* LessThan
* LessThanOrEqualTo
* GreaterThan
* GreaterThanOrEqualTo

It is a generic, but is constrained to only structs.  This will allow you to do filtering on ints, longs, DateTimes, etc.

Here are a few examples:

```csharp
var filter = new MyFilter();
filter.Speed.EqualTo(1);
filter.Speed.NotEqualTo(0);
filter.Speed.LessThan(10);
filter.Speed.LessThanOrEqualTo(20);
filter.Speed.GreaterThan(-6);
filter.Speed.GreaterThanOrEqualTo(5);

query = QueryFilterBuilder.Build(query, filter);
```

If the target entity is nullable, the value will be converted for you automatically.  Thus, you are allowed to enter:

filter.Speed.EqualTo(null);  However, if it is not nullable, the default(T) will be used instead (i.e. 0), so you could have unintended consequences if you aren't careful.

Just for laughs, here is the SQL EF produced from applying these lines of code (with nullable int in the database):

```csharp
WHERE (1 = [Extent1].[Speed]) AND ( NOT ((0 = [Extent1].[Speed]) AND ([Extent1].[Speed] IS NOT NULL))) AND ([Extent1].[Speed] < 10) AND ([Extent1].[Speed] <= 20) AND ([Extent1].[Speed] > -6) AND ([Extent1].[Speed] >= 5)
```

## FilterEquatable

FilterEquatable allows you to filter in the following ways:

* EqualTo
* NotEqualTo

## FilterGroup and IFilterGroup


## Complex Types

## Custom Mapping

