QueryFilter provides a simple way of providing complex filtering on IQueryable<> with dynamic expression trees.  With one property, you can do multiple filters, contains, less than, not equals, etc for anything that implements IQueryable.  This also applies to Entity Framework.  You also get the ability to add grouping to your queries with a very simple syntax to support ands/ors.  Best of all, you only have to work with the filter properties.  You simply send your filter object and the query you want to apply the .Where clause to and QueryFilter takes care of the rest!  You can even deserialize JSON into your filter if you want the UI to be able to apply these complex filters!

## Basic Usage

```csharp
public class SomeFilter
{
   [MapToProperty]
   public FilterString Name { get; set; }

   public SomeFilter()
   {
      Name = new FilterString();
   }
}
```

Now if you want a query where Name contains "x" and does not startswith "y":

```csharp
var filter = new SomeFilter();
filter.Name.Contains("x");
filter.Name.NotStartsWith("y");
```

Done!  Now all you have to do is pass it into the query builder:

```csharp
IQueryable<MyEntity> query = ctx.MyEntities;
query = QueryFilterBuilder<MyEntity, SomeFilter>.New().Build(query, filter);
```

Done! 

## Initializing FilterString/Range/Equatable

Unfortunately, I haven't come up with a good way around being forced to new up each filter property in the constructor before you start using it.  If someone has a clever idea, I'm all ears! In the meantime, you will just have to new up each property you declare in the constructor.  (Note: Making these a struct won't work because they all store a list of FilterItems internally...)

## MapToPropertyAttribute

In order to determine which property a filter is for on the entity side, the MapToProperty attribute is used.  If no value is provided, QueryFilter assumes the name of the property on the filter is the same name on the entity.  Otherwise, you can pass in a string name of the entity to map to.  If you have a complex property (i.e. Parent.Child.A), then you could pass in "Child.A" and the path will be parsed for you.  If you have some sort of complex scenario, then you can use the AddCustomMapping call discussed later on.

```csharp
public class MyFilter
{
   [MapToProperty("SomeOtherName")]
   public FilterString Name { get; set; }

   [MapToProperty]
   public FilterRange<int> Speed { get; set; }

   [MapToProperty]
   public FilterEquatable<bool?> HasImage { get; set; }

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

query = QueryFilterBuilder<MyEntity, MyFilter>.New().Build(query, filter);
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

query = QueryFilterBuilder<MyEntity, MyFilter>.New().Build(query, filter);
```

If the target entity property is nullable, the value will be converted for you automatically.  Thus, you are allowed to enter:
```csharp
filter.Speed.EqualTo(null);  
```
However, if it is not nullable, the default(T) will be used instead (i.e. 0), so you could have unintended consequences if you aren't careful.

Just for laughs, here is the SQL EF produced from applying these lines of code (with nullable int in the database):

```csharp
WHERE (1 = [Extent1].[Speed]) AND ( NOT ((0 = [Extent1].[Speed]) AND ([Extent1].[Speed] IS NOT NULL))) AND ([Extent1].[Speed] < 10) AND ([Extent1].[Speed] <= 20) AND ([Extent1].[Speed] > -6) AND ([Extent1].[Speed] >= 5)
```

## FilterEquatable

FilterEquatable allows you to filter in the following ways:

* EqualTo
* NotEqualTo

Equatable is meant for any types that really only support equal to or not (i.e. bool).  Here are a few examples:

```csharp
var filter = new MyFilter();
filter.HasImage.EqualTo(true);
filter.HasImage.NotEqualTo(false);

query = QueryFilterBuilder<MyEntity, MyFilter>.New().Build(query, filter);
```

Just for laughs, here is the SQL EF produced from applying these lines of code (with nullable bit in the database):

```csharp
WHERE (1 = [Extent1].[HasImage]) AND ( NOT ((0 = [Extent1].[HasImage]) AND ([Extent1].[HasImage] IS NOT NULL)))
```

## Filtering on String Length
To filter on the length of a string, you'll need to do something slightly different.  In the filter, you need to define a FilterRange<int> for the length check.  However, you will then need to have the MapToProperty (or custom map) point to the string property.  i.e.:

```csharp
public class SomeFilter
{
   [MapToProperty]
   public FilterString Name { get; set; }

   [MapToProperty("Name")]
   public FilterRange<int> NameLength { get; set; }
}
```
Then you just treat the NameLength check as you would any other FilterRange!

## FilterGroup and IFilterGroup

FilterGroups are how you can mix ORs, ANDs, and groups ( ) into your queries.  I added a helper method called "AddGroups" that took a param array of FilterGroup.  Here are a few examples:

```csharp
            filter.AddGroups(
                FilterGroup.New(
                    FilterGroupTypeEnum.And,
                    filter.Name.Contains("Leo"),
                    filter.Name.Contains("mon")
                ),
                FilterGroup.New(
                    FilterGroupTypeEnum.Or,
                    filter.Name.EqualTo("Agumon"),
                    FilterGroup.New(
                        FilterGroupTypeEnum.And,
                        filter.Name.Contains("Tort"),
                        filter.Name.Contains("mon")
                    )
                )
            );
```

This reads: (Leo and Mon) Or (Agumon Or (Tort And Mon)).  

Here is the SQL it produces:

```csharp
WHERE (([Extent1].[Name] LIKE N'%Leo%') AND ([Extent1].[Name] LIKE N'%mon%')) OR (N'Agumon' = [Extent1].[Name]) OR (([Extent1].[Name] LIKE N'%Tort%') AND ([Extent1].[Name] LIKE N'%mon%'))
```

Each new group at the top level determines the And/Or to come before it along with its siblings.  If you needed an And outside and Or inside, you'd have to do a double FilterGroup.New() with the corresponding FilterGroupTypeEnum values.  Here is how that would look:

```csharp
            filter.AddGroups(
                FilterGroup.New(
                    FilterGroupTypeEnum.And,
                    filter.Name.Contains("Leo"),
                    filter.Name.Contains("mon")
                ),
                FilterGroup.New(
                    FilterGroupTypeEnum.And,
                    FilterGroup.New(
                       FilterGroupTypeEnum.Or,
                       filter.Name.EqualTo("Agumon"),
                       FilterGroup.New(
                           FilterGroupTypeEnum.And,
                           filter.Name.Contains("Tort"),
                           filter.Name.Contains("mon")
                       )
                    )
                )
            );
```

Here is the SQL it produces:

```csharp
WHERE (([Extent1].[Name] LIKE N'%Leo%') AND ([Extent1].[Name] LIKE N'%mon%')) AND (N'Agumon' = [Extent1].[Name]) OR (([Extent1].[Name] LIKE N'%Tort%') AND ([Extent1].[Name] LIKE N'%mon%'))
```

In order to figure out if a filter has any groups, it must implement the IFilterGroup interface.  Otherwise, I have no other way of pulling in groups since they are stored at the filter level in one place across all properties.  You don't have to stick to the same Filter type when Or/Anding them together.

## Complex Types

If you have a situation where you want to filter on another table from the same filter, you can use the "Child.Property" syntax with the parameter into MapToPropertyAttribute.  This will automatically parse and build the correct pathing for you.

## Custom Mapping

Should an odd situation arise or you just hate using the MapToPropertyAttribute, you can specify the maps directly yourself.  Here is what the Name property would look like:

```csharp
query = QueryFilterBuilder<MyEntity, SomeFilter>.New()
                .AddCustomMap(a => a.Name, filter.Name)
                .Build(query, filter);
```

AddCustomMap can be chained, so you can add every filter reference by hand here and have the lamda support (no hard-coded strings).  In the end, the MapToProperty attribute is doing the same work as AddCustomMap for you.

## JSON

Here is the JSON representation of that previous big grouping with Name values:

```csharp
{
  "Name": {
    "Filters": [
      {
        "Id": "1",
        "Value": "Leo",
        "IgnoreCase": false,
        "Operator": "Contains"
      },
      {
        "Id": "2",
        "Value": "mon",
        "IgnoreCase": false,
        "Operator": "Contains"
      },
      {
        "Id": "3",
        "Value": "Agumon",
        "IgnoreCase": false,
        "Operator": "EqualTo"
      },
      {
        "Id": "4",
        "Value": "Tort",
        "IgnoreCase": false,
        "Operator": "Contains"
      },
      {
        "Id": "5",
        "Value": "mon",
        "IgnoreCase": false,
        "Operator": "Contains"
      }
    ]
  },
  "HasImage": {
    "Filters": [],
  },
  "FilterGroups": [
    {
      "GroupType": "And",
      "FilterItems": [
        {
        "Id": "1",
          "Value": "Leo",
          "IgnoreCase": false,
          "Operator": "Contains"
        },
        {
        "Id": "2",
          "Value": "mon",
          "IgnoreCase": false,
          "Operator": "Contains"
        }
      ]
    },
    {
      "GroupType": "Or",
      "FilterItems": [
        {
         "Id": "3",
          "Value": "Agumon",
          "IgnoreCase": false,
          "Operator": "EqualTo"
        },
        {
          "GroupType": "And",
          "FilterItems": [
            {
             "Id": "4",
              "Value": "Tort",
              "IgnoreCase": false,
              "Operator": "Contains"
            },
            {
              "Id": "5",
              "Value": "mon",
              "IgnoreCase": false,
              "Operator": "Contains"
            }
          ]
        }
      ]
    }
  ]
}
```

One thing to note.  The Operator can use 1 instead of "EqualTo" and it parses just fine.

That's it!  This doesn't have any support for sub lists, so let me know if you want me to experiment with something like that!  Thanks!

