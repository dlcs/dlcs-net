
# NamedQuery

A stored query that will generate IIIF manifests


```
/customers/{0}/namedQueries/{1}
```


## Supported operations


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieve a Named Query| |vocab:NamedQuery|200 OK, 404 Not found|
|PUT|create or replace a Named Query|vocab:NamedQuery|vocab:NamedQuery|200 OK, 201 Created Named Query, 404 Not found|
|PATCH|Update the supplied fields of the Named Query|vocab:NamedQuery|vocab:NamedQuery|205 Accepted Named Query, reset view, 400 Bad request, 404 Not found|
|DELETE|Delete the Named Query| |owl:Nothing|205 Accepted Named Query, reset view, 404 Not found|


## Supported properties


### data

The data for the query. JSON object?


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:NamedQuery|xsd:string|False|False|

