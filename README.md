# QueryFilter
QueryFilter provides way of providing complex filtering on IQueryable<> with dynamic expression trees. With one property, you can do multiple filters, ands, ors, contains, less than, not equals, etc for anything that implements IQueryable. This also applies to Entity Framework. You also get the ability to add grouping to your queries with a very simple syntax. Best of all, you only have to work with the filter properties. You simply send your filter object and the query you want to apply the .Where clause to and QueryFilter takes care of the rest! You can even deserialize JSON into your filter if you want the UI to be able to apply these complex filters!

Here is a simple example:

```csharp
public class MyFilter
{
   [MapToPropertey("Name")]
   public QueryFilterString Name { get; set; }

   public MyFilter()
   {
      Name = new QueryFilterString();
   }
}
```

Now if you want a query where Name contains "x" and does not startswith "y":

```csharp
var filter = new MyFilter();
filter.Name.Contains("x");
filter.Name.NotStartsWith("y");
```

Done! Now all you have to do is pass it into the query builder:

```csharp
IQueryable<MyEntity> query = ctx.MyEntities;
query = QueryFilterBuilder.Buid(query, filter);
```

Done! 

*** More to come ***
