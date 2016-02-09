
# Space

You can use a Space to partition your images and give them different default settings.


```javascript
/customers/{0}/spaces/{1}
```


## Supported properties


### modelId

The internal identifier for the space within the customer (uri component)


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Space|xsd:integer|False|False|


### name

Space name


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Space|xsd:string|False|False|


### created

Date the space was created


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Space|xsd:dateTime|True|False|


### defaultTags

Default tags to apply to images created in this space


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Space|xsd:string|False|False|


### defaultMaxUnauthorised

Default size at which role-based authorisation will be enforced. -1=open, 0=always require auth


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Space|xsd:integer|False|False|


### defaultRoles

Default roles that will be applied to images in this space


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Space|hydra:Collection|False|False|

This property is a LINK...


```javascript
/customers/{0}/spaces/{1}/defaultRoles
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Role||hydra:Collection||
|POST|Creates a new Role|vocab:Role|vocab:Role|201 Role created.|


### images

All the images in the space


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Space|hydra:Collection|True|False|

This property is a LINK...


```javascript
/customers/{0}/spaces/{1}/images
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Image||hydra:Collection||
|POST|Creates a new Image|vocab:Image|vocab:Image|201 Image created.|


## Supported operations


```javascript
/customers/{0}/spaces/{1}
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Space||vocab:Space||
|PUT|create or replace a Space|vocab:Space|vocab:Space||
|PATCH|Update the supplied fields of the Space|vocab:Space|vocab:Space|200 patched Space|
|DELETE|Delete the Space||owl:Nothing||

