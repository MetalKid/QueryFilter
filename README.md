QueryFilter provides way of providing complex filtering on IQueryable<> with dynamic expression trees.  With one property, you can do multiple filters, contains, less than, not equals, etc for anything that implements IQueryable.  This also applies to Entity Framework.  You also get the ability to add grouping to your queries with a very simple syntax to support ands/ors.  Best of all, you only have to work with the filter properties.  You simply send your filter object and the query you want to apply the .Where clause to and QueryFilter takes care of the rest!  You can even deserialize JSON into your filter if you want the UI to be able to apply these complex filters!

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

Unfortunately, I haven't come up with a good way around being forced to new up each filter property n the constructor before you start using it.  If someone has a clever idea, I'm all ears! In the meantime, you will just have to new up each property you declare in the constructor.  (Note: Making these a struct won't work because they all store a list of FilterItems internally...)

## MapToProperty

In order to determine which property a filter is for on the entity side, the MapToProperty attribute is used.  If not value is provided, QueryFilter assumes the name of the property on the filter is the same name on the entity.  Otherwise, you can pass in a string name of the entity to map to.  If you have a complex property (i.e. Parent.Child.A), then you could pass in "Child.A" and the path will be parsed for you.  If you have some sort of complex scenario, then you can use the AddCustomMapping call discussed later on.

```csharp
public class SomeFilter
{
   [MapToProperty("SomeOtherName")]
   public FilterString Name { get; set; }
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

## FilterRange

FilterRange allows you to filter in the following ways:

* EqualTo
* NotEqualTo
* LessThan
* LessThanOrEqualTo
* GreaterThan
* GreaterThanOrEqualTo

## FilterEquatable

FilterEquatable allows you to filter in the following ways:

* EqualTo
* NotEqualTo

## FilterGroup and IFilterGroup


## Complex Types

## Custom Mapping

