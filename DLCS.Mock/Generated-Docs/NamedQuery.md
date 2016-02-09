
# NamedQuery

A stored query that will generate IIIF manifests

```javascript
/customers/{0}/namedQueries/{1}
```


## Supported properties


### data

The data for the query. JSON object?


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:NamedQuery|xsd:string|False|False|


## Supported operations

**Template: **```javascript
/customers/{0}/namedQueries/{1}
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Named Query||vocab:NamedQuery||
|PUT|create or replace a Named Query|vocab:NamedQuery|vocab:NamedQuery||
|PATCH|Update the supplied fields of the Named Query|vocab:NamedQuery|vocab:NamedQuery|200 patched Named Query|
|DELETE|Delete the Named Query||owl:Nothing||

